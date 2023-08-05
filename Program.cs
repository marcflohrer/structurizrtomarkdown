// See https://aka.ms/new-console-template for more information
using System.Data.Common;
using System.Text;
using CommandLine;
using interface_description_list;
using Structurizr;

Options options = ParseArguments(args);
Workspace workspace = LoadWorkspace(options);
StructuredTable structuredTable = ToStructuredTable(workspace);
var markdownList = ConvertToMarkdown(structuredTable);
WriteMarkDownToFile(options.WorkspaceJson, markdownList);

static void WriteMarkDownToFile(string dslFileName, List<string> markdownList)
{
    // change suffix from .json to .md
    var markdownFileName = dslFileName.Replace(".json", ".md");
    // Delete file if it exists already
    if (File.Exists(markdownFileName))
    {
        File.Delete(markdownFileName);
    }
    // write markdown to file
    File.WriteAllLines(markdownFileName, markdownList);
}

// The ConvertToMarkdown function takes a MarkdownTable object as a parameter and returns a list of strings.
// The MarkdownTable object contains the data and the maximum length of each column
// The data is padded to the length of the column

static List<string> ConvertToMarkdown(StructuredTable markdownTable)
{
    // Use key to get the column headers and write markdown syntax to output list 
    // Each string in the list is a line in the markdown file
    // Each column is padded to the longest string in the column. 
    // The maximum length of the column is stored in the MarkdownColumn class in the property MaxColumnLength
    var markdownList = new List<string>();
    var line = new StringBuilder();
    foreach (var key in markdownTable.Table.Keys)
    {
        line.Append($"| {key.PadRight(markdownTable.Table[key].MaxColumnLength)} ");
    }
    // The next line in the output list is the separation line between the column headers and the data, 
    // it starts with a pipe and then has a series of dashes, 
    // the number of dashes is the length of the column header plus 2
    markdownList.Add(line.Append('|').ToString());

    line = new StringBuilder();
    foreach (var key in markdownTable.Table.Keys)
    {
        line.Append($"| :--- ");
    }
    markdownList.Add(line.Append('|').ToString());

    // The following lines are the data, each line is a row in the table
    // The data is padded to the length of the column
    line = new StringBuilder();
    // Iterate over the first column in the table, the first column is used to determine the number of rows
    for (var i = 0; i < markdownTable.Table[markdownTable.Table.Keys.First()].Column.Count; i++)
    {
        foreach (var key in markdownTable.Table.Keys)
        {
            line.Append($"| {markdownTable.Table[key].Column[i].PadRight(markdownTable.Table[key].MaxColumnLength)} ");
        }
        markdownList.Add(line.Append('|').ToString());
        line = new StringBuilder();
    }
    markdownList.Add(line.Append('|').ToString());
    return markdownList;
}

// This code creates a table that shows the interface description list.
// The table is created by iterating through each property in the relationship
// and adding the key and value to the table.
// If the table already contains the key, the value is added to the table.
// If the table does not contain the key, the key and value are added to the table.

static StructuredTable CreateInterfaceDescriptionList(Relationship? relationShip, StructuredTable markdownTable)
{
    foreach (var (key, value) in from property in relationShip!.Properties
                                 let key = property.Key
                                 let value = property.Value
                                 select (key, value))
    {
        if (key == "structurizr.dsl.identifier")
        {
            continue;
        }
        if (markdownTable.Table.ContainsKey(key))
        {
            var valueList = markdownTable.Table[key];
            valueList.Column.Add(value);
            markdownTable.Table[key] = valueList;
        }
        else
        {
            markdownTable.Table.Add(key, new TableColumn() { Column = new List<string> { value } });
        }
    }

    return markdownTable;
}

// ParseArguments
//   - parses command line arguments
//   - ensures that dsl file is specified
//   - returns Options object

static Options ParseArguments(string[] args)
{
    var parsedOptions = Parser.Default.ParseArguments<Options>(args);
    var result = parsedOptions.MapResult(RunOptionsAndReturnExitCode, HandleParseError);
    Console.WriteLine("Return code= {0}", result);
    if (result != 0)
    {
        Environment.Exit(result);
    }
    return parsedOptions.Value;
}

//3)	//In sucess: the main logic to handle the options
static int RunOptionsAndReturnExitCode(Options o)
{
    if (o.WorkspaceJson == null)
    {
        Console.WriteLine($"File to workspace.json is not set. Aborting");
        return -1;
    }
    var workspaceJson = new FileInfo(o.WorkspaceJson);
    if (!workspaceJson.Exists)
    {
        Console.WriteLine($"File '{workspaceJson.FullName}' does not exist.");
        return -1;
    }
    Console.WriteLine("Success");
    var exitCode = 0;
    Console.WriteLine("props= {0}", string.Join(",", workspaceJson.FullName));
    return exitCode;
}

//in case of errors or --help or --version
static int HandleParseError(IEnumerable<CommandLine.Error> errs)
{
    var result = -2;
    Console.WriteLine("errors {0}", errs.Count());
    if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
        result = -1;
    Console.WriteLine("Exit code {0}", result);
    return result;
}

// Load workspace from JSON file

static Workspace LoadWorkspace(Options options)
{
    var workspaceJson = new FileInfo(options.WorkspaceJson);
    var workspace = WorkspaceUtils.LoadWorkspaceFromJson(workspaceJson);
    return workspace;
}

static StructuredTable ToStructuredTable(Workspace workspace)
{
    var relationShips = workspace.Model.Relationships;
    var markdownTable = new StructuredTable();
    foreach (var relationShip in relationShips)
    {
        markdownTable = CreateInterfaceDescriptionList(relationShip, markdownTable);
    }

    return markdownTable;
}
// Print interface description list as markdown
using System.Text;
using interface_description_list;

public static class MarkdownConverter
{
    // The ConvertToMarkdown function takes a MarkdownTable object as a parameter and returns a list of strings.
    // The MarkdownTable object contains the data and the maximum length of each column
    // The data is padded to the length of the column

    public static List<string> ConvertToMarkdown(StructuredTable markdownTable)
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
}
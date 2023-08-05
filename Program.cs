// See https://aka.ms/new-console-template for more information
using interface_description_list;

Options options = ArgumentParser.ParseArguments(args);
var workspace = WorkspaceParser.LoadWorkspace(options);
StructuredTable structuredTable = StructuredTableService.ToStructuredTable(workspace);
var markdownList = MarkdownConverter.ConvertToMarkdown(structuredTable);
MarkdownWriter.WriteMarkDownToFile(options.WorkspaceJson, markdownList);

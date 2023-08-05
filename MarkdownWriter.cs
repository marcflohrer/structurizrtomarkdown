public static class MarkdownWriter
{
    // The WriteMarkDownToFile function takes a file name and a list of strings as parameters.
    public static void WriteMarkDownToFile(string dslFileName, List<string> markdownList)
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
}
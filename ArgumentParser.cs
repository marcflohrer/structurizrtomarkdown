using CommandLine;

public class ArgumentParser
{
    // ParseArguments
    //   - parses command line arguments
    //   - ensures that dsl file is specified
    //   - returns Options object

    public static Options ParseArguments(string[] args)
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

    //In sucess: the main logic to handle the options
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
}
using Structurizr;

public static class WorkspaceParser
{
    // Load workspace from JSON file
    public static Workspace LoadWorkspace(Options options)
    {
        var workspaceJson = new FileInfo(options.WorkspaceJson);
        var workspace = WorkspaceUtils.LoadWorkspaceFromJson(workspaceJson);
        return workspace;
    }
}
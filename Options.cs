// See https://aka.ms/new-console-template for more information
using CommandLine;

public class Options {
	[Option ('w', "workspacejson", Required=true)]
	public string WorkspaceJson { get; set; }
}
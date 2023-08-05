# structurizrtomarkdown
Converts structurizr workspace.json into a interface description list listing the properties of the interfaces in markdown list.

## Prerequisites

You need a workspace.json file from structurizr. See https://structurizr.com/help/express/structurizr-json
that has relationships between interfaces and components with properties on the interfaces.
The properties for all interfaces need to have the same keys

## Usage

```dotnet interface-description-list.dll -w <path to workspace.json>```

The output file is the same as the input file with the extension changed to .md

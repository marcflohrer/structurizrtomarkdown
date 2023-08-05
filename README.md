# structurizr 2 markdown

Converts structurizr workspace.json into a interface description list listing the properties of the interfaces in left aligned markdown list.

## Prerequisites

You need a workspace.json file from structurizr. See https://structurizr.com/help/express/structurizr-json
that has relationships between interfaces and components with properties on the interfaces.
The properties for all interfaces need to have the same keys

## Usage

### From sources

1) Clone the repository
2) cd into the repository
3) Run `dotnet build`
4) Run `dotnet interface-description-list.dll -w <path to workspace.json>`

### From docker image

 1) Run `chmod +x run.sh && ./run.sh "<path to directory that contains workspace.json>`

#### Example

    chmod +x run.sh && ./run.sh /Users/username/Documents/structurizr/

### Output file

The  output file is the same as the input file with the extension changed to .md

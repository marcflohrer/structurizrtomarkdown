using interface_description_list;
using Structurizr;

public class StructuredTableService
{
    public static StructuredTable ToStructuredTable(Workspace workspace)
    {
        var relationShips = workspace.Model.Relationships;
        var markdownTable = new StructuredTable();
        foreach (var relationShip in relationShips)
        {
            markdownTable = CreateInterfaceDescriptionList(relationShip, markdownTable);
        }

        return markdownTable;
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
            if (markdownTable.Table.TryGetValue(key, out var valueList))
            {
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
}
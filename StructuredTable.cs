namespace interface_description_list;
public class StructuredTable
{
    public StructuredTable()
    {
        Table = new Dictionary<string, TableColumn>();
    }
    public Dictionary<string, TableColumn> Table { get; set; }
}

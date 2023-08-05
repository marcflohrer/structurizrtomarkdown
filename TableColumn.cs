namespace interface_description_list;
public class TableColumn
{
    public TableColumn()
    {
        Column = new List<string>();
    }
    public List<string> Column { get; set; }
    public int MaxColumnLength { get => Column.Max(x => x.Length); }
}


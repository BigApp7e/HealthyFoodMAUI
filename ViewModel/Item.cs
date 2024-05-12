using SQLite;

[Table("foods")]
public class Item
{
    [PrimaryKey]
    [AutoIncrement]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("calories")]
    public int Calories { get; set;  }
}

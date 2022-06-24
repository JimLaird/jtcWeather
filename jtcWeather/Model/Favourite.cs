using SQLite;

namespace jtcWeather.Model
{
    [Table("favourite")]
    public class Favourite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(30), Unique]
        public string Item { get; set; }
    }
}

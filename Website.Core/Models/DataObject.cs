namespace Website.Core.Models
{
    public abstract class DataObject
    {
        public int Id { get; set; }
        public string Cursor { get; set; }
    }
}

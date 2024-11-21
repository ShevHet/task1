namespace TASK1
{
    public class Directory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int? ParentId {  get; set; }
        public Directory? Parent { get; set; }

        public List<Directory> Children { get; set; } = new List<Directory>();
    }
}

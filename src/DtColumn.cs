namespace src
{
    public class DtColumn : IDtColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Orderable { get; set; }
        public bool Searchable { get; set; }
        public DtSearch Search { get; set; }
    }
}
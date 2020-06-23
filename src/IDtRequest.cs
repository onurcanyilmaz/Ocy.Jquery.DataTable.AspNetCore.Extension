using System.Collections.Generic;

namespace src
{
    public interface IDtRequest
    {
        public List<DtColumn> Columns { get; set; }
        public List<DtFilterColumn> FilterColumns { get; set; }
        public List<string> columnsDef { get; set; }
        public int Draw { get; set; }
        public int Length { get; set; }
        public List<DataOrder> Order { get; set; }
        public DtSearch Search { get; set; }
        public int Start { get; set; }
    }
}
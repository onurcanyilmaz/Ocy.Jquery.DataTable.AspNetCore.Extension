using System.Collections.Generic;

namespace src
{
    public class DtRequest : IDtRequest
    {
        public List<DtColumn> Columns { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public List<DtFilterColumn> FilterColumns { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public List<string> columnsDef { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int Draw { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int Length { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public List<DataOrder> Order { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public DtSearch Search { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public int Start { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
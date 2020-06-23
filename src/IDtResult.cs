using System.Collections.Generic;

namespace src
{
    public interface IDtResult<T> where T : class
    {
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public int sEcho { get; set; }
        public string sColumns { get; set; }
        public List<T> aaData { get; set; }
    }
}
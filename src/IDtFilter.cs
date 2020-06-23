using System.Collections.Generic;

namespace src
{
    public interface IDtFilter<T> where T : class
    {
        public List<T> Filter(List<T> data, DtRequest request, out int filteredCount);
    }
}
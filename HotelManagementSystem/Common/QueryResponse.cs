using System.Collections.Generic;

namespace Common {
    public class QueryResponse<T> : Response
    {
        public List<T> Data { get; set; }
    }
}

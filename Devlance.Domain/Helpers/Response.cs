using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Devlance.Domain.Helpers
{
    public class Response<T> where T : class
    {
        public Response()
        {
            
        }

        public Response(T data)
        {
            Data = data;
            IsSuucceded = true;
        }

        public T Data { get; set; }
        public bool IsSuucceded { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }
}

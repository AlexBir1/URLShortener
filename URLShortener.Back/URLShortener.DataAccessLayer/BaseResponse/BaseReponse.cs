using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.BaseResponse
{
    public class BaseReponse<T> : IBaseResponse<T>
    {
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public BaseReponse(T newData, IEnumerable<string> errors)
        {
            Data = newData;
            Errors = errors;
        }
    }
}

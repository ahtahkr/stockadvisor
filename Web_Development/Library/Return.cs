using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Return
    {
        public int Result { get; set; }
        public string Message { get; set; }

        public Return(int _result, string _message)
        {
            this.Result = _result;
            this.Message = _message;
        }
    }
}

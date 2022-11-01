using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFS.ProdCat.Model.Common
{
    public class ResponseVM<T>
    {
        public string RespCode { get; set; }
        public bool IsSuccess { get; set; }
        public string RespMessage { get; set; }
        public T RespData { get; set; }

        protected ResponseVM(string respCode, bool isSuccess, string respMessage, T respData)
        {
            RespCode = respCode;
            IsSuccess = isSuccess;
            RespMessage = respMessage;
            RespData = respData;
        }
        protected ResponseVM(string respCode, bool isSuccess, string respMessage)
        {
            RespCode = respCode;
            IsSuccess = isSuccess;
            RespMessage = respMessage;
        }
        public ResponseVM()
        {

        }

        public static ResponseVM<T> Success(T data, string message = "Success")
        {
            return new ResponseVM<T>("00", true, message, data);
        }

        public static ResponseVM<T> Failure(string message)
        {
            return new ResponseVM<T>("01", false, message);
        }
    }
}



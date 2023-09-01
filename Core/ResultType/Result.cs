using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ResultType
{
    public class Result<T> : IResult<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public bool? IsLastPackage { get; set; }
        public ResultTypeEnum ResultType { get; set; }
        public string Html { get; set; }

        public string[] MessagesParameters { get; set; }

        public Result()
        {
            Data = default;
        }

        public Result(bool ısSuccess)
        {
            IsSuccess = ısSuccess;
        }

        public Result(bool ısSuccess, string message) : this(ısSuccess)
        {
            Message = message;
        }

        public Result(bool IsSuccess, T Data, string Message, params string[] parameters)
        {
            this.IsSuccess = IsSuccess;
            this.Data = Data;
            this.Message = string.Format(Message, parameters);
        }

        public Result(bool? IsSuccess = false, T? Data = default, string? Message = "",
            bool? IsLastPackage = null, ResultTypeEnum? ResultType = ResultTypeEnum.None, string? Html = null,
                params string[] parameters
             )
        {
            this.IsSuccess = IsSuccess.HasValue ? IsSuccess.Value : false;
            this.Data = Data;
            this.IsLastPackage = IsLastPackage.HasValue ? IsLastPackage.Value : false;
            this.ResultType = ResultType.HasValue ? ResultType.Value : ResultTypeEnum.None;
            this.Html = Html;
            this.Message = string.Format(Message, parameters);
        }


    }

    public enum ResultTypeEnum
    {
        None = 0,
        Information = 1,
        Success = 2,
        Warning = 3,
        Error = 4,
        Redirect = 5,
        Nothing = 404,
        NotFound = 6,
        Activation = 7,
    };
}



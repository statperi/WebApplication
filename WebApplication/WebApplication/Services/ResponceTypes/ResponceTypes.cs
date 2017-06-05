using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Services.ResponceTypes
{
    public class ServiceResponce
    {
        public enum ResponseTypes
        {
            /// <summary>
            /// The operation completed succesfully.
            /// </summary>
            OperationSuccesful,
            /// <summary>
            /// The operation returned validation errors.
            /// </summary>
            ValidationError,
            /// <summary>
            /// A server error has occured.
            /// </summary>
            GenericError
        }

        public ResponseTypes Response { get; set; }
        public string Message { get; set; }

    }


}
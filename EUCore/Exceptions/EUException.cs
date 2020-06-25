using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace EUCore.Exceptions
{
    [Serializable]
    public class EUException : Exception
    {
        public int StatusCode { get; }
        public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

        public EUException(int statusCode, string message, Exception innerException = null) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
        protected EUException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            StatusCode = info.GetInt32("StatusCode");
            //Parameters
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));
            info.AddValue("StatusCode", StatusCode);
            info.AddValue("Parameters", Parameters);
            base.GetObjectData(info, context);
        }
    }
}

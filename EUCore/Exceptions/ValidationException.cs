using System;
using System.Runtime.Serialization;
namespace EUCore.Exceptions
{
    [Serializable]
    public class NotValidatedException : EUException
    {
        protected NotValidatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class ParameterValueCannotBeZeroException : EUException
    {
        public ParameterValueCannotBeZeroException(string parameterName) : base(400, "'{@@parameterName}' can not be zero") { Parameters.Add($"@{parameterName}", parameterName); }

        protected ParameterValueCannotBeZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class ParameterCannotBeNullOrEmptyException : EUException
    {
        public ParameterCannotBeNullOrEmptyException(string parameterName) : base(400, "'{@@parameterName} can not be null or empty!") { Parameters.Add($"@{parameterName}", parameterName); }

        protected ParameterCannotBeNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class ParameterValueGreaterThanZeroException : EUException
    {
        public ParameterValueGreaterThanZeroException(string parameterName) : base(400, "{@@parameterName} should be greater than zero!") { Parameters.Add($"@{parameterName}", parameterName); }

        protected ParameterValueGreaterThanZeroException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    [Serializable]
    public class ParameterValueGreaterThanContractDateException : EUException
    {
        public ParameterValueGreaterThanContractDateException(string parameterName) : base(400, "{@@parameterName} should be greater than contract date!") { Parameters.Add($"@{parameterName}", parameterName); }

        protected ParameterValueGreaterThanContractDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

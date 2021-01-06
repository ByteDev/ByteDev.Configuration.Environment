using System;
using System.Runtime.Serialization;

namespace ByteDev.Configuration.Environment
{
    /// <summary>
    /// Represents an unexpected environment variable type exception.
    /// </summary>
    [Serializable]
    public class UnexpectedEnvironmentVariableTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException" /> class.
        /// </summary>
        public UnexpectedEnvironmentVariableTypeException() : base("Unexpected environment variable type.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnexpectedEnvironmentVariableTypeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>       
        public UnexpectedEnvironmentVariableTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException" /> class.
        /// </summary>
        /// <param name="name">Environment variable's name.</param>
        /// <param name="value">Environment variable's value.</param>
        /// <param name="expectedType">The expected type of the environment variable value.</param>
        public UnexpectedEnvironmentVariableTypeException(string name, string value, Type expectedType)
            : base($"Environment variable: '{name}' value: '{value}' is not of expected type: {expectedType.Name}.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Configuration.Environment.UnexpectedEnvironmentVariableTypeException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected UnexpectedEnvironmentVariableTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
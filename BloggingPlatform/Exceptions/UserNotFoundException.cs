namespace BloggingPlatform.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base() { }
        public UserNotFoundException(string message) : base(message) { }
        public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }

        protected UserNotFoundException(System.Runtime.Serialization.SerializationInfo info,
                                    System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}

using System;

namespace my_books.Exceptions
{
    public class PublisherNameException: Exception
    {
        public string PublisherName { get; set; }

        public PublisherNameException()
        {
            
        }
        public PublisherNameException(string message) : base(message) 
        {
            
        }
        public PublisherNameException(string message, Exception inner): base(message, inner)
        {
            
        }
        public PublisherNameException(string message, string publishername) : this(message) // we send to the exception base class only the message parameter
        {
            PublisherName = publishername;
        }
    }
}

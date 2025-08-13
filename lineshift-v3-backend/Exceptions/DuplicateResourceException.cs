namespace lineshift_v3_backend.Exceptions
{
    public class DuplicateResourceException : Exception
    {
        public DuplicateResourceException(Exception ex) : base("The request could not be processed due to conflicting data.") { }

        public DuplicateResourceException(string? message, Exception ex) : base(message, ex)
        {
        }
    }
}

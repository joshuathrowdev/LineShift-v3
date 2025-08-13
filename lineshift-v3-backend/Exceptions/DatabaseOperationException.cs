namespace lineshift_v3_backend.Exceptions
{
    public class DatabaseOperationException : Exception
    {
        public DatabaseOperationException(Exception ex) : 
            base("A database error occurred during the operation.", ex) { }
    }
}

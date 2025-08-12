namespace lineshift_v3_backend.Exceptions
{
    public class ResourceCreationException : Exception
    {
        public ResourceCreationException(Exception ex) : base("An error occurred while attempting to add the resource.", ex) { }

        public ResourceCreationException(string resourceName) : base($"Invalid operation occurred while attempting to add the '{resourceName}' resource.") { }


    }
}

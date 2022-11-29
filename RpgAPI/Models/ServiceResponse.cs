namespace RpgAPI.Models
{
    public class ServiceResponse<T>
    {
        // The (?) allows for the DataType to be nullable
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";

    }
}

namespace BlazorApp.Client.Models
{
    public class CustomerApiResponse
    {
        public List<CustomerDto>? Data { get; set; }
        public int TotalCount { get; set; }
    }
}

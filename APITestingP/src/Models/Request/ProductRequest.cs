namespace APITestingP.Models.Request;

public class ProductRequest
{
    public string name { get; set; }
    public string description {get; set; } 
    public string image { get; set; }
    public string price { get; set; }
    public string categoryId { get; set; }
}
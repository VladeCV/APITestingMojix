namespace APITestingP.Models.Response;

public class ProductResponse
{
    public int id { get; set; }
    public string name { get; set; }
    public string slug { get; set; }
    public string description { get; set; }
    public string image { get; set; }
    public decimal price { get; set; }
    public int categoryId { get; set; }
    public string createdAt { get; set; }
    public string updatedAt { get; set; }

}
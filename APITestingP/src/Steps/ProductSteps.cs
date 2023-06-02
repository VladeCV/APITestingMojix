using RestSharp;
using TechTalk.SpecFlow;
using System.Net;
using APITestingP.Models.Request;
using APITestingP.Models.Response;
using Newtonsoft.Json;

namespace APITestingP.Steps
{
    [Binding]
    public class ProductSteps
    {
        private RestClient client;
        private RestRequest request;
        private RestResponse response;
        private string productId;
        private List<ProductResponse> allProducts;
        private string token;
        private ProductResponse productResponse;
        private ProductRequest productRequest;
    
        [Given(@"the Demo Store API is available")]
        public void GivenTheDemoStoreApiIsAvailable()
        {
            client = new RestClient("http://demostore.gatling.io/api");
        }
        
        [Given(@"I am authenticated as admin")]
        public void GivenIAmAuthenticatedAsAdmin()
        {
            var userRequest = new UserRequest { username = "admin", password = "admin" };
            var authRequest = new RestRequest("/authenticate", Method.Post);
            authRequest.AddJsonBody(userRequest);

            var authResponse = client.Execute<UserResponse>(authRequest);
            Assert.That(authResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            token = authResponse.Data.token;
        }
        
        [When(@"I send a GET request to products")]
        public void WhenISendAgetRequestTo()
        {
            request = new RestRequest("/product", Method.Get);

            response = client.Execute(request);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int correctRequest)
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"the response should contain a list of products")]
        public void ThenTheResponseShouldContainAListOfProducts()
        {
            var productResponseList = JsonConvert.DeserializeObject<List<ProductResponse>>(response.Content);
            Assert.That(productResponseList, Is.Not.Null);
            Assert.That(productResponseList != null && productResponseList.Count > 0);
            
        }


        [Given(@"a product with ID ""(.*)"" exists")]
        public void GivenAProductWithIdExists(string productId)
        {
            this.productId = productId;
        }

        [When(@"I send a GET request to product ""(.*)""")]
        public void WhenISendAgetRequestTo(string productId)
        {
            request = new RestRequest($"/product/{productId}", Method.Get);
            response = client.Execute<ProductResponse>(request);
        }

        [Then(@"the response should contain the product details")]
        public void ThenTheResponseShouldContainTheProductDetails()
        {
            this.productId = productId;
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            productResponse = JsonConvert.DeserializeObject<ProductResponse>(response.Content);
            Assert.IsNotNull(productResponse);
        }
      
        [When(@"I send a POST request with the following data: (.*), (.*), (.*), (.*), (.*)")]
        public void WhenISendApostRequestWithTheFollowingData(string name, string description, string image, string price, string categoryId)
        {
            productRequest = new ProductRequest
            {
                name = name,
                description = description,
                image = image,
                price = price,
                categoryId = categoryId
            };

            request = new RestRequest("/product", Method.Post);
            request.AddJsonBody(productRequest);
            request.AddHeader("Authorization", $"Bearer {token}");

            response = client.Execute(request);
        }

        [When(@"I send a PUT request with the following data: (.*), (.*), (.*), (.*), (.*)")]
        public void WhenISendAputRequestWithTheFollowingData(string name, string description, string image, string price, string categoryId)
        {
            productRequest = new ProductRequest
            {
                name = name,
                description = description,
                image = image,
                price = price,
                categoryId = categoryId
            };

            request = new RestRequest($"/product/{productId}", Method.Put);
            request.AddJsonBody(productRequest);
            request.AddHeader("Authorization", $"Bearer {token}");

            response = client.Execute(request);
        }
    }
}


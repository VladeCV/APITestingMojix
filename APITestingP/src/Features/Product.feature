Feature: Product
Gatling DemoStore API, product requests 
    Background:
        Given the Demo Store API is available
        And I am authenticated as admin
    
    Scenario: Get All Products
        When I send a GET request to products
        Then the response status code should be 200
        And the response should contain a list of products
        
    Scenario: Get product by ID
        Given a product with ID "17" exists
        When I send a GET request to product "17"
        Then the response status code should be 200
        And the response should contain the product details
    
    Scenario: Create a new product
        When I send a POST request with the following data: <name>, <description>, <image>, <price>, <categoryId>
        Then the response status code should be 200
        And the response should contain the product details
    Examples:
      | name           | description    | image              | price | categoryId |
      | Purple Glasses | Purple Glasses | purple-glasses.jpg | 17.99 | 7          |
      | Blue Glasses | Blue Glasses | pure-glasses.jpg | 15.99 | 7          |
      | Silver Glasses | Silver Glasses | ple-glasses.jpg | 7.99 | 7          |
      
    Scenario: Update a product
        Given a product with ID "20" exists
        When I send a PUT request with the following data: <name>, <description>, <image>, <price>, <categoryId>
        Then the response status code should be 200
        And the response should contain the product details
    Examples:
      | name           | description    | image              | price | categoryId |
      | Purple Glasses | Purple Glasses | purple-glasses.jpg | 17.99 | 7          |
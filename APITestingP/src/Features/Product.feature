Feature: Product
Simple calculator for adding two numbers
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
    
    
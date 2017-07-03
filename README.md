# refaction
A terribly written Web API project that can be used as a test for potential C# applicants.  It's terrible on purpose, so that you can show us how we can improve it.

## Getting started for applicants

Fork this repository and make your changes to this project to make it better.  Simple.  There are no rules, except that we know that this project is very badly written, on purpose.  So, your job, should you choose to accept it, is to make the project better in any way you see fit.

To set up the project:

* Visual Studio 2015 is preferred.
* Open in VS.
* Restore nuget packages and rebuild.
* Run the project.

There should be these endpoints:

1. `GET /products` - gets all products.
2. `GET /products?name={name}` - finds all products matching the specified name.
3. `GET /products/{id}` - gets the project that matches the specified ID - ID is a GUID.
4. `POST /products` - creates a new product.
5. `PUT /products/{id}` - updates a product.
6. `DELETE /products/{id}` - deletes a product and its options.
7. `GET /products/{id}/options` - finds all options for a specified product.
8. `GET /products/{id}/options/{optionId}` - finds the specified product option for the specified product.
9. `POST /products/{id}/options` - adds a new product option to the specified product.
10. `PUT /products/{id}/options/{optionId}` - updates the specified product option.
11. `DELETE /products/{id}/options/{optionId}` - deletes the specified product option.

All models are specified in the `/Models` folder, but should conform to:

**Product:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description",
  "Price": 123.45,
  "DeliveryPrice": 12.34
}
```

**Products:**
```
{
  "Items": [
    {
      // product
    },
    {
      // product
    }
  ]
}
```

**Product Option:**
```
{
  "Id": "01234567-89ab-cdef-0123-456789abcdef",
  "Name": "Product name",
  "Description": "Product description"
}
```

**Product Options:**
```
{
  "Items": [
    {
      // product option
    },
    {
      // product option
    }
  ]
}
```

## Once you're done

Create a pull request back into this repository and describe, in as much detail as you feel necessary, what you have done to improve this project. Include your full name in the title as it appears on your CV so we can match it back to your job application. We'll take it from there and review.

Good luck!

## Updates

Integration Tests

ProductTests: Started
Product 8f2e9176-35ee-4f0a-ae55-83023d2db1a3, Samsung Galaxy S7, Newest mobile product from Samsung., 1024.99, 16.99
Product de1287c0-4b15-4a7b-9d8a-dd21b3cafec3, Apple iPhone 6S, Newest mobile product from Apple., 1299.99, 15.99
ProductTests: TestGetAll OK
Product 8f2e9176-35ee-4f0a-ae55-83023d2db1a3, Samsung Galaxy S7, Newest mobile product from Samsung., 1024.99, 16.99
ProductTests: TestGet OK
Product 8f2e9176-35ee-4f0a-ae55-83023d2db1a3, Samsung Galaxy S7, Newest mobile product from Samsung., 1024.99, 16.99
ProductTests: TestGetByName OK
ProductTests: TestCreateAndDelete OK
ProductTests: TestUpdateAndDelete OK
ProductTests: Complete
ProductOptionTests: Started
ProductOption 0643ccf0-ab00-4862-b3c5-40e2731abcc9, 8f2e9176-35ee-4f0a-ae55-83023d2db1a3 White, White Samsung Galaxy S7
ProductOption a21d5777-a655-4020-b431-624bb331e9a2, 8f2e9176-35ee-4f0a-ae55-83023d2db1a3 Black, Black Samsung Galaxy S7
ProductOption 5c2996ab-54ad-4999-92d2-89245682d534, de1287c0-4b15-4a7b-9d8a-dd21b3cafec3 Rose Gold, Gold Apple iPhone 6S
ProductOption 9ae6f477-a010-4ec9-b6a8-92a85d6c5f03, de1287c0-4b15-4a7b-9d8a-dd21b3cafec3 White, White Apple iPhone 6S
ProductOption 4e2bc5f2-699a-4c42-802e-ce4b4d2ac0ef, de1287c0-4b15-4a7b-9d8a-dd21b3cafec3 Black, Black Apple iPhone 6S
ProductOptionTests: TestGetAll OK
ProductOption 0643ccf0-ab00-4862-b3c5-40e2731abcc9, 8f2e9176-35ee-4f0a-ae55-83023d2db1a3 White, White Samsung Galaxy S7
ProductOptionTests: TestGet OK
ProductOptionTests: TestCreateAndDelete OK
ProductOptionTests: Complete




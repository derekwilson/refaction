# The brief

Make your changes to this project to make it better.

Create a pull request back into this repository and describe, in as much detail as you feel necessary, what you have done to improve this project.

## Assumptions and constraints

Although the general architecture of the service is questionable, as is the wire protocol, without knowing the client requirements it seems foolish to make changes that will affect the external client. So I have kept the endpoints and the wire protocol the same.

Benefit could be gained from upgrading the application to later version of MVC (for example built in IoC) I have decided to keep the target platform requirements unchanged as I have no information about where it will be deployed. 

I only have 48 hours - which given that I am at work this week means that I have two evenings to complete the exercise. Although there are many more things that could be done I have prioritised the ones I feel are most important and I have identified what could be done next if more resource is available.


## Changes

### Testing

There is no tests of any description. I could implement unit tests or integration tests. I chose to start with integration tests because

1. I wanted to be sure that the major refactor of the data access layer was working before I started work on the web service.
1. Tests are most useful for exercising complex logic, unit tests would be good for testing the controllers or a service layer however as it stands there is very little logic in this layer. Most of the complex logic is in the data access layer which means that I get more benefit from integration tests at this stage of development. This will change as the service layer evolves but we can add unit tests when that happens.  

#### Integration Tests

To run the integration tests build the solution and find the `IntegrationTests.exe`, it is in `.\IntegrationTests\bin\debug`. You need to copy a `Database.mdf` into the same folder as the exe. Also the machine will need to have SQLServer 2014 installed - any SKU will do.

When you run the tests a successful result should look like this

```
ProductTests: Started
Product 8f2e9176-35ee-4f0a-ae55-83023d2db1a3, Samsung Galaxy S7, Newest mobile product from Samsung., 1024.99, 16.99 Product de1287c0-4b15-4a7b-9d8a-dd21b3cafec3, Apple iPhone 6S, Newest mobile product from Apple., 1299.99, 15.99
ProductTests: TestGetAll OK
Product 8f2e9176-35ee-4f0a-ae55-83023d2db1a3, Samsung Galaxy S7, Newest mobile product from Samsung., 1024.99, 16.99
ProductTests: TestGet OK
Product 8f2e9176-35ee-4f0a-ae55-83023d2db1a3, Samsung Galaxy S7, Newest mobile product from Samsung., 1024.99, 16.99
ProductTests: TestGetByName OK
ProductTests: TestCreateAndDelete OK
ProductTests: TestUpdateAndDelete OK
ProductTests: TestDeleteInvalid OK
ProductTests: Complete
ProductOptionTests: Started
ProductOption 0643ccf0-ab00-4862-b3c5-40e2731abcc9, 8f2e9176-35ee-4f0a-ae55-83023d2db1a3 White, White Samsung Galaxy S7 ProductOption a21d5777-a655-4020-b431-624bb331e9a2, 8f2e9176-35ee-4f0a-ae55-83023d2db1a3 Black, Black Samsung Galaxy S7 ProductOption 5c2996ab-54ad-4999-92d2-89245682d534, de1287c0-4b15-4a7b-9d8a-dd21b3cafec3 Rose Gold, Gold Apple iPhone 6S ProductOption 9ae6f477-a010-4ec9-b6a8-92a85d6c5f03, de1287c0-4b15-4a7b-9d8a-dd21b3cafec3 White, White Apple iPhone 6S ProductOption 4e2bc5f2-699a-4c42-802e-ce4b4d2ac0ef, de1287c0-4b15-4a7b-9d8a-dd21b3cafec3 Black, Black Apple iPhone 6S
ProductOptionTests: TestGetAll OK
ProductOption 0643ccf0-ab00-4862-b3c5-40e2731abcc9, 8f2e9176-35ee-4f0a-ae55-83023d2db1a3 White, White Samsung Galaxy S7
ProductOptionTests: TestGet OK
ProductOptionTests: TestCreateAndDelete OK
ProductOptionTests: Complete
ProductAndProductOptionTests: Started
ProductAndProductOptionTests: TestDelete OK
ProductAndProductOptionTests: Complete
```

The tests clean up after themselves so they can be run repeatedly.

### Assemble structure

Initially the code was all in one assembly. I have split the code across theses assemblies

1. Domain: this is the model and data access layer. It has very limited dependencies on core .NET, SQLServer Client and Dapper. All these are available in .NET core so this assembly could easily be converted to being dependent on .NET Standard.
1. refactor-me: the MVC web service, not the greatest name but I dont have resharper installed and could not face manually renaming the namespaces. Usually I would rename this assembly. It uses the Domain to access the DB.
1. IntegrationTests: The tests use the Domain to ensure the data access layer is working as expected. 
1. UnitTests: The unit tests use the refactor-me MVC web service to test the controller endpoints

### Data access

The initial data access objects had some problems.

1. they were embedded in the web service 
1. they concatenated SQL that was susceptible to SQL injection attacks.
1. the model objects and the data access methods were mixed up in the same class 
1. the connection string was hard-coded 
1. they were inefficient, for example loading an object before deleting it 
1. they were unsafe in that they attempted to delete ProductOptions and a Product in two separate commands without using a transaction

I thought that I would get the greatest improvement by concentrating on this layer. This lead me to start with integration tests rather than unit tests.

#### Changes

The changes made were

1. move all the data access layer code to the Domain assembly so it could be more easily tested 
1. provide the connection string via an IoC factory pattern so that the tests and the service could have different connections 
1. use Dapper for object mapping to reduce hard coded column names and to reduce the threat of SQL injection 
1. implement a factory pattern to provide a clear concrete interface for this layer and to enable IoC in the future 
1. Make the delete product method transactionally delete the product and its options in one operation and test it 
1. Integration tests as described above.

### Web service

The web service itself needed to be changed to use the new repository pattern but there were other changes made as well.

1. Remove all the existing Model objects as they are superseded by the Domain objects 
1. Adding a Dto object to ensure that the wire protocol stays the same 
1. Added Unity for dependency injection and inject the correct connection string 
1. Reworked the controller endpoints so that they return a IHttpActionResult and use the standard methods of Ok() and StatusCode() to return data and errors. This will make it easier to add unit testing and convert the service to be asynchronous (if that is needed for performance).

#### Unit Testing

A unit test project has been added using NUnit and Moq. Its only tests a couple of the controller endpoints but does demonstrate how the controller can be tested.

To run the unit tests you will need to make sure you have installed the NUnit 3 Test Adapter from the Tools | Extensions and Updates menu in Visual Studio.

#### End to End Testing

End-to-end testing was done using Postman and a simple suite of requests, these are also included.

## The future

If more resource was available then the following items should be addressed

1. Complete the unit tests for the MVC controller
1. Add a foreign key constraint for the ProductOption.ProductId to Product.Id relationship - this would usually be added into the script used to generate/update the DB schema 
1. Move logic from the controller to a service layer and write unit tests for the logic. For example update should 404 if the id specified cannot be found.
1. Add logging - NLog / Log4Net ?
1. Consider adding mini-profiler to instrument the service.


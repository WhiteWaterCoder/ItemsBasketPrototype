# ItemsBasket Prototype #

This is a prototype implementation of the requested API for allowing customers to manage a basket of items along with a client library for allowing .net applications to easily consume the REST api.

## Assumptions ##

The following assumptions were made for this implementation:

* If a registered customer does not have a basket, this will be created when the first call to the API is made.
* A customer can only have one basket at a time. 
* Since this is a single basket implementation, it is assumed that a shopping cart like functionality is required. This means that only the following operations are supported:
	* Fetch all items currently in the basket.
	* Add a single item to the basket (adding multiple items is rare in most websites)
	* Remove a single item from the basket (removing multiple items is rare in most websites, with the exception of clearing the basket as described below)
	* Clear all the items from the basket.
	* Update one or more items in the basket. In this case:
		* If the item does not exist in the basket it will be added.
		* If the quantity is changed to zero it will be removed.
* Once the user completes the purchase then the Clear all items call can be made to start from scratch.
* The backend store is an in memory dictionary so only a single instance of the services can run as of now.

## Services ##

A micro-services approach was used for the implementation of this solution.

### Authentication service ###

This service is responsible for 2 things:

* Manage user accounts by allowing anonymous users to add an account as well as modify and delete their accounts.
* Authenticate a user and return a token which is required for the basket API.

The implementation of this service is extremely basic and has no real security around it (plain test password etc). It is more of a stub than anything else but the idea is that the authentication is separated from the basket service. If a user has got a (non-expired) token in his session then there is no need to access this service so if it goes down then the disruption to the system is reduced.

When up and running, the service endpoints are:

* API: 
	* http://localhost:8001/api/Users
	* http://localhost:8001/api/Authentication
* Swagger: http://localhost:8001/swagger/

### Basket Service ###

The main API for dealing with user baskets. The API requires authorization so all calls require the user token. The API infers the user details (mainly the user ID) from the claims included in the token so the API calls are simpler (no need to include user details in it).

When up and running, the service endpoints are:

* API: 
	* http://localhost:8002/api/BasketItems
* Swagger: http://localhost:8002/swagger/

## Client Library ##

The client library is an abstraction over the REST API and offers the following features over consuming the REST APIs directly:

* Contains endpoint details so user does not need to supply them (this is a bit basic and could be extended to use service location)
* Crude token session handling.
:exclamation: The implementation not at all ideal for web scenarios as it requires the token manually set each time a call to the basket service is made. The token should be stored in the user's session (which is expected and fine) but should be set each time on the **HttpClientProvider** class to ensure that the correct user token is used.

The end state of this library would be to be packaged in Nuget.

## Debugging and deployment ##

You can simply pull the code and run it from Visual Studio. You will need to set multiple startup projects and start both the **Authentication** and **BasketItems** services.

Alternatively you can use Docker. Just navigate to the root folder of the project and type: **docker-compose up**. When complete, both the **Authentication** and **BasketItems** services should be running on their expected ports.

## Other API points ##

The following designs are applied throughout the web and client APIs:

* All methods are async and can be consumed as such.
* No exceptions are expected to be returned to calling methods. Response objects with a success flag and an error message string are used throughout so the consumer code can act accordingly.
* Swagger documentation is available on all web APIs.

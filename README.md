# ToyStore Project

## Requirements

- You will create a "Store Application" for a store type of your choosing
- (i.e. a chain of grocery stores, a chain or jewelry stores, clothing, skateboards, etc).
- You will create an HTML/CSS/JS user interface Front-end (FE)
- that allows the user to log in,
- choose a store from a list of locations,
- view all the available products of that store,
- fill a cart with a user-chosen number and type of products (from a single store at a time)
- and then check out.
- The user will be able to view details of only their own past orders
- The user will be able to view a history of order summaries (customer name and total) of any one specific location (at a time).
- You do not need to include an admin access. Assume any user can do/view these things.
- Upon log-out, the program will not quit but allow another user to log in.
- The FE will utilize a .NET ASP.NET WebApi to store data.
- The WebApp will use Entity Framework to persist data to a SSMS database.
- Follow SOLID principles.
- Use modularization of methods and creation of classes to handle logically related functionality.

## functionality

- place orders at store locations
- Register/Login
- search customers by name
- display details of an order
- display all order history of a store location
- display all order history of a customer
- client-side validation (in HTML/JS)
- exception handling
- persistent data (no prices, customers, order history, store locations, etc. hardcoded in C# or JS)
- logging where appropriate
- (optional: order history can be sorted by earliest, latest, cheapest, most expensive, etc)
- (optional: get a suggested order for a customer based on his order history)
- (optional: display statistics based on order history)
- (optional: asynchronous network & file I/O)

## design

- use EF Core (either database-first approach or code-first approach)
- use a SQL DB in third normal form (3NF)
- define and use at least one interface
- (optional: Deploy to publicly available Azure App Service website)

## core / domain and business logic

- is .NET class library
- contains all business logic
- contains domain classes (customer, order, store, product, etc.)
- documentation with XML comments on all public types and members

## customer

- ✔ has first name, last name, etc.
- (optional: has a default store location to order from)
- customer can view orders from whole store
- customer can order

## order details

- ✔ has a store location
- ✔ has a customer
- ✔ has order completion time
- ✔ can contain multiple types and quantities of product in the same order
- rejects orders with unreasonably high product quantities (or more than is available)
- (optional: some additional business rules, like special deals, bundles)

## location

- ✔ has an inventory
- inventory decreases when orders are accepted
- rejects orders that cannot be fulfilled with remaining inventory
- (optional: for a product, more than one inventory item decrements when ordering that product)
- (optional) tells you the other location has your option
- (optional) additional fee for ordering from a store using another store

## product

- has a
- description
- price
- quantity available
- name

## (optional) offer

- has multiple products
- description
- price
- inventory quantity <-- must not be greater than each of the products

## user interface

- HTML, CSS, and JavaScript pages that utilize a ASP.NET Core Web Application
- separate request processing and presentation concerns with MVC pattern
- customize the styling using stylesheets
- keep CodeNamesLikeThis out of the visible UI.
- Create different models for customers to view from and populate it from database model

# data access

- contained in separate class library project
- use Dependency Injection for database context
- contains EF Core DbContext and entity classes
- contains data access logic but no business logic
- use Repository Pattern for Separation of Concerns

## test

- at least 20 test methods
- focus on unit testing business logic

### for macos to create a SQL Server:

> docker run --name sql_server_demo -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=1Secure\*Password1' -e 'MSSQL_PID=Enterprise' -p 1433:1433 -d microsoft/mssql-server-linux:2017-latest

> docker exec -it sql_server_demo /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 1Secure\*Password1

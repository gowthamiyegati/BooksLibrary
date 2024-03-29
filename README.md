# ProjectInfo
Small books Library consisting Authors, Books, Logs and Publishers can be maintained using this web API application. Its built using .net 6.0 and used NUnit for unit test cases
This project used following technologies:
 - .NET 6.0
 - Three-layer architecture
 - Entity Framework Core
 - Microsoft SQL Server

# Approach
Followed TDD and AAA for tests

Dependency injection pattern for loosely coupled architecure.

Solid principles

# Prerequisite
Install VS2022 Professional or community edition.
Install Powershell.

# Assumptions
git available

# How to build the code
1. Clone the repository using GitBash/Powershell from Github
2. Open the .sln file using Visual Studio 2022
3. Press on Ctrl+Shift+B to build the code.

o	How to run the application
1. Run BookLibraryAPI application in visual studio
2. This will enable following APIs
   
### Authors Controller

 - GET - get-all - Getting list of all authors from database.
 - POST - add-author - Add author to database.
 - GET - get-author-with-books-by-id/{id} - Gets all the books of an author.

 ### Books Controller

 - POST - add-book-with-authors - Adding book to database.
 - GET - get-all-books - Getting list of all authors from database.
 - GET - get-book-by-id/{id} - Gets book details using Id.
 - PUT - update-book-by-id/{id} - Update book details using Id.
 - DELETE - delete-book-by-id/{id} - Delete book details using Id.
 
 ### Logs Controller

 - GET - get-all-logs-from-db - Gets all the logs of books in database.

 ### Publishers Controller

 - POST - add-publisher - Add publisher to database.
 - GET - get-all - Getting list of all publishers from database.
 - GET - get-publisher-by-id/{id} - Gets publisher details using Id.
 - GET - get-publisher-books-with-author/{id} - Gets all the books of an publisher. 
 - DELETE - delete-publisher--by-id/{id} - Delete publisher details using Id.
   
4. UnitTest
     
     BookLibraryTests.csproj is the unit testing project to run the unit test cases.
  
    
   
   

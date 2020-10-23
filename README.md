# _MVC Sweets and Treats_

#### _An app to market sweet and savory treats!, 10/23/20_

#### By _**Ben Stoller**_

## Description



## User Stories

* The application should have user authentication. A user should be able to log in and log out. Only logged in users should have create, update and delete functionality. All users should be able to have read functionality.

* There should be a many-to-many relationship between Treats and Flavors. A treat can have many flavors (such as sweet, savory, spicy, or creamy) and a flavor can have many treats. For instance, the "sweet" flavor could include chocolate croissants, cheesecake, and so on.

* A user should be able to navigate to a splash page that lists all treats and flavors. Users should be able to click on an individual treat or flavor to see all the treats/flavors that belong to it.


Stretch Goals Completed:






## Setup/Installation Requirements

NOTE - you can do the following to manually set up the databse for the project OR you can run the command 'dotnet ef database update' after you clone as explained below in 'Website Setup'.

## MySQL Workbench Schema Setup:
1. Open [MySql Workbench](https://www.mysql.com/products/workbench/) and connect to Local instance
2. Create a new sql tab by clicking the upper left icon: 'Create A New SQL Tab for Executing Queries'
3. Copy and paste the following code into "Query" and "Run":
---
### **Copy The Following Code:**


## Website Setup:
* Download or Clone project from Github repository.
* Open a terminal within Factory folder within the main project directory.
* Use command: 'dotnet restore' to install.
* After installation, type in 'dotnet build'.
* Use command 'dotnet ef database update' to scaffold the database for the project. 
* After build, run the program with 'dotnet run' in the terminal.
* If you don't have it already, create an "appsettings.json" file in the "ben_stoller" directory and Insert the code below with your user name and password for MySQL: 

> {
>  "ConnectionStrings": {
>      "DefaultConnection": "Server=localhost;Port=3306;database=ben_stoller;uid={YOUR USERNAME};pwd={YOUR USERNAME}"
>  }
>}

* Follow terminal prompts to see application run.


## Known Bugs




## Support and contact details

https://github.com/StollerSystem

## Technologies Used

C#, .NET Core, LINQ, Entity Framework Core, MySql, CSHTML, CSS, Bootstrap and Markdown.


### License

MIT

Copyright (c) 2020 **_Ben Stoller_**


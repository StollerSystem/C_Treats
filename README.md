# _MVC Sweets and Treats_

#### _An app to market sweet and savory treats!, 10/23/20_

#### By _**Ben Stoller**_

## Description

* As a user who has not logged in, you can view all the different Treats and Flavors, click on them to see their details and view corresponding Treats/Flavors they are combined with. 
* You can register an account, then after you are logged in you will be able to go to the "orders" tab and create new orders. Once you create an order you can start adding Treats to it! You can add from the order view OR from the details page of a specific treat you're viewing. Once you've added some treats to your order the order details page will display a grand total of how much your order will be! 
* In order to create, edit and delete Treats/Flavors you need to be logged in as an Admin  (email: admin@treats.local password: NotSecure123!!) 


## User Stories

* The application should have user authentication. A user should be able to log in and log out. Only logged in users should have create, update and delete functionality. All users should be able to have read functionality.

* There should be a many-to-many relationship between Treats and Flavors. A treat can have many flavors (such as sweet, savory, spicy, or creamy) and a flavor can have many treats. For instance, the "sweet" flavor could include chocolate croissants, cheesecake, and so on.

* A user should be able to navigate to a splash page that lists all treats and flavors. Users should be able to click on an individual treat or flavor to see all the treats/flavors that belong to it.


## Stretch Goals Completed:

* Have separate roles for admins and logged-in users. Only admins should be able to add, update and delete Treats and Flavors.

* Add an order form that only logged-in users can access. A logged-in user should be able to create, read, update and delete their own orders.



## Setup/Installation Requirements


* Download or Clone project from Github repository.
* Open a terminal within Treats folder within the main project directory.
* Use command: 'dotnet restore' to install dependencies.
* Use command 'dotnet ef database update' to scaffold the database for the project. 
* Use command 'dotnet build' to build the project.
* After build, run the program with 'dotnet run' in the terminal.
* If you don't have it already, create an "appsettings.json" file in the "ben_stoller" directory and Insert the code below with your user name and password for MySQL: 

> {
>  "ConnectionStrings": {
>      "DefaultConnection": "Server=localhost;Port=3306;database=ben_stoller;uid={YOUR USERNAME};pwd={YOUR USERNAME}"
>  }
>}



## Known Bugs



## Support and contact details

https://github.com/StollerSystem

## Technologies Used

C#, .NET Core, LINQ, Entity Framework Core, MySql, CSHTML, CSS, Bootstrap and Markdown.


### License

MIT

Copyright (c) 2020 **_Ben Stoller_**


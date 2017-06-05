# WebApplication by Periklis Stathopoulos
Company web Application

---------------------------------- About The App --------------------------------------------------------------------------------------------

This is an application made for test.
It handles the departments and employees of a company.
In order to do this, it implments the CRUD methods for department and employees.

---------------------------------- About The App --------------------------------------------------------------------------------------------





---------------------------------- Technologies Used ----------------------------------------------------------------------------------------

It is developed using Entity Framework (code first) , Asp.NET MVC and Unity application block. 
For the front end part, Bootstrap and JQuery are used.

The server side validation of data is made using the FluentValidation.

The Create methods are implemented using the bootstrap modal pop up and ajax calls.

The Edit methods on the other hand are implemented using the jquery fancybox. 
In order to achieve this the DepartmentEditView and EmployeeEditView views are used which hold the edit forms.


Two main classes are used Department and Employee which handle the basic functions for inserting, updating and deleting the objects from db.

A general Helper is also used, the CompanyManager which is registered to the IoC once and is called whenever we need to perform an action.

---------------------------------- Technologies Used ----------------------------------------------------------------------------------------





---------------------------------- Unit Test ------------------------------------------------------------------------------------------------

The solution contains a simple unit test which edit's the first employee and assigns her to the first available department (if there is one)

---------------------------------- Unit Test ------------------------------------------------------------------------------------------------





---------------------------------- Configuration --------------------------------------------------------------------------------------------

In web config file the section 'connectionStrings' defines the data source and database to be created and used for the project. If you want 
to use a database different from the local bd, all you have to do is change the 'connectionString' properly. 
In the 'Initial Catalog' property you can define the name of the database to be created. Also you can define a login. (user and password)

Keep in mind though that the data source you are about to use must have a user with the authority to Create Database or the solution will not
be able to create your database, and so it will not be able to work properly.
 
---------------------------------- Configuration --------------------------------------------------------------------------------------------



!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! CAUTION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

The project uses the latest .net framework 4.6.2, so if you have not installed it, then the visual studio will ask you to degrade the project 
to the latest you have, or to install 4.6.2 in your pc. 
The project will work either way.

!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! CAUTION!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


Thank you,
Have fun.

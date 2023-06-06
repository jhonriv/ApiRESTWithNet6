# ApiRESTWithNet6

**Description**
	
Simple REST API application with .NET 6, using JWT services for user access control.

--------------

**Usage Requirements**

REST APIs created and tested with:
	
	- Windows 11 64-bit operating system.
	- Microsoft Visual Studio 2022.
	-.Net 6.
	- Entity Framework Core.
	- Microsoft SQL Server 2019, 64-bit with Management Studio.
	- Postman.

--------------

**Instructions for use**

	1.- In the appsettings.json file, configure:
		1.1.- The connection string for your SQL Server. (DefaultConnection)
		1.2.- Write the values ​​for the JWT authentication, as indicated by the JWT documentation. https://jwt.io/introduction

	2.- Execute in the Package Manager Console:
		2.1- Add-Migration Initial
		2.2- Update-Database

	3.- You can start the project, the default user is user: "admin", pass: "admin". You can create more users with different roles:
		3.1- Admin role, has access to all endpoints, can view and edit products, view and edit users.
		3.2- User role, has access only to product endpoints, does not have access to users.
# Team Management App

ASP.NET MVC Team Management App using Syncfusion, Bootstrap, EF Core as ORM, SQL Server as DB, Identity for Auth. Consists of two pages: Kanban Board page and File page.

### Kanban Board
It consists of a Kanban Board at its main page where tasks can be assigned to different employees which are derived from the list of users that are registered via third party single-sign-on authentication like Google or its own email and password based authentication - both created from scaffolding Identity API.
![image](https://github.com/stefank1995/team-management-app/assets/132662524/a142902b-328b-444f-9252-4d8524ada15b)

Kanban board interface has been created using Syncfusion's Kanban Board. Tasks can be created by clicking the plus sign and filling the form that will pop up:
![Adding task](https://github.com/stefank1995/team-management-app/assets/132662524/69776d0d-3cf2-4145-94cb-aecf32beba2b)

Tasks can be edited by double clicking the task we want to edit:
![Editing tasks](https://github.com/stefank1995/team-management-app/assets/132662524/f8c0694e-c49e-4274-81d4-eeecc804ca9c)


Tasks can also be moved to other columns or employees by simply dragging the task to its desired position:
![Moving task](https://github.com/stefank1995/team-management-app/assets/132662524/b29da5b0-258c-4cc7-8c77-c6a325bca139)

Data persistence: All the Kanban data is being stored inside SQL Server database.

### File Management System
Aside from its main page it has a File Management System page where employees can upload and download files that are necessary for the team. It tracks which employee uploaded the file and its upload date and time. The app has a role-based authorization so depending on the role that a signed-in user has, it will permit or prevent the user from deleting files that were uploaded from other employees.
![image](https://github.com/stefank1995/team-management-app/assets/132662524/6145b275-dc40-4919-b8b5-668d94bc4620)



## Built With

* [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) - The web framework used
* [Syncfusion](https://www.syncfusion.com/) - Used for some UI components
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) - Used to persist data





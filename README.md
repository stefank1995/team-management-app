# Team Management App

ASP.NET MVC Team Management App using Syncfusion, Bootstrap, EF Core as ORM, SQL Server as DB, Identity for Auth. Consists of five pages: Kanban Board page, File page, Teams Page, Account and Settings.

### Login Page
Introducing a login page, built with Identity scaffolding, providing email and password-based authentication along with third-party login options like Google.
![Login](https://github.com/stefank1995/team-management-app/assets/132662524/93872f9a-03fe-4958-a815-ceeb4a3c7202)

### Kanban Board
Kanban Board is at app's main page where tasks can be assigned to different employees which are derived from the list of users that are registered via third party single-sign-on authentication like Google or its own email and password based authentication - both created from scaffolding Identity API.
![main](https://github.com/stefank1995/team-management-app/assets/132662524/47fd9dd1-ebdd-403e-8938-2a1583117949)


Kanban board interface has been created using Syncfusion's Kanban Board. Tasks can be created by clicking the plus sign and filling the form that will pop up:
![Adding task](https://github.com/stefank1995/team-management-app/assets/132662524/69776d0d-3cf2-4145-94cb-aecf32beba2b)

Tasks can be edited by double clicking the task we want to edit:
![Editing tasks](https://github.com/stefank1995/team-management-app/assets/132662524/f8c0694e-c49e-4274-81d4-eeecc804ca9c)


Tasks can also be moved to other columns or employees by simply dragging the task to its desired position:
![Moving task](https://github.com/stefank1995/team-management-app/assets/132662524/b29da5b0-258c-4cc7-8c77-c6a325bca139)

Data persistence: All the Kanban data is being stored inside SQL Server database.

### File Management System
Aside from its main page the app has a File Management System page where employees can upload and download files that are necessary for the team. It tracks which employee uploaded the file and its upload date and time. The app has a role-based authorization so depending on the role that a signed-in user has, it will permit or prevent the user from deleting files that were uploaded from other employees. Files are store inside a Datatables table with pagination.
![File](https://github.com/stefank1995/team-management-app/assets/132662524/657c250b-5696-4f5c-b3b8-01bb5aa67e7b)

### Teams
(Work in progress)Teams page where users can create teams, assign team members, and engage in chat conversations specifically related to their chosen teams. The chat functionality is facilitated by SignalR, ensuring real-time communication and notification updates.

### Settings
The Settings page allows users to control preferences like dark mode, Kanban board swimlanes, and create or assign authorisation user roles, if the user is administratior.
![image](https://github.com/stefank1995/team-management-app/assets/132662524/3ef78f21-d3c3-4778-a14d-bde4cc097267)

## Responsive UI
Responsive UI design adapts to various screen sizes, ensuring a seamless experience on any device.
![Responsive Main](https://github.com/stefank1995/team-management-app/assets/132662524/097bdda3-3282-43d7-8168-e59dd298b9a4)
![Responsive settings](https://github.com/stefank1995/team-management-app/assets/132662524/19ea3773-657f-4af8-8dcb-73cc2509bfaf)

## Built With

* [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) - The web framework used
* [Syncfusion](https://www.syncfusion.com/) - Used for some UI components
* [Bootstrap](https://getbootstrap.com/) - Used for some UI components
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) - Used to persist data
* [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr) - Used for adding real-time functionality





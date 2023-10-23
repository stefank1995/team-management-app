# Team Management App

ASP.NET MVC Team Management App using Syncfusion, Bootstrap, EF Core as ORM, SQL Server as DB, Identity for Auth. Consists of these pages: Login and Register page, Kanban Board homepage, File page, Teams Page, Account and Settings.

### Login Page
Introducing a login page, built with Identity scaffolding, providing email and password-based authentication along with a third-party login options like Google and Microsoft.
![image](https://github.com/stefank1995/team-management-app/assets/132662524/ed730e39-877b-4f60-881b-affc7e5d17fc)

### Kanban Board
Kanban Board is at apps main page where tasks can be assigned to different employees which are derived from the list of users that are registered via third party single-sign-on authentication like Google or its own email and password based authentication - both created from scaffolding Identity API.
![image](https://github.com/stefank1995/team-management-app/assets/132662524/3c7b930e-77c6-499e-b7e3-292fe8c20d8b)

App has also a swimlanes toggle in the settings page. Kanban swimlanes provide a visual way to categorize and separate tasks within a project. This feature enhances organization and clarity, making it easier to manage and track progress.
![image](https://github.com/stefank1995/team-management-app/assets/132662524/e8f23497-5a25-4b94-9919-056c46b939bc)

Kanban board interface has been created using Syncfusion's Kanban Board. Tasks can be created by clicking the plus sign and filling the form that will pop up:
![Adding task](https://github.com/stefank1995/team-management-app/assets/132662524/69776d0d-3cf2-4145-94cb-aecf32beba2b)

Tasks can be edited by double clicking the task we want to edit:
![Editing tasks](https://github.com/stefank1995/team-management-app/assets/132662524/f8c0694e-c49e-4274-81d4-eeecc804ca9c)


Tasks can also be moved to other columns or employees by simply dragging the task to its desired position:
![Moving task](https://github.com/stefank1995/team-management-app/assets/132662524/b29da5b0-258c-4cc7-8c77-c6a325bca139)

Data persistence: All the Kanban data is being stored inside SQL Server database.

### File Management System
Aside from its main page the app has a File Management System page where employees can upload and download files that are necessary for the team. It tracks which employee uploaded the file and its upload date and time. The app has a role-based authorization so depending on the role that a signed-in user has, it will permit or prevent the user from deleting files that were uploaded from other employees. Files are stored inside a Datatables table with pagination.
![File](https://github.com/stefank1995/team-management-app/assets/132662524/af41838d-1a9e-4389-a303-905c460be8a7)

### Teams
(Work in progress)Teams page where users can create teams, assign team members, and engage in chat conversations specifically related to their chosen teams. The chat functionality is facilitated by SignalR, ensuring real-time communication and notification updates.

### Settings
The Settings page allows users to control preferences like dark mode, Kanban board swimlanes, and create or assign authorisation user roles, if the user is administratior.
![image](https://github.com/stefank1995/team-management-app/assets/132662524/3ef78f21-d3c3-4778-a14d-bde4cc097267)

### Responsive UI
Responsive UI design adapts to various screen sizes, ensuring a seamless experience on any device.
![Responsive Main](https://github.com/stefank1995/team-management-app/assets/132662524/097bdda3-3282-43d7-8168-e59dd298b9a4)
![Responsive settings](https://github.com/stefank1995/team-management-app/assets/132662524/19ea3773-657f-4af8-8dcb-73cc2509bfaf)

## Built With

* [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) - The web framework used
* [Syncfusion](https://www.syncfusion.com/) - Used for some UI components
* [Bootstrap](https://getbootstrap.com/) - Used for some UI components
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) - Used to persist data
* [SignalR](https://dotnet.microsoft.com/en-us/apps/aspnet/signalr) - Used for adding real-time functionality





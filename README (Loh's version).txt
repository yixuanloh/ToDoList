# Important

This project template is downloaded from https://aspnetboilerplate.com/Templates (ASP.NET Core with Angular).
Login function is already included in the template but I hide it by commenting the route for a better visual but it's still there.
The default landing page would still be the about page, you have to navigate to To-Do-List by clicking To-Do-List at the sidebar menu.

# Setup

1. Download the source code from the github repository.

2. For frontend, cd into src folder and choose to open "ToDoList.Web.Host" by using any editor you like.
   2.1 Run "npm install" to install all dependencies.
   2.2 Run "npm start" to start the development on localhost.

3. For backend, click on the solution project which is "ToDoList.sln"
   3.1 Make sure that your local database name are the same as the connection string that is located in "appsetings.json" that is located in "ToDoList.Web.Host" project
   3.2 Switch to package-manager-console and select the default project to ToDoList.EntityFrameworkCore only run the command "Update-Database" to create the tables for the project.
   3.3 Before starting the backend server, make sure that you choose "ToDoList.Web.Host" and just only start it.

4. You're good to go!

# Instruction

1. Navigate to To-Do-List page by clicking on the sidebar menu.

2. The page you seeing would be the todolist page, create a new todolist.

3. Once a todolist is created, there would be 2 button on the right side, one would be the delete button and another would be the button to a new page where you can create a list of item under the todolist.

4. Once you're inside the new page under the todolist, you can create a new item by giving it a name and datetime.

5. Once you created a new item, there will be also 2 button on the right side, one would be the delete button and another would be the "mark as completed" button.

6. There will also be a desktop notification when the datetime of an item is reached.

# Additional Info

When all items in a todolist are marked as completed, the status of the todolist will be updated automatically **the small bar on the left side would turn green indicating complete**

When there are new items added into the todolist, the status of the todolist will be updated automatically as well **the small bar on the left side would turn yellow indicating incomplete**

When you delete an items or a few items, if the leftover items are all in complete status, the todolist will be updated to complete as well,
if the leftover items are a mixture of complete and incomplete status, the todolist will be updated to incomplete.
   
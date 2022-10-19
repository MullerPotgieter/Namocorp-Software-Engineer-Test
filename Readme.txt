1. Open the Namocorp Contacts Manager.sln solution file in an IDE. Preferably Microsoft Visual Studio.

2. Open the DatabaseConnectionString.json file and update the DatabaseConnectionString field to point to an existing database.

3. Open the NuGet package manager console. (In Visual Studio: Tools -> Nuget Package Manager -> Package Manager Console)

4. Run these commands:
	- Add-Migration "Initial Migration"
	- Update Database
	
5. Once these steps have been completed, the project may be run within an IDE.

6. If so wished, a deployable version of the project is located in the Deployable folder. It may be deployed in the IIS after the migrations have been executed.
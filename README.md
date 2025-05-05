# Volvo Project Challenge

This is the project for the Volvo recruitment process.

## Technologies Used

- **.NET 8 MVC**
- **Razor Views** for the user interface
- **Bootstrap** for responsive design
- **SQL Server** as the database
- **Entity Framework** as the ORM

## Running the Project

To run the project locally, follow these steps:

1. Open the solution in **Visual Studio**.
2. Set the **VolvoProject** project as the **Startup Project**.
   - Right-click on the `VolvoProject` project in the **Solution Explorer**.
   - Select **Set as Startup Project**.
3. Run the project by clicking the **Run** button or pressing `F5`.
4. Make sure to use **HTTPS** when prompted.

Once the project is running, the web application will be available at `https://localhost:5001` (or another port depending on your configuration).

## Database Setup

- The project uses **SQL Server** as the database.
- The **Entity Framework** is used for data access.
- You can create the database using the migration scripts, or the application may create it automatically when first run.

## Notes

- Ensure that your local SQL Server instance is running.
- Check the application settings in `appsettings.json` for the database connection string if you need to modify it.

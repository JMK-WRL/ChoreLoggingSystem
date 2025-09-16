# ChoreLoggingSystem
[![Ask DeepWiki](https://devin.ai/assets/askdeepwiki.png)](https://deepwiki.com/JMK-WRL/ChoreLoggingSystem)

The Chore Logging System is a .NET Windows Forms application designed for businesses to track and manage the completion of daily chores and tasks by staff members across different locations and shifts. It features distinct user roles for staff and managers, a comprehensive reporting dashboard, and data export capabilities.

## Key Features

- **Dual User Roles**:
    - **Staff**: A simple, focused interface for staff to log in, select their work shift and location, and check off completed tasks.
    - **Manager**: A powerful dashboard for overseeing operations, logging tasks on behalf of staff, viewing detailed analytics, and generating reports.

- **Task Logging**:
    - Staff members authenticate using a unique User ID and a 4-digit PIN.
    - The system dynamically loads a checklist of tasks based on the selected branch and shift.
    - Users can add optional notes to their submissions.

- **Manager Dashboard**:
    - **Analytics & Reporting**: View a comprehensive log of all completed tasks in a filterable data grid.
    - **Advanced Filtering**: Filter reports by date range, branch, shift, and specific staff members.
    - **Summary Statistics**: At-a-glance view of key metrics, including total tasks logged, number of active staff, and the most active branch.
    - **Log on Behalf of Staff**: Managers can submit task logs for any employee, which is recorded with an audit trail.

- **Data Export**:
    - Managers can export filtered report data into multiple formats:
        - **CSV**: A standard comma-separated values file for data analysis.
        - **Excel-compatible CSV**: A formatted CSV for easy viewing in Microsoft Excel.
        - **HTML Report**: A professionally styled report that can be opened in a web browser and printed to PDF.

## Technology Stack

- **Platform**: .NET 8
- **Language**: Visual Basic .NET (VB.NET)
- **User Interface**: Windows Forms
- **Database**: SQL Server / SQL Server LocalDB
- **Data Access**: ADO.NET using `Microsoft.Data.SqlClient`
- **Configuration**: `appsettings.json` for database connection strings

## Database Schema

The application relies on a SQL Server database with the following primary tables:

- `Staff`: Stores user credentials (UserID, PIN, FullName) for authentication and identification.
- `Branches`: Defines the different physical locations or branches of the business.
- `Shifts`: Contains information about different work shifts (e.g., Morning, Evening, Night).
- `Tasks`: Lists all possible tasks, each associated with a specific shift.
- `ChoreLog`: The main transactional table that records every completed task, linking the staff member, branch, shift, task, and a timestamp. It also tracks which manager authenticated the entry if logged by a manager.

## Setup and Installation

### Prerequisites

- Visual Studio 2022 or later
- .NET 8 SDK
- SQL Server or SQL Server LocalDB (typically installed with Visual Studio)

### Steps

1.  **Clone the Repository**:
    ```sh
    git clone https://github.com/jmk-wrl/choreloggingsystem.git
    cd choreloggingsystem
    ```

2.  **Database Setup**:
    - Open your SQL Server management tool (like SSMS or Azure Data Studio).
    - Create a new database named `ChoreLoggingDB`.
    - Open and execute the `dummy2.sql` script located in the root of the repository against the `ChoreLoggingDB` database. This will create the necessary tables and populate them with comprehensive sample data for both staff and managers.

3.  **Configure the Application**:
    - The project is pre-configured to connect to a SQL Server LocalDB instance. The connection string in `ChoreLoggingSystem/appsettings.json` is:
      ```json
      "Server=(localdb)\\MSSQLLocalDB;Database=ChoreLoggingDB;Integrated Security=true;"
      ```
    - If you are using a different SQL Server instance, update this connection string accordingly.

4.  **Run the Application**:
    - Open the `ChoreLoggingSystem.sln` solution file in Visual Studio.
    - Press `F5` or click the "Start" button to build and run the project.

## Usage

### Staff Login

- When the application starts, the staff login screen will appear.
- Use one of the sample staff credentials from the `dummy2.sql` script. For example:
    - **User ID**: `JS`
    - **PIN**: `1234`
- Select a location (Branch) and a work shift to load the corresponding tasks.
- Check the tasks you have completed, add any notes, and click `Submit Tasks`.

### Manager Login

- On the staff login screen, click the **Manager Login** button.
- A new login window will appear for authorized personnel.
- Use one of the sample manager credentials. For example:
    - **User ID**: `MGR1`
    - **PIN**: `1111`
- The Manager Dashboard will open, giving you access to the **Task Logging** and **Reports & Analytics** tabs.

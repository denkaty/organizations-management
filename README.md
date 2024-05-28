# organizations-management

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [organizations-1000000.csv](https://drive.usercontent.google.com/download?id=1uaUCN5vAMVz73RgfJykJzzlIq2yQTlYB&export=download&authuser=0)

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/denkaty/organizations-api.git
    ```

2. Navigate to the project directory:
    ```bash
    cd organizations-api
    ```

3. Restore the dependencies:
    ```bash
    dotnet restore
    ```

4. Set up the MSSQL database and update the connection strings in the `appsettings.json` file.

### Web API Structure

- `Organizations.Business.Abstraction`: Contains the business logic interfaces and abstractions.
- `Organizations.Business.Models`: Contains the business models used throughout the application.
- `Organizations.Business`: Implements the business logic and services.
- `Organizations.Data.Abstraction`: Contains the data access interfaces and abstractions.
- `Organizations.Data.Models`: Contains the data models representing the database schema.
- `Organizations.Data`: Implements data access logic and repository patterns using ADO.NET.
- `Organizations.Presentation.API`: Contains the Web API controllers and presentation logic.

### Data Importing Library Structure

- `DataImporting.Abstraction`: Contains the interfaces and abstractions for data importing logic.
- `DataImporting.Models`: Contains the models used during the data importing process.
- `DataImporting`: Implements the data importing logic to normalize and insert large datasets into the database.

## Features

- Layered architecture
- Dependency Injection for loose coupling and better testability
- ASP.NET Core for building robust Web APIs
- Efficient data importing from CSV to MSSQL using ADO.NET
- Normalization of data for optimal database insertion
- Authentication for secure access
- PDF generation for detailed organization reports
- File handling for importing and processing CSV files

## Data Importing Process

The `DataImporting` library handles the logic for importing and normalizing data from CSV files into an MSSQL database. It ensures that the data is formatted correctly and can be inserted into the database efficiently.

### CSV File Handling

1. Place your CSV files into the `organizations-management\Data\Input` directory with a `.csv` extension.
2. The application reads the CSV files, processes them, and moves the processed files to the `organizations-management\Data\MovedInputAfterProcessing` directory.
3. After processing, a JSON file is generated in the `organizations-management\Data\Output` directory containing information about how many rows were read from each CSV file.

### PDF Generation

The API can generate PDF files containing detailed information about specific organizations. This is handled using a PDF generator library integrated into the project.

-- Create Database
CREATE DATABASE ChoreLoggingDB;
GO

USE ChoreLoggingDB;
GO

-- Branches Table
CREATE TABLE Branches (
    BranchID INT IDENTITY(1,1) PRIMARY KEY,
    BranchName NVARCHAR(100) NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Shifts Table
CREATE TABLE Shifts (
    ShiftID INT IDENTITY(1,1) PRIMARY KEY,
    ShiftName NVARCHAR(50) NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Tasks Table
CREATE TABLE Tasks (
    TaskID INT IDENTITY(1,1) PRIMARY KEY,
    TaskName NVARCHAR(200) NOT NULL,
    ShiftID INT NOT NULL,
    EstimatedMinutes INT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ShiftID) REFERENCES Shifts(ShiftID)
);

-- ChoreLog Table
CREATE TABLE ChoreLog (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    StaffInitials NVARCHAR(10) NOT NULL,
    BranchID INT NOT NULL,
    ShiftID INT NOT NULL,
    TaskID INT NOT NULL,
    CompletedDateTime DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(20) DEFAULT 'Completed',
    Notes NVARCHAR(500),
    FOREIGN KEY (BranchID) REFERENCES Branches(BranchID),
    FOREIGN KEY (ShiftID) REFERENCES Shifts(ShiftID),
    FOREIGN KEY (TaskID) REFERENCES Tasks(TaskID)
);

-- Insert Sample Data
INSERT INTO Branches (BranchName) VALUES 
('Downtown'), ('Uptown'), ('Airport'), ('Mall Location');

INSERT INTO Shifts (ShiftName, StartTime, EndTime) VALUES 
('Morning', '06:00:00', '14:00:00'),
('Afternoon', '14:00:00', '22:00:00'),
('Night', '22:00:00', '06:00:00');

INSERT INTO Tasks (TaskName, ShiftID, EstimatedMinutes) VALUES 
('Clean reception area', 1, 15),
('Stock supplies', 1, 20),
('Vacuum common areas', 1, 30),
('Empty trash bins', 1, 10),
('Clean break room', 2, 25),
('Sanitize workstations', 2, 35),
('Lock up facility', 3, 10),
('Security check', 3, 20);
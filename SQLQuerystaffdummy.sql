USE ChoreLoggingDB;
GO

-- Create Staff table
CREATE TABLE Staff (
    StaffID INT IDENTITY(1,1) PRIMARY KEY,
    Initials NVARCHAR(10) NOT NULL UNIQUE,
    FullName NVARCHAR(100) NOT NULL,
    EmployeeID NVARCHAR(50) UNIQUE,
    PIN NVARCHAR(4) NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    LastLoginDate DATETIME,
    BranchID INT,
    FOREIGN KEY (BranchID) REFERENCES Branches(BranchID)
);

-- Insert sample staff for demo
INSERT INTO Staff (Initials, FullName, EmployeeID, PIN, BranchID) VALUES
('JS', 'John Smith', 'EMP001', '1234', 1),
('MJ', 'Mary Johnson', 'EMP002', '5678', 1),
('RB', 'Robert Brown', 'EMP003', '9012', 2),
('SK', 'Sarah Kim', 'EMP004', '3456', 2),
('AM', 'Admin Manager', 'ADM001', '0000', 1);

-- Enhance ChoreLog for better audit trail
ALTER TABLE ChoreLog ADD AuthenticatedStaffID INT;
ALTER TABLE ChoreLog ADD CONSTRAINT FK_ChoreLog_Staff 
    FOREIGN KEY (AuthenticatedStaffID) REFERENCES Staff(StaffID);

-- Verify data
SELECT s.Initials, s.FullName, s.PIN, b.BranchName 
FROM Staff s 
LEFT JOIN Branches b ON s.BranchID = b.BranchID
WHERE s.IsActive = 1;
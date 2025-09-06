-- Complete Test Data Setup for ChoreLoggingSystem
USE ChoreLoggingDB;
GO

-- Clear existing data (optional - for clean testing)
DELETE FROM ChoreLog;
DELETE FROM Tasks;
DELETE FROM Staff;
DELETE FROM Shifts;
DELETE FROM Branches;
GO

-- Reset identity seeds
DBCC CHECKIDENT ('ChoreLog', RESEED, 0);
DBCC CHECKIDENT ('Tasks', RESEED, 0);
DBCC CHECKIDENT ('Staff', RESEED, 0);
DBCC CHECKIDENT ('Shifts', RESEED, 0);
DBCC CHECKIDENT ('Branches', RESEED, 0);
GO

-- Insert Branches (Locations)
INSERT INTO Branches (BranchName) VALUES 
('Downtown Office'),
('Uptown Medical Center'),
('Airport Terminal'),
('Shopping Mall Location'),
('Riverside Clinic'),
('University Campus');
GO

-- Insert Shifts
INSERT INTO Shifts (ShiftName, StartTime, EndTime) VALUES 
('Early Morning', '05:00:00', '13:00:00'),
('Day Shift', '08:00:00', '16:00:00'),
('Evening Shift', '16:00:00', '00:00:00'),
('Night Shift', '00:00:00', '08:00:00'),
('Weekend Morning', '06:00:00', '14:00:00'),
('Weekend Evening', '14:00:00', '22:00:00');
GO

-- Insert Comprehensive Tasks for Each Shift
INSERT INTO Tasks (TaskName, ShiftID, EstimatedMinutes) VALUES 
-- Early Morning Shift (1)
('Unlock facility and disarm security', 1, 5),
('Turn on all lights and equipment', 1, 10),
('Check temperature and ventilation', 1, 15),
('Clean reception and waiting areas', 1, 30),
('Stock supplies in all departments', 1, 45),
('Sanitize high-touch surfaces', 1, 25),
('Prepare coffee station and break room', 1, 15),
('Review overnight reports and logs', 1, 20),

-- Day Shift (2)
('Customer service desk maintenance', 2, 20),
('Restock restrooms and supplies', 2, 30),
('Vacuum carpeted areas', 2, 40),
('Clean and organize storage rooms', 2, 35),
('Update information displays', 2, 15),
('Maintenance rounds and checks', 2, 60),
('Inventory count - medical supplies', 2, 45),
('Staff break room deep clean', 2, 30),

-- Evening Shift (3)
('Final customer area cleaning', 3, 25),
('Secure confidential documents', 3, 15),
('Equipment shutdown procedures', 3, 20),
('Empty all trash and recycling', 3, 35),
('Final restroom cleaning and restock', 3, 25),
('Security walkthrough', 3, 30),
('Prepare next day setup', 3, 20),
('Lock facility and set alarms', 3, 10),

-- Night Shift (4)
('Security perimeter check', 4, 45),
('Deep cleaning - floors and surfaces', 4, 90),
('Maintenance and repair tasks', 4, 60),
('Inventory restocking', 4, 40),
('Emergency equipment checks', 4, 30),
('Documentation and reporting', 4, 25),
('Facility safety inspection', 4, 35),

-- Weekend Morning (5)
('Weekend facility opening', 5, 10),
('Basic area maintenance', 5, 30),
('Light cleaning duties', 5, 45),
('Supply checks and restocking', 5, 25),
('Customer area preparation', 5, 20),

-- Weekend Evening (6)
('Weekend closing procedures', 6, 15),
('Basic security checks', 6, 20),
('Light cleaning and tidying', 6, 30),
('Facility lockdown', 6, 10);
GO

-- Insert Staff Members with Various Roles
INSERT INTO Staff (Initials, FullName, EmployeeID, PIN, BranchID, IsActive) VALUES
-- Managers (for login testing)
('ADM', 'System Administrator', 'EMP001', '0000', 1, 1),
('MGR1', 'John Manager', 'EMP002', '1111', 1, 1),
('MGR2', 'Sarah Manager', 'EMP003', '2222', 2, 1),

-- Regular Staff (for task logging testing)
('JS', 'John Smith', 'EMP101', '1234', 1, 1),
('MJ', 'Mary Johnson', 'EMP102', '2345', 1, 1),
('RB', 'Robert Brown', 'EMP103', '3456', 2, 1),
('SK', 'Sarah Kim', 'EMP104', '4567', 2, 1),
('DW', 'David Wilson', 'EMP105', '5678', 3, 1),
('LG', 'Lisa Garcia', 'EMP106', '6789', 3, 1),
('MT', 'Michael Taylor', 'EMP107', '7890', 4, 1),
('JD', 'Jennifer Davis', 'EMP108', '8901', 4, 1),
('CL', 'Chris Lee', 'EMP109', '9012', 5, 1),
('AM', 'Anna Martinez', 'EMP110', '0123', 5, 1),
('TR', 'Thomas Rodriguez', 'EMP111', '1357', 6, 1),
('KW', 'Karen White', 'EMP112', '2468', 6, 1),

-- Part-time Staff
('PT1', 'Alex Chen', 'EMP201', '1111', 1, 1),
('PT2', 'Jordan Foster', 'EMP202', '2222', 2, 1),
('PT3', 'Morgan Riley', 'EMP203', '3333', 3, 1);
GO

-- Insert Sample Task Log Entries (Historical Data for Reporting Tests)
DECLARE @StartDate DATE = DATEADD(DAY, -30, GETDATE());
DECLARE @CurrentDate DATE = @StartDate;

WHILE @CurrentDate <= GETDATE()
BEGIN
    -- Morning shift completions
    INSERT INTO ChoreLog (StaffInitials, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes, AuthenticatedStaffID)
    SELECT 
        'JS', 1, 1, TaskID, 
        DATEADD(HOUR, 6 + (TaskID % 3), @CurrentDate),
        'Completed',
        CASE WHEN TaskID % 5 = 0 THEN 'Extra attention needed on this task' ELSE NULL END,
        1
    FROM Tasks WHERE ShiftID = 1 AND TaskID % 2 = 1;

    INSERT INTO ChoreLog (StaffInitials, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes, AuthenticatedStaffID)
    SELECT 
        'MJ', 1, 1, TaskID, 
        DATEADD(HOUR, 7 + (TaskID % 2), @CurrentDate),
        'Completed',
        NULL,
        2
    FROM Tasks WHERE ShiftID = 1 AND TaskID % 3 = 0;

    -- Day shift completions
    INSERT INTO ChoreLog (StaffInitials, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes, AuthenticatedStaffID)
    SELECT 
        'RB', 2, 2, TaskID, 
        DATEADD(HOUR, 10 + (TaskID % 4), @CurrentDate),
        'Completed',
        CASE WHEN TaskID % 7 = 0 THEN 'Task took longer than expected' ELSE NULL END,
        2
    FROM Tasks WHERE ShiftID = 2 AND TaskID % 2 = 0;

    INSERT INTO ChoreLog (StaffInitials, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes, AuthenticatedStaffID)
    SELECT 
        'SK', 2, 2, TaskID, 
        DATEADD(HOUR, 12 + (TaskID % 3), @CurrentDate),
        'Completed',
        NULL,
        3
    FROM Tasks WHERE ShiftID = 2 AND TaskID % 3 = 1;

    -- Evening shift completions
    INSERT INTO ChoreLog (StaffInitials, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes, AuthenticatedStaffID)
    SELECT 
        'DW', 3, 3, TaskID, 
        DATEADD(HOUR, 18 + (TaskID % 2), @CurrentDate),
        'Completed',
        CASE WHEN TaskID % 6 = 0 THEN 'Completed ahead of schedule' ELSE NULL END,
        1
    FROM Tasks WHERE ShiftID = 3 AND TaskID % 2 = 1;

    -- Add some weekend data
    IF DATEPART(WEEKDAY, @CurrentDate) IN (1, 7) -- Sunday or Saturday
    BEGIN
        INSERT INTO ChoreLog (StaffInitials, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes, AuthenticatedStaffID)
        SELECT 
            'PT1', 1, 5, TaskID, 
            DATEADD(HOUR, 8 + (TaskID % 2), @CurrentDate),
            'Completed',
            'Weekend shift completion',
            2
        FROM Tasks WHERE ShiftID = 5 AND TaskID % 2 = 0;
    END

    SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
END;
GO

-- Add some recent data for today's testing
INSERT INTO ChoreLog (StaffInitials, BranchID, ShiftID, TaskID, CompletedDateTime, Status, Notes, AuthenticatedStaffID)
VALUES 
('JS', 1, 1, 1, GETDATE(), 'Completed', 'Morning opening went smoothly', 1),
('JS', 1, 1, 2, DATEADD(MINUTE, 15, GETDATE()), 'Completed', NULL, 1),
('MJ', 1, 2, 9, DATEADD(HOUR, 2, GETDATE()), 'Completed', 'Customer service area needed extra cleaning', 2),
('RB', 2, 2, 10, DATEADD(HOUR, 1, GETDATE()), 'Completed', NULL, 2),
('SK', 2, 2, 11, DATEADD(HOUR, 3, GETDATE()), 'Completed', 'Vacuum cleaner needed repair', 3);
GO

-- Verification queries
PRINT 'Data Setup Complete!';
PRINT '';
PRINT 'Summary:';
SELECT 'Branches' AS TableName, COUNT(*) AS RecordCount FROM Branches
UNION ALL
SELECT 'Shifts', COUNT(*) FROM Shifts  
UNION ALL
SELECT 'Tasks', COUNT(*) FROM Tasks
UNION ALL
SELECT 'Staff', COUNT(*) FROM Staff
UNION ALL
SELECT 'ChoreLog Entries', COUNT(*) FROM ChoreLog;

PRINT '';
PRINT 'Recent Task Completions (Last 24 hours):';
SELECT 
    cl.StaffInitials,
    b.BranchName,
    s.ShiftName,
    t.TaskName,
    cl.CompletedDateTime,
    cl.Notes
FROM ChoreLog cl
INNER JOIN Branches b ON cl.BranchID = b.BranchID
INNER JOIN Shifts s ON cl.ShiftID = s.ShiftID  
INNER JOIN Tasks t ON cl.TaskID = t.TaskID
WHERE cl.CompletedDateTime >= DATEADD(DAY, -1, GETDATE())
ORDER BY cl.CompletedDateTime DESC;
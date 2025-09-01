USE ChoreLoggingDB;
GO

-- Insert sample branches (if not already there)
IF NOT EXISTS (SELECT 1 FROM Branches)
BEGIN
    INSERT INTO Branches (BranchName) VALUES 
    ('Downtown'),
    ('Uptown'), 
    ('Airport'), 
    ('Mall Location');
    PRINT 'Branches inserted: 4 records';
END

-- Insert sample shifts (if not already there)
IF NOT EXISTS (SELECT 1 FROM Shifts)
BEGIN
    INSERT INTO Shifts (ShiftName, StartTime, EndTime) VALUES 
    ('Morning', '06:00:00', '14:00:00'),
    ('Afternoon', '14:00:00', '22:00:00'),
    ('Night', '22:00:00', '06:00:00');
    PRINT 'Shifts inserted: 3 records';
END

-- Insert sample tasks (if not already there)
IF NOT EXISTS (SELECT 1 FROM Tasks)
BEGIN
    INSERT INTO Tasks (TaskName, ShiftID, EstimatedMinutes) VALUES 
    -- Morning shift tasks (ShiftID = 1)
    ('Clean reception area', 1, 15),
    ('Stock supplies', 1, 20),
    ('Vacuum common areas', 1, 30),
    ('Empty trash bins', 1, 10),
    ('Prepare opening checklist', 1, 5),
    
    -- Afternoon shift tasks (ShiftID = 2)
    ('Clean break room', 2, 25),
    ('Sanitize workstations', 2, 35),
    ('Restock restrooms', 2, 15),
    ('Customer area maintenance', 2, 20),
    ('Inventory check', 2, 30),
    
    -- Night shift tasks (ShiftID = 3)
    ('Lock up facility', 3, 10),
    ('Security check', 3, 20),
    ('Turn off equipment', 3, 5),
    ('Final cleaning sweep', 3, 25),
    ('Prepare closing report', 3, 15);
    
    PRINT 'Tasks inserted: 15 records';
END

-- Verify data
SELECT 'Branches' AS TableName, COUNT(*) AS RecordCount FROM Branches
UNION ALL
SELECT 'Shifts', COUNT(*) FROM Shifts  
UNION ALL
SELECT 'Tasks', COUNT(*) FROM Tasks
UNION ALL
SELECT 'ChoreLog', COUNT(*) FROM ChoreLog;
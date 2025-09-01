-- Verify all tables have data
SELECT 'Branches' AS TableName, COUNT(*) AS RecordCount FROM Branches
UNION ALL
SELECT 'Shifts', COUNT(*) FROM Shifts  
UNION ALL
SELECT 'Tasks', COUNT(*) FROM Tasks
UNION ALL
SELECT 'ChoreLog', COUNT(*) FROM ChoreLog
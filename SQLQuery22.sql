USE ChoreLoggingDB;

-- Update all ChoreLog entries to today's date
UPDATE ChoreLog 
SET CompletedDateTime = DATEADD(DAY, DATEDIFF(DAY, '2025-09-08', GETDATE()), CompletedDateTime);

-- Verify the update
SELECT TOP 5 UserID, CompletedDateTime, Status 
FROM ChoreLog 
ORDER BY CompletedDateTime DESC;
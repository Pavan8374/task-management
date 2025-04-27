

-- Get all tasks assigned to a user
-- The below sql query can be run in MS SQL Server

DECLARE @UserId int;

SELECT 
    T.Id AS TaskId,
    T.Title AS TaskTitle,
    T.Description AS TaskDescription,
    T.TaskStatus AS TaskStatus,
    U.Id AS AssignedToUserId,
    U.FullName AS AssignedToUserFullName,
    U.Email AS AssignedToUserEmail
FROM 
    TASKS T
LEFT JOIN 
    USERS U ON T.AssignedToUserId = U.Id
WHERE 
    T.AssignedToUserId = @UserId



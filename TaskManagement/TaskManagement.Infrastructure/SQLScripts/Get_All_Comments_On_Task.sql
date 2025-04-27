

-- Get all comments on a task
-- This query can be run in MS Sql server

DEClaRE @TaskId int;

SELECT 
    TC.Id AS CommentId,
    TC.Description AS CommentDescription,
    TC.CreatedAt AS CommentCreatedAt,
    U.Id AS UserId,
    U.FullName AS UserFullName,
    U.Email AS UserEmail
FROM 
    TaskComments TC
JOIN 
    USERS U ON TC.UserId = U.Id
JOIN 
    TASKS T ON TC.TaskId = T.Id
WHERE 
    TC.TaskId = @TaskId
ORDER BY 
    TC.Id DESC

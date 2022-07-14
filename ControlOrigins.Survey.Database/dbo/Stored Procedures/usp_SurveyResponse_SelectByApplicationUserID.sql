

CREATE PROC [dbo].[usp_SurveyResponse_SelectByApplicationUserID] 
    @ApplicationUserID INT,
    @ApplicationID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT SurveyResponse.SurveyResponseID, 
       SurveyResponse.SurveyResponseNM, 
       ApplicationUser_Employee.AccountNM, 
       SurveyResponse.ModifiedID, 
       SurveyResponse.AssignedUserID, 
       SurveyResponse.StatusID, 
       SurveyStatus.StatusNM, 
       SurveyStatus.PreviousStatusID, 
       SurveyStatus.NextStatusID, 
       SurveyResponse.DataSource, 
       Survey.SurveyShortNM, 
       null as MANAGER_ID, 
       0 AS VariantCount, 
       0 AS ComplianceReview, 
       null as PERNR, 
       null as APC_USERID, 
       null as MANAGER_NAME, 
       null as MANAGER_EMAIL, 
       null as ORG_UNIT, 
       null as POSITION_TITLE, 
       null as HOME_HOST_LOCATION, 
       null as COMPANY_NAME, 
       null as BUILDING_DESCRIPTION, 
       null as LOCATION_CITY, 
       null as LOCATION_STATE, 
       null as LOCATION_COUNTRY, 
       null as EXEMPT_NONEXEMPT, 
       null as EMPLOYEE_GROUP, 
       null as EMPL_STATUS, 
       null as MANAGER_USERID, 
       null as JOB_FAMILY, 
       null as JOB_FUNCTION, 
       null as COST_CENTER, 
       null as AGENCY, 
       null as LEVEL1_MANAGER_NAME, 
       null as LEVEL2_MANAGER_NAME, 
       null as LEVEL3_MANAGER_NAME, 
       null as LEVEL4_MANAGER_NAME, 
       null as LEVEL2_MANAGER_USERID, 
       null as LEVEL3_MANAGER_USERID, 
       ApplicationUser_Employee.FirstNM, 
       ApplicationUser_Employee.LastNM, 
       ApplicationUser_Employee.eMailAddress, 
       Survey.SurveyNM, 
       SurveyResponse.SurveyID, 
       SurveyResponse.ApplicationID, 
       SurveyResponse.ModifiedDT, 
       null as LEAVE_DATE, 
       DATEDIFF(DAY, SurveyResponse.ModifiedDT, GETDATE()) AS DaySinceModified, 
       ApplicationUser_Employee.FirstNM as FIRST_NAME, 
       ApplicationUser_Employee.LastNM as LAST_NAME, 
       ApplicationUser_Supervisor.ApplicationUserID as SupervisorApplicationUserID
FROM   ApplicationUser AS ApplicationUser_Employee
INNER JOIN ApplicationUser AS ApplicationUser_Supervisor ON ApplicationUser_Employee.SupervisorAccountNM = ApplicationUser_Supervisor.AccountNM 
RIGHT OUTER JOIN
                      SurveyStatus INNER JOIN
                      SurveyResponse INNER JOIN
                      Survey ON SurveyResponse.SurveyID = Survey.SurveyID ON SurveyStatus.StatusID = SurveyResponse.StatusID AND 
                      SurveyStatus.SurveyID = SurveyResponse.SurveyID ON ApplicationUser_Employee.ApplicationUserID = SurveyResponse.AssignedUserID
WHERE SurveyResponse.ApplicationID =  @ApplicationID
AND  ( (SurveyResponse.StatusID IN (1, 2, 5)) AND (SurveyResponse.AssignedUserID = @ApplicationUserID ) )
OR   ( SurveyResponse.StatusID = 3 
       AND SurveyResponse.ApplicationID = @ApplicationID
       AND ApplicationUser_Employee.SupervisorAccountNM =
                          (SELECT  AccountNM 
                            FROM   ApplicationUser 
                            WHERE  ApplicationUserID = @ApplicationUserID   ) )
OR  ( 1 <  (SELECT Role.ReviewLevel
	            FROM    Role 
				INNER JOIN ApplicationUserRole ON Role.RoleID = ApplicationUserRole.RoleID
				WHERE  ApplicationUserRole.ApplicationUserID = @ApplicationUserID
				and SurveyResponse.ApplicationID = ApplicationUserRole.ApplicationID 
				and ApplicationUserRole.ApplicationID = @ApplicationID ) )
OR ( SurveyResponse.AssignedUserID is null and SurveyResponse.ApplicationID = @ApplicationID )

COMMIT






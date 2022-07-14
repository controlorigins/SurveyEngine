CREATE PROC [dbo].[usp_Survey_SelectByApplicationID] 
    @ApplicationID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     Survey.SurveyID, Survey.SurveyTypeID, Survey.UseQuestionGroupsFL, Survey.SurveyNM, Survey.SurveyShortNM, Survey.SurveyDS, Survey.CompletionMessage, 
                      Survey.ResponseNMTemplate, Survey.ReviewerAccountNM, Survey.AutoAssignFilter, Survey.StartDT, Survey.EndDT, Survey.ParentSurveyID, Survey.ModifiedID, 
                      Survey.ModifiedDT, ApplicationSurvey.DefaultRoleID, Role.RoleCD, Role.RoleNM, Role.RoleDS, Role.ReviewLevel, Role.ReadFL, Role.UpdateFL, 
                      ApplicationSurvey.ApplicationSurveyID, ApplicationSurvey.ApplicationID
FROM         Survey INNER JOIN
                      ApplicationSurvey ON Survey.SurveyID = ApplicationSurvey.SurveyID INNER JOIN
                      Role ON ApplicationSurvey.DefaultRoleID = Role.RoleID
	WHERE  [ApplicationSurvey].[ApplicationID] = @ApplicationID 

	COMMIT




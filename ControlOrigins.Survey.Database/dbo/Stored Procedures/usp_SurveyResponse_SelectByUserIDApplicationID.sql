
CREATE PROC [dbo].[usp_SurveyResponse_SelectByUserIDApplicationID] 
    @ApplicationUserID INT,
    @ApplicationID INT,
    @ForInput BIT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     Application.ApplicationID, Application.ApplicationNM, Survey.SurveyID, Survey.SurveyNM, SurveyResponse.SurveyResponseID, 
                      SurveyResponse.SurveyResponseNM, ApplicationUser.ApplicationUserID, ApplicationUser.AccountNM, SurveyResponse.StatusID, SurveyResponse.DataSource, 
                      Survey.UseQuestionGroupsFL, Survey.SurveyShortNM, Survey.SurveyDS, Survey.CompletionMessage, Survey.ResponseNMTemplate, Survey.ReviewerAccountNM, 
                      Survey.AutoAssignFilter, Survey.StartDT, Survey.EndDT
FROM         Survey INNER JOIN
                      Application INNER JOIN
                      ApplicationSurvey ON Application.ApplicationID = ApplicationSurvey.ApplicationID ON Survey.SurveyID = ApplicationSurvey.SurveyID INNER JOIN
                      ApplicationUser INNER JOIN
                      ApplicationUserRole ON ApplicationUser.ApplicationUserID = ApplicationUserRole.ApplicationUserID ON 
                      Application.ApplicationID = ApplicationUserRole.ApplicationID RIGHT OUTER JOIN
                      SurveyResponse ON Survey.SurveyID = SurveyResponse.SurveyID AND ApplicationUser.ApplicationUserID = SurveyResponse.AssignedUserID
                      WHERE  [ApplicationUser].ApplicationUserID = @ApplicationUserID  and [Application].ApplicationID = @ApplicationID

	COMMIT





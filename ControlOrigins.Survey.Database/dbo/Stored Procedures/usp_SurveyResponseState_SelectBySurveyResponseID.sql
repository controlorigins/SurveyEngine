
CREATE PROC [dbo].[usp_SurveyResponseState_SelectBySurveyResponseID] 
    @SurveyResponseID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     SurveyResponseState.SurveyResponseStateID, 
           SurveyResponseState.SurveyResponseID, 
           SurveyResponseState.StatusID, 
           SurveyResponseState.AssignedUserID, 
           SurveyResponseState.EmailSent, 
           SurveyResponseState.EmailBody, 
           SurveyResponseState.ModifiedID, 
           SurveyResponseState.ModifiedDT StateModifiedDT, 
           ApplicationUser.FirstNM, 
           ApplicationUser.LastNM, 
           SurveyResponse.SurveyResponseNM, 
           SurveyResponse.ModifiedDT AS SurveyResponseModifiedDT, 
           SurveyResponse.DataSource, Survey.SurveyNM, 
           SurveyStatus.StatusNM, 
           SurveyStatus.StatusDS, 
           SurveyResponse.AssignedUserID AS SurveyResponseAssignedUserID
FROM         SurveyResponseState INNER JOIN
                      SurveyResponse ON SurveyResponseState.SurveyResponseID = SurveyResponse.SurveyResponseID INNER JOIN
                      Survey ON SurveyResponse.SurveyID = Survey.SurveyID INNER JOIN
                      SurveyStatus ON Survey.SurveyID = SurveyStatus.SurveyID AND SurveyResponse.StatusID = SurveyStatus.StatusID LEFT OUTER JOIN
                      ApplicationUser ON SurveyResponseState.AssignedUserID = ApplicationUser.ApplicationUserID
	WHERE  (SurveyResponseState.SurveyResponseID = @SurveyResponseID ) 

	COMMIT





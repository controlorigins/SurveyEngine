CREATE PROC [dbo].[usp_SurveyResponseHistory_SelectBySurveyResponseID] 
    @SurveyResponseID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     SurveyResponseHistory.SurveyResponseHistoryID, SurveyResponseHistory.ApplicationUserID, SurveyResponseHistory.SurveyResponseID, 
                      SurveyResponseHistory.SurveyResponseNM, SurveyResponseHistory.StatusID, SurveyResponseHistory.QuestionGroupID, SurveyResponseHistory.UserNM, 
                      SurveyResponseHistory.Answers, SurveyResponseHistory.ModifiedID, SurveyResponseHistory.ModifiedDT, SurveyStatus.StatusNM, 
                      ApplicationUser.FirstNM + ' ' + ApplicationUser.LastNM AS ModifiedNM
FROM         SurveyResponseHistory INNER JOIN
                      SurveyStatus ON SurveyResponseHistory.StatusID = SurveyStatus.StatusID INNER JOIN
                      ApplicationUser ON SurveyResponseHistory.ApplicationUserID = ApplicationUser.ApplicationUserID INNER JOIN
                      SurveyResponse ON SurveyResponseHistory.SurveyResponseID = SurveyResponse.SurveyResponseID AND SurveyStatus.SurveyID = SurveyResponse.SurveyID	
WHERE  (SurveyResponseHistory.SurveyResponseID = @SurveyResponseID ) 
 order by SurveyResponseHistory.ModifiedDT desc 
 
	COMMIT





CREATE PROC [dbo].[usp_SurveyResponse_SelectBySurveyResponseID] 
    @SurveyResponseID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT        SurveyResponseNM, 
              StatusNM, 
			  StatusID, 
			  DataSource, 
			  SurveyShortNM, 
			  AnswerCount, 
			  QuestionCount, 
			  CommentCount, 
			  PendingReviewCount, 
			  PercentComplete, 
			  SurveyNM, 
			  ModifiedDT, 
			  DaySinceModified, 
			  AssignedUserID, 
			  SurveyResponseID, 
			  SurveyID, 
			  ApplicationID, 
			  ModifiedID, 
			  FirstNM, 
			  LastNM, 
			  eMailAddress, 
			  ApplicationUserID, 
			  SurveyResponseScore, 
			  ApplicationNM, 
			  ApplicationCD, 
			  ApplicationShortNM, 
			  AccountNM, 
			  SupervisorAccountNM
FROM            vwApplicationSurveyResponseSummary

WHERE     (SurveyResponseID = @SurveyResponseID)

COMMIT





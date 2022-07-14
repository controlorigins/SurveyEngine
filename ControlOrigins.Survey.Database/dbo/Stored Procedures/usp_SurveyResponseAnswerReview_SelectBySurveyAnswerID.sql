CREATE PROC [dbo].[usp_SurveyResponseAnswerReview_SelectBySurveyAnswerID] 
    @SurveyAnswerID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     SurveyResponseAnswerReview.SurveyResponseAnswerReviewID, SurveyResponseAnswerReview.SurveyAnswerID, 
                      SurveyResponseAnswerReview.ApplicationUserRoleID, SurveyResponseAnswerReview.ReviewStatusID, SurveyResponseAnswerReview.ReviewLevel, 
                      SurveyResponseAnswerReview.ModifiedID, SurveyResponseAnswerReview.ModifiedDT, SurveyResponseAnswerReview.ModifiedComment, 
                      ApplicationUser.AccountNM AS ReviewerAccountNM, ApplicationUser.FirstNM + ' ' + ApplicationUser.LastNM AS ReviewerNM, SurveyReviewStatus.ReviewStatusNM, 
                      SurveyReviewStatus.ApprovedFL
FROM         SurveyResponse INNER JOIN
                      SurveyResponseAnswerReview INNER JOIN
                      ApplicationUserRole ON SurveyResponseAnswerReview.ApplicationUserRoleID = ApplicationUserRole.ApplicationUserRoleID INNER JOIN
                      ApplicationUser ON ApplicationUserRole.ApplicationUserID = ApplicationUser.ApplicationUserID INNER JOIN
                      SurveyResponseAnswer ON SurveyResponseAnswerReview.SurveyAnswerID = SurveyResponseAnswer.SurveyAnswerID ON 
                      SurveyResponse.SurveyResponseID = SurveyResponseAnswer.SurveyResponseID INNER JOIN
                      SurveyReviewStatus ON SurveyResponseAnswerReview.ReviewStatusID = SurveyReviewStatus.ReviewStatusID AND 
                      SurveyResponse.SurveyID = SurveyReviewStatus.SurveyID
              WHERE  (SurveyResponseAnswerReview.SurveyAnswerID = @SurveyAnswerID ) 


	COMMIT




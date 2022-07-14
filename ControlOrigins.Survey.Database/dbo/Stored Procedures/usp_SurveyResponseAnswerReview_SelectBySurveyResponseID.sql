CREATE PROC [dbo].[usp_SurveyResponseAnswerReview_SelectBySurveyResponseID] 
    @SurveyResponseID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     SurveyResponseAnswerReview.SurveyResponseAnswerReviewID, 
           SurveyResponseAnswerReview.SurveyAnswerID, 
           SurveyResponseAnswerReview.ApplicationUserRoleID, 
           SurveyResponseAnswerReview.ReviewStatusID, 
           SurveyResponseAnswerReview.ReviewLevel,
           SurveyResponseAnswerReview.ModifiedID, 
           SurveyResponseAnswerReview.ModifiedDT, 
           SurveyResponseAnswerReview.ModifiedComment, 
           SurveyResponseAnswer.QuestionID, 
           SurveyResponseAnswer.QuestionAnswerID, 
           SurveyResponseAnswer.SequenceNumber, 
           SurveyResponseAnswer.AnswerComment
FROM       SurveyResponseAnswerReview INNER JOIN
           SurveyResponseAnswer ON SurveyResponseAnswerReview.SurveyAnswerID = SurveyResponseAnswer.SurveyAnswerID INNER JOIN
           SurveyResponseSequence ON SurveyResponseAnswer.SurveyResponseID = SurveyResponseSequence.SurveyResponseID AND 
           SurveyResponseAnswer.SequenceNumber = SurveyResponseSequence.SequenceNumber INNER JOIN
           SurveyResponse ON SurveyResponseSequence.SurveyResponseID = SurveyResponse.SurveyResponseID
WHERE  ([SurveyResponseAnswerReview].[SurveyAnswerID] in ( select SurveyAnswerID 
	                                from SurveyResponseAnswer 
	                               where SurveyResponseAnswer.SurveyResponseID = @SurveyResponseID) ) 

	COMMIT





CREATE PROC [dbo].[Question_SelectBySurveyID] 
    @SurveyID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     QuestionGroupMember.QuestionGroupMemberID,
           QuestionGroupMember.QuestionWeight, 
           QuestionGroupMember.DisplayOrder, 
           QuestionGroupMember.QuestionGroupID, 
           Question.QuestionID, Question.QuestionNM, 
           Question.QuestionShortNM, 
           Question.QuestionDS, 
           Question.QuestionSort, 
           Question.UnitOfMeasureID, 
           Question.ReviewRoleLevel, 
           Question.QuestionTypeID, 
           Question.QuestionValue, 
           Question.CommentFL, 
		   Question.FileData,
		   Question.Keywords,
           Question.SurveyTypeID, 
           lu_QuestionType.QuestionTypeCD, 
           lu_QuestionType.QuestionTypeDS, 
           lu_QuestionType.ControlName, 
           lu_QuestionType.AnswerDataType, 
           QuestionGroup.SurveyID, 
		   QuestionGroupMember.ModifiedID,
		   QuestionGroupMember.ModifiedDT,
           QuestionGroupMember.QuestionWeight * Question.QuestionValue AS MaxQuestionValue, 
           QuestionGroup.GroupOrder,
           QuestionGroup.QuestionGroupShortNM, 
                      QuestionGroup.QuestionGroupNM,                      
       ROW_NUMBER() over (ORDER BY dbo.QuestionGroup.GroupOrder,dbo.QuestionGroup.QuestionGroupID,  dbo.QuestionGroupMember.DisplayOrder, dbo.Question.QuestionSort, dbo.Question.QuestionShortNM) as SurveyQuestionOrder
FROM         Question INNER JOIN
                      QuestionGroupMember ON Question.QuestionID = QuestionGroupMember.QuestionID INNER JOIN
                      QuestionGroup ON QuestionGroupMember.QuestionGroupID = QuestionGroup.QuestionGroupID INNER JOIN
                      lu_QuestionType ON Question.QuestionTypeID = lu_QuestionType.QuestionTypeID
WHERE     (QuestionGroup.SurveyID =  @SurveyID )
ORDER BY QuestionGroup.GroupOrder,QuestionGroup.QuestionGroupID, QuestionGroupMember.DisplayOrder, Question.QuestionSort, Question.QuestionShortNM




	COMMIT





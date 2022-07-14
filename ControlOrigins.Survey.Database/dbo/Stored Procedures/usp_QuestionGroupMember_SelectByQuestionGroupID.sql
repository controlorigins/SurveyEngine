
CREATE PROC [dbo].[usp_QuestionGroupMember_SelectByQuestionGroupID] 
    @QuestionGroupID INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

SELECT     QuestionGroupMember.QuestionGroupMemberID, 
           QuestionGroupMember.QuestionWeight, 
           QuestionGroupMember.DisplayOrder, 
           QuestionGroupMember.ModifiedID, 
           QuestionGroupMember.ModifiedDT, 
           Question.QuestionID, 
           Question.QuestionShortNM, 
           Question.QuestionNM, 
           QuestionGroup.QuestionGroupID, 
           QuestionGroup.GroupOrder, 
           QuestionGroup.QuestionGroupShortNM, 
           QuestionGroup.QuestionGroupNM
FROM       QuestionGroupMember INNER JOIN
                      Question ON QuestionGroupMember.QuestionID = Question.QuestionID INNER JOIN
                      QuestionGroup ON QuestionGroupMember.QuestionGroupID = QuestionGroup.QuestionGroupID
where  (QuestionGroupMember.QuestionGroupID = @QuestionGroupID) 

COMMIT





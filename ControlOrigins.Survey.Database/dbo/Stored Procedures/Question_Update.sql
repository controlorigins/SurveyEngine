

-- ==========================================================================================
-- Entity Name:	Question_Update
-- Create date:	10/16/2015 12:12:44 PM
-- Description:	This stored procedure is intended for updating Question table
-- ==========================================================================================
CREATE Procedure [dbo].[Question_Update]
	@QuestionID int,
	@SurveyTypeID int,
	@QuestionShortNM nvarchar(75),
	@QuestionNM nvarchar(MAX),
	@QuestionDS nvarchar(MAX),
	@Keywords nvarchar(255),
	@QuestionSort int,
	@ReviewRoleLevel int,
	@QuestionTypeID int,
	@CommentFL bit,
	@QuestionValue int,
	@UnitOfMeasureID int,
	@ModifiedID int,
	@ModifiedDT datetime,
	@FileData varbinary(max)
As
Begin
	Update Question
	Set
		[SurveyTypeID] = @SurveyTypeID,
		[QuestionShortNM] = @QuestionShortNM,
		[QuestionNM] = @QuestionNM,
		[QuestionDS] = @QuestionDS,
		[Keywords] = @Keywords,
		[QuestionSort] = @QuestionSort,
		[ReviewRoleLevel] = @ReviewRoleLevel,
		[QuestionTypeID] = @QuestionTypeID,
		[CommentFL] = @CommentFL,
		[QuestionValue] = @QuestionValue,
		[UnitOfMeasureID] = @UnitOfMeasureID,
		[ModifiedID] = @ModifiedID,
		[ModifiedDT] = @ModifiedDT,
		[FileData] = @FileData
	Where		
		[QuestionID] = @QuestionID





SELECT     Question.QuestionID, Question.QuestionNM, 
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
           lu_QuestionType.AnswerDataType
FROM         Question INNER JOIN
                      lu_QuestionType ON Question.QuestionTypeID = lu_QuestionType.QuestionTypeID
	WHERE  (Question.QuestionID = @QuestionID) 
ORDER BY Question.QuestionSort, Question.QuestionShortNM


End


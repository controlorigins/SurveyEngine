

-- ==========================================================================================
-- Entity Name:	Question_Insert
-- Create date:	10/16/2015 12:12:44 PM
-- Description:	This stored procedure is intended for inserting values to Question table
-- ==========================================================================================
CREATE Procedure [dbo].[Question_Insert]
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
	Insert Into Question
		([SurveyTypeID],[QuestionShortNM],[QuestionNM],[QuestionDS],[Keywords],[QuestionSort],[ReviewRoleLevel],[QuestionTypeID],[CommentFL],[QuestionValue],[UnitOfMeasureID],[ModifiedID],[ModifiedDT],[FileData])
	Values
		(@SurveyTypeID,@QuestionShortNM,@QuestionNM,@QuestionDS,@Keywords,@QuestionSort,@ReviewRoleLevel,@QuestionTypeID,@CommentFL,@QuestionValue,@UnitOfMeasureID,@ModifiedID,@ModifiedDT,@FileData)

	Declare @QuestionID int
	Select @QuestionID = @@IDENTITY




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




-- ==========================================================================================
-- Entity Name:	lu_SurveyType_Insert
-- Create date:	10/5/2015 12:13:50 PM
-- Description:	This stored procedure is intended for inserting values to lu_SurveyType table
-- ==========================================================================================
CREATE Procedure [dbo].[lu_SurveyType_Insert]
	@SurveyTypeShortNM nvarchar(255),
	@SurveyTypeNM nvarchar(50),
	@SurveyTypeDS nvarchar(MAX),
	@SurveyTypeComment nvarchar(MAX),
	@ApplicationTypeID int,
	@ParentSurveyTypeID int,
	@MutiSequenceFL bit,
	@ModifiedID int,
	@ModifiedDT datetime
As
Begin
	Insert Into lu_SurveyType
		([SurveyTypeShortNM],[SurveyTypeNM],[SurveyTypeDS],[SurveyTypeComment],[ApplicationTypeID],[ParentSurveyTypeID],[MutiSequenceFL],[ModifiedID],[ModifiedDT])
	Values
		(@SurveyTypeShortNM,@SurveyTypeNM,@SurveyTypeDS,@SurveyTypeComment,@ApplicationTypeID,@ParentSurveyTypeID,@MutiSequenceFL,@ModifiedID,@ModifiedDT)

	Declare @SurveyTypeID int
	Select @SurveyTypeID = @@IDENTITY
	Select 
		[SurveyTypeID],
		[SurveyTypeShortNM],
		[SurveyTypeNM],
		[SurveyTypeDS],
		[SurveyTypeComment],
		[ApplicationTypeID],
		[ParentSurveyTypeID],
		[MutiSequenceFL],
		[ModifiedID],
		[ModifiedDT]
	From lu_SurveyType
	Where
		[SurveyTypeID] = @SurveyTypeID
End


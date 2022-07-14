

-- ==========================================================================================
-- Entity Name:	lu_SurveyType_Update
-- Create date:	10/5/2015 12:13:50 PM
-- Description:	This stored procedure is intended for updating lu_SurveyType table
-- ==========================================================================================
CREATE Procedure [dbo].[lu_SurveyType_Update]
	@SurveyTypeID int,
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
	Update lu_SurveyType
	Set
		[SurveyTypeShortNM] = @SurveyTypeShortNM,
		[SurveyTypeNM] = @SurveyTypeNM,
		[SurveyTypeDS] = @SurveyTypeDS,
		[SurveyTypeComment] = @SurveyTypeComment,
		[ApplicationTypeID] = @ApplicationTypeID,
		[ParentSurveyTypeID] = @ParentSurveyTypeID,
		[MutiSequenceFL] = @MutiSequenceFL,
		[ModifiedID] = @ModifiedID,
		[ModifiedDT] = @ModifiedDT
	Where		
		[SurveyTypeID] = @SurveyTypeID
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


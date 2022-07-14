

-- ==========================================================================================
-- Entity Name:	lu_ApplicationType_SelectRow
-- Create date:	10/7/2015 10:26:11 PM
-- Description:	This stored procedure is intended for selecting a specific row from lu_ApplicationType table
-- ==========================================================================================
CREATE Procedure [dbo].[lu_ApplicationType_SelectRow]
	@ApplicationTypeID int
As
Begin
SELECT        lu_ApplicationType.ApplicationTypeID, 
              lu_ApplicationType.ApplicationTypeNM, 
			  lu_ApplicationType.ApplicationTypeDS, 
			  lu_ApplicationType.ModifiedID, 
			  lu_ApplicationType.ModifiedDT,
			  count(distinct Application.ApplicationID) as ApplicationCount,
			  count(distinct lu_surveyType.SurveyTypeID) as SurveyTypeCount
FROM    lu_ApplicationType 
LEFT OUTER JOIN Application ON lu_ApplicationType.ApplicationTypeID = Application.ApplicationTypeID
LEFT OUTER JOIN lu_SurveyType ON lu_ApplicationType.ApplicationTypeID = lu_SurveyType.ApplicationTypeID
Where lu_ApplicationType.ApplicationTypeID = @ApplicationTypeID
group by lu_ApplicationType.ApplicationTypeID, 
         lu_ApplicationType.ApplicationTypeNM, 
		 lu_ApplicationType.ApplicationTypeDS, 
		 lu_ApplicationType.ModifiedID, 
		 lu_ApplicationType.ModifiedDT
End


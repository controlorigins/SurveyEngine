

CREATE PROC [dbo].[Application_SelectBySurveyID] 
    @SurveyID integer
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN


SELECT     Application.ApplicationID, 
           Application.ApplicationNM, 
		   Application.ApplicationCD, 
		   Application.ApplicationShortNM, 
		   Application.ApplicationTypeID,
		   Application.ApplicationDS, 
		   Application.MenuOrder
FROM   Application  
where ApplicationID in (Select ApplicationID from ApplicationSurvey where SurveyID = @SurveyID)



	COMMIT






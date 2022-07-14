

-- ==========================================================================================
-- Entity Name:	Application_Update
-- Author:	Mark Hazleton
-- Create date:	9/21/2015 8:43:54 AM
-- Description:	This stored procedure is intended for updating Application table
-- ==========================================================================================
CREATE Procedure [dbo].[Application_Update]
	@ApplicationID int,
	@ApplicationNM nvarchar(250),
	@ApplicationCD nvarchar(50),
	@ApplicationShortNM nvarchar(50),
	@ApplicationTypeID int,
	@ApplicationDS nvarchar(MAX),
	@MenuOrder int,
	@ModifiedID int,
	@ModifiedDT datetime,
	@ApplicationFolder nvarchar(150),
	@DefaultPageID int,
	@CompanyID int
As
Begin
	Update Application
	Set
		[ApplicationNM] = @ApplicationNM,
		[ApplicationCD] = @ApplicationCD,
		[ApplicationShortNM] = @ApplicationShortNM,
		[ApplicationTypeID] = @ApplicationTypeID,
		[ApplicationDS] = @ApplicationDS,
		[MenuOrder] = @MenuOrder,
		[ModifiedID] = @ModifiedID,
		[ModifiedDT] = @ModifiedDT,
		[ApplicationFolder] = @ApplicationFolder,
		[DefaultPageID] = @DefaultPageID,
		[CompanyID] = @CompanyID
	Where		
		[ApplicationID] = @ApplicationID


SELECT        Application.ApplicationID, Application.ApplicationNM, Application.ApplicationCD, Application.ApplicationShortNM, Application.ApplicationTypeID, Application.ApplicationDS, Application.MenuOrder, 
                         Application.ModifiedID, Application.ModifiedDT, Application.ApplicationFolder, Application.DefaultPageID, Company.CompanyID, Company.CompanyNM, Company.CompanyCD, 
                         lu_ApplicationType.ApplicationTypeNM, lu_ApplicationType.ApplicationTypeDS
FROM            Application LEFT OUTER JOIN
                         lu_ApplicationType ON Application.ApplicationTypeID = lu_ApplicationType.ApplicationTypeID LEFT OUTER JOIN
                         Company ON Application.CompanyID = Company.CompanyID
Where [ApplicationID] = @ApplicationID

End


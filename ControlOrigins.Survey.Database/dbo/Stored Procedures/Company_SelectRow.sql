

-- ==========================================================================================
-- Entity Name:	Company_SelectRow
-- Create date:	10/9/2015 2:14:07 PM
-- Description:	This stored procedure is intended for selecting a specific row from Company table
-- ==========================================================================================
CREATE Procedure [dbo].[Company_SelectRow]
	@CompanyID int
As
Begin

SELECT        Company.CompanyID, Company.CompanyNM, Company.CompanyCD, Company.CompanyDS, Company.Title, Company.Theme, Company.DefaultTheme, Company.GalleryFolder, Company.SiteURL, 
                         Company.Address1, Company.Address2, Company.City, Company.State, Company.Country, Company.PostalCode, Company.FaxNumber, Company.PhoneNumber, Company.DefaultPaymentTerms, 
                         Company.DefaultInvoiceDescription, Company.ActiveFL, Company.Component, Company.FromEmail, Company.SMTP, Company.ModifiedDT, Company.ModifiedID, 
                         COUNT(DISTINCT ApplicationUser.ApplicationUserID) AS UserCount, COUNT(DISTINCT Application.ApplicationID) AS ApplicationCount, count(distinct SurveyResponse.SurveyResponseID) as SurveyResponseCount
FROM            Company LEFT OUTER JOIN
                         ApplicationUser ON Company.CompanyID = ApplicationUser.CompanyID LEFT OUTER JOIN
                         SurveyResponse RIGHT OUTER JOIN
                         Application ON SurveyResponse.ApplicationID = Application.ApplicationID ON Company.CompanyID = Application.CompanyID
Where Company.[CompanyID] = @CompanyID
GROUP BY Company.CompanyID, Company.CompanyNM, Company.CompanyCD, Company.CompanyDS, Company.Title, Company.Theme, Company.DefaultTheme, Company.GalleryFolder, Company.SiteURL, 
                         Company.Address1, Company.Address2, Company.City, Company.State, Company.Country, Company.PostalCode, Company.FaxNumber, Company.PhoneNumber, Company.DefaultPaymentTerms, 
                         Company.DefaultInvoiceDescription, Company.ActiveFL, Company.Component, Company.FromEmail, Company.SMTP, Company.ModifiedDT, Company.ModifiedID


End


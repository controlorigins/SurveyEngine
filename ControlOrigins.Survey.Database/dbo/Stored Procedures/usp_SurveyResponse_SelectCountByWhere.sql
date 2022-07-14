
CREATE PROC [dbo].[usp_SurveyResponse_SelectCountByWhere] 
    @WhereClase nvarchar(4000)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

DECLARE @SQLString nvarchar(4000)
DECLARE @ParmDefinition NVARCHAR(500)
DECLARE @IntVariable INT
DECLARE @ReturnCount INT
SET @SQLString = N'SELECT @ReturnCountOUT = count(*)  FROM vwApplicationSurveyResponse ' + @WhereClase  + ' '  ;

SET @ParmDefinition = N'@ReturnCountOUT INT OUTPUT'
SET @IntVariable = 35
EXECUTE sp_executesql
@SQLString,
@ParmDefinition,
@ReturnCountOUT=@ReturnCount OUTPUT
SELECT @ReturnCount as ReturnCount















COMMIT





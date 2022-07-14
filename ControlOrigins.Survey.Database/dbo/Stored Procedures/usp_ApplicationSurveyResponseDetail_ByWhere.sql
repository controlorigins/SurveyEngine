
CREATE PROC [dbo].[usp_ApplicationSurveyResponseDetail_ByWhere] 
    @StartRow INT,
    @PageSize INT,
    @WhereClase nvarchar(4000)
    
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

declare @sql nvarchar(4000);

set @sql='WITH TMP_PageSegment AS (
                SELECT *,
                ROW_NUMBER() OVER ( ORDER BY ' + 'SurveyNM, SurveyResponseNM,QuestionShortNM ' + ' ) as RowIndex
                FROM vwApplicationSurveyResponseDetail
                ' + @WhereClase + ' 
            )
            SELECT * FROM TMP_PageSegment
            WHERE
            RowIndex BETWEEN '
            +  CONVERT(nvarchar(10),@StartRow)
            + ' AND ('
            + CONVERT(nvarchar(10),@StartRow) + ' + ' + CONVERT(nvarchar(10),@PageSize)
            + ') - 1  order by '
            + CONVERT(nvarchar(100),'SurveyNM, SurveyResponseNM,QuestionShortNM ');
exec sp_executesql @sql

	COMMIT





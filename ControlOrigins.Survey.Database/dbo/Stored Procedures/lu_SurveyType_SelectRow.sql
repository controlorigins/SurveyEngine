

-- ==========================================================================================
-- Entity Name:	lu_SurveyType_SelectRow
-- Create date:	10/5/2015 12:13:50 PM
-- Description:	This stored procedure is intended for selecting a specific row from lu_SurveyType table
-- ==========================================================================================
CREATE Procedure [dbo].[lu_SurveyType_SelectRow]
	@SurveyTypeID int
As
Begin

WITH tree (SurveyTypeID, ParentSurveyTypeID, level, rn) as 
(
   SELECT SurveyTypeID, ParentSurveyTypeID, 0 as level, 
       convert(varchar(max),right(row_number() over (order by SurveyTypeID), 10)) rn
   FROM lu_SurveyType
   WHERE ParentSurveyTypeID is null or ParentSurveyTypeID = 0 

   UNION ALL

   SELECT c2.SurveyTypeID, c2.ParentSurveyTypeID, tree.level + 1, 
       rn + '.' + convert(varchar(max),right(row_number() over (order by tree.SurveyTypeID),10))
   FROM lu_SurveyType c2 
     INNER JOIN tree ON tree.SurveyTypeID = c2.ParentSurveyTypeID
)



SELECT  ST.SurveyTypeID, 
        ST.SurveyTypeShortNM, 
        ST.SurveyTypeNM, 
		ST.SurveyTypeDS, 
		ST.SurveyTypeComment, 
		ST.ApplicationTypeID,
		ST.ParentSurveyTypeID, 
		ST.MutiSequenceFL, 
		ST.ModifiedID, 
		ST.ModifiedDT, 
		PST.SurveyTypeNM as ParentSurveyTypeNM, 
		AT.ApplicationTypeNM as ApplicationTypeNM,
		TREE.level,
		TREE.rn as TreeSort, 
		COUNT(DISTINCT Q.QuestionID) AS QuestionCount, 
		COUNT(DISTINCT S.SurveyID) AS SurveyCount,
		count(DISTINCT CST.SurveyTypeID) as ChildCount
FROM    tree
Join lu_SurveyType ST on ST.SurveyTypeID = tree.SurveyTypeID 
LEFT OUTER JOIN Question AS Q ON ST.SurveyTypeID = Q.SurveyTypeID 
LEFT OUTER JOIN Survey AS S ON ST.SurveyTypeID = S.SurveyTypeID 
Left Outer Join lu_SurveyType PST on tree.ParentSurveyTypeID = PST.SurveyTypeID 
LEFT OUTER JOIN lu_ApplicationType AT on ST.ApplicationTypeID = AT.ApplicationTypeID
LEFT OUTER JOIN lu_SurveyType CST on tree.SurveyTypeID = CST.ParentSurveyTypeID
Where ST.[SurveyTypeID] = @SurveyTypeID
GROUP BY ST.SurveyTypeID, 
         ST.SurveyTypeShortNM, 
		 ST.SurveyTypeNM, 
		 ST.SurveyTypeDS, 
		 ST.SurveyTypeComment, 
		 ST.ParentSurveyTypeID, 
		 ST.MutiSequenceFL, 
		 ST.ModifiedID, 
		 ST.ModifiedDT, 
		 ST.ApplicationTypeID,
		 ST.ParentSurveyTypeID,
		 PST.SurveyTypeNM, 
		 AT.ApplicationTypeNM,
		 TREE.level,
		 TREE.rn 

order by RN










End


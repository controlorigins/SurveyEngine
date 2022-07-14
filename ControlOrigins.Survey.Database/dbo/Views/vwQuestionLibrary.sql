CREATE VIEW [dbo].[vwQuestionLibrary]
AS
SELECT        dbo.Question.QuestionID, dbo.Question.QuestionShortNM, dbo.Question.QuestionNM, dbo.Question.QuestionDS, dbo.Question.QuestionSort, dbo.Question.ReviewRoleLevel, dbo.Question.QuestionValue, 
                         dbo.lu_SurveyType.SurveyTypeID, dbo.lu_SurveyType.SurveyTypeShortNM, dbo.lu_SurveyType.SurveyTypeNM, dbo.lu_QuestionType.QuestionTypeID, dbo.lu_QuestionType.QuestionTypeCD, 
                         dbo.lu_QuestionType.QuestionTypeDS, dbo.lu_QuestionType.AnswerDataType, COUNT(DISTINCT dbo.QuestionAnswer.QuestionAnswerID) AS AnswerCount, 
                         MIN(dbo.Question.QuestionValue * dbo.QuestionAnswer.QuestionAnswerValue) AS MinScore, MAX(dbo.Question.QuestionValue * dbo.QuestionAnswer.QuestionAnswerValue) AS MaxScore, 
                         COUNT(DISTINCT dbo.QuestionGroupMember.QuestionGroupMemberID) AS SurveyCount, AVG(CAST(dbo.Question.CommentFL AS int)) AS CommentFL, dbo.lu_UnitOfMeasure.UnitOfMeasureID, 
                         dbo.lu_UnitOfMeasure.UnitOfMeasureNM, dbo.lu_UnitOfMeasure.UnitOfMeasureDS, COUNT(DISTINCT dbo.SurveyResponseAnswer.SurveyAnswerID) AS ResponseAnswerCount, dbo.Question.Keywords, 
                         dbo.Question.FileData, dbo.Question.ModifiedID, dbo.Question.ModifiedDT
FROM            dbo.Question LEFT OUTER JOIN
                         dbo.SurveyResponseAnswer ON dbo.Question.QuestionID = dbo.SurveyResponseAnswer.QuestionID LEFT OUTER JOIN
                         dbo.QuestionAnswer ON dbo.Question.QuestionID = dbo.QuestionAnswer.QuestionID AND dbo.Question.QuestionID = dbo.QuestionAnswer.QuestionID LEFT OUTER JOIN
                         dbo.lu_UnitOfMeasure ON dbo.Question.UnitOfMeasureID = dbo.lu_UnitOfMeasure.UnitOfMeasureID LEFT OUTER JOIN
                         dbo.QuestionGroupMember ON dbo.Question.QuestionID = dbo.QuestionGroupMember.QuestionID LEFT OUTER JOIN
                         dbo.lu_QuestionType ON dbo.Question.QuestionTypeID = dbo.lu_QuestionType.QuestionTypeID LEFT OUTER JOIN
                         dbo.lu_SurveyType ON dbo.Question.SurveyTypeID = dbo.lu_SurveyType.SurveyTypeID
GROUP BY dbo.Question.QuestionID, dbo.Question.QuestionShortNM, dbo.Question.QuestionNM, dbo.Question.QuestionDS, dbo.Question.QuestionSort, dbo.Question.ReviewRoleLevel, dbo.Question.QuestionValue, 
                         dbo.lu_SurveyType.SurveyTypeID, dbo.lu_SurveyType.SurveyTypeShortNM, dbo.lu_SurveyType.SurveyTypeNM, dbo.lu_QuestionType.QuestionTypeID, dbo.lu_QuestionType.QuestionTypeCD, 
                         dbo.lu_QuestionType.QuestionTypeDS, dbo.lu_QuestionType.AnswerDataType, dbo.lu_UnitOfMeasure.UnitOfMeasureID, dbo.lu_UnitOfMeasure.UnitOfMeasureNM, dbo.lu_UnitOfMeasure.UnitOfMeasureDS, 
                         dbo.Question.Keywords, dbo.Question.FileData, dbo.Question.ModifiedID, dbo.Question.ModifiedDT

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[35] 4[13] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Question"
            Begin Extent = 
               Top = 11
               Left = 324
               Bottom = 283
               Right = 525
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "SurveyResponseAnswer"
            Begin Extent = 
               Top = 446
               Left = 795
               Bottom = 576
               Right = 1002
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QuestionAnswer"
            Begin Extent = 
               Top = 285
               Left = 815
               Bottom = 415
               Right = 1055
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lu_UnitOfMeasure"
            Begin Extent = 
               Top = 143
               Left = 822
               Bottom = 273
               Right = 1027
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QuestionGroupMember"
            Begin Extent = 
               Top = 2
               Left = 817
               Bottom = 132
               Right = 1059
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lu_QuestionType"
            Begin Extent = 
               Top = 15
               Left = 47
               Bottom = 145
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lu_SurveyType"
            Begin Extent = 
               Top = 161
               Left = 20
               Bottom = 291
               ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwQuestionLibrary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'Right = 240
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 26
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1245
         Width = 2520
         Width = 2520
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwQuestionLibrary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwQuestionLibrary';


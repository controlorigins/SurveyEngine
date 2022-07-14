CREATE VIEW [dbo].[vwContextQuestionAnswer]
AS
SELECT        dbo.SurveyResponseAnswer.SurveyResponseID, dbo.SurveyResponse.StatusID, dbo.SurveyResponseAnswer.SequenceNumber, dbo.SurveyResponseAnswer.SurveyAnswerID, 
                         dbo.SurveyResponseAnswer.QuestionID, dbo.SurveyResponseAnswer.QuestionAnswerID, dbo.Question.QuestionNM, dbo.QuestionAnswer.QuestionAnswerNM, dbo.SurveyResponse.SurveyID, 
                         dbo.SurveyResponse.ApplicationID, dbo.SurveyResponse.AssignedUserID, dbo.SurveyResponse.AssignedUserID AS ApplicationUserID, dbo.SurveyResponseAnswer.AnswerType, 
                         dbo.SurveyResponseAnswer.AnswerComment, dbo.lu_SurveyType.SurveyTypeShortNM, dbo.lu_SurveyType.SurveyTypeNM
FROM            dbo.Question INNER JOIN
                         dbo.QuestionAnswer ON dbo.Question.QuestionID = dbo.QuestionAnswer.QuestionID AND dbo.Question.QuestionID = dbo.QuestionAnswer.QuestionID INNER JOIN
                         dbo.SurveyResponseAnswer ON dbo.Question.QuestionID = dbo.SurveyResponseAnswer.QuestionID AND dbo.QuestionAnswer.QuestionAnswerID = dbo.SurveyResponseAnswer.QuestionAnswerID INNER JOIN
                         dbo.SurveyResponse ON dbo.SurveyResponseAnswer.SurveyResponseID = dbo.SurveyResponse.SurveyResponseID INNER JOIN
                         dbo.lu_SurveyType ON dbo.Question.SurveyTypeID = dbo.lu_SurveyType.SurveyTypeID
WHERE        (dbo.lu_SurveyType.SurveyTypeShortNM = 'CONTEXT')

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
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
               Top = 22
               Left = 352
               Bottom = 311
               Right = 537
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QuestionAnswer"
            Begin Extent = 
               Top = 155
               Left = 641
               Bottom = 285
               Right = 865
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SurveyResponseAnswer"
            Begin Extent = 
               Top = 8
               Left = 922
               Bottom = 282
               Right = 1113
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SurveyResponse"
            Begin Extent = 
               Top = 75
               Left = 1256
               Bottom = 205
               Right = 1450
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lu_SurveyType"
            Begin Extent = 
               Top = 21
               Left = 0
               Bottom = 217
               Right = 204
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
      Begin ColumnWidths = 9
         Width = 284
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
      Begin ColumnWidths = 11
         Column = ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwContextQuestionAnswer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'1440
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwContextQuestionAnswer';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwContextQuestionAnswer';


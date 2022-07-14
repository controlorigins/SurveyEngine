CREATE VIEW [dbo].[vwApplicationSurveyResponse]
AS
SELECT        TOP (100) PERCENT SR.SurveyResponseID, SR.SurveyResponseNM, AU_Employee.AccountNM, SR.ModifiedID, SR.AssignedUserID, SR.StatusID, SR.DataSource, S.SurveyShortNM, 
                         COUNT(SRA.SurveyAnswerID) AS AnswerCount, COUNT(QGM.QuestionGroupMemberID) AS QuestionCount, SUM(CASE QA.CommentFl WHEN 'True' THEN 1 ELSE 0 END) AS VariantCount, 
                         SUM(CASE WHEN Q.ReviewRoleLevel = 2 AND QA.CommentFL = 'True' THEN 1 ELSE 0 END) AS ComplianceReview, CASE WHEN COUNT(SRA.SurveyAnswerID) 
                         = 0 THEN 0 WHEN COUNT(QGM.QuestionGroupMemberID) = 0 THEN 0 ELSE COUNT(SRA.SurveyAnswerID) * 100 / COUNT(QGM.QuestionGroupMemberID) * 100 / 100 END AS PercentComplete, 
                         AU_Employee.FirstNM, AU_Employee.LastNM, AU_Employee.eMailAddress, S.SurveyNM, SR.SurveyID, SR.ApplicationID, SR.ModifiedDT, DATEDIFF(DAY, SR.ModifiedDT, GETDATE()) AS DaySinceModified, 
                         SS.StatusNM, AU_Supervisor.ApplicationUserID AS SupervisorApplicationUserID, AU_Supervisor.FirstNM AS SupervisorFirstNM, AU_Supervisor.LastNM AS SupervisorLastNM, 
                         AU_Supervisor.AccountNM AS SupervisorAccountNM
FROM            dbo.QuestionAnswer AS QA RIGHT OUTER JOIN
                         dbo.SurveyResponseAnswer AS SRA ON QA.QuestionAnswerID = SRA.QuestionAnswerID LEFT OUTER JOIN
                         dbo.Question AS Q ON SRA.QuestionID = Q.QuestionID RIGHT OUTER JOIN
                         dbo.SurveyStatus AS SS RIGHT OUTER JOIN
                         dbo.SurveyResponse AS SR LEFT OUTER JOIN
                         dbo.QuestionGroup AS QG LEFT OUTER JOIN
                         dbo.QuestionGroupMember AS QGM ON QG.QuestionGroupID = QGM.QuestionGroupID RIGHT OUTER JOIN
                         dbo.Survey AS S ON QG.SurveyID = S.SurveyID ON SR.SurveyID = S.SurveyID ON SS.SurveyID = S.SurveyID AND SS.StatusID = SR.StatusID ON SRA.QuestionID = QGM.QuestionID AND 
                         SRA.SurveyResponseID = SR.SurveyResponseID LEFT OUTER JOIN
                         dbo.ApplicationUser AS AU_Employee LEFT OUTER JOIN
                         dbo.ApplicationUser AS AU_Supervisor ON AU_Employee.SupervisorAccountNM = AU_Supervisor.AccountNM ON SR.AssignedUserID = AU_Employee.ApplicationUserID
GROUP BY S.SurveyNM, SR.SurveyResponseID, SR.SurveyResponseNM, AU_Employee.AccountNM, SR.ModifiedID, AU_Employee.FirstNM, AU_Employee.LastNM, AU_Employee.eMailAddress, SR.SurveyID, 
                         SR.AssignedUserID, SR.StatusID, SR.DataSource, S.SurveyShortNM, SR.ApplicationID, SR.ModifiedDT, SS.StatusNM, AU_Supervisor.ApplicationUserID, AU_Supervisor.FirstNM, AU_Supervisor.LastNM, 
                         AU_Supervisor.AccountNM
ORDER BY AnswerCount DESC, QuestionCount DESC

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[18] 4[43] 2[20] 3) )"
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
         Begin Table = "AU_Employee"
            Begin Extent = 
               Top = 53
               Left = 545
               Bottom = 410
               Right = 770
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AU_Supervisor"
            Begin Extent = 
               Top = 50
               Left = 83
               Bottom = 380
               Right = 308
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QA"
            Begin Extent = 
               Top = 97
               Left = 1513
               Bottom = 227
               Right = 1753
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SRA"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Q"
            Begin Extent = 
               Top = 666
               Left = 38
               Bottom = 796
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SS"
            Begin Extent = 
               Top = 798
               Left = 38
               Bottom = 928
               Right = 261
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SR"
            Begin Extent = 
               Top = 930
               Left = 38
               Bottom = 1060
               Right = 248
            End
            DisplayFlags = 280
            ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwApplicationSurveyResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'TopColumn = 0
         End
         Begin Table = "QG"
            Begin Extent = 
               Top = 1062
               Left = 38
               Bottom = 1192
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QGM"
            Begin Extent = 
               Top = 1194
               Left = 38
               Bottom = 1324
               Right = 280
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "S"
            Begin Extent = 
               Top = 1326
               Left = 38
               Bottom = 1456
               Right = 263
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
      Begin ColumnWidths = 29
         Width = 284
         Width = 1500
         Width = 1845
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
         Column = 3510
         Alias = 3615
         Table = 2490
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwApplicationSurveyResponse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwApplicationSurveyResponse';


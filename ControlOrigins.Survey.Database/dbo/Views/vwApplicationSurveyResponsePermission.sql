CREATE VIEW [dbo].[vwApplicationSurveyResponsePermission]
AS
SELECT        TOP (100) PERCENT dbo.SurveyResponse.SurveyResponseID, dbo.SurveyResponse.SurveyResponseNM, dbo.ApplicationUser.AccountNM, dbo.SurveyResponse.ModifiedID, 
                         dbo.SurveyResponse.AssignedUserID, dbo.SurveyResponse.StatusID, dbo.SurveyResponse.DataSource, dbo.Survey.SurveyShortNM, 0 AS VariantCount, 0 AS ComplianceReview, dbo.ApplicationUser.FirstNM, 
                         dbo.ApplicationUser.LastNM, dbo.ApplicationUser.eMailAddress, dbo.Survey.SurveyNM, dbo.SurveyResponse.SurveyID, dbo.SurveyResponse.ApplicationID, dbo.SurveyResponse.ModifiedDT, DATEDIFF(DAY, 
                         dbo.SurveyResponse.ModifiedDT, GETDATE()) AS DaySinceModified, dbo.SurveyStatus.StatusNM, dbo.SurveyStatus.NextStatusID, dbo.SurveyStatus.PreviousStatusID
FROM            dbo.ApplicationUser RIGHT OUTER JOIN
                         dbo.SurveyStatus INNER JOIN
                         dbo.SurveyResponse INNER JOIN
                         dbo.Survey ON dbo.SurveyResponse.SurveyID = dbo.Survey.SurveyID ON dbo.SurveyStatus.SurveyID = dbo.Survey.SurveyID AND dbo.SurveyStatus.StatusID = dbo.SurveyResponse.StatusID AND 
                         dbo.SurveyStatus.SurveyID = dbo.SurveyResponse.SurveyID ON dbo.ApplicationUser.ApplicationUserID = dbo.SurveyResponse.AssignedUserID

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
         Begin Table = "ApplicationUser"
            Begin Extent = 
               Top = 41
               Left = 496
               Bottom = 171
               Right = 705
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SurveyStatus"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SurveyResponse"
            Begin Extent = 
               Top = 270
               Left = 283
               Bottom = 400
               Right = 477
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Survey"
            Begin Extent = 
               Top = 402
               Left = 38
               Bottom = 532
               Right = 247
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
         Or', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwApplicationSurveyResponsePermission';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N' = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwApplicationSurveyResponsePermission';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwApplicationSurveyResponsePermission';


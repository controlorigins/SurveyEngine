CREATE VIEW [dbo].[vwValidateImportUser]
AS
SELECT        TOP (100) PERCENT CASE WHEN dbo.SurveyResponse.SurveyResponseID IS NULL THEN 'ADD' ELSE 'UPDATE' END AS RowAction, dbo.Application.ApplicationID, dbo.Application.ApplicationNM, 
                         dbo.Survey.SurveyID, dbo.Survey.SurveyNM, dbo.ApplicationUserRole.ApplicationUserID, dbo.SurveyResponse.SurveyResponseNM, dbo.SurveyResponse.SurveyResponseID, 
                         ApplicationUser_Employee.ApplicationUserID AS EmployeeApplicationUserID, ApplicationUser_Employee.AccountNM, dbo.ApplicationUserRole.ApplicationUserRoleID, dbo.SurveyResponse.DataSource, 
                         dbo.SurveyResponse.StatusID, dbo.ApplicationUserRole.RoleID, ApplicationUser_Employee.FirstNM, ApplicationUser_Employee.LastNM, ApplicationUser_Employee.eMailAddress, 
                         ApplicationUser_Employee.CommentDS, ApplicationUser_Supervisor.ApplicationUserID AS SupervisorApplicationUserID, ApplicationUser_Supervisor.AccountNM AS SupervisorAccountNM, 
                         ApplicationUser_Supervisor.FirstNM AS SupervisorFirstNM, ApplicationUser_Supervisor.LastNM AS SupervisorLastNM, ApplicationUser_Supervisor.eMailAddress AS SupervisoreMailAddress
FROM            dbo.SurveyResponse RIGHT OUTER JOIN
                         dbo.ApplicationUser AS ApplicationUser_Supervisor RIGHT OUTER JOIN
                         dbo.Application INNER JOIN
                         dbo.ApplicationSurvey ON dbo.Application.ApplicationID = dbo.ApplicationSurvey.ApplicationID INNER JOIN
                         dbo.Survey ON dbo.ApplicationSurvey.SurveyID = dbo.Survey.SurveyID INNER JOIN
                         dbo.ApplicationUserRole ON dbo.Application.ApplicationID = dbo.ApplicationUserRole.ApplicationID INNER JOIN
                         dbo.ApplicationUser AS ApplicationUser_Employee ON dbo.ApplicationUserRole.ApplicationUserID = ApplicationUser_Employee.ApplicationUserID ON 
                         ApplicationUser_Supervisor.AccountNM = ApplicationUser_Employee.SupervisorAccountNM ON dbo.SurveyResponse.SurveyID = dbo.Survey.SurveyID AND 
                         dbo.SurveyResponse.ApplicationID = dbo.ApplicationUserRole.ApplicationID AND dbo.SurveyResponse.AssignedUserID = dbo.ApplicationUserRole.ApplicationUserID
ORDER BY dbo.ApplicationUserRole.ApplicationUserID

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[17] 4[44] 2[20] 3) )"
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
         Begin Table = "ApplicationUser_Supervisor"
            Begin Extent = 
               Top = 26
               Left = 1404
               Bottom = 332
               Right = 1613
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Application"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 236
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ApplicationSurvey"
            Begin Extent = 
               Top = 270
               Left = 274
               Bottom = 400
               Right = 470
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
         Begin Table = "ApplicationUserRole"
            Begin Extent = 
               Top = 534
               Left = 38
               Bottom = 664
               Right = 245
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ApplicationUser_Employee"
            Begin Extent = 
               Top = 65
               Left = 914
               Bottom = 278
               Right = 1123
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "SurveyResponse"
            Begin Extent = 
               Top = 534
               Left = 283
               Bottom = 664
   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwValidateImportUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'            Right = 477
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
         Column = 6315
         Alias = 5955
         Table = 4470
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwValidateImportUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'vwValidateImportUser';


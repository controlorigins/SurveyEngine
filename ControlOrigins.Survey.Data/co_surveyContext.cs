using Microsoft.EntityFrameworkCore;
using System;

namespace ControlOrigins.Survey.Data
{
    public partial class co_surveyContext : DbContext
    {
        public co_surveyContext()
        {
        }

        public co_surveyContext(DbContextOptions<co_surveyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppProperty> AppProperties { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationSurvey> ApplicationSurveys { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public virtual DbSet<ChartSetting> ChartSettings { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<ImportHistory> ImportHistories { get; set; }
        public virtual DbSet<LuApplicationType> LuApplicationTypes { get; set; }
        public virtual DbSet<LuQuestionType> LuQuestionTypes { get; set; }
        public virtual DbSet<LuReviewStatus> LuReviewStatuses { get; set; }
        public virtual DbSet<LuSurveyResponseStatus> LuSurveyResponseStatuses { get; set; }
        public virtual DbSet<LuSurveyType> LuSurveyTypes { get; set; }
        public virtual DbSet<LuUnitOfMeasure> LuUnitOfMeasures { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionGroup> QuestionGroups { get; set; }
        public virtual DbSet<QuestionGroupMember> QuestionGroupMembers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SiteAppMenu> SiteAppMenus { get; set; }
        public virtual DbSet<SiteRole> SiteRoles { get; set; }
        public virtual DbSet<Survey> Surveys { get; set; }
        public virtual DbSet<SurveyEmailTemplate> SurveyEmailTemplates { get; set; }
        public virtual DbSet<SurveyResponse> SurveyResponses { get; set; }
        public virtual DbSet<SurveyResponseAnswer> SurveyResponseAnswers { get; set; }
        public virtual DbSet<SurveyResponseAnswerError> SurveyResponseAnswerErrors { get; set; }
        public virtual DbSet<SurveyResponseAnswerReview> SurveyResponseAnswerReviews { get; set; }
        public virtual DbSet<SurveyResponseHistory> SurveyResponseHistories { get; set; }
        public virtual DbSet<SurveyResponseSequence> SurveyResponseSequences { get; set; }
        public virtual DbSet<SurveyResponseState> SurveyResponseStates { get; set; }
        public virtual DbSet<SurveyReviewStatus> SurveyReviewStatuses { get; set; }
        public virtual DbSet<SurveyStatus> SurveyStatuses { get; set; }
        public virtual DbSet<TblFile> TblFiles { get; set; }
        public virtual DbSet<UserAppProperty> UserAppProperties { get; set; }
        public virtual DbSet<UserMessage> UserMessages { get; set; }
        public virtual DbSet<VwApplication> VwApplications { get; set; }
        public virtual DbSet<VwApplicationPermissison> VwApplicationPermissisons { get; set; }
        public virtual DbSet<VwApplicationSurveyResponse> VwApplicationSurveyResponses { get; set; }
        public virtual DbSet<VwApplicationSurveyResponseDetail> VwApplicationSurveyResponseDetails { get; set; }
        public virtual DbSet<VwApplicationSurveyResponsePermission> VwApplicationSurveyResponsePermissions { get; set; }
        public virtual DbSet<VwApplicationSurveyResponseSummary> VwApplicationSurveyResponseSummaries { get; set; }
        public virtual DbSet<VwContextQuestionAnswer> VwContextQuestionAnswers { get; set; }
        public virtual DbSet<VwQuestionLibrary> VwQuestionLibraries { get; set; }
        public virtual DbSet<VwSurveyQuestion> VwSurveyQuestions { get; set; }
        public virtual DbSet<VwSurveyResponseContext> VwSurveyResponseContexts { get; set; }
        public virtual DbSet<VwSurveyResponseDetail> VwSurveyResponseDetails { get; set; }
        public virtual DbSet<VwSurveyResponseGroupSummary> VwSurveyResponseGroupSummaries { get; set; }
        public virtual DbSet<VwSurveyResponseSummary> VwSurveyResponseSummaries { get; set; }
        public virtual DbSet<VwValidateImportUser> VwValidateImportUsers { get; set; }
        public virtual DbSet<WebPortal> WebPortals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:markhazleton.database.windows.net,1433;Initial Catalog=co_survey;Persist Security Info=False;User ID=markhazleton;Password=JustD01t;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppProperty>(entity =>
            {
                entity.ToTable("AppProperty");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SiteAppId).HasColumnName("SiteAppID");

                entity.HasOne(d => d.SiteApp)
                    .WithMany(p => p.AppProperties)
                    .HasForeignKey(d => d.SiteAppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppProperty_Application");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("Application_PK")
                    .IsClustered(false);

                entity.ToTable("Application");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationDs).HasColumnName("ApplicationDS");

                entity.Property(e => e.ApplicationFolder)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasDefaultValueSql("(N'SurveyAdmin')");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DefaultPageId)
                    .HasColumnName("DefaultPageID")
                    .HasDefaultValueSql("((63))");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.ApplicationType)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.ApplicationTypeId)
                    .HasConstraintName("System_FK01");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_Application_Company");
            });

            modelBuilder.Entity<ApplicationSurvey>(entity =>
            {
                entity.HasKey(e => e.ApplicationSurveyId)
                    .HasName("ApplicationSurvey_PK")
                    .IsClustered(false);

                entity.ToTable("ApplicationSurvey");

                entity.HasIndex(e => new { e.ApplicationId, e.SurveyId }, "UK_ApplicationSurvey")
                    .IsUnique();

                entity.Property(e => e.ApplicationSurveyId).HasColumnName("ApplicationSurveyID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.DefaultRoleId).HasColumnName("DefaultRoleID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationSurveys)
                    .HasForeignKey(d => d.ApplicationId)
                    .HasConstraintName("SystemSurvey_FK01");

                entity.HasOne(d => d.DefaultRole)
                    .WithMany(p => p.ApplicationSurveys)
                    .HasForeignKey(d => d.DefaultRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationSurvey_Role");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.ApplicationSurveys)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SystemSurvey_FK00");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.ApplicationUserId)
                    .HasName("ApplicationUser_PK")
                    .IsClustered(false);

                entity.ToTable("ApplicationUser");

                entity.HasIndex(e => e.AccountNm, "UK_ApplicationUser_AccountNM")
                    .IsUnique();

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.AccountNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.CommentDs).HasColumnName("CommentDS");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasDefaultValueSql("('Display Name')");

                entity.Property(e => e.EMailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastLoginDt)
                    .HasColumnType("datetime")
                    .HasColumnName("LastLoginDT");

                entity.Property(e => e.LastLoginLocation).HasMaxLength(50);

                entity.Property(e => e.LastNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasDefaultValueSql("(N'password')");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("((4))");

                entity.Property(e => e.SupervisorAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("SupervisorAccountNM");

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasDefaultValueSql("('User Login')");

                entity.Property(e => e.VerifyCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'Verify Code')");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_ApplicationUser_Company");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUser_SiteRole");
            });

            modelBuilder.Entity<ApplicationUserRole>(entity =>
            {
                entity.HasKey(e => e.ApplicationUserRoleId)
                    .HasName("ApplicationUserRole_PK")
                    .IsClustered(false);

                entity.ToTable("ApplicationUserRole");

                entity.HasIndex(e => new { e.ApplicationId, e.ApplicationUserId }, "UK_ApplicationUserRole")
                    .IsUnique();

                entity.Property(e => e.ApplicationUserRoleId).HasColumnName("ApplicationUserRoleID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StartUpDate).HasColumnType("date");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationUserRoles)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserRole_Application");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.ApplicationUserRoles)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .HasConstraintName("UserRole_FK01");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ApplicationUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserRole_Role");
            });

            modelBuilder.Entity<ChartSetting>(entity =>
            {
                entity.ToTable("ChartSetting");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");

                entity.Property(e => e.SettingName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SettingType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SettingValue).IsRequired();

                entity.Property(e => e.SiteAppId).HasColumnName("SiteAppID");

                entity.Property(e => e.SiteUserId).HasColumnName("SiteUserID");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.HasIndex(e => e.CompanyCd, "UK_CompanyCD")
                    .IsUnique();

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ActiveFl).HasColumnName("ActiveFL");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Address2).HasMaxLength(100);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CompanyCd)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("CompanyCD");

                entity.Property(e => e.CompanyDs)
                    .HasMaxLength(255)
                    .HasColumnName("CompanyDS");

                entity.Property(e => e.CompanyNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("CompanyNM");

                entity.Property(e => e.Component).HasMaxLength(50);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DefaultPaymentTerms).HasMaxLength(255);

                entity.Property(e => e.DefaultTheme)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.FaxNumber).HasMaxLength(30);

                entity.Property(e => e.FromEmail).HasMaxLength(50);

                entity.Property(e => e.GalleryFolder)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PhoneNumber).HasMaxLength(30);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SiteUrl)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("SiteURL");

                entity.Property(e => e.Smtp)
                    .HasMaxLength(50)
                    .HasColumnName("SMTP");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Theme)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ImportHistory>(entity =>
            {
                entity.ToTable("ImportHistory");

                entity.Property(e => e.ImportHistoryId).HasColumnName("ImportHistoryID");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ImportType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<LuApplicationType>(entity =>
            {
                entity.HasKey(e => e.ApplicationTypeId)
                    .HasName("ApplicationType_PK")
                    .IsClustered(false);

                entity.ToTable("lu_ApplicationType");

                entity.HasIndex(e => e.ApplicationTypeNm, "UK_lu_ApplicationType_ApplicationTypeNM")
                    .IsUnique();

                entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");

                entity.Property(e => e.ApplicationTypeDs).HasColumnName("ApplicationTypeDS");

                entity.Property(e => e.ApplicationTypeNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationTypeNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<LuQuestionType>(entity =>
            {
                entity.HasKey(e => e.QuestionTypeId)
                    .HasName("aaaaaQuestionType_PK")
                    .IsClustered(false);

                entity.ToTable("lu_QuestionType");

                entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");

                entity.Property(e => e.AnswerDataType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ControlName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionTypeCd)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("QuestionTypeCD");

                entity.Property(e => e.QuestionTypeDs)
                    .IsRequired()
                    .HasColumnName("QuestionTypeDS");
            });

            modelBuilder.Entity<LuReviewStatus>(entity =>
            {
                entity.HasKey(e => e.ReviewStatusId);

                entity.ToTable("lu_ReviewStatus");

                entity.Property(e => e.ReviewStatusId).HasColumnName("ReviewStatusID");

                entity.Property(e => e.ApprovedFl).HasColumnName("ApprovedFL");

                entity.Property(e => e.CommentFl).HasColumnName("CommentFL");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReviewStatusDs)
                    .IsRequired()
                    .HasColumnName("ReviewStatusDS");

                entity.Property(e => e.ReviewStatusNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ReviewStatusNM");
            });

            modelBuilder.Entity<LuSurveyResponseStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("lu_SurveyResponseStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NextStatusId).HasColumnName("NextStatusID");

                entity.Property(e => e.PreviousStatusId).HasColumnName("PreviousStatusID");

                entity.Property(e => e.StatusDs)
                    .IsRequired()
                    .HasColumnName("StatusDS");

                entity.Property(e => e.StatusNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");
            });

            modelBuilder.Entity<LuSurveyType>(entity =>
            {
                entity.HasKey(e => e.SurveyTypeId)
                    .HasName("SurveyType_PK")
                    .IsClustered(false);

                entity.ToTable("lu_SurveyType");

                entity.HasIndex(e => e.SurveyTypeNm, "UK_lu_SurveyType_SurveyTypeNM")
                    .IsUnique();

                entity.HasIndex(e => e.SurveyTypeShortNm, "UK_lu_SurveyType_SurveyTypeShortNM")
                    .IsUnique();

                entity.Property(e => e.SurveyTypeId).HasColumnName("SurveyTypeID");

                entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MutiSequenceFl).HasColumnName("MutiSequenceFL");

                entity.Property(e => e.ParentSurveyTypeId).HasColumnName("ParentSurveyTypeID");

                entity.Property(e => e.SurveyTypeDs).HasColumnName("SurveyTypeDS");

                entity.Property(e => e.SurveyTypeNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyTypeNM");

                entity.Property(e => e.SurveyTypeShortNm)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("SurveyTypeShortNM");
            });

            modelBuilder.Entity<LuUnitOfMeasure>(entity =>
            {
                entity.HasKey(e => e.UnitOfMeasureId)
                    .HasName("aaaaaUnitOfMeasure_PK")
                    .IsClustered(false);

                entity.ToTable("lu_UnitOfMeasure");

                entity.Property(e => e.UnitOfMeasureId).HasColumnName("UnitOfMeasureID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitOfMeasureDs).HasColumnName("UnitOfMeasureDS");

                entity.Property(e => e.UnitOfMeasureNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("UnitOfMeasureNM");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .IsClustered(false);

                entity.ToTable("Question");

                entity.HasIndex(e => e.QuestionShortNm, "UK_QuestionShortName")
                    .IsUnique();

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.CommentFl).HasColumnName("CommentFL");

                entity.Property(e => e.Keywords).HasMaxLength(255);

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionDs)
                    .IsRequired()
                    .HasColumnName("QuestionDS");

                entity.Property(e => e.QuestionNm)
                    .IsRequired()
                    .HasColumnName("QuestionNM");

                entity.Property(e => e.QuestionShortNm)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("QuestionShortNM");

                entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");

                entity.Property(e => e.SurveyTypeId).HasColumnName("SurveyTypeID");

                entity.Property(e => e.UnitOfMeasureId).HasColumnName("UnitOfMeasureID");

                entity.HasOne(d => d.QuestionType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuestionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_QuestionType");

                entity.HasOne(d => d.SurveyType)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.SurveyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_SurveyType");

                entity.HasOne(d => d.UnitOfMeasure)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.UnitOfMeasureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_lu_UnitOfMeasure");
            });

            modelBuilder.Entity<QuestionAnswer>(entity =>
            {
                entity.HasKey(e => e.QuestionAnswerId)
                    .HasName("QuestionAnswer_PK")
                    .IsClustered(false);

                entity.ToTable("QuestionAnswer");

                entity.HasIndex(e => new { e.QuestionId, e.QuestionAnswerShortNm }, "UK_QuestionAnswer_ShortNMQuestionID")
                    .IsUnique();

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.ActiveFl).HasColumnName("ActiveFL");

                entity.Property(e => e.CommentFl).HasColumnName("CommentFL");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionAnswerDs)
                    .IsRequired()
                    .HasColumnName("QuestionAnswerDS");

                entity.Property(e => e.QuestionAnswerNm)
                    .IsRequired()
                    .HasColumnName("QuestionAnswerNM");

                entity.Property(e => e.QuestionAnswerShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionAnswerShortNM");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionAnswer_Question");
            });

            modelBuilder.Entity<QuestionGroup>(entity =>
            {
                entity.HasKey(e => e.QuestionGroupId)
                    .IsClustered(false);

                entity.ToTable("QuestionGroup");

                entity.Property(e => e.QuestionGroupId).HasColumnName("QuestionGroupID");

                entity.Property(e => e.DependentMaxScore).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DependentMinScore).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DependentQuestionGroupId).HasColumnName("DependentQuestionGroupID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionGroupDs).HasColumnName("QuestionGroupDS");

                entity.Property(e => e.QuestionGroupNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupNM");

                entity.Property(e => e.QuestionGroupShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupShortNM");

                entity.Property(e => e.QuestionGroupWeight)
                    .HasColumnType("decimal(18, 4)")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.QuestionGroups)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionGroup_Survey");
            });

            modelBuilder.Entity<QuestionGroupMember>(entity =>
            {
                entity.HasKey(e => e.QuestionGroupMemberId)
                    .HasName("QuestionGroupMember_PK")
                    .IsClustered(false);

                entity.ToTable("QuestionGroupMember");

                entity.Property(e => e.QuestionGroupMemberId).HasColumnName("QuestionGroupMemberID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionGroupId).HasColumnName("QuestionGroupID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionWeight).HasColumnType("decimal(18, 4)");

                entity.HasOne(d => d.QuestionGroup)
                    .WithMany(p => p.QuestionGroupMembers)
                    .HasForeignKey(d => d.QuestionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("QuestionGroupMember_FK01");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionGroupMembers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("QuestionGroupMember_FK02");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("aaaaaRole_PK")
                    .IsClustered(false);

                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReadFl).HasColumnName("ReadFL");

                entity.Property(e => e.ReviewLevel).HasDefaultValueSql("((1))");

                entity.Property(e => e.RoleCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("RoleCD");

                entity.Property(e => e.RoleDs)
                    .IsRequired()
                    .HasColumnName("RoleDS");

                entity.Property(e => e.RoleNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("RoleNM");

                entity.Property(e => e.UpdateFl).HasColumnName("UpdateFL");
            });

            modelBuilder.Entity<SiteAppMenu>(entity =>
            {
                entity.ToTable("SiteAppMenu");

                entity.HasIndex(e => new { e.MenuText, e.SiteAppId }, "UK_SiteAppMenu")
                    .IsUnique();

                entity.Property(e => e.GlyphName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MenuText)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SiteAppId).HasColumnName("SiteAppID");

                entity.Property(e => e.SiteRoleId).HasColumnName("SiteRoleID");

                entity.Property(e => e.TartgetPage).IsRequired();

                entity.HasOne(d => d.SiteApp)
                    .WithMany(p => p.SiteAppMenus)
                    .HasForeignKey(d => d.SiteAppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SiteAppMenu_Application");
            });

            modelBuilder.Entity<SiteRole>(entity =>
            {
                entity.ToTable("SiteRole");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(e => e.SurveyId)
                    .HasName("Survey_PK")
                    .IsClustered(false);

                entity.ToTable("Survey");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.CompletionMessage).IsRequired();

                entity.Property(e => e.EndDt)
                    .HasColumnType("date")
                    .HasColumnName("EndDT");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ParentSurveyId).HasColumnName("ParentSurveyID");

                entity.Property(e => e.ResponseNmtemplate)
                    .HasMaxLength(100)
                    .HasColumnName("ResponseNMTemplate");

                entity.Property(e => e.ReviewerAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("ReviewerAccountNM");

                entity.Property(e => e.StartDt)
                    .HasColumnType("date")
                    .HasColumnName("StartDT");

                entity.Property(e => e.SurveyDs)
                    .IsRequired()
                    .HasColumnName("SurveyDS");

                entity.Property(e => e.SurveyNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");

                entity.Property(e => e.SurveyTypeId).HasColumnName("SurveyTypeID");

                entity.Property(e => e.UseQuestionGroupsFl).HasColumnName("UseQuestionGroupsFL");

                entity.HasOne(d => d.SurveyType)
                    .WithMany(p => p.Surveys)
                    .HasForeignKey(d => d.SurveyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Survey_SurveyType");
            });

            modelBuilder.Entity<SurveyEmailTemplate>(entity =>
            {
                entity.ToTable("SurveyEmailTemplate");

                entity.Property(e => e.SurveyEmailTemplateId).HasColumnName("SurveyEmailTemplateID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.EmailTemplate).IsRequired();

                entity.Property(e => e.EndDt)
                    .HasColumnType("datetime")
                    .HasColumnName("EndDT");

                entity.Property(e => e.FromEmailAddress)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.StartDt)
                    .HasColumnType("datetime")
                    .HasColumnName("StartDT");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SubjectTemplate).IsRequired();

                entity.Property(e => e.SurveyEmailTemplateNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyEmailTemplateNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyEmailTemplates)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyEmailTemplate_Survey");
            });

            modelBuilder.Entity<SurveyResponse>(entity =>
            {
                entity.HasKey(e => e.SurveyResponseId)
                    .HasName("SurveyResponse_PK")
                    .IsClustered(false);

                entity.ToTable("SurveyResponse");

                entity.HasIndex(e => new { e.SurveyResponseNm, e.AssignedUserId }, "SurveyResponse_UK")
                    .IsUnique();

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.SurveyResponses)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponse_Application");

                entity.HasOne(d => d.AssignedUser)
                    .WithMany(p => p.SurveyResponses)
                    .HasForeignKey(d => d.AssignedUserId)
                    .HasConstraintName("SurveyResponse_FK02");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyResponses)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SurveyResponse_FK00");
            });

            modelBuilder.Entity<SurveyResponseAnswer>(entity =>
            {
                entity.HasKey(e => e.SurveyAnswerId)
                    .HasName("SurveyResponseAnswer_PK")
                    .IsClustered(false);

                entity.ToTable("SurveyResponseAnswer");

                entity.Property(e => e.SurveyAnswerId).HasColumnName("SurveyAnswerID");

                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.AnswerType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.SequenceNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.HasOne(d => d.QuestionAnswer)
                    .WithMany(p => p.SurveyResponseAnswers)
                    .HasForeignKey(d => d.QuestionAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseAnswer_QuestionAnswer");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.SurveyResponseAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseAnswer_Question");

                entity.HasOne(d => d.S)
                    .WithMany(p => p.SurveyResponseAnswers)
                    .HasPrincipalKey(p => new { p.SurveyResponseId, p.SequenceNumber })
                    .HasForeignKey(d => new { d.SurveyResponseId, d.SequenceNumber })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseAnswer_SurveyResponseSequence");
            });

            modelBuilder.Entity<SurveyResponseAnswerError>(entity =>
            {
                entity.HasKey(e => e.SurveyAnswerErrorId)
                    .HasName("aaaaaSurveyResponseAnswer_Error_PK")
                    .IsClustered(false);

                entity.ToTable("SurveyResponseAnswer_Error");

                entity.Property(e => e.SurveyAnswerErrorId).HasColumnName("SurveyAnswer_ErrorID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProgramName).IsRequired();

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.SequenceNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");
            });

            modelBuilder.Entity<SurveyResponseAnswerReview>(entity =>
            {
                entity.ToTable("SurveyResponseAnswerReview");

                entity.Property(e => e.SurveyResponseAnswerReviewId).HasColumnName("SurveyResponseAnswerReviewID");

                entity.Property(e => e.ApplicationUserRoleId).HasColumnName("ApplicationUserRoleID");

                entity.Property(e => e.ModifiedComment).IsRequired();

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReviewLevel).HasDefaultValueSql("((1))");

                entity.Property(e => e.ReviewStatusId).HasColumnName("ReviewStatusID");

                entity.Property(e => e.SurveyAnswerId).HasColumnName("SurveyAnswerID");

                entity.HasOne(d => d.ApplicationUserRole)
                    .WithMany(p => p.SurveyResponseAnswerReviews)
                    .HasForeignKey(d => d.ApplicationUserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseAnswerReview_ApplicationUserRole");

                entity.HasOne(d => d.SurveyAnswer)
                    .WithMany(p => p.SurveyResponseAnswerReviews)
                    .HasForeignKey(d => d.SurveyAnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseAnswerReview_SurveyResponseAnswer");
            });

            modelBuilder.Entity<SurveyResponseHistory>(entity =>
            {
                entity.HasKey(e => e.SurveyResponseHistoryId)
                    .HasName("aaaaaSurveyResponseHistory_PK")
                    .IsClustered(false);

                entity.ToTable("SurveyResponseHistory");

                entity.Property(e => e.SurveyResponseHistoryId).HasColumnName("SurveyResponseHistoryID");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.QuestionGroupId).HasColumnName("QuestionGroupID");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.UserNm)
                    .HasMaxLength(50)
                    .HasColumnName("UserNM");

                entity.HasOne(d => d.SurveyResponse)
                    .WithMany(p => p.SurveyResponseHistories)
                    .HasForeignKey(d => d.SurveyResponseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseHistory_SurveyResponse");
            });

            modelBuilder.Entity<SurveyResponseSequence>(entity =>
            {
                entity.HasKey(e => e.SurveyResponseSequenceId)
                    .HasName("aaaaaSurveyResponseSequence_PK")
                    .IsClustered(false);

                entity.ToTable("SurveyResponseSequence");

                entity.HasIndex(e => new { e.SurveyResponseId, e.SequenceNumber }, "UK_SurveyResponseSequence")
                    .IsUnique();

                entity.Property(e => e.SurveyResponseSequenceId).HasColumnName("SurveyResponseSequenceID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SequenceNumber).HasDefaultValueSql("((1))");

                entity.Property(e => e.SequenceText).HasMaxLength(255);

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.HasOne(d => d.SurveyResponse)
                    .WithMany(p => p.SurveyResponseSequences)
                    .HasForeignKey(d => d.SurveyResponseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SurveyResponseSequence_FK00");
            });

            modelBuilder.Entity<SurveyResponseState>(entity =>
            {
                entity.ToTable("SurveyResponseState");

                entity.Property(e => e.SurveyResponseStateId).HasColumnName("SurveyResponseStateID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.HasOne(d => d.AssignedUser)
                    .WithMany(p => p.SurveyResponseStates)
                    .HasForeignKey(d => d.AssignedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseState_ApplicationUser");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SurveyResponseStates)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseState_lu_SurveyResponseStatus");

                entity.HasOne(d => d.SurveyResponse)
                    .WithMany(p => p.SurveyResponseStates)
                    .HasForeignKey(d => d.SurveyResponseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyResponseState_SurveyResponse");
            });

            modelBuilder.Entity<SurveyReviewStatus>(entity =>
            {
                entity.ToTable("SurveyReviewStatus");

                entity.HasIndex(e => new { e.SurveyId, e.ReviewStatusId }, "UK_SurveyReviewStatus_SurveyStatus")
                    .IsUnique();

                entity.Property(e => e.SurveyReviewStatusId).HasColumnName("SurveyReviewStatusID");

                entity.Property(e => e.ApprovedFl).HasColumnName("ApprovedFL");

                entity.Property(e => e.CommentFl).HasColumnName("CommentFL");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ReviewStatusDs)
                    .IsRequired()
                    .HasColumnName("ReviewStatusDS");

                entity.Property(e => e.ReviewStatusId).HasColumnName("ReviewStatusID");

                entity.Property(e => e.ReviewStatusNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ReviewStatusNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyReviewStatuses)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyReviewStatus_Survey");
            });

            modelBuilder.Entity<SurveyStatus>(entity =>
            {
                entity.ToTable("SurveyStatus");

                entity.HasIndex(e => new { e.SurveyId, e.StatusId }, "UK_SurveyStatus_SurveyStatus")
                    .IsUnique();

                entity.Property(e => e.SurveyStatusId).HasColumnName("SurveyStatusID");

                entity.Property(e => e.EmailSubjectTemplate).IsRequired();

                entity.Property(e => e.EmailTemplate).IsRequired();

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.NextStatusId).HasColumnName("NextStatusID");

                entity.Property(e => e.PreviousStatusId).HasColumnName("PreviousStatusID");

                entity.Property(e => e.StatusDs)
                    .IsRequired()
                    .HasColumnName("StatusDS");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.HasOne(d => d.Survey)
                    .WithMany(p => p.SurveyStatuses)
                    .HasForeignKey(d => d.SurveyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SurveyStatus_Survey");
            });

            modelBuilder.Entity<TblFile>(entity =>
            {
                entity.ToTable("tblFiles");

                entity.HasIndex(e => new { e.Name, e.ContentType }, "UK_tblFiles_Type_Name")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Data).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserAppProperty>(entity =>
            {
                entity.ToTable("UserAppProperty");

                entity.HasIndex(e => new { e.AppId, e.UserId }, "UK_UserAppProperty")
                    .IsUnique();

                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Value).IsRequired();

                entity.HasOne(d => d.App)
                    .WithMany(p => p.UserAppProperties)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAppProperty_Application");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAppProperties)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAppProperty_ApplicationUser");
            });

            modelBuilder.Entity<UserMessage>(entity =>
            {
                entity.Property(e => e.AppId).HasColumnName("AppID");

                entity.Property(e => e.CratedDateTime).HasColumnType("datetime");

                entity.Property(e => e.FromUserId).HasColumnName("FromUserID");

                entity.Property(e => e.Subject).HasMaxLength(50);

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.UserMessageFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("FK_UserMessages_FROM_ApplicationUser");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.UserMessageToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .HasConstraintName("FK_UserMessages_TO_ApplicationUser");
            });

            modelBuilder.Entity<VwApplication>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplication");

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationDs).HasColumnName("ApplicationDS");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.ApplicationTypeDs).HasColumnName("ApplicationTypeDS");

                entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");

                entity.Property(e => e.ApplicationTypeNm)
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationTypeNM");
            });

            modelBuilder.Entity<VwApplicationPermissison>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplicationPermissison");

                entity.Property(e => e.AccountNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationDs).HasColumnName("ApplicationDS");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.ApplicationUserRoleId).HasColumnName("ApplicationUserRoleID");

                entity.Property(e => e.CommentDs).HasColumnName("CommentDS");

                entity.Property(e => e.EMailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastLoginDt)
                    .HasColumnType("datetime")
                    .HasColumnName("LastLoginDT");

                entity.Property(e => e.LastLoginLocation).HasMaxLength(50);

                entity.Property(e => e.LastNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.ReadFl).HasColumnName("ReadFL");

                entity.Property(e => e.RoleCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("RoleCD");

                entity.Property(e => e.RoleDs)
                    .IsRequired()
                    .HasColumnName("RoleDS");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("RoleNM");

                entity.Property(e => e.UpdateFl).HasColumnName("UpdateFL");
            });

            modelBuilder.Entity<VwApplicationSurveyResponse>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplicationSurveyResponse");

                entity.Property(e => e.AccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EMailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastNm)
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusNm)
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");

                entity.Property(e => e.SupervisorAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("SupervisorAccountNM");

                entity.Property(e => e.SupervisorApplicationUserId).HasColumnName("SupervisorApplicationUserID");

                entity.Property(e => e.SupervisorFirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("SupervisorFirstNM");

                entity.Property(e => e.SupervisorLastNm)
                    .HasMaxLength(100)
                    .HasColumnName("SupervisorLastNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.SurveyShortNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");
            });

            modelBuilder.Entity<VwApplicationSurveyResponseDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplicationSurveyResponseDetail");

                entity.Property(e => e.AccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ActiveFl).HasColumnName("ActiveFL");

                entity.Property(e => e.AnswerDataType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.AnswerType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationDs).HasColumnName("ApplicationDS");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.ApplicationTypeDs).HasColumnName("ApplicationTypeDS");

                entity.Property(e => e.ApplicationTypeId).HasColumnName("ApplicationTypeID");

                entity.Property(e => e.ApplicationTypeNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationTypeNM");

                entity.Property(e => e.CommentDs).HasColumnName("CommentDS");

                entity.Property(e => e.CompletionMessage).IsRequired();

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DependentMaxScore).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DependentMinScore).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DependentQuestionGroupId).HasColumnName("DependentQuestionGroupID");

                entity.Property(e => e.EMailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.EmailSubjectTemplate).IsRequired();

                entity.Property(e => e.EmailTemplate).IsRequired();

                entity.Property(e => e.EndDt)
                    .HasColumnType("date")
                    .HasColumnName("EndDT");

                entity.Property(e => e.FirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastLoginDt)
                    .HasColumnType("datetime")
                    .HasColumnName("LastLoginDT");

                entity.Property(e => e.LastLoginLocation).HasMaxLength(50);

                entity.Property(e => e.LastNm)
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.MutiSequenceFl).HasColumnName("MutiSequenceFL");

                entity.Property(e => e.QuestionAnswerCommentFl).HasColumnName("QuestionAnswerCommentFL");

                entity.Property(e => e.QuestionAnswerDs)
                    .IsRequired()
                    .HasColumnName("QuestionAnswerDS");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.QuestionAnswerNm)
                    .IsRequired()
                    .HasColumnName("QuestionAnswerNM");

                entity.Property(e => e.QuestionAnswerShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionAnswerShortNM");

                entity.Property(e => e.QuestionCommentFl).HasColumnName("QuestionCommentFL");

                entity.Property(e => e.QuestionDs)
                    .IsRequired()
                    .HasColumnName("QuestionDS");

                entity.Property(e => e.QuestionGroupDs).HasColumnName("QuestionGroupDS");

                entity.Property(e => e.QuestionGroupId).HasColumnName("QuestionGroupID");

                entity.Property(e => e.QuestionGroupMemberId).HasColumnName("QuestionGroupMemberID");

                entity.Property(e => e.QuestionGroupNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupNM");

                entity.Property(e => e.QuestionGroupShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupShortNM");

                entity.Property(e => e.QuestionGroupWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionNm)
                    .IsRequired()
                    .HasColumnName("QuestionNM");

                entity.Property(e => e.QuestionShortNm)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("QuestionShortNM");

                entity.Property(e => e.QuestionTypeCd)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("QuestionTypeCD");

                entity.Property(e => e.QuestionTypeDs)
                    .IsRequired()
                    .HasColumnName("QuestionTypeDS");

                entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");

                entity.Property(e => e.QuestionWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ResponseNmtemplate)
                    .HasMaxLength(100)
                    .HasColumnName("ResponseNMTemplate");

                entity.Property(e => e.ReviewerAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("ReviewerAccountNM");

                entity.Property(e => e.SequenceText).HasMaxLength(255);

                entity.Property(e => e.StartDt)
                    .HasColumnType("date")
                    .HasColumnName("StartDT");

                entity.Property(e => e.StatusDs)
                    .IsRequired()
                    .HasColumnName("StatusDS");

                entity.Property(e => e.StatusNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");

                entity.Property(e => e.SupervisorAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("SupervisorAccountNM");

                entity.Property(e => e.SurveyAnswerId).HasColumnName("SurveyAnswerID");

                entity.Property(e => e.SurveyDs)
                    .IsRequired()
                    .HasColumnName("SurveyDS");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.SurveyResponseSequenceId).HasColumnName("SurveyResponseSequenceID");

                entity.Property(e => e.SurveyShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");

                entity.Property(e => e.SurveyTypeDs).HasColumnName("SurveyTypeDS");

                entity.Property(e => e.SurveyTypeId).HasColumnName("SurveyTypeID");

                entity.Property(e => e.SurveyTypeNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyTypeNM");

                entity.Property(e => e.SurveyTypeShortNm)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("SurveyTypeShortNM");

                entity.Property(e => e.UseQuestionGroupsFl).HasColumnName("UseQuestionGroupsFL");
            });

            modelBuilder.Entity<VwApplicationSurveyResponsePermission>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplicationSurveyResponsePermission");

                entity.Property(e => e.AccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EMailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastNm)
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.NextStatusId).HasColumnName("NextStatusID");

                entity.Property(e => e.PreviousStatusId).HasColumnName("PreviousStatusID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.SurveyShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");
            });

            modelBuilder.Entity<VwApplicationSurveyResponseSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwApplicationSurveyResponseSummary");

                entity.Property(e => e.AccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EMailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastNm)
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");

                entity.Property(e => e.SupervisorAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("SupervisorAccountNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.SurveyResponseScore).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.SurveyShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");
            });

            modelBuilder.Entity<VwContextQuestionAnswer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwContextQuestionAnswer");

                entity.Property(e => e.AnswerType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.QuestionAnswerNm)
                    .IsRequired()
                    .HasColumnName("QuestionAnswerNM");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionNm)
                    .IsRequired()
                    .HasColumnName("QuestionNM");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SurveyAnswerId).HasColumnName("SurveyAnswerID");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyTypeNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyTypeNM");

                entity.Property(e => e.SurveyTypeShortNm)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("SurveyTypeShortNM");
            });

            modelBuilder.Entity<VwQuestionLibrary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwQuestionLibrary");

                entity.Property(e => e.AnswerDataType).HasMaxLength(255);

                entity.Property(e => e.CommentFl).HasColumnName("CommentFL");

                entity.Property(e => e.Keywords).HasMaxLength(255);

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.QuestionDs)
                    .IsRequired()
                    .HasColumnName("QuestionDS");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionNm)
                    .IsRequired()
                    .HasColumnName("QuestionNM");

                entity.Property(e => e.QuestionShortNm)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("QuestionShortNM");

                entity.Property(e => e.QuestionTypeCd)
                    .HasMaxLength(255)
                    .HasColumnName("QuestionTypeCD");

                entity.Property(e => e.QuestionTypeDs).HasColumnName("QuestionTypeDS");

                entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");

                entity.Property(e => e.SurveyTypeId).HasColumnName("SurveyTypeID");

                entity.Property(e => e.SurveyTypeNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyTypeNM");

                entity.Property(e => e.SurveyTypeShortNm)
                    .HasMaxLength(255)
                    .HasColumnName("SurveyTypeShortNM");

                entity.Property(e => e.UnitOfMeasureDs).HasColumnName("UnitOfMeasureDS");

                entity.Property(e => e.UnitOfMeasureId).HasColumnName("UnitOfMeasureID");

                entity.Property(e => e.UnitOfMeasureNm)
                    .HasMaxLength(50)
                    .HasColumnName("UnitOfMeasureNM");
            });

            modelBuilder.Entity<VwSurveyQuestion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSurveyQuestions");

                entity.Property(e => e.ActiveFl).HasColumnName("ActiveFL");

                entity.Property(e => e.AnswerDataType)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CommentFl).HasColumnName("CommentFL");

                entity.Property(e => e.CompletionMessage).IsRequired();

                entity.Property(e => e.ControlName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.EndDt)
                    .HasColumnType("date")
                    .HasColumnName("EndDT");

                entity.Property(e => e.QuestionAnswerCommentFl).HasColumnName("QuestionAnswerCommentFL");

                entity.Property(e => e.QuestionAnswerDs)
                    .IsRequired()
                    .HasColumnName("QuestionAnswerDS");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.QuestionAnswerNm)
                    .IsRequired()
                    .HasColumnName("QuestionAnswerNM");

                entity.Property(e => e.QuestionAnswerShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionAnswerShortNM");

                entity.Property(e => e.QuestionDs)
                    .IsRequired()
                    .HasColumnName("QuestionDS");

                entity.Property(e => e.QuestionGroupDs).HasColumnName("QuestionGroupDS");

                entity.Property(e => e.QuestionGroupId).HasColumnName("QuestionGroupID");

                entity.Property(e => e.QuestionGroupNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupNM");

                entity.Property(e => e.QuestionGroupShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupShortNM");

                entity.Property(e => e.QuestionGroupWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionNm)
                    .IsRequired()
                    .HasColumnName("QuestionNM");

                entity.Property(e => e.QuestionShortNm)
                    .IsRequired()
                    .HasMaxLength(75)
                    .HasColumnName("QuestionShortNM");

                entity.Property(e => e.QuestionTypeCd)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("QuestionTypeCD");

                entity.Property(e => e.QuestionTypeDs)
                    .IsRequired()
                    .HasColumnName("QuestionTypeDS");

                entity.Property(e => e.QuestionTypeId).HasColumnName("QuestionTypeID");

                entity.Property(e => e.QuestionWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ResponseNmtemplate)
                    .HasMaxLength(100)
                    .HasColumnName("ResponseNMTemplate");

                entity.Property(e => e.ReviewerAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("ReviewerAccountNM");

                entity.Property(e => e.StartDt)
                    .HasColumnType("date")
                    .HasColumnName("StartDT");

                entity.Property(e => e.SurveyDs)
                    .IsRequired()
                    .HasColumnName("SurveyDS");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");

                entity.Property(e => e.UnitOfMeasureId).HasColumnName("UnitOfMeasureID");

                entity.Property(e => e.UseQuestionGroupsFl).HasColumnName("UseQuestionGroupsFL");
            });

            modelBuilder.Entity<VwSurveyResponseContext>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSurveyResponseContext");

                entity.Property(e => e.AccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.CommentDs).HasColumnName("CommentDS");

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EMailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastNm)
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.QuestionAnswerNm).HasColumnName("QuestionAnswerNM");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionNm).HasColumnName("QuestionNM");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SupervisorAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("SupervisorAccountNM");

                entity.Property(e => e.SurveyDs)
                    .IsRequired()
                    .HasColumnName("SurveyDS");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.SurveyShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");
            });

            modelBuilder.Entity<VwSurveyResponseDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSurveyResponseDetail");

                entity.Property(e => e.AccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.AnswerDate).HasColumnType("datetime");

                entity.Property(e => e.AnswerType).HasMaxLength(20);

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EMailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastLoginDt)
                    .HasColumnType("datetime")
                    .HasColumnName("LastLoginDT");

                entity.Property(e => e.LastLoginLocation).HasMaxLength(50);

                entity.Property(e => e.LastNm)
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.QuestionAnswerDs).HasColumnName("QuestionAnswerDS");

                entity.Property(e => e.QuestionAnswerId).HasColumnName("QuestionAnswerID");

                entity.Property(e => e.QuestionAnswerNm).HasColumnName("QuestionAnswerNM");

                entity.Property(e => e.QuestionAnswerShortNm)
                    .HasMaxLength(50)
                    .HasColumnName("QuestionAnswerShortNM");

                entity.Property(e => e.QuestionDs).HasColumnName("QuestionDS");

                entity.Property(e => e.QuestionGroupDs).HasColumnName("QuestionGroupDS");

                entity.Property(e => e.QuestionGroupId).HasColumnName("QuestionGroupID");

                entity.Property(e => e.QuestionGroupNm)
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupNM");

                entity.Property(e => e.QuestionGroupShortNm)
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupShortNM");

                entity.Property(e => e.QuestionGroupWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionNm).HasColumnName("QuestionNM");

                entity.Property(e => e.QuestionShortNm)
                    .HasMaxLength(75)
                    .HasColumnName("QuestionShortNM");

                entity.Property(e => e.QuestionWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusNm)
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseGroupScore).HasColumnType("decimal(38, 4)");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.SurveyResponseScore).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.SurveyShortNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");
            });

            modelBuilder.Entity<VwSurveyResponseGroupSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSurveyResponseGroupSummary");

                entity.Property(e => e.AccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EMailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastLoginDt)
                    .HasColumnType("datetime")
                    .HasColumnName("LastLoginDT");

                entity.Property(e => e.LastLoginLocation).HasMaxLength(50);

                entity.Property(e => e.LastNm)
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.QuestionGroupDs).HasColumnName("QuestionGroupDS");

                entity.Property(e => e.QuestionGroupId).HasColumnName("QuestionGroupID");

                entity.Property(e => e.QuestionGroupNm)
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupNM");

                entity.Property(e => e.QuestionGroupShortNm)
                    .HasMaxLength(50)
                    .HasColumnName("QuestionGroupShortNM");

                entity.Property(e => e.QuestionGroupWeight).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusNm)
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseGroupScore).HasColumnType("decimal(38, 4)");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.SurveyResponseScore).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.SurveyShortNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");
            });

            modelBuilder.Entity<VwSurveyResponseSummary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSurveyResponseSummary");

                entity.Property(e => e.AccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ApplicationCd)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationCD");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationShortNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("ApplicationShortNM");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.AssignedUserId).HasColumnName("AssignedUserID");

                entity.Property(e => e.DataSource)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.EMailAddress)
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.FirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastLoginDt)
                    .HasColumnType("datetime")
                    .HasColumnName("LastLoginDT");

                entity.Property(e => e.LastLoginLocation).HasMaxLength(50);

                entity.Property(e => e.LastNm)
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT");

                entity.Property(e => e.ModifiedId).HasColumnName("ModifiedID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.StatusNm)
                    .HasMaxLength(50)
                    .HasColumnName("StatusNM");

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");

                entity.Property(e => e.SurveyResponseScore).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.SurveyShortNm)
                    .HasMaxLength(50)
                    .HasColumnName("SurveyShortNM");
            });

            modelBuilder.Entity<VwValidateImportUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwValidateImportUser");

                entity.Property(e => e.AccountNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("AccountNM");

                entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

                entity.Property(e => e.ApplicationNm)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("ApplicationNM");

                entity.Property(e => e.ApplicationUserId).HasColumnName("ApplicationUserID");

                entity.Property(e => e.ApplicationUserRoleId).HasColumnName("ApplicationUserRoleID");

                entity.Property(e => e.CommentDs).HasColumnName("CommentDS");

                entity.Property(e => e.DataSource).HasMaxLength(250);

                entity.Property(e => e.EMailAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("eMailAddress");

                entity.Property(e => e.EmployeeApplicationUserId).HasColumnName("EmployeeApplicationUserID");

                entity.Property(e => e.FirstNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("FirstNM");

                entity.Property(e => e.LastNm)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("LastNM");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RowAction)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.SupervisorAccountNm)
                    .HasMaxLength(50)
                    .HasColumnName("SupervisorAccountNM");

                entity.Property(e => e.SupervisorApplicationUserId).HasColumnName("SupervisorApplicationUserID");

                entity.Property(e => e.SupervisorFirstNm)
                    .HasMaxLength(100)
                    .HasColumnName("SupervisorFirstNM");

                entity.Property(e => e.SupervisorLastNm)
                    .HasMaxLength(100)
                    .HasColumnName("SupervisorLastNM");

                entity.Property(e => e.SupervisoreMailAddress).HasMaxLength(100);

                entity.Property(e => e.SurveyId).HasColumnName("SurveyID");

                entity.Property(e => e.SurveyNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SurveyNM");

                entity.Property(e => e.SurveyResponseId).HasColumnName("SurveyResponseID");

                entity.Property(e => e.SurveyResponseNm)
                    .HasMaxLength(250)
                    .HasColumnName("SurveyResponseNM");
            });

            modelBuilder.Entity<WebPortal>(entity =>
            {
                entity.ToTable("WebPortal");

                entity.Property(e => e.WebPortalId).HasColumnName("WebPortalID");

                entity.Property(e => e.ActiveFl)
                    .IsRequired()
                    .HasColumnName("ActiveFL")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ModifiedDT")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedId)
                    .HasColumnName("ModifiedID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.WebPortalDs).HasColumnName("WebPortalDS");

                entity.Property(e => e.WebPortalNm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("WebPortalNM");

                entity.Property(e => e.WebPortalUrl)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("WebPortalURL");

                entity.Property(e => e.WebServiceUrl)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("WebServiceURL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

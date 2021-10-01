using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace IS_Proj_HIT.Models.Data
{
    public partial class WCTCHealthSystemContext : DbContext
    {
        public WCTCHealthSystemContext()
        {
        }

        public WCTCHealthSystemContext(DbContextOptions<WCTCHealthSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AdminMethod> AdminMethods { get; set; }
        public virtual DbSet<AdministrationStatus> AdministrationStatuses { get; set; }
        public virtual DbSet<AdmitType> AdmitTypes { get; set; }
        public virtual DbSet<AdvancedDirective> AdvancedDirectives { get; set; }
        public virtual DbSet<AlertType> AlertTypes { get; set; }
        public virtual DbSet<Allergen> Allergens { get; set; }
        public virtual DbSet<AnestheticType> AnestheticTypes { get; set; }
        public virtual DbSet<ApprovedMedicinesToAdminister> ApprovedMedicinesToAdministers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<BloodPressureRouteType> BloodPressureRouteTypes { get; set; }
        public virtual DbSet<Bmimethod> Bmimethods { get; set; }
        public virtual DbSet<BodyPart> BodyParts { get; set; }
        public virtual DbSet<BodySystemType> BodySystemTypes { get; set; }
        public virtual DbSet<CareSystemAssessment> CareSystemAssessments { get; set; }
        public virtual DbSet<CareSystemParameter> CareSystemParameters { get; set; }
        public virtual DbSet<CareSystemType> CareSystemTypes { get; set; }
        public virtual DbSet<ClinicalReminder> ClinicalReminders { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Discharge> Discharges { get; set; }
        public virtual DbSet<Dosage> Dosages { get; set; }
        public virtual DbSet<Emar> Emars { get; set; }
        public virtual DbSet<Employment> Employments { get; set; }
        public virtual DbSet<Encounter> Encounters { get; set; }
        public virtual DbSet<EncounterAlert> EncounterAlerts { get; set; }
        public virtual DbSet<EncounterHistory> EncounterHistories { get; set; }
        public virtual DbSet<EncounterPhysician> EncounterPhysicians { get; set; }
        public virtual DbSet<EncounterType> EncounterTypes { get; set; }
        public virtual DbSet<Ethnicity> Ethnicities { get; set; }
        public virtual DbSet<ExamBodySystem> ExamBodySystems { get; set; }
        public virtual DbSet<ExamType> ExamTypes { get; set; }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<FallRisk> FallRisks { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<GenericMedicine> GenericMedicines { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Insurance> Insurances { get; set; }
        public virtual DbSet<IntakeFrequency> IntakeFrequencies { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public virtual DbSet<MedStateType> MedStateTypes { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<MedicineAdministrationInfo> MedicineAdministrationInfos { get; set; }
        public virtual DbSet<MedicineDosage> MedicineDosages { get; set; }
        public virtual DbSet<MedicineIngredient> MedicineIngredients { get; set; }
        public virtual DbSet<MedicineSideEffect> MedicineSideEffects { get; set; }
        public virtual DbSet<MedicineUse> MedicineUses { get; set; }
        public virtual DbSet<MedicineWarning> MedicineWarnings { get; set; }
        public virtual DbSet<NoteType> NoteTypes { get; set; }
        public virtual DbSet<O2deliveryType> O2deliveryTypes { get; set; }
        public virtual DbSet<OrderInfo> OrderInfos { get; set; }
        public virtual DbSet<OrderType> OrderTypes { get; set; }
        public virtual DbSet<PainParameter> PainParameters { get; set; }
        public virtual DbSet<PainRating> PainRatings { get; set; }
        public virtual DbSet<PainRatingImage> PainRatingImages { get; set; }
        public virtual DbSet<PainScaleType> PainScaleTypes { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientAdvancedDirective> PatientAdvancedDirectives { get; set; }
        public virtual DbSet<PatientAlert> PatientAlerts { get; set; }
        public virtual DbSet<PatientAllergy> PatientAllergies { get; set; }
        public virtual DbSet<PatientClinicalReminder> PatientClinicalReminders { get; set; }
        public virtual DbSet<PatientContactDetail> PatientContactDetails { get; set; }
        public virtual DbSet<PatientEmergencyContact> PatientEmergencyContacts { get; set; }
        public virtual DbSet<PatientFallRisk> PatientFallRisks { get; set; }
        public virtual DbSet<PatientGeneral> PatientGenerals { get; set; }
        public virtual DbSet<PatientLanguage> PatientLanguages { get; set; }
        public virtual DbSet<PatientMilitaryService> PatientMilitaryServices { get; set; }
        public virtual DbSet<PatientRace> PatientRaces { get; set; }
        public virtual DbSet<PatientRestriction> PatientRestrictions { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PaymentPlan> PaymentPlans { get; set; }
        public virtual DbSet<PaymentSource> PaymentSources { get; set; }
        public virtual DbSet<Pcacomment> Pcacomments { get; set; }
        public virtual DbSet<PcacommentType> PcacommentTypes { get; set; }
        public virtual DbSet<PcapainAssessment> PcapainAssessments { get; set; }
        public virtual DbSet<Pcarecord> Pcarecords { get; set; }
        public virtual DbSet<Physician> Physicians { get; set; }
        public virtual DbSet<PhysicianAssessment> PhysicianAssessments { get; set; }
        public virtual DbSet<PhysicianAssessmentDetail> PhysicianAssessmentDetails { get; set; }
        public virtual DbSet<PhysicianAssessmentType> PhysicianAssessmentTypes { get; set; }
        public virtual DbSet<PhysicianRole> PhysicianRoles { get; set; }
        public virtual DbSet<PlaceOfServiceOutPatient> PlaceOfServiceOutPatients { get; set; }
        public virtual DbSet<PointOfOrigin> PointOfOrigins { get; set; }
        public virtual DbSet<PreferredContactTime> PreferredContactTimes { get; set; }
        public virtual DbSet<PreferredModeOfContact> PreferredModeOfContacts { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<ProcedureReport> ProcedureReports { get; set; }
        public virtual DbSet<ProcedureReportAnestheticType> ProcedureReportAnestheticTypes { get; set; }
        public virtual DbSet<ProcedureReportPhysician> ProcedureReportPhysicians { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramFacility> ProgramFacilities { get; set; }
        public virtual DbSet<ProgressNote> ProgressNotes { get; set; }
        public virtual DbSet<ProviderType> ProviderTypes { get; set; }
        public virtual DbSet<PulseRouteType> PulseRouteTypes { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<Restriction> Restrictions { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public virtual DbSet<Sex> Sexes { get; set; }
        public virtual DbSet<SideEffect> SideEffects { get; set; }
        public virtual DbSet<Specialty> Specialties { get; set; }
        public virtual DbSet<TempRouteType> TempRouteTypes { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestCategory> TestCategories { get; set; }
        public virtual DbSet<TestedBodyPart> TestedBodyParts { get; set; }
        public virtual DbSet<Use> Uses { get; set; }
        public virtual DbSet<UserFacility> UserFacilities { get; set; }
        public virtual DbSet<UserProgram> UserPrograms { get; set; }
        public virtual DbSet<UserQuestion> UserQuestions { get; set; }
        public virtual DbSet<UserTable> UserTables { get; set; }
        public virtual DbSet<Warning> Warnings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=bitsql.wctc.edu;Database=WCTCHealthSystem;User ID=HealthSystemApp;Password=WCTC_H3alth");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Address_CountryID");
            });

            modelBuilder.Entity<AdminMethod>(entity =>
            {
                entity.ToTable("AdminMethod");

                entity.Property(e => e.AdminMethodId).HasColumnName("AdminMethodID");

                entity.Property(e => e.AdminMethodName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Instructions)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AdministrationStatus>(entity =>
            {
                entity.ToTable("AdministrationStatus");

                entity.Property(e => e.AdministrationStatusId).HasColumnName("AdministrationStatusID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AdmitType>(entity =>
            {
                entity.ToTable("AdmitType");

                entity.Property(e => e.AdmitTypeId).HasColumnName("AdmitTypeID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Explaination).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WiPopCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<AdvancedDirective>(entity =>
            {
                entity.Property(e => e.AdvancedDirectiveId).HasColumnName("AdvancedDirectiveID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AlertType>(entity =>
            {
                entity.HasKey(e => e.AlertId);

                entity.ToTable("AlertType");

                entity.Property(e => e.AlertId).HasColumnName("AlertID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Allergen>(entity =>
            {
                entity.ToTable("Allergen");

                entity.Property(e => e.AllergenId).HasColumnName("AllergenID");

                entity.Property(e => e.AllergenName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<AnestheticType>(entity =>
            {
                entity.ToTable("AnestheticType");

                entity.Property(e => e.AnestheticTypeId).HasColumnName("AnestheticTypeID");

                entity.Property(e => e.AnestheticType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AnestheticType");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ApprovedMedicinesToAdminister>(entity =>
            {
                entity.HasKey(e => e.ApprovedMedicineId)
                    .HasName("PK__Approved__B74A445564E0A68F");

                entity.ToTable("ApprovedMedicinesToAdminister");

                entity.HasIndex(e => new { e.EncounterId, e.MedicineAdminId, e.OrderInfoId, e.IntakeFrequencyId }, "FK");

                entity.Property(e => e.ApprovedMedicineId).HasColumnName("ApprovedMedicineID");

                entity.Property(e => e.EncounterId).HasColumnName("EncounterID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IntakeFrequencyId).HasColumnName("IntakeFrequencyID");

                entity.Property(e => e.MedicineAdminId).HasColumnName("MedicineAdminID");

                entity.Property(e => e.OrderInfoId).HasColumnName("OrderInfoID");

                entity.Property(e => e.SpecialInstructions)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Encounter)
                    .WithMany(p => p.ApprovedMedicinesToAdministers)
                    .HasForeignKey(d => d.EncounterId)
                    .HasConstraintName("FK__ApprovedM__Encou__7B7B4DDC");

                entity.HasOne(d => d.IntakeFrequency)
                    .WithMany(p => p.ApprovedMedicinesToAdministers)
                    .HasForeignKey(d => d.IntakeFrequencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ApprovedM__Intak__7E57BA87");

                entity.HasOne(d => d.MedicineAdmin)
                    .WithMany(p => p.ApprovedMedicinesToAdministers)
                    .HasForeignKey(d => d.MedicineAdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ApprovedM__Medic__7C6F7215");

                entity.HasOne(d => d.OrderInfo)
                    .WithMany(p => p.ApprovedMedicinesToAdministers)
                    .HasForeignKey(d => d.OrderInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ApprovedM__Order__7D63964E");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<BloodPressureRouteType>(entity =>
            {
                entity.ToTable("BloodPressureRouteType");

                entity.Property(e => e.BloodPressureRouteTypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BloodPressureRouteTypeID");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bmimethod>(entity =>
            {
                entity.ToTable("BMIMethod");

                entity.Property(e => e.BmimethodId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BMIMethodID");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BodyPart>(entity =>
            {
                entity.HasKey(e => e.PartId)
                    .HasName("PK__BodyPart__7C3F0D3057EE8F29");

                entity.ToTable("BodyPart");

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BodySystemType>(entity =>
            {
                entity.HasKey(e => e.CareSystemTypeId)
                    .HasName("pk_BodySystemType");

                entity.ToTable("BodySystemType");

                entity.Property(e => e.CareSystemTypeId).HasColumnName("CareSystemTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NormalLimitsDescription).IsUnicode(false);
            });

            modelBuilder.Entity<CareSystemAssessment>(entity =>
            {
                entity.HasKey(e => new { e.CareSystemAssessmentId, e.Pcaid });

                entity.ToTable("CareSystemAssessment");

                entity.Property(e => e.CareSystemAssessmentId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CareSystemAssessmentID");

                entity.Property(e => e.Pcaid).HasColumnName("PCAID");

                entity.Property(e => e.CareSystemParameterId).HasColumnName("CareSystemParameterID");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.IsWithinNormalLimits)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CareSystemParameter)
                    .WithMany(p => p.CareSystemAssessments)
                    .HasForeignKey(d => d.CareSystemParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CareSystemAssessment_CareSystemParameterID");

                entity.HasOne(d => d.Pca)
                    .WithMany(p => p.CareSystemAssessments)
                    .HasForeignKey(d => d.Pcaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CareSystemAssessment_PCAID");
            });

            modelBuilder.Entity<CareSystemParameter>(entity =>
            {
                entity.ToTable("CareSystemParameter");

                entity.Property(e => e.CareSystemParameterId).HasColumnName("CareSystemParameterID");

                entity.Property(e => e.CareSystemTypeId).HasColumnName("CareSystemTypeID");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NormalLimitsDescription).IsUnicode(false);

                entity.HasOne(d => d.CareSystemType)
                    .WithMany(p => p.CareSystemParameters)
                    .HasForeignKey(d => d.CareSystemTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CareSystemParameter_CareSystemTypeID");
            });

            modelBuilder.Entity<CareSystemType>(entity =>
            {
                entity.ToTable("CareSystemType");

                entity.Property(e => e.CareSystemTypeId).HasColumnName("CareSystemTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClinicalReminder>(entity =>
            {
                entity.Property(e => e.ClinicalReminderId).HasColumnName("ClinicalReminderID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Discharge>(entity =>
            {
                entity.ToTable("Discharge");

                entity.Property(e => e.DischargeId).HasColumnName("DischargeID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.WiPopCode)
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dosage>(entity =>
            {
                entity.ToTable("Dosage");

                entity.Property(e => e.DosageId).HasColumnName("DosageID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UnitOfMeasurement)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Emar>(entity =>
            {
                entity.HasKey(e => e.MedicationAdministrationId)
                    .HasName("PK__EMAR__417866C5DFA79EB5");

                entity.ToTable("EMAR");

                entity.HasIndex(e => new { e.AdministrationStatusId, e.AssignedAdministrator, e.AdministeredBy }, "FK");

                entity.Property(e => e.MedicationAdministrationId).HasColumnName("MedicationAdministrationID");

                entity.Property(e => e.AdministeredAttempt).HasColumnType("datetime");

                entity.Property(e => e.AdministrationCost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AdministrationStatusId).HasColumnName("AdministrationStatusID");

                entity.Property(e => e.ApprovedMedicineId).HasColumnName("ApprovedMedicineID");

                entity.HasOne(d => d.AdministeredByNavigation)
                    .WithMany(p => p.EmarAdministeredByNavigations)
                    .HasForeignKey(d => d.AdministeredBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMAR__Administer__0ABD916C");

                entity.HasOne(d => d.AdministrationStatus)
                    .WithMany(p => p.Emars)
                    .HasForeignKey(d => d.AdministrationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMAR__Administra__08D548FA");

                entity.HasOne(d => d.AssignedAdministratorNavigation)
                    .WithMany(p => p.EmarAssignedAdministratorNavigations)
                    .HasForeignKey(d => d.AssignedAdministrator)
                    .HasConstraintName("FK__EMAR__AssignedAd__09C96D33");
            });

            modelBuilder.Entity<Employment>(entity =>
            {
                entity.ToTable("Employment");

                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Occupation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Encounter>(entity =>
            {
                entity.ToTable("Encounter");

                entity.Property(e => e.EncounterId).HasColumnName("EncounterID");

                entity.Property(e => e.AdmitDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AdmitTypeId).HasColumnName("AdmitTypeID");

                entity.Property(e => e.AdmittingDiagnosis).IsUnicode(false);

                entity.Property(e => e.ChiefComplaint)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.DischargeComment).IsUnicode(false);

                entity.Property(e => e.DischargeDateTime).HasColumnType("datetime");

                entity.Property(e => e.DischargeDietInstructions).IsUnicode(false);

                entity.Property(e => e.DischargeDispositionNote).IsUnicode(false);

                entity.Property(e => e.EncounterPhysiciansId).HasColumnName("EncounterPhysiciansID");

                entity.Property(e => e.EncounterRestrictionId).HasColumnName("EncounterRestrictionID");

                entity.Property(e => e.EncounterTypeId).HasColumnName("EncounterTypeID");

                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.HistoryOfPresentIllness).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MedicationsOnDischarge).IsUnicode(false);

                entity.Property(e => e.Mrn)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.PhysicianId).HasColumnName("PhysicianID");

                entity.Property(e => e.PlaceOfServiceId).HasColumnName("PlaceOfServiceID");

                entity.Property(e => e.PointOfOriginId).HasColumnName("PointOfOriginID");

                entity.Property(e => e.RoomNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SignificantFindings).IsUnicode(false);

                entity.Property(e => e.WrittenDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AdmitType)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.AdmitTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Encounter_AdmitTypeID");

                entity.HasOne(d => d.AuthoringProviderNavigation)
                    .WithMany(p => p.EncounterAuthoringProviderNavigations)
                    .HasForeignKey(d => d.AuthoringProvider)
                    .HasConstraintName("fk_EncounterAuthoringProvider");

                entity.HasOne(d => d.CoSignatureNavigation)
                    .WithMany(p => p.EncounterCoSignatureNavigations)
                    .HasForeignKey(d => d.CoSignature)
                    .HasConstraintName("fk_EncounterCoSignature");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("fk_Encounter_DepartmentID");

                entity.HasOne(d => d.DischargeDispositionNavigation)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.DischargeDisposition)
                    .HasConstraintName("fk_Encounter_DischargeDisposition");

                entity.HasOne(d => d.EncounterPhysicians)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.EncounterPhysiciansId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Encounter_EncounterPhysiciansID");

                entity.HasOne(d => d.EncounterType)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.EncounterTypeId)
                    .HasConstraintName("fk_Encounter_EncounterTypeID");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Encounter_FacilityID");

                entity.HasOne(d => d.MrnNavigation)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.Mrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Encounter_MRN");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("fk_Encounter_PaymentID");

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.EncounterPhysicians)
                    .HasForeignKey(d => d.PhysicianId)
                    .HasConstraintName("fk_EncounterPhysicianID");

                entity.HasOne(d => d.PlaceOfService)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.PlaceOfServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Encounter_PlaceOfServiceID");

                entity.HasOne(d => d.PointOfOrigin)
                    .WithMany(p => p.Encounters)
                    .HasForeignKey(d => d.PointOfOriginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Encounter_PointOfOriginID");
            });

            modelBuilder.Entity<EncounterAlert>(entity =>
            {
                entity.HasKey(e => new { e.EncounterId, e.PatientAlertId });

                entity.Property(e => e.EncounterId).HasColumnName("EncounterID");

                entity.Property(e => e.PatientAlertId).HasColumnName("PatientAlertID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Encounter)
                    .WithMany(p => p.EncounterAlerts)
                    .HasForeignKey(d => d.EncounterId)
                    .HasConstraintName("fk_EncounterAlerts_EncounterID");

                entity.HasOne(d => d.PatientAlert)
                    .WithMany(p => p.EncounterAlerts)
                    .HasForeignKey(d => d.PatientAlertId)
                    .HasConstraintName("fk_EncounterAlerts_PatientAlertID");
            });

            modelBuilder.Entity<EncounterHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Encounter_History");

                entity.Property(e => e.AdmitDate).HasColumnName("Admit Date");

                entity.Property(e => e.ChiefComplaint)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("Chief Complaint");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Department Name");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.DischargeDate)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Discharge Date");

                entity.Property(e => e.EncounterId).HasColumnName("Encounter ID");

                entity.Property(e => e.Explaination).IsUnicode(false);

                entity.Property(e => e.FacilityName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Facility Name");

                entity.Property(e => e.Mrn)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<EncounterPhysician>(entity =>
            {
                entity.HasKey(e => e.EncounterPhysiciansId);

                entity.Property(e => e.EncounterPhysiciansId).HasColumnName("EncounterPhysiciansID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhysicianId).HasColumnName("PhysicianID");

                entity.Property(e => e.PhysicianRoleId).HasColumnName("PhysicianRoleID");

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.EncounterPhysiciansNavigation)
                    .HasForeignKey(d => d.PhysicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_EncounterPhysicians_PhysicianID");

                entity.HasOne(d => d.PhysicianRole)
                    .WithMany(p => p.EncounterPhysicians)
                    .HasForeignKey(d => d.PhysicianRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_EncounterPhysicians_PhysicianRoleID");
            });

            modelBuilder.Entity<EncounterType>(entity =>
            {
                entity.ToTable("EncounterType");

                entity.Property(e => e.EncounterTypeId).HasColumnName("EncounterTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ethnicity>(entity =>
            {
                entity.ToTable("Ethnicity");

                entity.Property(e => e.EthnicityId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EthnicityID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExamBodySystem>(entity =>
            {
                entity.HasKey(e => new { e.ExamTypeCode, e.CareSystemTypeId })
                    .HasName("pk_ExamBodySystems");

                entity.Property(e => e.CareSystemTypeId).HasColumnName("CareSystemTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CareSystemType)
                    .WithMany(p => p.ExamBodySystems)
                    .HasForeignKey(d => d.CareSystemTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ECareSystemType");

                entity.HasOne(d => d.ExamTypeCodeNavigation)
                    .WithMany(p => p.ExamBodySystems)
                    .HasForeignKey(d => d.ExamTypeCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_EFKExamType");
            });

            modelBuilder.Entity<ExamType>(entity =>
            {
                entity.HasKey(e => e.ExamTypeCode)
                    .HasName("pk_ExamType");

                entity.ToTable("ExamType");

                entity.Property(e => e.ExamType1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ExamType");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.ToTable("Facility");

                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Facilities)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Facility_AddressID");
            });

            modelBuilder.Entity<FallRisk>(entity =>
            {
                entity.Property(e => e.FallRiskId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FallRiskID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.GenderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GenderID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GenericMedicine>(entity =>
            {
                entity.HasKey(e => e.GenericMedId)
                    .HasName("PK__GenericM__5DCAA08F9BB6FD86");

                entity.ToTable("GenericMedicine");

                entity.Property(e => e.GenericMedId).HasColumnName("GenericMedID");

                entity.Property(e => e.GenericName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.ToTable("Ingredient");

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Insurance>(entity =>
            {
                entity.ToTable("Insurance");

                entity.Property(e => e.InsuranceId).HasColumnName("InsuranceID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Fax)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InsuranceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentPlanId).HasColumnName("PaymentPlanID");

                entity.Property(e => e.PaymentSourceId).HasColumnName("PaymentSourceID");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Subscriber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Insurances)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Insurance_AddressID");

                entity.HasOne(d => d.PaymentPlan)
                    .WithMany(p => p.Insurances)
                    .HasForeignKey(d => d.PaymentPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Insurance_PaymentPlanID");

                entity.HasOne(d => d.PaymentSource)
                    .WithMany(p => p.Insurances)
                    .HasForeignKey(d => d.PaymentSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Insurance_PaymentSourceID");
            });

            modelBuilder.Entity<IntakeFrequency>(entity =>
            {
                entity.ToTable("IntakeFrequency");

                entity.Property(e => e.IntakeFrequencyId).HasColumnName("IntakeFrequencyID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaritalStatus>(entity =>
            {
                entity.ToTable("MaritalStatus");

                entity.Property(e => e.MaritalStatusId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("MaritalStatusID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MedStateType>(entity =>
            {
                entity.ToTable("MedStateType");

                entity.Property(e => e.MedStateTypeId).HasColumnName("MedStateTypeID");

                entity.Property(e => e.MedState)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.HasIndex(e => e.GenericMedId, "FK");

                entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.GenericMedId).HasColumnName("GenericMedID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.GenericMed)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.GenericMedId)
                    .HasConstraintName("FK__Medicine__Generi__5B0E7E4A");
            });

            modelBuilder.Entity<MedicineAdministrationInfo>(entity =>
            {
                entity.HasKey(e => e.MedicineAdminId)
                    .HasName("PK__Medicine__32FA22F8237E1828");

                entity.ToTable("MedicineAdministrationInfo");

                entity.HasIndex(e => new { e.MedStateTypeId, e.MedicineId, e.AdminMethodId }, "FK");

                entity.Property(e => e.MedicineAdminId).HasColumnName("MedicineAdminID");

                entity.Property(e => e.AdminMethodId).HasColumnName("AdminMethodID");

                entity.Property(e => e.MedStateTypeId).HasColumnName("medStateTypeID");

                entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

                entity.HasOne(d => d.AdminMethod)
                    .WithMany(p => p.MedicineAdministrationInfos)
                    .HasForeignKey(d => d.AdminMethodId)
                    .HasConstraintName("FK__MedicineA__Admin__5FD33367");

                entity.HasOne(d => d.MedStateType)
                    .WithMany(p => p.MedicineAdministrationInfos)
                    .HasForeignKey(d => d.MedStateTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineA__medSt__5DEAEAF5");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineAdministrationInfos)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineA__Medic__5EDF0F2E");
            });

            modelBuilder.Entity<MedicineDosage>(entity =>
            {
                entity.HasKey(e => new { e.MedicineAdminId, e.DosageId })
                    .HasName("PK__Medicine__0D2D9C521BFA3D34");

                entity.HasIndex(e => new { e.MedicineAdminId, e.DosageId }, "PK/FK");

                entity.Property(e => e.MedicineAdminId).HasColumnName("MedicineAdminID");

                entity.Property(e => e.DosageId).HasColumnName("DosageID");

                entity.HasOne(d => d.Dosage)
                    .WithMany(p => p.MedicineDosages)
                    .HasForeignKey(d => d.DosageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineD__Dosag__74CE504D");

                entity.HasOne(d => d.MedicineAdmin)
                    .WithMany(p => p.MedicineDosages)
                    .HasForeignKey(d => d.MedicineAdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineD__Medic__73DA2C14");
            });

            modelBuilder.Entity<MedicineIngredient>(entity =>
            {
                entity.HasIndex(e => new { e.MedicineId, e.IngredientId }, "FK");

                entity.Property(e => e.MedicineIngredientId).HasColumnName("MedicineIngredientID");

                entity.Property(e => e.IngredientId).HasColumnName("IngredientID");

                entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.MedicineIngredients)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineI__Ingre__789EE131");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineIngredients)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineI__Medic__77AABCF8");
            });

            modelBuilder.Entity<MedicineSideEffect>(entity =>
            {
                entity.HasKey(e => new { e.MedicineId, e.SideEffectId })
                    .HasName("PK__Medicine__BCF26602D8C03FC2");

                entity.HasIndex(e => new { e.MedicineId, e.SideEffectId }, "PK/FK");

                entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

                entity.Property(e => e.SideEffectId).HasColumnName("SideEffectID");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineSideEffects)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineS__Medic__695C9DA1");

                entity.HasOne(d => d.SideEffect)
                    .WithMany(p => p.MedicineSideEffects)
                    .HasForeignKey(d => d.SideEffectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineS__SideE__6A50C1DA");
            });

            modelBuilder.Entity<MedicineUse>(entity =>
            {
                entity.HasKey(e => new { e.MedicineId, e.UsesId })
                    .HasName("PK__Medicine__0E45A455DD595D90");

                entity.HasIndex(e => new { e.MedicineId, e.UsesId }, "PK/FK");

                entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

                entity.Property(e => e.UsesId).HasColumnName("UsesID");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineUses)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineU__Medic__01342732");

                entity.HasOne(d => d.Uses)
                    .WithMany(p => p.MedicineUses)
                    .HasForeignKey(d => d.UsesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineU__UsesI__02284B6B");
            });

            modelBuilder.Entity<MedicineWarning>(entity =>
            {
                entity.HasKey(e => new { e.MedicineId, e.WarningId })
                    .HasName("PK__Medicine__DD357FEB2251A5E3");

                entity.HasIndex(e => new { e.MedicineId, e.WarningId }, "PK/FK");

                entity.Property(e => e.MedicineId).HasColumnName("MedicineID");

                entity.Property(e => e.WarningId).HasColumnName("WarningID");

                entity.HasOne(d => d.Medicine)
                    .WithMany(p => p.MedicineWarnings)
                    .HasForeignKey(d => d.MedicineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineW__Medic__0504B816");

                entity.HasOne(d => d.Warning)
                    .WithMany(p => p.MedicineWarnings)
                    .HasForeignKey(d => d.WarningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MedicineW__Warni__05F8DC4F");
            });

            modelBuilder.Entity<NoteType>(entity =>
            {
                entity.ToTable("NoteType");

                entity.Property(e => e.NoteTypeId).HasColumnName("NoteTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NoteType1)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NoteType");
            });

            modelBuilder.Entity<O2deliveryType>(entity =>
            {
                entity.ToTable("O2DeliveryType");

                entity.Property(e => e.O2deliveryTypeId).HasColumnName("O2DeliveryTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.O2deliveryTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("O2DeliveryTypeName");
            });

            modelBuilder.Entity<OrderInfo>(entity =>
            {
                entity.ToTable("OrderInfo");

                entity.HasIndex(e => new { e.EncounterId, e.OrderTypeId, e.OrderingAuthor, e.PriorityId, e.CoAuthorId }, "FK");

                entity.Property(e => e.OrderInfoId).HasColumnName("OrderInfoID");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.CoAuthorId).HasColumnName("CoAuthorID");

                entity.Property(e => e.EncounterId).HasColumnName("EncounterID");

                entity.Property(e => e.Notes)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderTypeId).HasColumnName("OrderTypeID");

                entity.Property(e => e.PriorityId).HasColumnName("PriorityID");

                entity.HasOne(d => d.CoAuthor)
                    .WithMany(p => p.OrderInfoCoAuthors)
                    .HasForeignKey(d => d.CoAuthorId)
                    .HasConstraintName("FK__OrderInfo__CoAut__668030F6");

                entity.HasOne(d => d.Encounter)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.EncounterId)
                    .HasConstraintName("FK__OrderInfo__Encou__62AFA012");

                entity.HasOne(d => d.OrderType)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.OrderTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderInfo__Order__63A3C44B");

                entity.HasOne(d => d.OrderingAuthorNavigation)
                    .WithMany(p => p.OrderInfoOrderingAuthorNavigations)
                    .HasForeignKey(d => d.OrderingAuthor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderInfo__Order__6497E884");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.PriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderInfo__Prior__658C0CBD");
            });

            modelBuilder.Entity<OrderType>(entity =>
            {
                entity.ToTable("OrderType");

                entity.Property(e => e.OrderTypeId).HasColumnName("OrderTypeID");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.OrderName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PainParameter>(entity =>
            {
                entity.ToTable("PainParameter");

                entity.Property(e => e.PainParameterId).HasColumnName("PainParameterID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.PainScaleTypeId).HasColumnName("PainScaleTypeID");

                entity.Property(e => e.ParameterName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.PainScaleType)
                    .WithMany(p => p.PainParameters)
                    .HasForeignKey(d => d.PainScaleTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PainParameter_PainScaleTypeID");
            });

            modelBuilder.Entity<PainRating>(entity =>
            {
                entity.ToTable("PainRating");

                entity.Property(e => e.PainRatingId).HasColumnName("PainRatingID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.PainParameterId).HasColumnName("PainParameterID");

                entity.HasOne(d => d.PainParameter)
                    .WithMany(p => p.PainRatings)
                    .HasForeignKey(d => d.PainParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PainRating_PainParameterID");
            });

            modelBuilder.Entity<PainRatingImage>(entity =>
            {
                entity.HasKey(e => e.PainRatingId)
                    .HasName("pk_PainRatingImage");

                entity.ToTable("PainRatingImage");

                entity.Property(e => e.PainRatingId)
                    .ValueGeneratedNever()
                    .HasColumnName("PainRatingID");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.HasOne(d => d.PainRating)
                    .WithOne(p => p.PainRatingImage)
                    .HasForeignKey<PainRatingImage>(d => d.PainRatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PainRatingImage_PainRatingID");
            });

            modelBuilder.Entity<PainScaleType>(entity =>
            {
                entity.ToTable("PainScaleType");

                entity.Property(e => e.PainScaleTypeId).HasColumnName("PainScaleTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PainScaleTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UseDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Mrn);

                entity.ToTable("Patient");

                entity.Property(e => e.Mrn)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.Property(e => e.AliasFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AliasLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AliasMiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");

                entity.Property(e => e.EthnicityId).HasColumnName("EthnicityID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaidenName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatusID");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MothersMaidenName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReligionId).HasColumnName("ReligionID");

                entity.Property(e => e.SexId).HasColumnName("SexID");

                entity.Property(e => e.Ssn)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("SSN")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Employment)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.EmploymentId)
                    .HasConstraintName("fk_Patient_EmploymentID");

                entity.HasOne(d => d.Ethnicity)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.EthnicityId)
                    .HasConstraintName("fk_Patient_EthnicityID");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.GenderId)
                    .HasConstraintName("fk_Patient_GenderID");

                entity.HasOne(d => d.MaritalStatus)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.MaritalStatusId)
                    .HasConstraintName("fk_Patient_MaritalStatusID");

                entity.HasOne(d => d.Religion)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.ReligionId)
                    .HasConstraintName("fk_Patient_ReligionID");

                entity.HasOne(d => d.Sex)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.SexId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Patient_SexID");
            });

            modelBuilder.Entity<PatientAdvancedDirective>(entity =>
            {
                entity.Property(e => e.PatientAdvancedDirectiveId).HasColumnName("PatientAdvancedDirectiveID");

                entity.Property(e => e.AdvancedDirectiveId).HasColumnName("AdvancedDirectiveID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PatientAlertId).HasColumnName("PatientAlertID");

                entity.HasOne(d => d.AdvancedDirective)
                    .WithMany(p => p.PatientAdvancedDirectives)
                    .HasForeignKey(d => d.AdvancedDirectiveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientAdvancedDirectives_AdvancedDirectiveID");

                entity.HasOne(d => d.PatientAlert)
                    .WithMany(p => p.PatientAdvancedDirectives)
                    .HasForeignKey(d => d.PatientAlertId)
                    .HasConstraintName("fk_PatientAdvancedDirectives_PatientAlertID");
            });

            modelBuilder.Entity<PatientAlert>(entity =>
            {
                entity.Property(e => e.PatientAlertId).HasColumnName("PatientAlertID");

                entity.Property(e => e.AlertTypeId).HasColumnName("AlertTypeID");

                entity.Property(e => e.Comments).IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mrn)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AlertType)
                    .WithMany(p => p.PatientAlerts)
                    .HasForeignKey(d => d.AlertTypeId)
                    .HasConstraintName("fk_PatientAlerts_AlertTypeID");

                entity.HasOne(d => d.MrnNavigation)
                    .WithMany(p => p.PatientAlerts)
                    .HasForeignKey(d => d.Mrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientAlerts_MRN");
            });

            modelBuilder.Entity<PatientAllergy>(entity =>
            {
                entity.ToTable("PatientAllergy");

                entity.Property(e => e.PatientAllergyId).HasColumnName("PatientAllergyID");

                entity.Property(e => e.AllergenId).HasColumnName("AllergenID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PatientAlertId).HasColumnName("PatientAlertID");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.HasOne(d => d.Allergen)
                    .WithMany(p => p.PatientAllergies)
                    .HasForeignKey(d => d.AllergenId)
                    .HasConstraintName("fk_PatientAllergy_AllergenID");

                entity.HasOne(d => d.PatientAlert)
                    .WithMany(p => p.PatientAllergies)
                    .HasForeignKey(d => d.PatientAlertId)
                    .HasConstraintName("fk_PatientAllergy_PatientAlertID");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.PatientAllergies)
                    .HasForeignKey(d => d.ReactionId)
                    .HasConstraintName("fk_PatientAllergy_ReactionID");
            });

            modelBuilder.Entity<PatientClinicalReminder>(entity =>
            {
                entity.Property(e => e.PatientClinicalReminderId).HasColumnName("PatientClinicalReminderID");

                entity.Property(e => e.ClinicalReminderId).HasColumnName("ClinicalReminderID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PatientAlertId).HasColumnName("PatientAlertID");

                entity.HasOne(d => d.ClinicalReminder)
                    .WithMany(p => p.PatientClinicalReminders)
                    .HasForeignKey(d => d.ClinicalReminderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientClinicalReminders_ClinicalReminderID");

                entity.HasOne(d => d.PatientAlert)
                    .WithMany(p => p.PatientClinicalReminders)
                    .HasForeignKey(d => d.PatientAlertId)
                    .HasConstraintName("fk_PatientClinicalReminders_PatientAlertID");
            });

            modelBuilder.Entity<PatientContactDetail>(entity =>
            {
                entity.HasKey(e => e.PatientContactId);

                entity.Property(e => e.PatientContactId).HasColumnName("PatientContactID");

                entity.Property(e => e.CellPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MailingAddressId).HasColumnName("MailingAddressID");

                entity.Property(e => e.Mrn)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.Property(e => e.ResidenceAddressId).HasColumnName("ResidenceAddressID");

                entity.Property(e => e.WorkPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.MailingAddress)
                    .WithMany(p => p.PatientContactDetailMailingAddresses)
                    .HasForeignKey(d => d.MailingAddressId)
                    .HasConstraintName("fk_PatientContactDetails_MailingAddressID");

                entity.HasOne(d => d.MrnNavigation)
                    .WithMany(p => p.PatientContactDetails)
                    .HasForeignKey(d => d.Mrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientContactDetails_MRN");

                entity.HasOne(d => d.PreferredModeOfContactNavigation)
                    .WithMany(p => p.PatientContactDetails)
                    .HasForeignKey(d => d.PreferredModeOfContact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientContactDetails_PreferredModeOfContact");

                entity.HasOne(d => d.PreferredTimeToContactNavigation)
                    .WithMany(p => p.PatientContactDetails)
                    .HasForeignKey(d => d.PreferredTimeToContact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientContactDetails_PreferredTimeToContact");

                entity.HasOne(d => d.ResidenceAddress)
                    .WithMany(p => p.PatientContactDetailResidenceAddresses)
                    .HasForeignKey(d => d.ResidenceAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientContactDetails_ResidenceAddressID");
            });

            modelBuilder.Entity<PatientEmergencyContact>(entity =>
            {
                entity.HasKey(e => e.EmergencyContactId);

                entity.ToTable("PatientEmergencyContact");

                entity.Property(e => e.EmergencyContactId).HasColumnName("EmergencyContactID");

                entity.Property(e => e.ContactAddressId).HasColumnName("ContactAddressID");

                entity.Property(e => e.ContactName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ContactRelationshipId).HasColumnName("ContactRelationshipID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mrn)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.HasOne(d => d.ContactAddress)
                    .WithMany(p => p.PatientEmergencyContacts)
                    .HasForeignKey(d => d.ContactAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientEmergencyContact_ContactAddressID");

                entity.HasOne(d => d.ContactRelationship)
                    .WithMany(p => p.PatientEmergencyContacts)
                    .HasForeignKey(d => d.ContactRelationshipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientEmergencyContact_ContactRelationshipID");

                entity.HasOne(d => d.MrnNavigation)
                    .WithMany(p => p.PatientEmergencyContacts)
                    .HasForeignKey(d => d.Mrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientEmergencyContact_MRN");
            });

            modelBuilder.Entity<PatientFallRisk>(entity =>
            {
                entity.Property(e => e.PatientFallRiskId).HasColumnName("PatientFallRiskID");

                entity.Property(e => e.FallRiskId).HasColumnName("FallRiskID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PatientAlertId).HasColumnName("PatientAlertID");

                entity.HasOne(d => d.FallRisk)
                    .WithMany(p => p.PatientFallRisks)
                    .HasForeignKey(d => d.FallRiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientFallRisks_FallRiskID");

                entity.HasOne(d => d.PatientAlert)
                    .WithMany(p => p.PatientFallRisks)
                    .HasForeignKey(d => d.PatientAlertId)
                    .HasConstraintName("fk_PatientFallRisks_PatientAlertID");
            });

            modelBuilder.Entity<PatientGeneral>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Patient_General");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First Name");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastFourSsn)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Last Four SSN");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last Name");

                entity.Property(e => e.MiddleInitial)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Middle Initial");

                entity.Property(e => e.Mrn)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.Property(e => e.PreferredPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Preferred Phone");

                entity.Property(e => e.PrimaryInsuranceProvider)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Primary Insurance Provider");

                entity.Property(e => e.ResidentAddress)
                    .IsRequired()
                    .HasMaxLength(368)
                    .IsUnicode(false)
                    .HasColumnName("Resident Address");
            });

            modelBuilder.Entity<PatientLanguage>(entity =>
            {
                entity.HasKey(e => new { e.LanguageId, e.Mrn });

                entity.ToTable("PatientLanguage");

                entity.Property(e => e.LanguageId).HasColumnName("LanguageID");

                entity.Property(e => e.Mrn)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.PatientLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientLanguage_LanguageID");

                entity.HasOne(d => d.MrnNavigation)
                    .WithMany(p => p.PatientLanguages)
                    .HasForeignKey(d => d.Mrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientLanguage_MRN");
            });

            modelBuilder.Entity<PatientMilitaryService>(entity =>
            {
                entity.HasKey(e => e.MilitaryServiceId);

                entity.ToTable("PatientMilitaryService");

                entity.Property(e => e.MilitaryServiceId).HasColumnName("MilitaryServiceID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Mrn)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.HasOne(d => d.MrnNavigation)
                    .WithMany(p => p.PatientMilitaryServices)
                    .HasForeignKey(d => d.Mrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientMilitaryService_MRN");
            });

            modelBuilder.Entity<PatientRace>(entity =>
            {
                entity.HasKey(e => new { e.RaceId, e.Mrn });

                entity.ToTable("PatientRace");

                entity.Property(e => e.RaceId).HasColumnName("RaceID");

                entity.Property(e => e.Mrn)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MRN")
                    .IsFixedLength(true);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.MrnNavigation)
                    .WithMany(p => p.PatientRaces)
                    .HasForeignKey(d => d.Mrn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientRace_MRN");

                entity.HasOne(d => d.Race)
                    .WithMany(p => p.PatientRaces)
                    .HasForeignKey(d => d.RaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientRace_RaceID");
            });

            modelBuilder.Entity<PatientRestriction>(entity =>
            {
                entity.HasKey(e => e.RestrictionId);

                entity.Property(e => e.RestrictionId).HasColumnName("RestrictionID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PatientAlertId).HasColumnName("PatientAlertID");

                entity.Property(e => e.RestrictionTypeId).HasColumnName("RestrictionTypeID");

                entity.HasOne(d => d.PatientAlert)
                    .WithMany(p => p.PatientRestrictions)
                    .HasForeignKey(d => d.PatientAlertId)
                    .HasConstraintName("fk_PatientRestrictions_PatientAlertID");

                entity.HasOne(d => d.RestrictionType)
                    .WithMany(p => p.PatientRestrictions)
                    .HasForeignKey(d => d.RestrictionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PatientRestrictions_RestrictionID");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrimaryInsuranceId).HasColumnName("PrimaryInsuranceID");

                entity.Property(e => e.SecondaryInsuranceId).HasColumnName("SecondaryInsuranceID");

                entity.HasOne(d => d.PrimaryInsurance)
                    .WithMany(p => p.PaymentPrimaryInsurances)
                    .HasForeignKey(d => d.PrimaryInsuranceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Insurance_PrimaryInsuranceID");

                entity.HasOne(d => d.SecondaryInsurance)
                    .WithMany(p => p.PaymentSecondaryInsurances)
                    .HasForeignKey(d => d.SecondaryInsuranceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Insurance_SecondaryInsuranceID");
            });

            modelBuilder.Entity<PaymentPlan>(entity =>
            {
                entity.ToTable("PaymentPlan");

                entity.Property(e => e.PaymentPlanId).HasColumnName("PaymentPlanID");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WiPopCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<PaymentSource>(entity =>
            {
                entity.ToTable("PaymentSource");

                entity.Property(e => e.PaymentSourceId).HasColumnName("PaymentSourceID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WiPopCode)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pcacomment>(entity =>
            {
                entity.ToTable("PCAComment");

                entity.Property(e => e.PcacommentId).HasColumnName("PCACommentID");

                entity.Property(e => e.DateCommentAdded).HasColumnType("datetime");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Pcacomment1)
                    .IsUnicode(false)
                    .HasColumnName("PCAComment");

                entity.Property(e => e.PcacommentTypeId).HasColumnName("PCACommentTypeID");

                entity.Property(e => e.Pcaid).HasColumnName("PCAID");

                entity.HasOne(d => d.PcacommentType)
                    .WithMany(p => p.Pcacomments)
                    .HasForeignKey(d => d.PcacommentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PCAComment_PCACommentTypeID");

                entity.HasOne(d => d.Pca)
                    .WithMany(p => p.Pcacomments)
                    .HasForeignKey(d => d.Pcaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PCAComment_PCAID");
            });

            modelBuilder.Entity<PcacommentType>(entity =>
            {
                entity.ToTable("PCACommentType");

                entity.Property(e => e.PcacommentTypeId).HasColumnName("PCACommentTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PcacommentTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PCACommentTypeName");
            });

            modelBuilder.Entity<PcapainAssessment>(entity =>
            {
                entity.HasKey(e => e.PainAssessmentId)
                    .HasName("pk_PCAPainAssessment");

                entity.ToTable("PCAPainAssessment");

                entity.Property(e => e.PainAssessmentId).HasColumnName("PainAssessmentID");

                entity.Property(e => e.LastModified).HasColumnType("datetime");

                entity.Property(e => e.PainParameterId).HasColumnName("PainParameterID");

                entity.Property(e => e.PainRatingId).HasColumnName("PainRatingID");

                entity.Property(e => e.Pcaid).HasColumnName("PCAID");

                entity.HasOne(d => d.PainParameter)
                    .WithMany(p => p.PcapainAssessments)
                    .HasForeignKey(d => d.PainParameterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PCAPainAssessment_PainParameterID");

                entity.HasOne(d => d.PainRating)
                    .WithMany(p => p.PcapainAssessments)
                    .HasForeignKey(d => d.PainRatingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PCAPainAssessment_PainRatingID");

                entity.HasOne(d => d.Pca)
                    .WithMany(p => p.PcapainAssessments)
                    .HasForeignKey(d => d.Pcaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PCAPainAssessment_PCAID");
            });

            modelBuilder.Entity<Pcarecord>(entity =>
            {
                entity.HasKey(e => e.Pcaid);

                entity.ToTable("PCARecord");

                entity.Property(e => e.Pcaid).HasColumnName("PCAID");

                entity.Property(e => e.BloodPressureRouteTypeId).HasColumnName("BloodPressureRouteTypeID");

                entity.Property(e => e.BmimethodId).HasColumnName("BMIMethodID");

                entity.Property(e => e.BodyMassIndex).HasColumnType("decimal(7, 3)");

                entity.Property(e => e.DateVitalsAdded).HasColumnType("datetime");

                entity.Property(e => e.EncounterId).HasColumnName("EncounterID");

                entity.Property(e => e.HeadCircumference).HasColumnType("decimal(7, 3)");

                entity.Property(e => e.HeadCircumferenceUnits)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Height).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.HeightUnits)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.O2deliveryTypeId).HasColumnName("O2DeliveryTypeID");

                entity.Property(e => e.OxygenFlow)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PainScaleTypeId).HasColumnName("PainScaleTypeID");

                entity.Property(e => e.PercentOxygenDelivered).HasColumnType("decimal(5, 4)");

                entity.Property(e => e.PulseRouteTypeId).HasColumnName("PulseRouteTypeID");

                entity.Property(e => e.TempRouteTypeId).HasColumnName("TempRouteTypeID");

                entity.Property(e => e.Temperature).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Weight).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.WeightUnits)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BloodPressureRouteType)
                    .WithMany(p => p.Pcarecords)
                    .HasForeignKey(d => d.BloodPressureRouteTypeId)
                    .HasConstraintName("fk_PCARecord_BloodPressureRouteTypeID");

                entity.HasOne(d => d.Bmimethod)
                    .WithMany(p => p.Pcarecords)
                    .HasForeignKey(d => d.BmimethodId)
                    .HasConstraintName("fk_PCARecord_BMIMethodID");

                entity.HasOne(d => d.Encounter)
                    .WithMany(p => p.Pcarecords)
                    .HasForeignKey(d => d.EncounterId)
                    .HasConstraintName("fk_PCARecord_EncounterID");

                entity.HasOne(d => d.O2deliveryType)
                    .WithMany(p => p.Pcarecords)
                    .HasForeignKey(d => d.O2deliveryTypeId)
                    .HasConstraintName("fk_PCARecord_O2DeliveryTypeID");

                entity.HasOne(d => d.PainScaleType)
                    .WithMany(p => p.Pcarecords)
                    .HasForeignKey(d => d.PainScaleTypeId)
                    .HasConstraintName("fk_PCARecord_PainScaleTypeID");

                entity.HasOne(d => d.PulseRouteType)
                    .WithMany(p => p.Pcarecords)
                    .HasForeignKey(d => d.PulseRouteTypeId)
                    .HasConstraintName("fk_PCARecord_PulseRouteTypeID");

                entity.HasOne(d => d.TempRouteType)
                    .WithMany(p => p.Pcarecords)
                    .HasForeignKey(d => d.TempRouteTypeId)
                    .HasConstraintName("fk_PCARecord_TempRouteTypeID");
            });

            modelBuilder.Entity<Physician>(entity =>
            {
                entity.ToTable("Physician");

                entity.Property(e => e.PhysicianId).HasColumnName("PhysicianID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Credentials)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.License)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderTypeId).HasColumnName("ProviderTypeID");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Physicians)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK_Physician_AddressID");

                entity.HasOne(d => d.ProviderType)
                    .WithMany(p => p.Physicians)
                    .HasForeignKey(d => d.ProviderTypeId)
                    .HasConstraintName("FK_Physician_ProviderTypeID");

                entity.HasOne(d => d.Specialty)
                    .WithMany(p => p.Physicians)
                    .HasForeignKey(d => d.SpecialtyId)
                    .HasConstraintName("FK_Physician_SpecialtyID");
            });

            modelBuilder.Entity<PhysicianAssessment>(entity =>
            {
                entity.ToTable("PhysicianAssessment");

                entity.Property(e => e.PhysicianAssessmentId).HasColumnName("PhysicianAssessmentID");

                entity.Property(e => e.Assessment).IsUnicode(false);

                entity.Property(e => e.ChiefComplaint).IsUnicode(false);

                entity.Property(e => e.EncounterId).HasColumnName("EncounterID");

                entity.Property(e => e.FamilyHistory).IsUnicode(false);

                entity.Property(e => e.HistoryOfPresentIllness).IsUnicode(false);

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhysicianAssessmentDate).HasColumnType("date");

                entity.Property(e => e.PhysicianAssessmentTypeId).HasColumnName("PhysicianAssessmentTypeID");

                entity.Property(e => e.Plan).IsUnicode(false);

                entity.Property(e => e.SignificantDiagnosticTests).IsUnicode(false);

                entity.Property(e => e.SocialHistory).IsUnicode(false);

                entity.Property(e => e.WrittenDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AuthoringProviderNavigation)
                    .WithMany(p => p.PhysicianAssessmentAuthoringProviderNavigations)
                    .HasForeignKey(d => d.AuthoringProvider)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PAAuthoringProvider");

                entity.HasOne(d => d.CoSignatureNavigation)
                    .WithMany(p => p.PhysicianAssessmentCoSignatureNavigations)
                    .HasForeignKey(d => d.CoSignature)
                    .HasConstraintName("fk_PACoSignature");

                entity.HasOne(d => d.Encounter)
                    .WithMany(p => p.PhysicianAssessments)
                    .HasForeignKey(d => d.EncounterId)
                    .HasConstraintName("fk_PAEncounterID");

                entity.HasOne(d => d.PhysicianAssessmentType)
                    .WithMany(p => p.PhysicianAssessments)
                    .HasForeignKey(d => d.PhysicianAssessmentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PhysicianAssessmentTypeID");

                entity.HasOne(d => d.ReferringProviderNavigation)
                    .WithMany(p => p.PhysicianAssessmentReferringProviderNavigations)
                    .HasForeignKey(d => d.ReferringProvider)
                    .HasConstraintName("fk_PAReferringProvider");
            });

            modelBuilder.Entity<PhysicianAssessmentDetail>(entity =>
            {
                entity.ToTable("PhysicianAssessmentDetail");

                entity.Property(e => e.PhysicianAssessmentDetailId).HasColumnName("PhysicianASsessmentDetailID");

                entity.Property(e => e.CareSystemTypeId).HasColumnName("CareSystemTypeID");

                entity.Property(e => e.Comment).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhysicianAssessmentId).HasColumnName("PhysicianAssessmentID");

                entity.HasOne(d => d.CareSystemType)
                    .WithMany(p => p.PhysicianAssessmentDetails)
                    .HasForeignKey(d => d.CareSystemTypeId)
                    .HasConstraintName("fk_CareSystemType");

                entity.HasOne(d => d.ExamTypeCodeNavigation)
                    .WithMany(p => p.PhysicianAssessmentDetails)
                    .HasForeignKey(d => d.ExamTypeCode)
                    .HasConstraintName("fk_ExamType");

                entity.HasOne(d => d.PhysicianAssessment)
                    .WithMany(p => p.PhysicianAssessmentDetails)
                    .HasForeignKey(d => d.PhysicianAssessmentId)
                    .HasConstraintName("fk_PhysicianAssessment");
            });

            modelBuilder.Entity<PhysicianAssessmentType>(entity =>
            {
                entity.ToTable("PhysicianAssessmentType");

                entity.Property(e => e.PhysicianAssessmentTypeId).HasColumnName("PhysicianAssessmentTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PhysicianRole>(entity =>
            {
                entity.ToTable("PhysicianRole");

                entity.Property(e => e.PhysicianRoleId).HasColumnName("PhysicianRoleID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PlaceOfServiceOutPatient>(entity =>
            {
                entity.HasKey(e => e.PlaceOfServiceId);

                entity.ToTable("PlaceOfServiceOutPatient");

                entity.Property(e => e.PlaceOfServiceId).HasColumnName("PlaceOfServiceID");

                entity.Property(e => e.Description)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<PointOfOrigin>(entity =>
            {
                entity.ToTable("PointOfOrigin");

                entity.Property(e => e.PointOfOriginId).HasColumnName("PointOfOriginID");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Explaination).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WiPopCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PreferredContactTime>(entity =>
            {
                entity.HasKey(e => e.ContactTimeId);

                entity.ToTable("PreferredContactTime");

                entity.Property(e => e.ContactTimeId).HasColumnName("ContactTimeID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PreferredModeOfContact>(entity =>
            {
                entity.HasKey(e => e.ModeOfContactId);

                entity.ToTable("PreferredModeOfContact");

                entity.Property(e => e.ModeOfContactId).HasColumnName("ModeOfContactID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority");

                entity.Property(e => e.PriorityId).HasColumnName("PriorityID");

                entity.Property(e => e.PriorityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProcedureReport>(entity =>
            {
                entity.ToTable("ProcedureReport");

                entity.Property(e => e.ProcedureReportId).HasColumnName("ProcedureReportID");

                entity.Property(e => e.Complications).IsUnicode(false);

                entity.Property(e => e.DescriptionOfProcedure).IsUnicode(false);

                entity.Property(e => e.Drains).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.EncounterId).HasColumnName("EncounterID");

                entity.Property(e => e.EstiamtedBloodLoss).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OperativeIndications).IsUnicode(false);

                entity.Property(e => e.PostoperativeDiagnosis).IsUnicode(false);

                entity.Property(e => e.PreoperativeDiagonsis).IsUnicode(false);

                entity.Property(e => e.ProcedureDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(CONVERT([date],getdate()))");

                entity.Property(e => e.ProcedurePerformed).IsUnicode(false);

                entity.Property(e => e.WrittenDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AuthoringProviderNavigation)
                    .WithMany(p => p.ProcedureReportAuthoringProviderNavigations)
                    .HasForeignKey(d => d.AuthoringProvider)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_AuthoringProvider");

                entity.HasOne(d => d.CoSignatureNavigation)
                    .WithMany(p => p.ProcedureReportCoSignatureNavigations)
                    .HasForeignKey(d => d.CoSignature)
                    .HasConstraintName("fk_PRCoSignature");

                entity.HasOne(d => d.Encounter)
                    .WithMany(p => p.ProcedureReports)
                    .HasForeignKey(d => d.EncounterId)
                    .HasConstraintName("fk_PREncounterID");
            });

            modelBuilder.Entity<ProcedureReportAnestheticType>(entity =>
            {
                entity.HasKey(e => new { e.ProcedureReportId, e.AnestheticTypeId })
                    .HasName("pk_ProcedureReportAnestheticType");

                entity.ToTable("ProcedureReportAnestheticType");

                entity.Property(e => e.ProcedureReportId).HasColumnName("ProcedureReportID");

                entity.Property(e => e.AnestheticTypeId).HasColumnName("AnestheticTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AnestheticType)
                    .WithMany(p => p.ProcedureReportAnestheticTypes)
                    .HasForeignKey(d => d.AnestheticTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_AnestheticTypeID");

                entity.HasOne(d => d.ProcedureReport)
                    .WithMany(p => p.ProcedureReportAnestheticTypes)
                    .HasForeignKey(d => d.ProcedureReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRATProcedureReportID");
            });

            modelBuilder.Entity<ProcedureReportPhysician>(entity =>
            {
                entity.HasKey(e => new { e.ProcedureReportId, e.PhysicianId })
                    .HasName("pk_ProcedureReportPhysician");

                entity.ToTable("ProcedureReportPhysician");

                entity.Property(e => e.ProcedureReportId).HasColumnName("ProcedureReportID");

                entity.Property(e => e.PhysicianId).HasColumnName("PhysicianID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhysicianRoleId).HasColumnName("PhysicianRoleID");

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.ProcedureReportPhysicians)
                    .HasForeignKey(d => d.PhysicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRPPhysicianID");

                entity.HasOne(d => d.ProcedureReport)
                    .WithMany(p => p.ProcedureReportPhysicians)
                    .HasForeignKey(d => d.ProcedureReportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PRPProcedureReportID");
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.ToTable("Program");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProgramFacility>(entity =>
            {
                entity.HasKey(e => e.ProgramFacilitiesId)
                    .HasName("pk_ProgramFacilities");

                entity.HasIndex(e => new { e.ProgramId, e.FacilityId }, "uk_ProgramFacilities")
                    .IsUnique();

                entity.Property(e => e.ProgramFacilitiesId).HasColumnName("ProgramFacilitiesID");

                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.ProgramFacilities)
                    .HasForeignKey(d => d.FacilityId)
                    .HasConstraintName("fk_ProgramFacilities_FacilityID");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.ProgramFacilities)
                    .HasForeignKey(d => d.ProgramId)
                    .HasConstraintName("fk_ProgramFacilities_ProgramID");
            });

            modelBuilder.Entity<ProgressNote>(entity =>
            {
                entity.ToTable("ProgressNote");

                entity.Property(e => e.ProgressNoteId).HasColumnName("ProgressNoteID");

                entity.Property(e => e.CoPhysicianId).HasColumnName("CoPhysicianID");

                entity.Property(e => e.EncounterId).HasColumnName("EncounterID");

                entity.Property(e => e.LastUpdated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.NoteTypeId).HasColumnName("NoteTypeID");

                entity.Property(e => e.PhysicianId).HasColumnName("PhysicianID");

                entity.Property(e => e.WrittenDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.CoPhysician)
                    .WithMany(p => p.ProgressNoteCoPhysicians)
                    .HasForeignKey(d => d.CoPhysicianId)
                    .HasConstraintName("fk_CoPhysicanID");

                entity.HasOne(d => d.Encounter)
                    .WithMany(p => p.ProgressNotes)
                    .HasForeignKey(d => d.EncounterId)
                    .HasConstraintName("fk_PNEncounterID");

                entity.HasOne(d => d.NoteType)
                    .WithMany(p => p.ProgressNotes)
                    .HasForeignKey(d => d.NoteTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_NoteTypeID");

                entity.HasOne(d => d.Physician)
                    .WithMany(p => p.ProgressNotePhysicians)
                    .HasForeignKey(d => d.PhysicianId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PNPhysicanID");
            });

            modelBuilder.Entity<ProviderType>(entity =>
            {
                entity.ToTable("ProviderType");

                entity.Property(e => e.ProviderTypeId).HasColumnName("ProviderTypeID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PulseRouteType>(entity =>
            {
                entity.ToTable("PulseRouteType");

                entity.Property(e => e.PulseRouteTypeId).HasColumnName("PulseRouteTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PulseRouteTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Race>(entity =>
            {
                entity.ToTable("Race");

                entity.Property(e => e.RaceId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RaceID");

                entity.Property(e => e.Category)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.ToTable("Reaction");

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Relationship>(entity =>
            {
                entity.ToTable("Relationship");

                entity.Property(e => e.RelationshipId).HasColumnName("RelationshipID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Religion>(entity =>
            {
                entity.ToTable("Religion");

                entity.Property(e => e.ReligionId).HasColumnName("ReligionID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Restriction>(entity =>
            {
                entity.Property(e => e.RestrictionId).HasColumnName("RestrictionID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SecurityQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Security__0DC06F8C6052F184");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sex>(entity =>
            {
                entity.ToTable("Sex");

                entity.Property(e => e.SexId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("SexID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SideEffect>(entity =>
            {
                entity.ToTable("SideEffect");

                entity.Property(e => e.SideEffectId).HasColumnName("SideEffectID");

                entity.Property(e => e.SideEffectDescription)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Specialty>(entity =>
            {
                entity.ToTable("Specialty");

                entity.Property(e => e.SpecialtyId).HasColumnName("SpecialtyID");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TempRouteType>(entity =>
            {
                entity.ToTable("TempRouteType");

                entity.Property(e => e.TempRouteTypeId).HasColumnName("TempRouteTypeID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TempRouteTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.HasIndex(e => e.TestCategoryId, "FK");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.TestCategoryId).HasColumnName("TestCategoryID");

                entity.Property(e => e.TestName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TestCategory)
                    .WithMany(p => p.Tests)
                    .HasForeignKey(d => d.TestCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Test__TestCatego__6D2D2E85");
            });

            modelBuilder.Entity<TestCategory>(entity =>
            {
                entity.ToTable("TestCategory");

                entity.Property(e => e.TestCategoryId).HasColumnName("TestCategoryID");

                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.TestCategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TestedBodyPart>(entity =>
            {
                entity.HasKey(e => new { e.TestId, e.PartId })
                    .HasName("PK__TestedBo__9B00C1D35B010DBE");

                entity.HasIndex(e => new { e.TestId, e.PartId }, "PK/FK");

                entity.Property(e => e.TestId).HasColumnName("TestID");

                entity.Property(e => e.PartId).HasColumnName("PartID");

                entity.HasOne(d => d.Part)
                    .WithMany(p => p.TestedBodyParts)
                    .HasForeignKey(d => d.PartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TestedBod__PartI__70FDBF69");

                entity.HasOne(d => d.Test)
                    .WithMany(p => p.TestedBodyParts)
                    .HasForeignKey(d => d.TestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TestedBod__TestI__70099B30");
            });

            modelBuilder.Entity<Use>(entity =>
            {
                entity.HasKey(e => e.UsesId)
                    .HasName("PK__Uses__1648CA5564176E83");

                entity.Property(e => e.UsesId).HasColumnName("UsesID");

                entity.Property(e => e.UseName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserFacility>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.FacilityId });

                entity.ToTable("UserFacility");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.FacilityId).HasColumnName("FacilityID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Facility)
                    .WithMany(p => p.UserFacilities)
                    .HasForeignKey(d => d.FacilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserFacility_FacilityID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFacilities)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserFacility_UserID");
            });

            modelBuilder.Entity<UserProgram>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProgramId })
                    .HasName("pk_UserPrograms");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ProgramId).HasColumnName("ProgramID");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.UserPrograms)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserPrograms_ProgramID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPrograms)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserPrograms_UserID");
            });

            modelBuilder.Entity<UserQuestion>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.UserId })
                    .HasName("PK__UserQues__DCB8E346DBDD63AF");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.AnswerHash)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.UserQuestions)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserQuest__Quest__7C8F6DA6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserQuestions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserQuest__UserI__7D8391DF");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_User");

                entity.ToTable("UserTable");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.AspNetUsersId)
                    .IsRequired()
                    .HasMaxLength(450)
                    .HasColumnName("AspNetUsersID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("('12/31/9999')");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InstructorId).HasColumnName("InstructorID");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProgramEnrolledIn)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AspNetUsers)
                    .WithMany(p => p.UserTables)
                    .HasForeignKey(d => d.AspNetUsersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserTable_AspNetUsersID");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.InverseInstructor)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("fk_InstructorID");
            });

            modelBuilder.Entity<Warning>(entity =>
            {
                entity.ToTable("Warning");

                entity.Property(e => e.WarningId).HasColumnName("WarningID");

                entity.Property(e => e.WarningDescription)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.HasSequence<int>("MRN_ID");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

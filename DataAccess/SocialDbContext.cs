using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class SocialDbContext : IdentityDbContext
    {
        public SocialDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<ActiveSchoolTerm> ActiveSchoolTerms { get; set; }
        public DbSet<SchoolSubjects> SchoolSubjects { get; set; }
        public DbSet<CurrentGradingSystem> CurrentGradingSystems { get; set; }
        public DbSet<JuniorSchoolSubject> JuniorSchoolSubjects { get; set; }
        public DbSet<SeniorSchoolSubject> SeniorSchoolSubjects { get; set; }
        public DbSet<ClassInSchool> ClassInSchools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SubjectTeaching> SubjectTeachings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<JuniorSchoolSubject>(ConfigureJuniorSchoolSubject);
            builder.Entity<SeniorSchoolSubject>(ConfigureSeniorSchoolSubject);
            builder.Entity<SchoolSubjects>(ConfigureSchoolSubjects);
            builder.Entity<ClassInSchool>(ConfigureClassInSchool);
            builder.Entity<Teacher>(ConfigureTeacher);
            builder.Entity<SubjectTeaching>(ConfigureSubjectTeaching);
            base.OnModelCreating(builder);
        }

        private void ConfigureSchoolSubjects(EntityTypeBuilder<SchoolSubjects> entity)
        {

            entity.ToTable("SchoolSubjects");

            entity.HasIndex(e => e.Subjects)
                .IsUnique();

            entity.Property(e => e.Subjects)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Subjects")
                .IsFixedLength();
        }
        private void ConfigureClassInSchool(EntityTypeBuilder<ClassInSchool> entity)
        {

            entity.HasIndex(e => e.ClassName)
                .IsUnique();

            entity.Property(e => e.ClassName)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("ClassName")
                .IsFixedLength();
        }
        private void ConfigureTeacher(EntityTypeBuilder<Teacher> entity)
        {
            entity.HasIndex(e => e.Email)
                .IsUnique();
        }
        private void ConfigureSubjectTeaching(EntityTypeBuilder<SubjectTeaching> entity)
        {

            entity.HasOne(d => d.SchoolSubjects)
                 .WithMany(f => f.SubjectTeaching)
                 .HasForeignKey(d => d.SchoolSubjectsId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Teacher)
                 .WithMany(f => f.SubjectTeaching)
                 .HasForeignKey(d => d.SchoolSubjectsId)
                 .OnDelete(DeleteBehavior.ClientSetNull);


        }

        private void ConfigureJuniorSchoolSubject(EntityTypeBuilder<JuniorSchoolSubject> entity)
        {
            entity.HasOne(d => d.SchoolSubjects)
                 .WithMany()
                 .HasForeignKey(d => d.SubjectId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SchoolSubjects)
                .WithMany()
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        private void ConfigureSeniorSchoolSubject(EntityTypeBuilder<SeniorSchoolSubject> entity)
        {

            entity.HasOne(d => d.SchoolSubjects)
                 .WithMany()
                 .HasForeignKey(d => d.SubjectId)
                 .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.SchoolSubjects)
                .WithMany()
                .HasForeignKey(d => d.SubjectId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

    }
}

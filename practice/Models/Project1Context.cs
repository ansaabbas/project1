using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace practice.Models
{
    public partial class Project1Context : DbContext
    {
        public Project1Context()
        {
        }

        public Project1Context(DbContextOptions<Project1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Student1> Student1 { get; set; }
        public virtual DbSet<Teacher1> Teacher1 { get; set; }

       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-MIEUPJ6\\SQLEXPRESS;Database=Project1;Trusted_Connection=True; User ID=sa; Password=ansa;");
            }
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student1>(entity =>
            {
                entity.ToTable("student1");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cv)
                    .HasColumnName("CV")
                    .HasMaxLength(250);

                entity.Property(e => e.Department).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(250);

                entity.Property(e => e.FatherName)
                    .HasColumnName("Father_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gpa).HasColumnName("GPA");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ProfilePicture)
                    .HasColumnName("Profile_Picture")
                    .HasMaxLength(250);

                entity.Property(e => e.RollNo).HasColumnName("Roll_No");

                entity.Property(e => e.Subject).HasMaxLength(50);
                entity.Property(e => e.Actions).HasMaxLength(50);
            });

            modelBuilder.Entity<Teacher1>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(10);

                entity.Property(e => e.Cv)
                    .HasColumnName("CV")
                    .HasMaxLength(10);

              

                entity.Property(e => e.Email).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(10);

                entity.Property(e => e.PhoneNo)
                    .HasColumnName("Phone_No")
                    .HasMaxLength(10);

                entity.Property(e => e.Post).HasMaxLength(10);

                entity.Property(e => e.ProfilePicture)
                    .HasColumnName("Profile_Picture")
                    .HasMaxLength(10);

                entity.Property(e => e.Qualification).HasMaxLength(10);

                entity.Property(e => e.Subject).HasMaxLength(10);
            });
        }
    }
}

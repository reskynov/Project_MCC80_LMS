using API.DTOs.Roles;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task = API.Models.Task;

namespace API.Data
{
    public class LmsDbContext : DbContext
    {
        public LmsDbContext(DbContextOptions<LmsDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserClassroom> UserClassrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
               new NewDefaultRoleDto
               {
                   Guid = Guid.Parse("5eeda544-ee8f-495d-9366-6c04e0904a5c"),
                   Name = "Teacher"
               }, new NewDefaultRoleDto
               {
                   Guid = Guid.Parse("4016bbf3-5514-4478-97f8-85a3baef09c2"),
                   Name = "Student"
               }, new NewDefaultRoleDto
               {
                   Guid = Guid.Parse("24706f51-2651-4cd2-9ca0-c8e510969b7d"),
                   Name = "Admin"
               });

            modelBuilder.Entity<User>().HasIndex(user => new
            {
                user.Email,
                user.PhoneNumber
            }).IsUnique();

            modelBuilder.Entity<Classroom>().HasIndex(classRoom => new
            {
                classRoom.Code,
            }).IsUnique();

            //USER
            //One User to many UserClassroom (1:N)
            modelBuilder.Entity<User>()
                        .HasMany(u => u.UserClassrooms)
                        .WithOne(uClass => uClass.User)
                        .HasForeignKey(uClas => uClas.UserGuid);

            //One User to one Account (1:1)
            modelBuilder.Entity<User>()
                        .HasOne(user => user.Account)
                        .WithOne(acc => acc.User)
                        .HasForeignKey<Account>(acc => acc.Guid);

            //ACCOUNT
            //One Account to many Account Role (1:N)
            modelBuilder.Entity<Account>()
                        .HasMany(acc => acc.AccountRoles)
                        .WithOne(accRole => accRole.Account)
                        .HasForeignKey(accRole => accRole.AccountGuid);

            //ROLE
            //One Role to many Account Role (1:N)
            modelBuilder.Entity<Role>()
                        .HasMany(r => r.AccountRoles)
                        .WithOne(accRole => accRole.Role)
                        .HasForeignKey(accRole => accRole.RoleGuid);

            //CLASSROOM
            //One Classroom to many UserClassroom (1:N)
            modelBuilder.Entity<Classroom>()
                        .HasMany(c => c.UserClassrooms)
                        .WithOne(uClass => uClass.Classroom)
                        .HasForeignKey(uClass => uClass.ClassroomGuid);

            //One Classroom to many Lesson (1:N)
            modelBuilder.Entity<Classroom>()
                        .HasMany(c => c.Lessons)
                        .WithOne(lesson => lesson.Classroom)
                        .HasForeignKey(lesson => lesson.ClassroomGuid);

            //LESSON
            //One Lesson to many Task (1:N)
            modelBuilder.Entity<Lesson>()
                        .HasMany(lesson => lesson.Tasks)
                        .WithOne(task => task.Lesson)
                        .HasForeignKey(task => task.LessonGuid);

            //TASK
            //One User to many Task (1:N)
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Tasks)
                        .WithOne(task => task.User)
                        .HasForeignKey(task => task.UserGuid);

            //One Task to many Grade (1:N)
            modelBuilder.Entity<Task>()
                        .HasMany(task => task.Grades)
                        .WithOne(grade => grade.Task)
                        .HasForeignKey(grade => grade.TaskGuid)
                        .OnDelete(DeleteBehavior.Restrict);

            //One User to many Grade (1:N)
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Grades)
                        .WithOne(grade => grade.User)
                        .HasForeignKey(grade => grade.UserGuid)
                        .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

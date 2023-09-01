using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework.Context
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        {
           
        }

        public BaseDbContext()
        {
        }

        public virtual DbSet<User>  Users{ get; set; }
        public virtual DbSet<Role> Roles{ get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<ModuleRoles> ModuleRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('user_id_seq'::regclass)");

                entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255);

                entity.Property(e => e.Surname)
                .HasColumnName("surname")
                .HasMaxLength(255);

                entity.Property(e => e.UserName)
                .HasColumnName("username")
                .HasMaxLength(255);

                entity.Property(e => e.PasswordHash)
                .HasColumnName("password_hash");

                entity.Property(e => e.PasswordSalt)
                .HasColumnName("password_salt");

                entity.Property(e => e.RolId)
                .HasColumnName("role_id");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("nextval('roles_id_seq'::regclass)");

                entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("modules");

                entity.Property(e => e.Id)
                .HasColumnName("id")
                .HasDefaultValueSql("nextval('modules_id_seq'::regclass)");

                entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(255);

                entity.Property(e => e.Controller)
                .HasColumnName("controller")
                .HasMaxLength(255);

                entity.Property(e => e.Action)
                .HasColumnName("action")
                .HasMaxLength(255);

                entity.Property(e => e.Address)
                .HasColumnName("address")
                .HasMaxLength(255);

                entity.Property(e => e.Menu)
                .HasColumnName("menu");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .HasColumnName("icon");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

            });

            modelBuilder.Entity<ModuleRoles>(entity =>
            {
                entity.ToTable("module_role");

                entity.Property(e => e.RolId)
                 .HasColumnName("role_id");

                entity.Property(e => e.ModuleId)
                .HasColumnName("module_id");

                entity.Property(e => e.UserId)
                .HasColumnName("user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('module_role_id_seq'::regclass)");
            });

        }
    }
}

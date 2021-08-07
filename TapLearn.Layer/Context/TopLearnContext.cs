using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
//using TapLearn.Core.Course;
//using TapLearn.Layer.Entites.CoursGroup;
using TapLearn.Layer.Entites.permission;
using TapLearn.Layer.Entites.User;
using TapLearn.Layer.Entites.Wallet;
using TapLearn.Layer.Entites.Order;
using System.Linq;
using TapLearn.Layer.Entites.Orders;
using TapLearn.Layer.Entites.Course;

namespace TapLearn.Layer.Context
{
    public class TopLearnContext:DbContext
    {
        public TopLearnContext(DbContextOptions<TopLearnContext>options):base(options)
        {

        }
        #region User
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<UserRole> userRoles { get; set; }
        public DbSet<UseCourse> useCourses { get; set; }
        #endregion
        #region wallet
        public DbSet<wallet> Wallets { get; set; }
        public DbSet<WalletType> walletTypes { get; set; }
        #endregion
        #region Permission
        public DbSet<permission> permissions { get; set;}
        public DbSet<RolePermission> rolePermissions { get; set; }
        #endregion
        #region CoursGroup
        public DbSet<CourseGroup> courseGroups { get; set; }
        public DbSet<CourseEpisod> courseEpisods { get; set; }
        public DbSet<CourseLevel> courseLevels { get; set; }
        public DbSet<CourseStatus> courseStatuses { get; set; }
        public DbSet<Curse> curses { get; set; }
        public DbSet<Note> notes { get; set; }
        public DbSet<CourseVolte> courseVoltes { get; set; }

        #endregion
        #region Order
        public DbSet<Order> orders { get; set; }
         public DbSet<Details> detail { get; set; }
        public DbSet<DisCount> disCounts { get; set; }
        public DbSet<UserDiscountCode > userDiscountCodes { get; set; }
        

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
              .SelectMany(t => t.GetForeignKeys())
              .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
             foreach (var fk in cascadeFKs)
                 fk.DeleteBehavior = DeleteBehavior.Restrict;
             modelBuilder.Entity<Curse>()

              .HasOne<CourseGroup>(f => f.CourseGroup)

             .WithMany(g => g.Courses)

             .HasForeignKey(f => f.GroupId);



            modelBuilder.Entity<Curse>()

          .HasOne<CourseGroup>(f => f.Group)

           .WithMany(g => g.SubGroup);


            //modelBuilder.Entity<OrderDetails>().HasMany(o => o.order)
            //  .WithRequired().HasForeignKey(con => con.oi);

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Role>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<CourseGroup>().HasQueryFilter(u => !u.IsDelete);
            
            //modelBuilder.Entity<CourseComment>()
            //    .HasOne(c => c.curse)
            //    .WithMany(s => s.CourseComments)
            //    .HasForeignKey(c => c.C_id);

            base.OnModelCreating(modelBuilder);
        }
    }
}

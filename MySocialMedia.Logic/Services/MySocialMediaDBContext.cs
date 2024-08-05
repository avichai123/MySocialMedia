using Microsoft.EntityFrameworkCore;
using MySocialMedia.Common.DBTabales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialMedia.Logic.Services
{
    public interface IDbContext
    {
        void Connect(Action<MySocialMediaDBContext> p_action, bool p_save = false);
        T Connect<T>(Func<MySocialMediaDBContext , T> p_action, bool p_save = false);
    }
    public class SMDbContext:IDbContext
    {
        public void Connect(Action<MySocialMediaDBContext> p_action , bool p_save = false)
        {
            using(var db = new MySocialMediaDBContext())
            {
                p_action(db);
                if (p_save)
                {
                    db.SaveChanges();
                }
            }
        }
        public T Connect<T>(Func<MySocialMediaDBContext , T> p_action, bool p_save = false)
        {
            using (var db = new MySocialMediaDBContext())
            {
                var res = p_action(db); 
                p_action(db);
                if (p_save)
                {
                    db.SaveChanges();
                }
                return res; 
            }
        }
    }
    public class MySocialMediaDBContext: DbContext  
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var cs = Properties.Resources.ConnectionString;
            optionsBuilder.UseMySql(cs, ServerVersion.AutoDetect(cs));
        }
        public DbSet<users> users { get; set; }    
        public DbSet<user_messages> user_messages { get; set; }    
        public DbSet<user_session> user_sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<users>().HasKey(x => x.ID);
            modelBuilder.Entity<user_messages>().HasKey(x => x.ID);
            modelBuilder.Entity<user_session>().HasKey(x => x.ID);

            modelBuilder.Entity<users>()
                .HasMany(x => x.send_user_messages)
                .WithOne(x => x.send_user)
                .HasForeignKey(x => x.SENDER_USER_ID);
            modelBuilder.Entity<users>()
                .HasMany(x => x.receive_user_messages)
                .WithOne(x => x.receive_user)
                .HasForeignKey(x => x.RECEIVER_USER_ID);
            modelBuilder.Entity<users>()
                .HasMany(x => x.user_sessions)
                .WithOne(x => x.users)
                .HasForeignKey(x => x.USER_ID);
        }
      
    }
}

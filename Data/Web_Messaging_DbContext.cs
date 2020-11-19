using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web_Messaging_MVC.Models;

namespace Web_Messaging_MVC.Data
{
    public class Web_Messaging_DbContext : DbContext
    {
        public Web_Messaging_DbContext (DbContextOptions<Web_Messaging_DbContext> options)
            : base(options)
        {
        }

        public DbSet<Web_Messaging_MVC.Models.Message> Message { get; set; }

        public DbSet<Web_Messaging_MVC.Models.MessagingType> MessagingType { get; set; }

        public DbSet<Web_Messaging_MVC.Models.Receiver> Receiver { get; set; }

        public DbSet<Web_Messaging_MVC.Models.Sender> Sender { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainR
{
    class Database : DbContext
    {
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static bool _created = false;

        public Database()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionbuilder)
        {
            optionbuilder.UseSqlite(@"Data Source=" + path + "\\TrainR.db");
        }

        public DbSet<Cities> City { get; set; }
        public DbSet<Connections> Connection { get; set; }
        public DbSet<Departures> Departure { get; set; }
        public DbSet<Trains> Train { get; set; }

    }

    public class Cities
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Connections
    {
        public int ID { get; set; }
        public int Start_ID { get; set; }
        public int Destination_ID { get; set; }
        public int Train_type { get; set; }
    }

    public class Departures
    {
        public int ID { get; set; }
        public int Connection_ID { get; set; }
        public TimeSpan Time { get; set; }
        public int Travel_time { get; set; }
    }

    public class Trains
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

}

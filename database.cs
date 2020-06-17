using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        public DbSet<City> City { get; set; }
        public DbSet<Connection> Connection { get; set; }
        public DbSet<Departure> Departure { get; set; }
        public DbSet<Train> Train { get; set; }

    }

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Start")]
        public ICollection<Connection> StartId { get; set; }
        [InverseProperty("Destination")]
        public ICollection<Connection> DestinationId { get; set; }

    }

    public class Connection
    {
        public int Id { get; set; }
        public Train Train { get; set; }
        public City Start { get; set; }
        public City Destination { get; set; }

        [InverseProperty("Connection")]
        public ICollection<Departure> ConnectionID { get; set; }
    }

    public class Departure
    {
        public int Id { get; set; }
        public Connection Connection { get; set; }
        public TimeSpan Time { get; set; }
        public int Travel_time { get; set; }
    }

    public class Train
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Train")]
        public ICollection<Connection> TrainType { get; set; }
    }

}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrainR_Admin
{
    class TimeTable : DbContext
    {
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static bool _created = false;

        public TimeTable()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                    new City { Id = 1, Name = "Warszawa" },
                    new City { Id = 2, Name = "Kraków" },
                    new City { Id = 3, Name = "Gdańsk" },
                    new City { Id = 4, Name = "Poznań" },
                    new City { Id = 5, Name = "Wrocław" },
                    new City { Id = 6, Name = "Dębica" },
                    new City { Id = 7, Name = "Olsztyn" },
                    new City { Id = 8, Name = "Gdynia" }
                );

            modelBuilder.Entity<Train>().HasData(
                    new Train { Id = 1, Name = "Osobowy"},
                    new Train { Id = 2, Name = "Pospieszny"},
                    new Train { Id = 3, Name = "Express"}
                );

            modelBuilder.Entity<Connection>().HasData(
                    new Connection { Id = 1, TrainId = 1, StartId = 1, DestinationId = 3 },
                    new Connection { Id = 2, TrainId = 2, StartId = 3, DestinationId = 5 },
                    new Connection { Id = 3, TrainId = 2, StartId = 6, DestinationId = 8 },
                    new Connection { Id = 4, TrainId = 1, StartId = 8, DestinationId = 2 },
                    new Connection { Id = 5, TrainId = 3, StartId = 2, DestinationId = 1 }
                );

            modelBuilder.Entity<Departure>().HasData(
                    new Departure { Id = 1, ConnectionId = 2, Time = new TimeSpan(13, 0, 0),  TravelTime = 90},
                    new Departure { Id = 2, ConnectionId = 1, Time = new TimeSpan(8, 0, 0),  TravelTime = 150},
                    new Departure { Id = 3, ConnectionId = 4, Time = new TimeSpan(20, 0, 0),  TravelTime = 120},
                    new Departure { Id = 4, ConnectionId = 2, Time = new TimeSpan(15, 30, 0),  TravelTime = 70},
                    new Departure { Id = 5, ConnectionId = 3, Time = new TimeSpan(16, 20, 0),  TravelTime = 60}
                );
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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Start")]
        public ICollection<Connection> StartId { get; set; }
        [InverseProperty("Destination")]
        public ICollection<Connection> DestinationId { get; set; }

    }

    public class Connection
    {
        public int Id { get; set; }
        public int StartId { get; set; }
        public int DestinationId { get; set; }
        public int TrainId { get; set; }
        
        public City Start { get; set; }
        public City Destination { get; set; }
        public Train Train { get; set; }

        [InverseProperty("Connection")]
        public ICollection<Departure> ConnectionID { get; set; }
    }

    public class Departure
    {
        public int Id { get; set; }
        public int ConnectionId { get; set; }
        public TimeSpan Time { get; set; }
        /// <summary>
        /// Travel time in minutes.
        /// </summary>
        public int TravelTime { get; set; }

        public Connection Connection { get; set; }
    }

    public class Train
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [InverseProperty("Train")]
        public ICollection<Connection> TrainId { get; set; }
    }
}

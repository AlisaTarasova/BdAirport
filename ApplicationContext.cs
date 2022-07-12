using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BdAirport.Bd;

namespace BdAirport
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Aircraft> Aircraft { get; set; }
        public DbSet<Airline> Airline { get; set; }
        public DbSet<АirportDeparture> АirportDeparture { get; set; }
        public DbSet<АirportDestination> АirportDestination { get; set; }
        public DbSet<BookingTicket> BookingTicket { get; set; }
        public DbSet<Crew> Crew { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<Passenger> Passenger { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Ticket> Ticket { get; set; }

        public static string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=airportdb;Trusted_Connection=True;";

        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>().HasData(
            new Aircraft[]
            {
                new Aircraft { Id=1, Model = "Airbus-A320", BoardNumber = "RA-73734", NumberOfSeats = 180, YearOfRelease = 2001, AirlineId = 1},
                new Aircraft { Id=2, Model = "Airbus-A310", BoardNumber = "D-AHLB",  NumberOfSeats = 280, YearOfRelease = 1997, AirlineId = 2},
                new Aircraft { Id=3, Model = "Boeing-737", BoardNumber = "RA-73068",  NumberOfSeats = 189, YearOfRelease = 2000, AirlineId = 3},
            });

            modelBuilder.Entity<Airline>().HasData(
            new Airline[]
            {
                new Airline { Id=1, Name ="Aeroflot", DateOfCreation = new DateTime(1923, 04, 17), Representative = "Aeroflot"},
                new Airline { Id=2, Name ="S7 Airlines", DateOfCreation = new DateTime(1992, 05, 01), Representative = "S7 Airlines"},
                new Airline { Id=3, Name ="Victory", DateOfCreation = new DateTime(2014, 09, 16), Representative = "Victory"},
            });

            modelBuilder.Entity<АirportDeparture>().HasData(
            new АirportDeparture[]
            {
                new АirportDeparture { Id=1, City ="Moscow", Address = "Sheremetyevo"},
                new АirportDeparture { Id=2, City ="Moscow", Address = "Domodedovo"},
                new АirportDeparture { Id=3, City ="Moscow", Address = "Vnucovo"},
            });

            modelBuilder.Entity<АirportDestination>().HasData(
            new АirportDestination[]
            {
                new АirportDestination {Id=1, City ="Perm", Address = "Bolshoe Savino"}               
            });

            modelBuilder.Entity<BookingTicket>().HasData(
            new BookingTicket[]
            {
                new BookingTicket { Id=1, TicketId = 1, Prepayment = 1000, BookingDate = new DateTime(2022, 04, 11, 11, 47, 00), BookingPeriod = new DateTime(2022, 04, 12, 11, 47, 00)},
                new BookingTicket { Id=2, TicketId = 2, Prepayment = 1500, BookingDate = new DateTime(2022, 06, 02, 15, 03, 00), BookingPeriod = new DateTime(2022, 06, 03, 15, 03, 00)},
                new BookingTicket { Id=3, TicketId = 3, Prepayment = 3000, BookingDate = new DateTime(2022, 06, 29, 22, 56, 00), BookingPeriod = new DateTime(2022, 06, 30, 22, 56, 00)},
            });

            modelBuilder.Entity<Crew>().HasData(
            new Crew[]
            {
                new Crew { Id=1, FlightId = 1, EmployeeId = 1, PostId = 1},
                new Crew { Id=2, FlightId = 2, EmployeeId = 2, PostId = 2},
                new Crew { Id=3, FlightId = 3, EmployeeId = 3, PostId = 3},
            });

            modelBuilder.Entity<Employee>().HasData(
            new Employee[]
            {
                new Employee { Id=1, Experience = 10, PersonId = 1},
                new Employee { Id=2, Experience = 5, PersonId = 2},
                new Employee { Id=3, Experience = 13, PersonId = 3},
            });

            modelBuilder.Entity<Flight>().HasData(
            new Flight[]
            {
                new Flight { Id=1, AirctraftId = 1, АirportDepartureId = 1, АirportDestinationId = 1, FlightNumber= "SU-123", DepartureDateTime = new DateTime(2022, 05, 11, 12, 15, 00), FlightDuration = 120},
                new Flight { Id=2, AirctraftId = 3, АirportDepartureId = 2, АirportDestinationId = 1, FlightNumber= "UA-167", DepartureDateTime = new DateTime(2022, 06, 28, 06, 45, 00), FlightDuration = 125},
                new Flight { Id=3, AirctraftId = 3, АirportDepartureId = 3, АirportDestinationId = 1, FlightNumber= "SU-839", DepartureDateTime = new DateTime(2022, 07, 7, 21, 00, 00), FlightDuration = 150},
            });

            modelBuilder.Entity<Passenger>().HasData(
            new Passenger[]
            {
                new Passenger { Id=1, PersonId = 1, PhoneNumber="89825678280"},
                new Passenger { Id=2, PersonId = 2, PhoneNumber="89023457788"},
                new Passenger { Id=3, PersonId = 3, PhoneNumber="891950522376"},
            });

            modelBuilder.Entity<Person>().HasData(
            new Person[]
            {
                new Person { Id=1, Surname = "Ivanov", Name="Ivan", Patronymic = "Ivanovich", Age=23, Passport = "5617 266902"},
                new Person { Id=2, Surname = "Smirnova", Name="Alice", Patronymic = "Alexeyevna", Age=28, Passport = "5615 423978"},
                new Person { Id=3, Surname = "Petrova", Name="Anna",Patronymic = "Sergeevna", Age=35, Passport = "5610 282873"}
            });

            modelBuilder.Entity<Post>().HasData(
            new Post[]
            {
                new Post { Id=1, Position = "Captain"},
                new Post { Id=2, Position = "Сo-pilot"},
                new Post { Id=3, Position = "Flight attendant"},
            });

            modelBuilder.Entity<Ticket>().HasData(
            new Ticket[]
            {
                new Ticket { Id=1, FlightId = 1, PassengerId = 1, Class = "Economy", Place = "23A", BaggageAvailability = true, Price = 3500},
                new Ticket { Id=2, FlightId = 2, PassengerId = 2, Class = "Economy", Place = "05A", BaggageAvailability = true, Price = 4987},
                new Ticket { Id=3, FlightId = 3, PassengerId = 3, Class = "Business", Place = "14B", BaggageAvailability = true, Price = 8902},
            });

            //modelBuilder.Entity<Flight>().Property(u => u.АirportIdDeparture).HasColumnName("АirportIdDeparture");
            //modelBuilder.Entity<Flight>().Property(u => u.АirportIdDestination).HasColumnName("АirportIdDestination");

            /*modelBuilder.Entity<Flight>()
            .HasOne(p => p.АirportDeparture)
            .WithMany()
            .HasForeignKey(p => p.АirportIdDeparture);

            modelBuilder.Entity<Flight>()
           .HasOne(p => p.АirportDestination)
           .WithMany()
           .HasForeignKey(p => p.АirportIdDestination);*/
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}

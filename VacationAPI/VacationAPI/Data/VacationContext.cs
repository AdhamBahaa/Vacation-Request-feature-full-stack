using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;
using VacationAPI.Models;

namespace VacationData
{
    public class VacationContext : DbContext
    {
        public VacationContext(DbContextOptions<VacationContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<RequestVac> Requests { get; set; }
        public DbSet<VacationType> Types { get; set; }
        public DbSet<Status> Statuses { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var newUsers = new User[]
            {
                new() { Id = 1, Name = "Adham Bahaa" , CasualDays = 3, AnnualDays = 24, 
                    Email = "adham@gmail.com", Password="0000", ManagerId = 2},
                new () { Id = 2, Name = "Mohamed Rabie" , CasualDays = 3, AnnualDays = 24, 
                    Email = "Rabie@gmail.com", Password="0000",},
                new () { Id = 3, Name = "John Doe" , CasualDays = 5, AnnualDays = 20, 
                    Email = "Doe@gmail.com", Password="0000"},
                new () { Id = 4, Name = "Khaled Ismail" , CasualDays = 2, AnnualDays = 13, 
                    Email = "Ismail@gmail.com", Password="0000", ManagerId = 2},
                new () {Id = 5, Name ="Carlos Alcaraz", CasualDays= 2, AnnualDays = 17, 
                    Email = "Alcaraz@gmail.com", Password="0000", ManagerId = 3}
            };
            modelBuilder.Entity<User>().HasData(newUsers);

            var newTypes = new VacationType[]
            {
                new() {Id = 1, VacType = Enum.GetName(TypeEnum.Casual).ToString()},
                new() {Id = 2, VacType = Enum.GetName(TypeEnum.Annual).ToString()}
            };
            modelBuilder.Entity<VacationType>().HasData(newTypes);

            var newStatus = new Status[]
            {
                new() {Id = 1, StatusType = Enum.GetName(StatusEnum.Pending).ToString()},
                new() {Id = 2, StatusType = Enum.GetName(StatusEnum.Approved).ToString()},
                new() {Id = 3, StatusType = Enum.GetName(StatusEnum.Rejected).ToString()},
                new() {Id = 4, StatusType = Enum.GetName(StatusEnum.Completed).ToString()},
            };
            modelBuilder.Entity<Status>().HasData(newStatus);

            var newRequests = new RequestVac[]
            {
                new() {Id = 201, TypeId = 1, FromDate = new DateOnly(2024, 08, 22),
                    ToDate = new DateOnly(2024, 08, 23), UserId = 1},
                new() {Id = 202, TypeId = 2, FromDate = new DateOnly(2024, 08, 13),
                    ToDate = new DateOnly(2024, 08, 23), UserId = 2},
                new() {Id = 203, TypeId = 1, FromDate = new DateOnly(2024, 08, 03),
                    ToDate = new DateOnly(2024, 08, 04), UserId = 2},
                new() {Id = 204, TypeId = 2, FromDate = new DateOnly(2024, 08, 02),
                    ToDate = new DateOnly(2024, 08, 12), UserId = 1},
                new() {Id = 205, TypeId = 2, FromDate = new DateOnly(2024, 09, 02),
                    ToDate = new DateOnly(2024, 09, 12), UserId = 4},
            };
            modelBuilder.Entity<RequestVac>().HasData(newRequests);

        }

    }
}


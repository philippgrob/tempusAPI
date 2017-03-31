using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace tempusAPI.Models
{
    public class BookingEFRepository
    {
        static BookingEFRepository()
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

                var b1 = new Booking
                {
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Now.AddHours(2)
                };

                var b2 = new Booking
                {
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Now.AddHours(4)
                };

                var e1 = new Employee
                {
                    FirstName = "Hans",
                    LastName = "Huber",
                    UserName = "hHuber"
                };

                var e2 = new Employee
                {
                    FirstName = "Karin",
                    LastName = "Kogler",
                    UserName = "kKogler"
                };

                var p1 = new Project
                {
                    AccountingNumber = "DE123850",
                    ConfidentialStatus = "Confidential",
                    ProjectDescription = "Thyssen Krupp AG",
                };

                e1.Bookings.Add(b1);
                p1.Bookings.Add(b1);

                e2.Bookings.Add(b2);
                p1.Bookings.Add(b2);

                ctx.Bookings.Add(b1);
                ctx.Bookings.Add(b2);
                ctx.Employees.Add(e1);
                ctx.Employees.Add(e2);
                ctx.Projects.Add(p1);

                ctx.SaveChanges();

            }
        }

        public List<Booking> FindAllBookings()
            {
                using (var ctx = new BookingDbContext())
                {
                    return ctx
                        .Bookings
                        .OrderBy(n => n.BookingId)                    
                        .ToList();

                }
            }

        public List<Employee> FindAllEmployees()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Employees.Include(n=>n.Bookings).OrderBy(n=>n.EmployeeId).ToList();
            }
        }

        public List<Project> FindAllProjects()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Projects.Include(n=>n.Bookings).OrderBy(n=>n.ProjectId).ToList();
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Employees.Include(n=> n.Bookings).First(n => n.EmployeeId == id);
            }
        }

        public Booking GetBookingById(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Bookings.First(n => n.BookingId == id);
            }
        }

        public Employee GetEmployeeByUserName(String userName)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Employees.Include(n=> n.Bookings).First(n => n.UserName == userName);
            }
        }

        public void SaveBooking(Booking booking)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Bookings.Add(booking);
                ctx.SaveChanges();
            }
        }
        }
    }

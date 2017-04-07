using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing;

namespace tempusAPI.Models
{
    public class BookingEfRepository
    {
        static BookingEfRepository()
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

                var b1 = new Booking
                {
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Now.AddHours(2),
                    Completed =  true
                };

                var b2 = new Booking
                {
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Now.AddHours(4),
                    Completed = true
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
                        ProjectName = "Thyssen Krupp AG",
                };
                var p2 = new Project
                {
                    ProjectName = "OMV AG",
                };

                e1.Bookings.Add(b1);
                p1.Bookings.Add(b1);

                e2.Bookings.Add(b2);
                p2.Bookings.Add(b2);

                ctx.Bookings.Add(b1);
                ctx.Bookings.Add(b2);
                ctx.Employees.Add(e1);
                ctx.Employees.Add(e2);
                ctx.Projects.Add(p1);
                ctx.Projects.Add(p2);

                ctx.SaveChanges();

            }
        }

        internal void RemoveBooking(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Bookings.Remove(ctx.Bookings.FirstOrDefault(n => n.BookingId == id));
                ctx.SaveChanges();
            }
        }

        public List<Booking> FindBookingByEmployeeIdDateRestrictedByDateAndCompletion(int employeeId, string beginDate, bool completed)
        {

            using (var ctx = new BookingDbContext())
            {

                DateTime dateBorder = DateTime.Now;
                
                try
                {
                     dateBorder = DateTime.Parse(beginDate);
                }
                catch
                {
                    throw new Exception("DateTimeParsing fails! Daniel has send the wrong Date Format");
                }
                return ctx
                    .Bookings
                    .Where(n=> n.EmployeeId == employeeId)
                    .Where(n => n.Completed == completed)
                    .Where(n => n.BeginDate > dateBorder)
                    .ToList();
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
                return ctx.Employees.OrderBy(n=>n.EmployeeId).ToList();
            }
        }

        public List<Project> FindAllProjects()
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Projects.OrderBy(n=>n.ProjectId).ToList();
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (var ctx = new BookingDbContext())
            {
                return ctx.Employees.First(n => n.EmployeeId == id);
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
                return ctx.Employees.First(n => n.UserName == userName);
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

        public void PatchBooking(Booking booking)
        {
            using (var ctx = new BookingDbContext())
            {
                ctx.Bookings.Update(booking);
                ctx.SaveChanges();
            }
        }
        }
    }

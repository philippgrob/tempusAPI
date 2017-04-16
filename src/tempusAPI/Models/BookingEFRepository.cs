using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

                List<Employee> initialEmployees = new List<Employee>();

                string employeeMockData = File.ReadAllText("D:/employeeMockData.json");

                initialEmployees = JsonConvert.DeserializeObject<List<Employee>>(employeeMockData);
                  
                ctx.Employees.AddRange(initialEmployees);



                List<Project> initialProjects = new List<Project>();

                string projectMockData = File.ReadAllText("D:/projectMockData.json");

                initialProjects = JsonConvert.DeserializeObject<List<Project>>(projectMockData);

                ctx.Projects.AddRange(initialProjects);


                List<Booking> initialBookings = new List<Booking>();
                string bookingsMockData = File.ReadAllText("D:/bookingsMockData.json");

                initialBookings = JsonConvert.DeserializeObject<List<Booking>>(bookingsMockData);

                ctx.Bookings.AddRange(initialBookings);

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

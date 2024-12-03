using CourseProject.Domain.Entities;
using CourseProject.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Web
{
    public class DbInitialazer
    {
        private readonly AppDbContext db;

        public DbInitialazer(AppDbContext context)
        {
            db = context;
        }
        public async Task InitializeAsync()
        {
            db.Database.EnsureCreated();

            //await db.Database.MigrateAsync();

            if (db.TariffPlans.Any())
            {
                return;
            }
            if (!db.Users.Any())
            {
                // Создаём объект пользователя
                var admin = new User
                {
                    UserName = "admin",
                    Name = "admin",
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword("admin"),
                    Role = "admin"
                };
                await db.Users.AddAsync(admin);

                List<User> users = new List<User>();
                for (int i = 0; i < 100; i++)
                {
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword("user_" + i);
                    // Создаём объект пользователя
                    var user = new User
                    {
                        UserName = "user_" + i,
                        Name = "user_" + i,
                        HashedPassword = hashedPassword,
                        Role = "user"
                    };
                    users.Add(user);
                }
                await db.Users.AddRangeAsync(users);

                await db.SaveChangesAsync();
            }

            // Initialize data for TariffPlans
            var tariffPlans = new TariffPlan[20];
            for (int i = 0; i < 20; i++)
            {
                tariffPlans[i] = new TariffPlan
                {
                    Id = Guid.NewGuid(),
                    Name = $"Plan_{i + 1}",
                    SubscriptionFee = 10.00m + i,
                    LocalCallCost = 0.10m + (i * 0.01m),
                    LongDistanceCallCost = 0.50m + (i * 0.02m),
                    InternationalCallCost = 1.00m + (i * 0.05m),
                    BillingType = (i % 2 == 0) ? "minute" : "second",
                    SmsCost = 0.05m + (i * 0.005m),
                    MmsCost = 0.10m + (i * 0.01m),
                    DataTransferCost = 0.02m + (i * 0.002m)
                };
                db.TariffPlans.Add(tariffPlans[i]);
            }
            db.SaveChanges();

            // Initialize data for Subscribers
            var subscribers = new Subscriber[20];
            for (int i = 0; i < 20; i++)
            {
                subscribers[i] = new Subscriber
                {
                    Id = Guid.NewGuid(),
                    FullName = $"Subscriber_{i + 1}",
                    HomeAddress = $"{100 + i} Main Street",
                    PassportData = $"AB{i:000000}"
                };
                db.Subscribers.Add(subscribers[i]);
            }
            db.SaveChanges();

            // Initialize data for Employees
            var employees = new Employee[20];
            string[] positions = { "Manager", "Consultant", "Technician", "Salesperson" };
            string[] educations = { "Bachelor's Degree", "Associate Degree", "High School Diploma" };
            for (int i = 0; i < 20; i++)
            {
                employees[i] = new Employee
                {
                    Id = Guid.NewGuid(),
                    FullName = $"Employee_{i + 1}",
                    Position = positions[i % positions.Length],
                    Education = educations[i % educations.Length]
                };
                db.Employees.Add(employees[i]);
            }
            db.SaveChanges();

            // Initialize data for ServiceContracts
            var serviceContracts = new ServiceContract[20];
            Random rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                var subscriber = subscribers[rand.Next(subscribers.Length)];
                var tariffPlan = tariffPlans[rand.Next(tariffPlans.Length)];
                var employee = employees[rand.Next(employees.Length)];
                serviceContracts[i] = new ServiceContract
                {
                    Id = Guid.NewGuid(),
                    SubscriberId = subscriber.Id,
                    ContractDate = DateTime.Now.AddDays(-rand.Next(0, 365)),
                    TariffPlanName = tariffPlan.Name,
                    PhoneNumber = $"555-100{rand.Next(0, 999):D3}",
                    EmployeeId = employee.Id
                };
                db.ServiceContracts.Add(serviceContracts[i]);
            }
            db.SaveChanges();

            // Initialize data for ServiceStatistics
            var serviceStatistics = new ServiceStatistic[20];
            for (int i = 0; i < 20; i++)
            {
                var contract = serviceContracts[rand.Next(serviceContracts.Length)];
                serviceStatistics[i] = new ServiceStatistic
                {
                    Id = Guid.NewGuid(),
                    ServiceContractId = contract.Id,
                    CallDuration = rand.Next(0, 500), // in minutes
                    SmsCount = rand.Next(0, 200),
                    MmsCount = rand.Next(0, 50),
                    DataTransferAmount = rand.Next(0, 5000) // in MB
                };
                db.ServiceStatistics.Add(serviceStatistics[i]);
            }
            db.SaveChanges();
        }
    }
}

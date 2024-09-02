using DataAccess;
using Microsoft.EntityFrameworkCore;
using PezzaApi.User.DTO;
using PezzaApi.User.Handlers;

namespace PezzaApi.Tests.User
{
    [TestFixture]
    public class CustomerTests : IDisposable
    {
        private PezzaDbContext dbContext;
        private CustomerHandler handler;
        private List<Guid> addedCustomerIds;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PezzaDbContext>()
                .UseNpgsql("Server=localhost;Port=5432;Database=PezzaTestDb;Username=postgres;Password=admin")
                .Options;

            dbContext = new PezzaDbContext(options);
            dbContext.Database.EnsureCreated();
            handler = new CustomerHandler(dbContext);

            addedCustomerIds = new List<Guid>();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var customerId in addedCustomerIds)
            {
                var customer = dbContext.Customers.Find(customerId);
                if (customer != null)
                {
                    dbContext.Customers.Remove(customer);
                }
            }

            dbContext.SaveChanges();
        }

        [Test, Order(1)]
        public async Task CreateCustomer_ShouldAddCustomerToDatabase()
        {
            var customerDTO = new CustomerDTO
            {
                Name = "Terence",
                Address = "22 Diascia Ave, Karenpark",
                Cellphone = "0760317085",
                Email = "Terence.Frade@Entelect.co.za"
            };

            var createdCustomer = await handler.CreateCustomer(customerDTO);

            // Track the ID of the added Customer
            addedCustomerIds.Add(createdCustomer.Id);

            var customerInDb = await dbContext.Customers.FindAsync(createdCustomer.Id);
            Assert.IsNotNull(customerInDb);
            Assert.AreEqual("Terence", customerInDb.Name);
        }

        [Test, Order(2)]
        public async Task GetCustomer_ShouldReturnCustomerFromDatabase()
        {
            var customerDTO = new CustomerDTO
            {
                Name = "Diana",
                Address = "22 Diascia Ave, Karenpark",
                Cellphone = "0860316082",
                Email = "DianaFrade@gmail.co.za"
            };

            var createdCustomer = await handler.CreateCustomer(customerDTO);
            addedCustomerIds.Add(customerDTO.Id);

            var retrievedCustomer = await handler.GetCustomerById(createdCustomer.Id);
            Assert.IsNotNull(retrievedCustomer);
            Assert.AreEqual("Diana", retrievedCustomer.Name);
        }

        [Test, Order(3)]
        public async Task UpdateCustomer_ShouldUpdateCustomerInDatabase()
        {
            var customerDTO = new CustomerDTO
            {
                Name = "Darrel",
                Address = "Japan",
                Cellphone = "0660316011",
                Email = "Darrel@gmail.co.za"
            };

            var createdCustomer = await handler.CreateCustomer(customerDTO);
            addedCustomerIds.Add(createdCustomer.Id);

            customerDTO.Id = createdCustomer.Id;
            customerDTO.Name = "Darrel Frade";
            customerDTO.Email = "DarrelFrade@gmail.co.za";
            await handler.UpdateCustomer(customerDTO);

            var updatedCustomer = await handler.GetCustomerById(createdCustomer.Id);
            Assert.AreEqual("Darrel Frade", updatedCustomer.Name);
        }

        [Test, Order(4)]
        public async Task DeleteCustomer_ShouldRemoveCustomerFromDatabase()
        {
            var customerDTO = new CustomerDTO
            {
                Name = "Joseph",
                Address = "22 Sugarbush St",
                Cellphone = "0770316023",
                Email = "Joseph@gmail.co.za"
            };

            var createdCustomer = await handler.CreateCustomer(customerDTO);
            addedCustomerIds.Add(createdCustomer.Id);

            await handler.DeleteCustomer(createdCustomer.Id);
            addedCustomerIds.Remove(createdCustomer.Id);

            var CustomerInDb = await dbContext.Customers.FindAsync(createdCustomer.Id);
            Assert.IsNull(CustomerInDb);
        }

        [Test, Order(5)]
        public async Task GetAllCustomers_ShouldGetAllCustomersFromDatabase()
        {
            var Customers = await handler.GetCustomers();

            Assert.IsNotEmpty(Customers);
        }

        public void Dispose()
        {
            // Dispose of the DbContext after all tests have run
            dbContext.Dispose();
        }
    }
}
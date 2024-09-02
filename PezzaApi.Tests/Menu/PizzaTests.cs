using DataAccess;
using Microsoft.EntityFrameworkCore;
using PezzaApi.Menu.DTO;

namespace PezzaApi.Tests.Menu
{
    [TestFixture]
    public class PizzaTests : IDisposable
    {
        private PezzaDbContext dbContext;
        private PizzaHandler pizzaHandler;
        private List<int> addedPizzaIds;  // Track IDs of added pizzas

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<PezzaDbContext>()
                .UseNpgsql("Server=localhost;Port=5432;Database=PezzaTestDb;Username=postgres;Password=admin")
                .Options;

            dbContext = new PezzaDbContext(options);
            dbContext.Database.EnsureCreated(); // Ensure the test database is created
            pizzaHandler = new PizzaHandler(dbContext);

            // Initialize the list of added pizza IDs
            addedPizzaIds = new List<int>();
        }

        [TearDown]
        public void TearDown()
        {
            // Remove only the pizzas added in the current test
            foreach (var pizzaId in addedPizzaIds)
            {
                var pizza = dbContext.Pizzas.Find(pizzaId);
                if (pizza != null)
                {
                    dbContext.Pizzas.Remove(pizza);
                }
            }

            dbContext.SaveChanges();  // Commit the removals
        }

        [Test, Order(1)]
        public async Task CreatePizza_ShouldAddPizzaToDatabase()
        {
            var pizzaDTO = new PizzaDTO
            {
                Name = "Hawaiian",
                Description = "Pineapple and Ham",
                Price = 120
            };

            var createdPizza = await pizzaHandler.CreatePizza(pizzaDTO);

            // Track the ID of the added pizza
            addedPizzaIds.Add(createdPizza.Id);

            var pizzaInDb = await dbContext.Pizzas.FindAsync(createdPizza.Id);
            Assert.IsNotNull(pizzaInDb);
            Assert.AreEqual("Hawaiian", pizzaInDb.Name);
        }

        [Test, Order(2)]
        public async Task GetPizza_ShouldReturnPizzaFromDatabase()
        {
            var pizzaDTO = new PizzaDTO
            {
                Name = "Margherita",
                Description = "Classic Cheese",
                Price = 100
            };

            var createdPizza = await pizzaHandler.CreatePizza(pizzaDTO);
            addedPizzaIds.Add(pizzaDTO.Id);

            var retrievedPizza = await pizzaHandler.GetPizzaById(createdPizza.Id);
            Assert.IsNotNull(retrievedPizza);
            Assert.AreEqual("Margherita", retrievedPizza.Name);
        }

        [Test, Order(3)]
        public async Task UpdatePizza_ShouldUpdatePizzaInDatabase()
        {
            var pizzaDTO = new PizzaDTO
            {
                Name = "Pepperoni",
                Description = "Spicy Pepperoni",
                Price = 150
            };

            var createdPizza = await pizzaHandler.CreatePizza(pizzaDTO);
            addedPizzaIds.Add(createdPizza.Id);

            pizzaDTO.Id = createdPizza.Id;
            pizzaDTO.Name = "Updated Pepperoni";
            await pizzaHandler.UpdatePizza(pizzaDTO);

            var updatedPizza = await pizzaHandler.GetPizzaById(createdPizza.Id);
            Assert.AreEqual("Updated Pepperoni", updatedPizza.Name);
        }

        [Test, Order(4)]
        public async Task DeletePizza_ShouldRemovePizzaFromDatabase()
        {
            var pizzaDTO = new PizzaDTO
            {
                Name = "Four Seasons",
                Description = "A mix of toppings",
                Price = 180
            };

            var createdPizza = await pizzaHandler.CreatePizza(pizzaDTO);
            addedPizzaIds.Add(createdPizza.Id);

            await pizzaHandler.DeletePizza(createdPizza.Id);
            addedPizzaIds.Remove(createdPizza.Id); // Remove from list as it's deleted

            var pizzaInDb = await dbContext.Pizzas.FindAsync(createdPizza.Id);
            Assert.IsNull(pizzaInDb);
        }

        [Test, Order(5)]
        public async Task GetAllPizzas_ShouldGetAllPizzasFromDatabase()
        {
            var pizzas = await pizzaHandler.GetPizzas();

            Assert.IsNotEmpty(pizzas);
        }

        public void Dispose()
        {
            // Dispose of the DbContext after all tests have run
            dbContext.Dispose();
        }
    }
}
using PezzaApi.DTO;

namespace PezzaApi.Common.Interfaces
{
    public interface IPizzaHandler
    {
        Task<IEnumerable<Pizza>> GetPizzas();

        Task<Pizza> GetPizzaById(Guid id);

        Task UpdatePizza(Guid id, Pizza pizza);

        Task<Pizza> CreatePizza(Pizza pizza);

        Task DeletePizza(Guid id);
    }
}
using PezzaApi.Menu.DTO;

namespace PezzaApi.Menu.Interfaces
{
    public interface IPizzaHandler
    {
        Task<IEnumerable<PizzaDTO>> GetPizzas();

        Task<PizzaDTO> GetPizzaById(Guid id);

        Task UpdatePizza(PizzaDTO pizzaDTO);

        Task<PizzaDTO> CreatePizza(PizzaDTO pizzaDTO);

        Task DeletePizza(Guid id);
    }
}
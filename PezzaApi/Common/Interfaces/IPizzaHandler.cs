using PezzaApi.DTO;

namespace PezzaApi.Common.Interfaces
{
    public interface IPizzaHandler
    {
        Task<IEnumerable<PizzaDTO>> GetPizzas();

        Task<PizzaDTO> GetPizzaById(Guid id);

        Task UpdatePizza(Guid id, PizzaDTO pizza);

        Task<PizzaDTO> CreatePizza(PizzaDTO pizza);

        Task DeletePizza(Guid id);
    }
}
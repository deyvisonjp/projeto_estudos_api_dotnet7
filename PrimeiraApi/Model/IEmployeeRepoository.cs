namespace PrimeiraApi.Model
{
    public interface IEmployeeRepoository
    {
        void Add(Employee employee);
        Task<List<Employee>> GetPerPageAsync(int pageNumber, int pageQuantity);
        Task<List<Employee>> GetAllAsync();
        Employee? Get(int id);
        int GetTotalCount();
    }
}

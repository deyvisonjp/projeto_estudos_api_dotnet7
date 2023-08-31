namespace PrimeiraApi.Model
{
    public interface IEmployeeRepoository
    {
        void Add(Employee employee);
        List<Employee> GetPerPage(int pageNumber, int pageQuantity);
        List<Employee> GetAll();
        Employee? Get(int id);
    }
}

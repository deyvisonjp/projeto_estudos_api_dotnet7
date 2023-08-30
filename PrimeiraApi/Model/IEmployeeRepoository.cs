namespace PrimeiraApi.Model
{
    public interface IEmployeeRepoository
    {
        void Add(Employee employee);
        List<Employee> GetAll();
        Employee? Get(int id);
    }
}

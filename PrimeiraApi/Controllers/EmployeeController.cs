using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimeiraApi.Infra;
using PrimeiraApi.Model;
using PrimeiraApi.ViewModel;

namespace PrimeiraApi.Controllers
{
    //https://www.youtube.com/watch?v=0_V-xHiRbgY
    [Route("api/v1/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepoository _employeeRepoository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepoository employeeRepoository, ILogger<EmployeeController> logger)
        {
            _employeeRepoository = employeeRepoository ?? throw new ArgumentNullException(nameof(_employeeRepoository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            var filePath = Path.Combine("Storage", employeeView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeView.Photo.CopyTo(fileStream);

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath);

            _employeeRepoository.Add(employee);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepoository.Get(id);

            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

            return File(dataBytes, "image/jpeg");
        }

        [HttpGet]
        [Route("getPerPage")]
        public async Task<IActionResult> GetPerPageAsync(int pageNumber, int pageQuantity)
        {
            if (pageNumber < 1 || pageQuantity < 1)
            {
                return BadRequest("pageNumber e pageQuantity devem ser maiores ou iguais a 1.");
            }

            if (pageQuantity > 1000)
            {
                _logger.Log(LogLevel.Error, "Usuário fez uma consulta maior que mil registros");
                return BadRequest("Tantos registros de uma só vez poderão causar travamento na consulta ao banco de dados.");
            }

            try
            {
                var totalEmployees = _employeeRepoository.GetTotalCount();
                var employees = await _employeeRepoository.GetPerPageAsync(pageNumber-1, pageQuantity);

                //if(totalEmployees < pageNumber * pageQuantity) 
                //{
                //    throw new Exception($"Existem apenas {totalEmployees}, página não existe!");
                //}

                _logger.LogInformation("Paginação realizada com sucesso!");
                return Ok(new
                {
                    totalEmployees,
                    pageNumber,
                    pageQuantity,
                    employees
                });
            }
            catch (Exception ex)
            {
                // Trate o erro de forma apropriada, como logando-o ou retornando uma resposta de erro.
                return StatusCode(500, "Ocorreu um erro ao buscar os registros.");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get() 
        {
            var employees = _employeeRepoository.GetAllAsync();
            return Ok(employees);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FolhaPagamento.Data;
using FolhaPagamento.Models;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/funcionario")]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public EmployeeController(AppDataContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("listar")]
        public IActionResult GetAll()
        {
            try
            {
                // Ajuste a consulta LINQ para não incluir a propriedade Address
                List<Employee> employees = _ctx.Employees.ToList();

                return employees.Count == 0 ? NotFound() : Ok(employees);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("getByString/{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            try
            {
                Employee? employee = _ctx.Employees
                    .FirstOrDefault(x => x.nome == name);

                if (employee != null)
                {
                    return Ok(employee);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Post([FromBody] Employee employee)
        {
            try
            {
                // Remova a consulta para encontrar o endereço
                // Address address = _ctx.Adresses.Find(employee.AddressId);

                // Verifique se o objeto Employee está correto
                if (employee == null)
                {
                    return BadRequest("Dados inválidos para o funcionário.");
                }

                // Remova a atribuição do endereço
                // employee.Address = address;

                _ctx.Employees.Add(employee);
                _ctx.SaveChanges();

                return Created("", employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("put/{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] Employee employee)
        {
            try
            {
                // Remova a consulta para encontrar o endereço
                // Address address = _ctx.Adresses.Find(employee.AddressId);

                Employee? existingEmployee = _ctx.Employees.FirstOrDefault(x => x.EmployeeId == id);

                if (existingEmployee != null)
                {
                    existingEmployee.nome = employee.nome;
                    existingEmployee.cpf = employee.cpf;

                    _ctx.Employees.Update(existingEmployee);
                    _ctx.SaveChanges();

                    return Ok();
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                Employee? employee = _ctx.Employees.Find(id);

                if (employee != null)
                {
                    _ctx.Employees.Remove(employee);
                    _ctx.SaveChanges();
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}


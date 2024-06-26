using CronoManage.Aplication.Services;
using CronoManage.Domain.Entities;
using CronoManage.Domain.Validations;
using Microsoft.AspNetCore.Mvc;

namespace CronoManage.Presentation.Controllers
{
    [ApiController]
    [Route("projeto")]
    public class MyProjectController : ControllerBase
    {
        private readonly MyProjectServices _services;
        public MyProjectController(MyProjectServices services)
        {
            _services = services;
        }

        [HttpGet("obter-todos")]
        public async Task<ActionResult<string>> FoundProjects()
        {
            var dataProject = await _services.GetAllProjects();

            if (dataProject.Count() == 0)
            {
                return NotFound("Não foi encontrado nenhum projeto");
            }

            return Ok(dataProject);
        }

        [HttpGet("obter-por-id")]
        public async Task<IActionResult> GetOne(int id)
        {
            var dataProject = await _services.FindByIdAsync(id);

            if (dataProject == null)
            {
                return NotFound(id);
            }
            return Ok(dataProject);
        }

        [HttpPost("adicionar-projeto")]
        public async Task<IActionResult> StartProject(MyProjectValidation project)
        {
            var dataProject = await _services.FindByNameAsync(project.Name);

            if (dataProject == null)
            {
                dataProject = new MyProject()
                {
                    Name = project.Name,
                    Description = project.Description
                };

                await _services.CreateAsync(dataProject);
                return Ok(dataProject);
            }
            return BadRequest("Projeto já existente");
        }

        [HttpPost("encerrar-projeto")]
        public async Task<IActionResult> StopProject(int id)
        {
            var dataProject = await _services.FindByIdAsync(id);

            if (dataProject != null)
            {
                await _services.StopProjectAsync(dataProject);

                return Ok(dataProject);
            }
            return NotFound(id);
        }


        [HttpPut("atualizar-projeto")]
        public async Task<IActionResult> UpdateProject(int id, MyProjectValidation project)
        {
            var dataProject = await _services.FindByIdAsync(id);

            if (dataProject != null)
            {
                try
                {
                    await _services.UpdateAsync(dataProject, project);

                    var dataNew = await _services.FindByIdAsync(id);

                    return Ok(dataNew);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return NotFound(id);
        }

        [HttpDelete("deletar-projeto")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var dataProject = await _services.FindByIdAsync(id);

            if (dataProject != null)
            {
                await _services.DeleteAsync(dataProject);

                return Ok("Deletado com Sucesso!");
            }
            return NotFound(id);
        }
    }
}

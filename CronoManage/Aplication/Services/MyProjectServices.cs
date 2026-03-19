using CronoManage.Domain.Entities;
using CronoManage.Domain.Validations;
using CronoManage.Infraestructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CronoManage.Aplication.Services
{
    public class MyProjectServices
    {
        private readonly CronoContext _context;

        public MyProjectServices(CronoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MyProject>> GetAllProjects()
        {
            var dataProject = await _context.Projects.ToListAsync();
            return dataProject;
        }

        public async Task<MyProject?> FindByNameAsync(string name)
        {
            var dataProject = await _context.Projects.FirstOrDefaultAsync(x => x.Name == name);

            return dataProject;
        }

        public async Task<MyProject?> FindByIdAsync(int id)
        {
            var dataProject = await _context.Projects.FindAsync(id);

            return dataProject;
        }

        public async Task CreateAsync(MyProject project)
        {
            project.StartDate = DateTime.Now;
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task StopProjectAsync(MyProject project)
        {
            project.EndDate = DateTime.Now;
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(MyProject project)
        {
            _context.Projects.Remove(project);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task<Task> UpdateAsync(MyProject project, MyProjectValidation newProject)
        {
            var dataProject = _context.Projects.ToList();

            if (dataProject.Any(x => x.Name == newProject.Name))
                throw new ArgumentException("O nome do projeto deve ser único!!!");

            project.Name = newProject.Name;
            project.Description = newProject.Description;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
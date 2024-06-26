using CronoManage.Domain.Entities;
using CronoManage.Domain.Validations;
using CronoManage.Infraestructure.Data.Context;
using System.Diagnostics;

namespace CronoManage.Aplication.Services
{
    public class MyProjectServices
    {
        private readonly CronoContext _context;
        private readonly Stopwatch _stopwatch;

        public MyProjectServices(CronoContext context, Stopwatch stopWatch)
        {
            _context = context;
            _stopwatch = stopWatch;
        }

        public async Task<IEnumerable<MyProject>> GetAllProjects()
        {
            var dataProject = _context.Projects.ToList();

            return dataProject;
        }
        public async Task<MyProject?> FindByNameAsync(string name)
        {
            var dataProject = _context.Projects.ToList().Find(x => x.Name == name);

            return dataProject;
        }

        public async Task<MyProject?> FindByIdAsync(int id)
        {
            var dataProject = _context.Projects.ToList().Find(x => x.Id == id);

            return dataProject;
        }

        public Task CreateAsync(MyProject project)
        {
            _stopwatch.Start();
            project.Elapsed = _stopwatch.Elapsed;

            _context.Projects.Add(project);
            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public Task StopProjectAsync(MyProject project)
        {
            _stopwatch.Stop();
            project.Elapsed = _stopwatch.Elapsed;

            _context.Projects.Update(project);
            _context.SaveChanges();

            _stopwatch.Reset();

            return Task.CompletedTask;
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

            if (!dataProject.Any(x => x.Name == newProject.Name))
            {
                project.Name = newProject.Name;
                project.Description = newProject.Description;

                _context.Update(project);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            throw new ArgumentException("O nome do projeto deve ser único!!!");
        }
    }
}

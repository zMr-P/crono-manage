using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CronoManage.Domain.Entities
{
    public class MyProject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Elapsed { get; set; }
    }
}

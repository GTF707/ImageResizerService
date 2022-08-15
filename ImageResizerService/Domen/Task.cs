using System.Threading.Tasks;
using FreeSql;

namespace ImageResizerService.Domen
{
    public class Task
    {

        public Task(string name, long id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; set; }
        public long Id { get; set; }
    }
}


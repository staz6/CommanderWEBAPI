using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly DataContext _context;
        public CommandRepo(DataContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if(command==null)
            {
                throw new System.ArgumentNullException(nameof(command));

            }
            _context.Commands.Add(command);
        }

        public void Delete(Command command)
        {
            _context.Commands.Remove(command);
        }

        public IEnumerable<Command> getAllCommand()
        {
            return _context.Commands.ToList();
        }

        public Command getCommandByIp(int id)
        {
            return _context.Commands.FirstOrDefault(x=>x.Id==id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >=0);
        }
    }
}
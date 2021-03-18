using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();
         IEnumerable<Command> getAllCommand();
         Command getCommandByIp(int id);
         void CreateCommand(Command command);

         void Delete(Command command);
    }
}
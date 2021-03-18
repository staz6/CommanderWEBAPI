using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommandRepo : ICommandRepo
    {
        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> getAllCommand()
        {
            var command = new List<Command>()
            {
                new Command{Id=0,HowTo="ABC",Line="QWE",Platform="QWERTY"},
                new Command{Id=1,HowTo="ABC",Line="QWE",Platform="QWERTY"},
                new Command{Id=2,HowTo="ABC",Line="QWE",Platform="QWERTY"}
            };
            return command;
        }

        public Command getCommandByIp(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}
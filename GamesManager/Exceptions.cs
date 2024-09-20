using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesManager
{
    public class IncompleteConfigurationException : Exception
    {
        public IncompleteConfigurationException() : base("Incomplete configuration")
        {
        }

        public IncompleteConfigurationException(string message) : base(message)
        {
        }
    }
}

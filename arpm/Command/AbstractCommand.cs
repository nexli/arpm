using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpm.Command
{
    abstract class AbstractCommand<OPT> : ICommand<OPT>
    {
        public OPT Option { get; set; }

        public abstract void Execute();
    }
}

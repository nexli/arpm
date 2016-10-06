using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arpm.Command
{
    interface ICommand<OPT>
    {
        OPT Option { get; set; }

        void Execute();
    }
}

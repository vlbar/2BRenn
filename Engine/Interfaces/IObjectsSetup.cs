using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoBRenn.Engine.Interfaces
{
    interface IObjectsSetup
    {
        HashSet<RennObject> GetObjects();
    }
}

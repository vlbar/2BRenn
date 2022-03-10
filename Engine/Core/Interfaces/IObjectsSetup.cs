using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoBRenn.Engine.Core;

namespace TwoBRenn.Engine.Interfaces
{
    interface IObjectsSetup
    {
        HashSet<RennObject> GetObjects();
    }
}

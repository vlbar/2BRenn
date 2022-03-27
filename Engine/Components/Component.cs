﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoBRenn.Engine.Components
{
    abstract class Component
    {
        public RennObject rennObject { get; set; }
        public abstract void OnUpdate();
        public abstract void OnUnload();
    }
}

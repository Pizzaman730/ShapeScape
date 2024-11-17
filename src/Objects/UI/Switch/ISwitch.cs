using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeScape
{
    public interface ISwitch
    {
        public void ToggleOn();
        public void ToggleOff();
    }
}
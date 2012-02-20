using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kirby
{
    interface IStateable
    {        
        void AddState(int state);
        void RemoveState(int state);
        bool HasState(int state);
    }
}

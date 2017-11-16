using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEngine.Log
{
   public interface ILog
    {
        void LogException(Exception message);
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ironbound {
    class Program {
        static void Main(string[] args) {
            while (true) {
                DebugManager.LogInfo($"test");
                System.Threading.Thread.Sleep(1000);
                DebugManager.LogError($"help");
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}

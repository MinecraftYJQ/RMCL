using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Launcher.cs.Make_Skin
{
    internal class Skin_Initialize
    {
        public Skin_Initialize()
        { 
            var command = new Command("md RMCL\\Skin",true);
            command = new Command("md .minecraft", true);
            command = new Command("md .minecraft\\resourcepacks", true);
            command = new Command("md RMCL\\Skin\\assets", true);
            command = new Command("md RMCL\\Skin\\assets\\minecraft", true);
            command = new Command("md RMCL\\Skin\\assets\\minecraft\\textures", true);
            command = new Command("md RMCL\\Skin\\assets\\minecraft\\textures\\entity", true);
            command = new Command("md RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player", true);
            command = new Command("md RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\wide", true);
            command = new Command("md RMCL\\Skin\\assets\\minecraft\\textures\\entity\\player\\slim", true);

            Thread.Sleep(100);
            //File.WriteAllBytes("RMCL\\Sink\\assets\\minecraft\\textures\\entity\\player\\Steve.png", File.ReadAllBytes("RMCL\\Sink.png"));

        }
    }
}

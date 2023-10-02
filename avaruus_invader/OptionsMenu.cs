using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_CsLo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace avaruus_invader
{
    internal class OptionsMenu
    {
        public void StartOptions()
        {
            RayGui.GuiTextBox(new Rectangle(280, 50, 90, 20), "Options Menu", 20, false);
            RayGui.GuiTextBox(new Rectangle(280, 100, 90, 20), "Vaikeustaso", 20, false);
            RayGui.GuiButton(new Rectangle(240,120,60,20),"Easy");
            RayGui.GuiButton(new Rectangle(280, 120, 60, 20), "Medium");
            RayGui.GuiButton(new Rectangle(340, 120, 60, 20), "Hard");
            RayGui.GuiTextBox(new Rectangle(280, 140, 90, 20), "Move with keyboard||mouse", 20, false);

        }
    }
}

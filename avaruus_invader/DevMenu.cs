using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace avaruus_invader
{
    internal class DevMenu
    {
        public event EventHandler ResetButtonPressedEvent;
        public event EventHandler DeactivateButtonPressedEvent;
        public void StartDev()
        {
            RayGui.GuiTextBox(new Rectangle(280, 50, 90, 20), "Developer menu", 20, false);
            if(RayGui.GuiButton(new Rectangle(280, 250, 70, 20), "Reset game"))
            {
                ResetButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }    
            if(RayGui.GuiButton(new Rectangle(280, 300, 100, 20), "Deactivate enemies"))
            {
                DeactivateButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }
            RayGui.GuiTextBox(new Rectangle(280, 350, 90, 20), "Set score", 20, false);
        }
    }
}

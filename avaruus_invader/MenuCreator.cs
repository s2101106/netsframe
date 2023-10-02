using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaruus_invader
{
    internal class MenuCreator
    {
        private int x;
        private int y;
        private int rowHeight;
        private int menuWidth;
        
        public MenuCreator(int x, int y, int rowHeight,int menuWidth) 
        { 
            this.x = x;
            this.y = y;
            this.rowHeight = rowHeight;
            this.menuWidth = menuWidth;
        
        }

        public void Text(string text)
        {
            RayGui.GuiTextBox(new Rectangle(x, y, menuWidth, rowHeight),
                text, 20, false);
            y += rowHeight;
        }
 
        public bool Button(string text)
        {
            bool painos;
            RayGui.GuiButton(new Rectangle(x, y, menuWidth, rowHeight), text);
            if (RayGui.GuiButton(new Rectangle(x, y, menuWidth, rowHeight), text))
            {
                painos= true;
            }
            else
            {
                painos= false;
            }
            y += rowHeight;
            return painos;

        }
    }
}

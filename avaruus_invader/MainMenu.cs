using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaruus_invader
{
    class MainMenu
    {
        public event EventHandler StartButtonPressedEvent;
        public event EventHandler EndButtonPressedEvent;
        public void StartMain()
        {
            MenuCreator menucreator = new MenuCreator(280, 50, 50, 200);
            menucreator.Text("Space Invaders");
                menucreator.Text("Spacebar - ampuu \n A,D tai hiiri - " +
                    "liikkuu sivulta sivlle");
            if(menucreator.Button("Aloita peli"))
            {
                StartButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }
            if(menucreator.Button("Lopeta peli"))
            {
                EndButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }
        }

    }
}

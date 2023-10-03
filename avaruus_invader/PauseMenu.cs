using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaruus_invader
{
    class PauseMenu
    {
        public event EventHandler ResumeButtonPressedEvent;
        public event EventHandler MainButtonPressedEvent;
        public event EventHandler OptionsButtonPressedEvent;
        public event EventHandler RestartButtonPressedEvent;
        
        public void StartPause()
        {
            MenuCreator menucreator1 = new MenuCreator(280, 50, 50, 200);
            menucreator1.Text("Space Invaders");
            menucreator1.Text("Spacebar - ampuu \n A,D tai hiiri - " +
                "liikkuu sivulta sivlle");
            if (menucreator1.Button("Jatka peliä"))
            {
                ResumeButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }
            if (menucreator1.Button("Aloitusvalikko"))
            {
                MainButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }
            if (menucreator1.Button("Options"))
            {
                OptionsButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }
            if(menucreator1.Button("Aloita alusta"))
            {
                RestartButtonPressedEvent.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

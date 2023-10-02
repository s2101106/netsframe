using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace avaruus_invader
{
    internal class Player
    {

        public TransformComponent transform { get; private set; }
        public CollisionComponent collision;
        SpriteRenderer spriteRenderer;
        public bool active=false;
        double shootInterval = 0.3;
        double lastShootTime;
        Vector2 mousePos;

        public Player(Vector2 startPos, float speed, int size,Texture image, Color color)
        {
            
            transform = new TransformComponent(startPos, new Vector2(0, 0), speed);
            collision = new CollisionComponent(new Vector2(size, size));
            spriteRenderer=new SpriteRenderer(image,color,transform,collision);
            lastShootTime = -shootInterval;
            active= true;
        }
        /// <summary>
        /// Kuuntelee näppäimistöä ja päivittää hahmoa
        /// </summary>
        /// <returns>True jos voi ja haluaa ampua.</returns>
        public bool Update()
        {
            mousePos=Raylib.GetMousePosition();
            float deltaTime = Raylib.GetFrameTime();
            if (mousePos.X < 300 && active == true&&mousePos.Y>150&&Invaders.moveMouse==true)
            {
                transform.position.X -= transform.speed * deltaTime;
            }
            if (mousePos.X > 400 && active == true&& mousePos.Y > 150&&Invaders.moveMouse==true)
            {
                transform.position.X += transform.speed * deltaTime;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)&&active==true&&Invaders.moveMouse==false)
            {
                transform.position.X -= transform.speed * deltaTime;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D)&&active==true&&Invaders.moveMouse==false)
            {
                transform.position.X += transform.speed * deltaTime;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && active == true)
            {
                double timeNow = Raylib.GetTime();
                double timeSinceLastShot = timeNow - lastShootTime;
                if (timeSinceLastShot >= shootInterval)
                {
                    lastShootTime = timeNow;
                    return true;
                }
            }
            return false;

        }

        public void Draw()
        {
            if (active)
            {
                Raylib.DrawRectangleV(transform.position, collision.size, Raylib.SKYBLUE);
                spriteRenderer.Draw();

            }
        }
    }
}

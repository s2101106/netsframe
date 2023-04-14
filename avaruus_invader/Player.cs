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

        double shootInterval = 0.3;
        double lastShootTime;

        public Player(Vector2 startPos, float speed, int size)
        {
            transform = new TransformComponent(startPos, new Vector2(0, 0), speed);
            collision = new CollisionComponent(new Vector2(size, size));

            lastShootTime = -shootInterval;
        }

        public bool Update()
        {
            float deltaTime = Raylib.GetFrameTime();
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                transform.position.X -= transform.speed * deltaTime;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                transform.position.X += transform.speed * deltaTime;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
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
            Raylib.DrawRectangleV(transform.position, collision.size, Raylib.SKYBLUE);
        }
    }
}

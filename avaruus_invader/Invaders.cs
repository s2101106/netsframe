using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace avaruus_invader
{
    internal class Invaders
    {
        int window_width = 640;
        int window_height = 420;

        Player player;
        List<Bullet> bullets;
        Enemy enemy;

        public void Run()
        {
            Init();
            GameLoop();
        }

        void Init()
        {

            Raylib.InitWindow(window_width, window_height, "Space Invaders Demo");
            Raylib.SetTargetFPS(30);

            float playerSpeed = 120;
            int playerSize = 40;
            Vector2 playerStart = new Vector2(window_width / 2, window_height - playerSize * 2);
            player = new Player(playerStart, playerSpeed, playerSize);

            bullets = new List<Bullet>();

            Vector2 enemyStart = new Vector2(window_width / 2, playerSize * 2);
            enemy = new Enemy(enemyStart, new Vector2(1, 0), playerSpeed, playerSize);
        }
        void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                // UPDATE
                Update();
                // DRAW
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Raylib.YELLOW);
                Draw();
                Raylib.EndDrawing();
            }
        }
        void Update()
        {
            bool playerShoots = player.Update();
            KeepInsideArea(player.transform, player.collision,
                0, 0, window_width, window_height);
            if (playerShoots)
            {
                Bullet bullet = new Bullet(player.transform.position,
                    new Vector2(0, -1),
                    300, 20);

                bullets.Add(bullet);
            }

            Rectangle enemyRec = getRectangle(enemy.transform, enemy.collision);
            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
                Rectangle bulletRec = getRectangle(bullet.transform, bullet.collision);
                if (Raylib.CheckCollisionRecs(bulletRec, enemyRec))
                {
                    if (enemy.active)
                    {

                        enemy.active = false;
                    }
                }
            }

            enemy.Update();
            bool enemyOut = KeepInsideArea(enemy.transform, enemy.collision, 0, 0, window_width, window_height);
            if (enemyOut)
            {
                enemy.transform.direction.X *= -1.0f;
            }
        }

        Rectangle getRectangle(TransformComponent t, CollisionComponent c)
        {
            Rectangle r = new Rectangle(t.position.X,
                t.position.Y, c.size.X, c.size.Y);
            return r;
        }

        bool KeepInsideArea(TransformComponent transform, CollisionComponent collision,
            int left, int top, int right, int bottom)
        {
            float newX = Math.Clamp(transform.position.X, left, right - collision.size.X);
            float newY = Math.Clamp(transform.position.Y, top, bottom - collision.size.Y);

            bool xChange = newX != transform.position.X;
            bool yChange = newY != transform.position.Y;

            transform.position.X = newX;
            transform.position.Y = newY;

            return xChange || yChange;
        }

        void Draw()
        {
            player.Draw();

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw();
            }

            enemy.Draw();
        }
    }
}

﻿using Raylib_CsLo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace avaruus_invader
{
    internal class Invaders
    {
        
        enum GameState
        {
            Start,
            Play,
            ScoreScreen,
            Pause,
            Options,
            DevMenu
        }
        GameState state;
        int window_width = 640;
        int window_height = 420;
        float score_float = 0.0f;
        float move = 0.0f;
        public double doubleTime = 0.0f;
        public int Time = 0;
        int move1=0;
        static public bool moveMouse = false;
        Player player;
        List<Bullet> bullets;
        List<Bullet> enemyBullets;
        List<Enemy> enemies;
        Enemy enemy;
        int placeX = 0;
        int placeY = 0;
        int enemiesAlive = 0;
        int score = 0;
        int health = 10;
        // Two rows, with 4 enemies in each
        int rows = 2;
        int columns = 4;

        int enemySize = 20;
        int spaceBetween = 40;

        int finalscore = 0;
        public int enemiesShot { get; set; }
        //Texture playerImage;
        //List<Texture> enemyImages;
        Texture bulletImage;
        Texture enemyImage;
        Texture playerImage;
        Texture enemyBulletImage;

        Sound ampuu;
        Sound playerdmg;
        Sound enemydmg;

        


        public void Run()
        {
            Init();
            GameLoop();
        }

        void Init()
        {
            Raylib.InitAudioDevice();
            Raylib.InitWindow(window_width, window_height, "Space Invaders Demo");

            Raylib.SetTargetFPS(30);
            Raylib.SetExitKey(KeyboardKey.KEY_DELETE);
            enemyImage = Raylib.LoadTexture("kuvat\\enemyBlack1.png");
            bulletImage = Raylib.LoadTexture("kuvat\\laserRed08.png");
            playerImage = Raylib.LoadTexture("kuvat\\ufoRed.png");
            enemyBulletImage = Raylib.LoadTexture("kuvat\\laserGreen15.png");

            

            ampuu = Raylib.LoadSound("aanet\\blaster-2-81267.wav");
            playerdmg = Raylib.LoadSound("aanet\\punch-2-123106.wav");
            enemydmg = Raylib.LoadSound("aanet\\glass-breaking-93803.wav");

            float playerSpeed = 120;
            int playerSize = 40;
            Vector2 playerStart = new Vector2(window_width / 2, window_height - playerSize * 2);
            player = new Player(playerStart, playerSpeed, playerSize, playerImage, Raylib.RED);

            bullets = new List<Bullet>();
            enemyBullets = new List<Bullet>();

            enemies = new List<Enemy>();
            for (int row = 0; row < rows; row++)
            {
                placeX = 0;  // Before new row, reset x position to left border
                for (int column = 0; column < columns; column++)
                {
                    Vector2 place = new Vector2(placeX, placeY);
                    enemies.Add(new Enemy(place, new Vector2(1, 0), playerSpeed, playerSize, enemyImage));
                    placeX += enemySize + spaceBetween; // Space between enemies
                }
                placeY += enemySize + spaceBetween; // Space between rows
            }

            Vector2 enemyStart = new Vector2(window_width / 2, playerSize * 2);
            //enemy = new Enemy(enemyStart, new Vector2(1, 0), playerSpeed, playerSize,enemyImage);
            enemiesAlive = enemies.Count;
        }
        void GameLoop()
        {
            while (Raylib.WindowShouldClose() == false)
            {
                Raylib.BeginDrawing();
                switch (state)
                {
                    case GameState.Start:
                        Raylib.ClearBackground(Raylib.BLACK);
                        UpdateStart();
                        break;
                    case GameState.Play:
                        // normaali peli
                        Raylib.ClearBackground(Raylib.YELLOW);
                        UpdateGame();
                        DrawGame();
                        break;
                    case GameState.ScoreScreen:
                        // näytä lopputulos ja odota kunnes pelaaja haluaa aloittaa uudestaan
                        Raylib.ClearBackground(Raylib.BLACK);
                        UpdateScore();
                        DrawScore();
                        break;
                    case GameState.Pause:
                        Raylib.ClearBackground(Raylib.BLACK);
                        PauseMen();
                        break;
                    case GameState.Options:
                        Raylib.ClearBackground(Raylib.BLACK);
                        OptionsMen(); 
                        break;
                    case GameState.DevMenu:
                        Raylib.ClearBackground(Raylib.BLACK);
                        DevMen();
                        break;
                }

                Raylib.EndDrawing();
            }
            Raylib.CloseAudioDevice();
            Raylib.UnloadSound(ampuu);
            Raylib.UnloadSound(enemydmg);
            Raylib.UnloadSound(playerdmg);
        }
        /// <summary>
        /// Aloitetaan peli alusta
        /// </summary>
        void ResetGame()
        {
            foreach(Bullet enemyBullet in enemyBullets)
            {
                enemyBullet.active = false;
            }
            float playerSpeed = 120;
            int playerSize = 40;
            Vector2 playerStart = new Vector2(window_width / 2, window_height - playerSize * 2);
            player = new Player(playerStart, playerSpeed, playerSize, playerImage, Raylib.RED);
            bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            rows = 2;
            columns = 4;
            enemySize = 20;
            spaceBetween = 40;
            placeY = 20;
            score = 0;
            health = 10;
            for (int row = 0; row < rows; row++)
            {
                placeX = 0;  // Before new row, reset x position to left border
                for (int column = 0; column < columns; column++)
                {
                    Vector2 place = new Vector2(placeX, placeY);
                    enemies.Add(new Enemy(place, new Vector2(1, 0), playerSpeed, playerSize, enemyImage));
                    placeX += enemySize + spaceBetween; // Space between enemies
                }
                placeY += enemySize + spaceBetween; // Space between rows
            }

            //Vector2 enemyStart = new Vector2(window_width / 2, playerSize * 2);
            //enemy = new Enemy(enemyStart, new Vector2(1, 0), playerSpeed, playerSize,enemyImage);
            enemiesAlive = enemies.Count;
        }
        void UpdateGame()
        {
            doubleTime = Raylib.GetTime();
            if (move1 == 1)
            {
                moveMouse = true;
            }
            if (move1 == 0)
            {
                moveMouse = false;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                state = GameState.Pause;
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_CAPS_LOCK))
            {
                state = GameState.DevMenu;
            }
            if (enemiesAlive == 0 || health == 0)
            {
                finalscore = score;
                state = GameState.ScoreScreen;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_ENTER) && enemiesAlive == 0 || Raylib.IsKeyDown(KeyboardKey.KEY_ENTER) && health == 0)
            {
                ResetGame();
            }

            bool playerShoots = player.Update();
            KeepInsideArea(player.transform, player.collision,
                0, 0, window_width, window_height);
            if (playerShoots && enemiesAlive > 0 && health > 0)
            {
                Raylib.PlaySound(ampuu);
                Bullet bullet = new Bullet(player.transform.position,
                    new Vector2(0, -1),
                    300, 20, bulletImage, Raylib.RED);

                bullets.Add(bullet);
            }

            foreach (Enemy enemy in enemies)
            {
                bool enemyShoots = enemy.Update();
                if (enemyShoots && enemiesAlive > 0 && health > 0)
                {
                    Bullet bullet = new Bullet(enemy.transform.position,
                    new Vector2(0, 1),
                    300, 20, enemyBulletImage, Raylib.RED);
                    enemyBullets.Add(bullet);
                }
                if (health > 0)
                {
                    enemy.Update();

                }
                bool enemyOut = KeepInsideArea(enemy.transform, enemy.collision, 0, 0, window_width, window_height);
                if (enemyOut)
                {
                    enemy.transform.direction.X *= -1.0f;
                    enemy.transform.position.Y += 20.0f;
                }
            }
            foreach (Bullet enemyBullet in enemyBullets)
            {
                if (health > 0)
                {
                    enemyBullet.Update();

                }
                bool enemyBulletOut = KeepInsideArea(enemyBullet.transform, enemyBullet.collision, 0, -20, window_width, window_height);
                if (enemyBulletOut)
                {
                    enemyBullet.active = false;
                }
                Rectangle bulletRec = getRectangle(enemyBullet.transform, enemyBullet.collision);
                Rectangle playerRec = getRectangle(player.transform, player.collision);
                if (Raylib.CheckCollisionRecs(bulletRec, playerRec))
                {
                    health -= 1;
                    enemyBullet.transform.position.Y = 500.0f;
                    enemyBullet.active = false;
                    Raylib.PlaySound(playerdmg);
                }

            }
            if (health == 0)
            {
                player.active = false;
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
                bool bulletOut = KeepInsideArea(bullet.transform, bullet.collision, 0, -20, window_width, window_height);
                if (bulletOut)
                {
                    bullet.active = false;
                }
                Rectangle bulletRec = getRectangle(bullet.transform, bullet.collision);
                foreach (Enemy enemy in enemies)
                {
                    Rectangle enemyRec = getRectangle(enemy.transform, enemy.collision);
                    if (Raylib.CheckCollisionRecs(bulletRec, enemyRec))
                    {
                        if (enemy.active)
                        {
                            score += 1;
                            enemiesAlive -= 1;
                            enemiesShot++;
                            enemy.active = false;
                            bullet.transform.position.Y = 500.0f;
                            bullet.active = false;
                            Raylib.PlaySound(enemydmg);

                        }
                    }

                }
            }



        }

        Rectangle getRectangle(TransformComponent t, CollisionComponent c)
        {
            Rectangle r = new Rectangle(t.position.X,
                t.position.Y, c.size.X, c.size.Y);
            return r;
        }
        /// <summary>
        /// pitää pelaajan ja vastustajat kentän sisällä, käytetään myös tarkistamaan onko luoti kentän sisällä
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="collision"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <returns>True jos kentän ulkopuolella</returns>
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

        void DrawGame()
        {
            if (enemiesAlive > 0 && health > 0)
            {
                player.Draw();

            }
            Raylib.DrawText(Raylib.TextFormat($"Score:{score}"), 550, 400, 20, Raylib.RED);
            Raylib.DrawText(Raylib.TextFormat($"Health:{health}"), 20, 400, 20, Raylib.RED);
            if(health>0) 
            { 
                foreach (Bullet bullet in bullets)
                {   
                    bullet.Draw();
                }
            
                foreach (Bullet enemyBullet in enemyBullets)
                {
                    enemyBullet.Draw();
                }
                foreach (Enemy enemy in enemies)
                {
                    enemy.Draw();
                }
            }

        }
        void UpdateStart()
        {
            MainMen();
        }
        void UpdateScore()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
                ResetGame();
                state = GameState.Play;
            }
        }
        void DrawScore()
        {
            if (enemiesAlive == 0)
            {
                Raylib.DrawText(Raylib.TextFormat($"Voitit! Lopullinen tulos:{finalscore}"), 175, 200, 20, Raylib.RED);
                Raylib.DrawText(Raylib.TextFormat($"Paina enter aloittaaksesi alusta"), 150, 300, 20, Raylib.RED);
            }
            if (health == 0)
            {
                Raylib.DrawText($"Hävisit! Lopullinen tulos:{finalscore}", 175, 200, 20, Raylib.RED);
                Raylib.DrawText(Raylib.TextFormat($"Paina enter aloittaaksesi alusta"), 150, 300, 20, Raylib.RED);

            }
        }
        void MainMen()
        {
            MainMenu mainMenu=new MainMenu();
            mainMenu.StartButtonPressedEvent += OnStartButtonPressed;
            mainMenu.EndButtonPressedEvent += OnEndButtonPressed;
            mainMenu.StartMain();
        }
        void OnStartButtonPressed(Object sender, EventArgs e)
        {
            ResetGame();
            state = GameState.Play;
        }
        void OnEndButtonPressed(Object sender, EventArgs e)
        {
            Raylib.EndDrawing();
            Raylib.CloseWindow();
        }
        void PauseMen()
        {
            Time = Convert.ToInt32(doubleTime);
            PauseMenu pauseMenu= new PauseMenu();
            RayGui.GuiTextBox(new Rectangle(100, 200, 110, 20),
                "Vihollisia tuhottu:"+enemiesShot.ToString(), 20, false);
            RayGui.GuiTextBox(new Rectangle(100, 220, 110, 20),
                "Pelattu aika:" + Time.ToString()+"s", 20, false);
            pauseMenu.ResumeButtonPressedEvent += OnResumeButtonPressed;
            pauseMenu.MainButtonPressedEvent += OnMainButtonPressed;
            pauseMenu.OptionsButtonPressedEvent += OnOptionsButtonPressed;
            pauseMenu.RestartButtonPressedEvent += OnResetButtonPressed;
            pauseMenu.StartPause();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                state = GameState.Play;
            }

        }
        void OnResumeButtonPressed(Object sender, EventArgs e)
        {
            state = GameState.Play;
        }
        void OnMainButtonPressed(Object sender, EventArgs e)
        {
            state = GameState.Start;
        }
        void OnOptionsButtonPressed(Object sender, EventArgs e)
        {
            state = GameState.Options;
        }
        void OnResetButtonPressed(Object sender, EventArgs e)
        {
            ResetGame();
            state= GameState.Play;
        }
        void OnDeactivateButtonPressed(Object sender, EventArgs e)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.active = false;
            }
            state = GameState.Play;
        }

        void OptionsMen()
        {
            OptionsMenu optionsMenu= new OptionsMenu();
            move = RayGui.GuiSlider(new Rectangle(280, 170, 70, 20), "0", "1", move, 0f, 1f);
            move1 = Convert.ToInt32(move);
            optionsMenu.StartOptions();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                state = GameState.Pause;
            }
        }
        void DevMen()
        {
            DevMenu devMenu= new DevMenu();
            devMenu.ResetButtonPressedEvent += OnResetButtonPressed;
            devMenu.DeactivateButtonPressedEvent += OnDeactivateButtonPressed;
            score_float =RayGui.GuiSlider(new Rectangle(280, 370, 70, 20), "0","20",score_float,0f,20f);
            score = Convert.ToInt32(score_float);
            devMenu.StartDev();
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_ESCAPE))
            {
                state = GameState.Play;
            }
        }

    }
}

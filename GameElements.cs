using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel_Projekt_Dennis
{
    class GameElements
    {
        static Player player;
        static List<Enemies> enemies;
        static Base bas;
        static PrintText scoreText;
        static double timeSinceLastKill = 0;
        static Menu menu;

        public enum State { Menu, Level, EnterHighscore, Highscore, Quit };
        
        public static State currentstate;

        public static void Initialize()
        {

        }
        public static void LoadContent(ContentManager content, GameWindow window)
        {
            enemies = new List<Enemies>();
            Random random = new Random();
            menu = new Menu((int)State.Menu);
            menu.AddItem(content.Load<Texture2D>("sprites/menu/start"),(int)State.Level);
            menu.AddItem(content.Load<Texture2D>("sprites/menu/highscore"), (int)State.Highscore);
            menu.AddItem(content.Load<Texture2D>("sprites/menu/exit"), (int)State.Quit);
            player = new Player(content.Load<Texture2D>("sprites/player/tempplayer"), 380, 400);
            scoreText = new PrintText(content.Load<SpriteFont>("sprites/Fonts/firstfont"));

            Texture2D enemyOne = content.Load<Texture2D>("sprites/enemies/enemyOne");
            Texture2D enemyTwo = content.Load<Texture2D>("sprites/enemies/enemyThree");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(140, window.ClientBounds.Width - enemyOne.Width - 140);
                int tempY = 100;
                NormalZombieBear temp1 = new NormalZombieBear(enemyOne, rndX, tempY, 1f, 0, 0);
                enemies.Add(temp1);
            }
            for (int i = 0; i < 3; i++)
            {
                int rndX = random.Next(100, window.ClientBounds.Width - enemyTwo.Width);
                int Y = 50;
                Enemy_2 temp2 = new Enemy_2(enemyTwo, rndX, Y, 1f, 0, 0);
                enemies.Add(temp2);                                                           
            }
            bas = new Base(content.Load<Texture2D>("sprites/player/Stacket"), 0, 280);            
        }
        private static void Reset(GameWindow window, ContentManager content)
        {
            player.Reset();
            enemies.Clear();

            Random random = new Random();
            Texture2D enemyOne = content.Load<Texture2D>("sprites/enemies/enemyOne");
            Texture2D enemyTwo = content.Load<Texture2D>("sprites/enemies/enemyThree");
            for (int i = 0; i < 5; i++)
            {
                int rndX = random.Next(140, window.ClientBounds.Width - enemyOne.Width - 140);
                int tempY = 100;
                NormalZombieBear temp1 = new NormalZombieBear(enemyOne, rndX, tempY, 1f, 0, 0);
                enemies.Add(temp1);
            }
            for (int i = 0; i < 2; i++)
            {
                int rndX = random.Next(100, window.ClientBounds.Width - enemyTwo.Width);
                int Y = 50;
                Enemy_2 temp2 = new Enemy_2(enemyTwo, rndX, Y, 1f, 0, 0);
                enemies.Add(temp2);                                                           
            }
        }
        public static State MenuUpdate(GameTime gameTime)
        {
            return (State)menu.Update(gameTime);
        }
        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
        public static State RunUpdate(ContentManager content, GameWindow window, GameTime gameTime)
        {            
            Random random = new Random();
            int rnd;
            MouseState state = Mouse.GetState();
            Texture2D enemyOne = content.Load<Texture2D>("sprites/enemies/enemyOne");
            Texture2D enemyTwo = content.Load<Texture2D>("sprites/enemies/enemyThree");


            player.Update(window, gameTime);
            
            foreach (Enemies e in enemies.ToList())
                if (e.IsAlive)
                {
                    e.Update(window);

                    if (state.LeftButton == ButtonState.Pressed && player.CheckCollision(e))
                    {
                        if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastKill + 300)
                        {
                            e.IsAlive = false;
                            player.Points++;
                            enemies.Remove(e);
                            timeSinceLastKill = gameTime.TotalGameTime.TotalMilliseconds;
                        }

                    }
                    if (bas.CheckCollision(e))
                    {
                        player.IsAlive = false;
                    }
                }
                if (enemies.Count < 8)
                {
                    rnd = random.Next(1, 3);

                    switch (rnd)
                    {
                        case 1:
                            int rndX = random.Next(100, window.ClientBounds.Width - enemyOne.Width - 100);
                            int tempY = 100;
                            NormalZombieBear temp = new NormalZombieBear(enemyOne, rndX, tempY, 1f, 0, 0);
                            enemies.Add(temp);
                            break;
                        case 2:
                            int rndSpeedX = random.Next(1, 3);
                            int rndX2 = random.Next(140, window.ClientBounds.Width - enemyTwo.Width - 140);
                            int tempY2 = 50;                            
                            Enemy_2 temp2 = new Enemy_2(enemyTwo, rndX2, tempY2, 1f, 0, 0);
                            enemies.Add(temp2);                                                           
                            break;
                    }
                }
            if (!player.IsAlive)
            {
                Reset(window, content);
                return State.Menu;                
            }
            return State.Level;
        }

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            foreach (Enemies e in enemies) e.Draw(spriteBatch);
            player.Draw(spriteBatch);
            bas.Draw(spriteBatch);
            scoreText.Print("Score:" + player.Points, spriteBatch, 0, 0);
        }
        public static State HighscoreUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape)) return State.Menu;
            return State.Highscore;
        }
        public static void HighscoreDraw(SpriteBatch spriteBatch)
        {

        }

    }
}

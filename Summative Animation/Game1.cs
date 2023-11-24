using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Summative_Animation
{
    // Raihan C

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
   
        List<Texture2D> cityFrames = new List<Texture2D>();
        List<Texture2D> flyingSuperman = new List<Texture2D>();

        Screen screen;
        bool fightBegins = true;

        enum Screen
        {
            Intro,
            BeforeFight,
            Fight,
            Ending
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 500;
            this.Window.Title = "Batman Vs. Superman";
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            screen = Screen.Intro;

            for (int i = 0; i < 60; i++)    // Adds all city frames;
            {
                cityFrames.Add(Content.Load<Texture2D>($"frame_{i}_delay-0.04s"));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // add background code



            if (screen == Screen.Intro)
            {

            }
            else if (screen == Screen.BeforeFight)
            {

            }
            else if (screen == Screen.Fight)
            {

            }
            else if (screen == Screen.Ending)
            {


            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            // Add Background Code


            if (screen == Screen.Intro)
            {

            }
            else if (screen == Screen.BeforeFight) 
            { 

            }
            else if (screen == Screen.Fight)
            {

            }
            else if (screen == Screen.Ending)
            { 


            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
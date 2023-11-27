using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        Texture2D introScreenTexture;
        Screen screen;
        KeyboardState keyboardState;
        private SoundEffect introSong, endSong;
        SoundEffectInstance introInstance, endInstance;
        SpriteFont introText;
        bool fightBegins = false;
        int cityFrameCounter = 0;
        float cityInterval = 0.06f;
        float cityTime = 0;
        float cityTimeStamp;
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
            _graphics.ApplyChanges();
            this.Window.Title = "Batman Vs. Superman";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            screen = Screen.Intro;
            cityTimeStamp = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < 60; i++)    // Adds all city frames;
            {
                cityFrames.Add(Content.Load<Texture2D>($"frame_{i}_delay-0.04s"));
            }

            introScreenTexture = Content.Load<Texture2D>("BatmanvSuperman");
            introSong = Content.Load<SoundEffect>("BatmanTrimmed");
            introInstance = introSong.CreateInstance();
            introInstance.IsLooped = false;
            introText = Content.Load<SpriteFont>("DragonFont");
            //TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // add background code

            cityTime = (float)gameTime.TotalGameTime.TotalSeconds - cityTimeStamp;

            if (cityTime > cityInterval && fightBegins == true)
            {
                cityFrameCounter += 1;
                cityTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                if (cityFrameCounter >= 59)
                {
                    cityFrameCounter = 0;
                }
            }



            if (screen == Screen.Intro)
            {
                fightBegins = false;
                introInstance.Play();
                if (keyboardState.IsKeyDown(Keys.Space))
                    screen = Screen.BeforeFight;

            }
            else if (screen == Screen.BeforeFight)
            {
                fightBegins = true;
                introInstance.Stop();
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

            if (cityFrameCounter < 60 && fightBegins == true)
            {
                _spriteBatch.Draw(cityFrames[cityFrameCounter], new Rectangle(0, 0, 900, 500), Color.White);
            }
            else
            {
                cityFrameCounter = 0;
            }



            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introScreenTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(introText, "BATMAN Vs. SUPERMAN", new Vector2(190, 200), Color.Red);
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
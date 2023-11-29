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
        // Raihan C

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
   
        List<Texture2D> cityFrames = new List<Texture2D>();
        List<Texture2D> flyingSuperman = new List<Texture2D>();   
        Texture2D introScreenTexture;

        Texture2D buildingTexture;


        Screen screen;
        KeyboardState keyboardState;
        private SoundEffect introSong, endSong, teleportSound, flyingSound;
        SoundEffectInstance introInstance, endInstance, flyingSoundInstance;
        SpriteFont introText;
        SpriteFont introInstructions;
        bool fightBegins = false;
        int cityFrameCounter = 0;
        int supermanFrame = 0;
        float cityInterval = 0.06f;
        float cityTime = 0;
        float cityTimeStamp;
        float supermanTime;
        float supermanTimeStamp;
        float supermanInterval = 0.09f;
        float totalTime;
        float totalTimeStamp;
        bool teleport = false;
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

            for (int i = 1; i < 13; i++)    // Adds all Flying Superman frames;
            {
                flyingSuperman.Add(Content.Load<Texture2D>($"FlyingSuperman{i}-removebg-preview"));
            }

            introScreenTexture = Content.Load<Texture2D>("BatmanvSuperman");
            introSong = Content.Load<SoundEffect>("BatmanTrimmed");
            introInstance = introSong.CreateInstance();
            introInstance.IsLooped = false;
            introText = Content.Load<SpriteFont>("DragonFont");
            introInstructions = Content.Load<SpriteFont>("IntroInstructions");
            teleportSound = Content.Load<SoundEffect>("TeleportWav");
            flyingSound = Content.Load<SoundEffect>("FlyingSound");
            flyingSoundInstance = flyingSound.CreateInstance();
            flyingSoundInstance.IsLooped = false;
            buildingTexture = Content.Load<Texture2D>("Stadium");
            //TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();



            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // add background code

            if (screen == Screen.Intro)
            {
                fightBegins = false;
                introInstance.Play();
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    screen = Screen.BeforeFight;
                    introInstance.Stop();
                }

                if (introInstance.State == SoundState.Stopped)
                {
                    screen = Screen.BeforeFight;
                }
                totalTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
            }
            else if (screen == Screen.BeforeFight)
            {

                totalTime = (float)gameTime.TotalGameTime.TotalSeconds - totalTimeStamp;
                supermanTime = (float)gameTime.TotalGameTime.TotalSeconds - supermanTimeStamp;

                fightBegins = true;
                introInstance.Stop();

                if (totalTime < 10)
                {
                    flyingSoundInstance.Play();

                    cityTime = (float)gameTime.TotalGameTime.TotalSeconds - cityTimeStamp;

                    if (cityTime > cityInterval && fightBegins == true && totalTime < 10)
                    {
                        cityFrameCounter += 1;
                        cityTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        if (cityFrameCounter >= 59)
                        {
                            cityFrameCounter = 0;
                        }
                    }

                    if (supermanTime > supermanInterval && totalTime < 10)
                    {
                        supermanFrame += 1;
                        supermanTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        if (supermanFrame >= 9)
                        {
                            supermanFrame = 2;
                        }
                    }
                }
                else
                {
                    flyingSoundInstance.Stop();

                    if (keyboardState.IsKeyDown(Keys.T))
                    {
                        supermanFrame = 9;
                        teleport = true;
                        teleportSound.Play();
                    }

                    if (teleport)
                    {
                        if (supermanTime>supermanInterval)
                        {
                            supermanTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                            supermanFrame += 1;

                            if (supermanFrame >= 12)
                            {
                                screen = Screen.Fight;
                            }
                        }                     
                    }

                    // Make it so it prompts user to press T to teleport to Batman
                }
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
                _spriteBatch.Draw(introScreenTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(introText, "BATMAN Vs. SUPERMAN", new Vector2(190, 200), Color.Red);
                _spriteBatch.DrawString(introInstructions, "Press 'Space' to Continue", new Vector2(240, 270), Color.Purple);
            }
            else if (screen == Screen.BeforeFight) 
            {
                if (cityFrameCounter < 60 && fightBegins == true)
                {
                    _spriteBatch.Draw(cityFrames[cityFrameCounter], new Rectangle(0, 0, 900, 500), Color.White);
                }

                if (supermanFrame < 10)
                {
                    _spriteBatch.Draw(flyingSuperman[supermanFrame], new Vector2(50,90), Color.White);
                }

                if (teleport)
                {
                    _spriteBatch.Draw(flyingSuperman[supermanFrame], new Vector2(50, 90), Color.White);
                }

            }
            else if (screen == Screen.Fight)
            {
                _spriteBatch.Draw(buildingTexture, new Rectangle(0, 0, 900, 500), Color.White);

            }
            else if (screen == Screen.Ending)
            { 


            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
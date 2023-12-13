using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

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
        List<Texture2D> fightingSuperman = new List<Texture2D>();
        List<Texture2D> fightingBatman = new List<Texture2D>();

        Texture2D introScreenTexture;
        Texture2D buildingTexture;
        Texture2D endingTexture;
        Screen screen;
        KeyboardState keyboardState;
        private SoundEffect introSong, endSong, teleportSound, flyingSound, ClapSound, boneCrackSound;
        SoundEffectInstance introInstance, endInstance, flyingSoundInstance;
        SpriteFont introText, endingText, teleportText;
        SpriteFont introInstructions;
        bool fightBegins = false;
        int cityFrameCounter = 0;
        int supermanFrame = 0;
        int batmanFrame = 0;
        int supermanX = 200;
        int supermanY = 350;
        int batmanX = 500, batmanY = 345;
       
        float cityInterval = 0.06f;
        float cityTime = 0;
        float cityTimeStamp;
        float supermanTime;
        float supermanTimeStamp;
        float supermanInterval = 0.09f;
        float totalTime;
        float totalTimeStamp;
        float waitTime;
        float waitTimeStamp;
        bool teleport = false;
        bool attackSequence = false, ending = false, textPrompt = false;
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

            for (int i = 1; i < 19; i++)    // Adds all Fighting Superman frames;
            {
                fightingSuperman.Add(Content.Load<Texture2D>($"FightingSuperman{i}-removebg-preview"));
            }


            for (int i = 0; i < 3; i++)    // Adds all Batman frames;
            {
                fightingBatman.Add(Content.Load<Texture2D>($"BatmanFighting{i}"));
            }

            introScreenTexture = Content.Load<Texture2D>("BatmanvSuperman");
            endingTexture = Content.Load<Texture2D>("BatmanSuperman");
            introSong = Content.Load<SoundEffect>("BatmanTrimmed");
            introInstance = introSong.CreateInstance();
            introInstance.IsLooped = false;
            introText = Content.Load<SpriteFont>("DragonFont");
            endingText = Content.Load<SpriteFont>("endingText");
            teleportText = Content.Load<SpriteFont>("teleportText");
            introInstructions = Content.Load<SpriteFont>("IntroInstructions");
            ClapSound = Content.Load<SoundEffect>("Clap");
            teleportSound = Content.Load<SoundEffect>("TeleportWav");
            flyingSound = Content.Load<SoundEffect>("FlyingSound");
            boneCrackSound = Content.Load<SoundEffect>("BoneCrack");
            flyingSoundInstance = flyingSound.CreateInstance();
            flyingSoundInstance.IsLooped = false;
            buildingTexture = Content.Load<Texture2D>("BlackRect2");
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

                    if (!teleport)
                    textPrompt = true;


                    if (keyboardState.IsKeyDown(Keys.T))
                    {
                        textPrompt = false;
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
                                supermanFrame = 0;
                                supermanInterval = 0.1f;
                                screen = Screen.Fight;
                            }
                        }                     
                    }

                    // Make it so it prompts user to press T to teleport to Batman
                }
            }
            else if (screen == Screen.Fight)
            {

                supermanTime = (float)gameTime.TotalGameTime.TotalSeconds - supermanTimeStamp;
                waitTime = (float)gameTime.TotalGameTime.TotalSeconds - waitTimeStamp;

                if (supermanTime > supermanInterval && supermanFrame < 9)
                {        
                        supermanTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        supermanFrame += 1;               
                }
                else if (supermanFrame == 9) // Changes speed after 9th frame
                {
                        supermanInterval = 0.25f;
                        waitTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                        supermanFrame++;
                }
                else if (supermanFrame == 10 && waitTime >=1)
                {
                    supermanFrame++;
                    attackSequence = true;
                    waitTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                    ClapSound.Play();
                }


                if (attackSequence && waitTime >= 1.5)
                {                   

                    supermanX = 450;
                    supermanY = 320;
                    if (supermanFrame >= 10 && supermanFrame < 16)   // Scene 2    // Make it so on frame 10 it teleports to above Batman
                    {
                        if (supermanTime > supermanInterval)
                        {
                            supermanTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                            supermanFrame += 1;
                        }

                        if (supermanFrame == 13)
                        {
                            batmanY = 370;
                            batmanFrame = 1;
                            boneCrackSound.Play();
                        }
                        else if (supermanFrame == 16)
                        {
                            batmanFrame = 2;
                            ending = true;
                            waitTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                            supermanFrame++;
                        }
                    }              
                }

                if (ending && waitTime >= 4.5)
                {
                    screen = Screen.Ending;
                }
                
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

                if (textPrompt)
                {
                    _spriteBatch.DrawString(teleportText, "BATMAN SPOTTED!", new Vector2(550, 100), Color.Yellow);
                    _spriteBatch.DrawString(teleportText, "Press 'T' to Teleport", new Vector2(600, 130), Color.Yellow);
                }

            }
            else if (screen == Screen.Fight)
            {
                _spriteBatch.Draw(cityFrames[56], new Rectangle(0, 0, 900, 500), Color.White);
                _spriteBatch.Draw(buildingTexture, new Rectangle(100, 400, 700, 200), Color.White);
          
                _spriteBatch.Draw(fightingBatman[batmanFrame], new Vector2(batmanX, batmanY), Color.White);

                if (supermanFrame < 10) // Teleport Sequence
                {
                    _spriteBatch.Draw(fightingSuperman[supermanFrame], new Vector2(supermanX, supermanY), Color.White);

                }
                else if (supermanFrame >= 9)
                {
                    _spriteBatch.Draw(fightingSuperman[supermanFrame], new Vector2(supermanX, supermanY), Color.White);
                }      


            }
            else if (screen == Screen.Ending)
            {
                _spriteBatch.Draw(endingTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(endingText, "The Batman is Dead", new Vector2(300, 170), Color.DarkGray);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
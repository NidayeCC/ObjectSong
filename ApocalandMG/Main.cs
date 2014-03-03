using System;
using System.Linq;
using ApocalandMG.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ObjectSongEngineMG;

namespace ApocalandMG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Game
    {
        // The XNA graphics device manager
        [UsedImplicitly]
        internal GraphicsDeviceManager Graphics;

        //XNA objects
        private SpriteBatch _spriteBatch;
        private SpriteFont _menufont;
        
        //ObjectSong Objects
        private OSEInput _input;
        private OSECursor _defaultcursor;
        private OSEMenu _mapbuildmenu;
        private OSEPlayObject _humanplayer;
        private OSEPlayObject _pile;
        private OSELabel _scorelabel;
  
        // Game play mode
        private Int32 _mode;

        //Internal variables for game play
        private Int32 _score;


        public Main() 
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            // Set the game mode to the main menu
            _mode = 1;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _score = 0;

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // Create the font for our OSEMenu
            _menufont = Content.Load<SpriteFont>("Arial");

            //Instantiate our mouse & keyboard controller
            _input = new OSEInput();

            // Load the Default OSE Cursor
            _defaultcursor = new OSECursor(new OSESize2D(32,32), new OSELocation2D(0,0));
            _defaultcursor.LoadTexture(GraphicsDevice, Content, "OSEContent/CrossHair32x32");

            // We enable hit boxes for collision detection
            _defaultcursor.CreateHitBox(GraphicsDevice);
            // Offset the hitbox to the middle of the cursor rectangle and make it very small
            _defaultcursor.Hitbox.Size.Height = 2;
            _defaultcursor.Hitbox.Size.Width = 2;
            _defaultcursor.Hitbox.Offset = new OSELocation2D(15,15);

            // To facilitate debugging, we draw the hitbox as a white rectangle
            // this can be shut off in production code
            _defaultcursor.Hitbox.Visible = true;

            //Build a menu for the Play and Exit functions
            _mapbuildmenu = new OSEMenu(_menufont);
            _mapbuildmenu.Location = new OSELocation2D(10,10);
            _mapbuildmenu.AddItem("Play", "Play", 0);
            _mapbuildmenu.AddItem("Exit", "Exit", 1);
            // Create hitbox outlines on all menu items for collision detection with cursor
            _mapbuildmenu.CreateHitBoxes(GraphicsDevice);
            _mapbuildmenu.HitBoxesVisible = true;

            //Build the human player
            _humanplayer = new OSEPlayObject(new OSESize2D(64, 128), new OSELocation2D(0,0));
            _humanplayer.LoadTexture(GraphicsDevice, Content, "uglyman");
            _humanplayer.Attributes.Add("walkspeed", "5");
            _humanplayer.CreateHitBox(GraphicsDevice);
            _humanplayer.Hitbox.Visible = true;

            _pile = new OSEPlayObject(new OSESize2D(32,32), new OSELocation2D(0,0));
            _pile.LoadTexture(GraphicsDevice, Content, "pile");
            _pile.Attributes.Add("pointvalue", "100");
            _pile.CreateHitBox(GraphicsDevice);
            _pile.Hitbox.Visible = true;
            _pile.Visible = false;

            _scorelabel = new OSELabel("0", _menufont);
            _scorelabel.Location = new OSELocation2D(400, 10);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _input.Update();

            if (_input.NewKeyState.Contains(Keys.Escape))
            {
                _mode = 1;
                _score = 0;
            }

            switch (_mode)
            {
                case 0:
                    UpdateRunTime();
                    break;
                case 1:
                    UpdateMapEditor(gameTime);
                    break;
            }


            base.Update(gameTime);
        }


        public void UpdateRunTime()
        {
            UpdateScore();
            UpdatePlayer();
            UpdatePile();
        }


        public void UpdateScore()
        {
            _scorelabel.Text = "Score: " + _score.ToString();
        }


        public void UpdatePile()
        {

            if (_pile.Visible == false)
            {
                Random rnd1 = new Random();
                Random rnd2 = new Random();

                _pile.Location = new OSELocation2D(rnd1.Next(620), rnd2.Next(460));
                _pile.Visible = true;
            }
            else
            {
                if (_pile.CheckForHit(_humanplayer))
                {
                    _pile.Visible = false;
                    _score += Convert.ToInt32(_pile.Attributes.GetValue("pointvalue"));
                }
            }

            _pile.Update();
        }


        public void UpdatePlayer()
        {
            var playerspeed = Convert.ToInt32(_humanplayer.Attributes.GetValue("walkspeed"));

            if (_input.NewKeyState.Contains(Keys.Right))
            {
                _humanplayer.Location.X += playerspeed;
            }

            if (_input.NewKeyState.Contains(Keys.Left))
            {
                _humanplayer.Location.X -= playerspeed;
            }

            if (_input.NewKeyState.Contains(Keys.Up))
            {
                _humanplayer.Location.Y -= playerspeed;
            }

            if (_input.NewKeyState.Contains(Keys.Down))
            {
                _humanplayer.Location.Y += playerspeed;
            }
    
            // We must call Update on the sprite after changes
            _humanplayer.Update();
        }


        public void UpdateMapEditor(GameTime gameTime)
        {
            // Update the cursor location from the mouse position
            _defaultcursor.Location = new OSELocation2D(_input.NewMouseState);

            _mapbuildmenu.Update(_defaultcursor);

            if (_mapbuildmenu.SelectedItem != null && _input.NewMouseState.LeftButton == ButtonState.Pressed)
            {
                if (_mapbuildmenu.SelectedItem.Action == "Exit")
                {
                    Exit();
                }
                if (_mapbuildmenu.SelectedItem.Action == "Play")
                {
                    _mode = 0;
                }

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SandyBrown);

            

            // Draw the Run Time
            if (_mode == 0)
            {
                _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);

                _pile.Draw(_spriteBatch);
                _humanplayer.Draw(_spriteBatch);
                _scorelabel.Draw(_spriteBatch);

                _spriteBatch.End();
            }


            // Draw the Map Editor
            if (_mode == 1)
            {
                _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
                
                // Draw the menu
                _mapbuildmenu.Draw(_spriteBatch);

                // Draw the cursor last, so that it is on top
                _defaultcursor.Draw(_spriteBatch);

                _spriteBatch.End();
            }


            base.Draw(gameTime);
        }
    }
}

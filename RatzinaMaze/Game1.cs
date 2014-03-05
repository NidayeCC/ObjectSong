#region Using Statements
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ObjectSongEngineMG;

#endregion

namespace RatzinaMaze
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        // The XNA graphics device manager
        internal GraphicsDeviceManager Graphics;

        //XNA objects
        private SpriteBatch _spriteBatch;
        private SpriteFont _menufont;

        //ObjectSong Objects
        private OSEInput _input;
        private OSECursor _defaultcursor;
        private OSEMenu _buildmenu;
        private OSEPlayObject _humanplayer;
        private OSELabel _scorelabel;

        //Experimental - To Be Removed
        private OSEPlayObject _wallsegment;
        private OSEMap _map;

        // Game play mode
        private Int32 _mode;

        //Internal variables for game play
        private Int32 _score;


        public Game1()
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
            _defaultcursor = new OSECursor(new OSESize2D(32, 32), new OSELocation2D(0, 0));
            _defaultcursor.LoadTexture(GraphicsDevice, Content, "CrossHair32x32");

            // We enable hit boxes for collision detection
            _defaultcursor.CreateHitBox(GraphicsDevice);
            // Offset the hitbox to the middle of the cursor rectangle and make it very small
            _defaultcursor.Hitbox.Size.Height = 2;
            _defaultcursor.Hitbox.Size.Width = 2;
            _defaultcursor.Hitbox.Offset = new OSELocation2D(15, 15);

            // To facilitate debugging, we draw the hitbox as a white rectangle
            // this can be shut off in production code
            _defaultcursor.Hitbox.Visible = true;

            //Build a menu for the Play and Exit functions
            _buildmenu = new OSEMenu(_menufont);
            _buildmenu.Location = new OSELocation2D(10, 10);
            _buildmenu.AddItem("Play", "Play", 0);
            _buildmenu.AddItem("Exit", "Exit", 1);
            // Create hitbox outlines on all menu items for collision detection with cursor
            _buildmenu.CreateHitBoxes(GraphicsDevice);
            _buildmenu.HitBoxesVisible = true;

            //Build the human player
            _humanplayer = new OSEPlayObject(new OSESize2D(64, 64), new OSELocation2D(32, 48));
            _humanplayer.LoadTexture(GraphicsDevice, Content, "rat64x64");
            _humanplayer.Attributes.Add("walkspeed", "5");
            _humanplayer.CreateHitBox(GraphicsDevice);
            _humanplayer.Hitbox.Visible = true;
            _humanplayer.Origin = new OSELocation2D(32, 32);

            _scorelabel = new OSELabel("0", _menufont);
            _scorelabel.Location = new OSELocation2D(400, 10);

            //Experimental - To Be Removed
            _wallsegment = new OSEPlayObject(new OSESize2D(128, 16), new OSELocation2D(0, 0));
            _wallsegment.LoadTexture(GraphicsDevice, Content, "walltile16x16");
            _wallsegment.CreateHitBox(GraphicsDevice);
            _wallsegment.Hitbox.Visible = true;
            _wallsegment.IsObstacle = true;

            // Level Map
            _map = new OSEMap();
            _map.Items.Add(_wallsegment);

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
        }


        public void UpdateScore()
        {
            _scorelabel.Text = "Score: " + _score;
        }


        public void UpdatePlayer()
        {
            var playerspeed = Convert.ToInt32(_humanplayer.Attributes.GetValue("walkspeed"));

            if (_input.NewKeyState.Contains(Keys.Right))
            {
                _humanplayer.Location.X += playerspeed;             
                _humanplayer.Orientation = OSESpriteOrientation.Right;
            }
            else
            if (_input.NewKeyState.Contains(Keys.Left))
            {
                _humanplayer.Location.X -= playerspeed;
                _humanplayer.Orientation = OSESpriteOrientation.Left;
            }
            else
            if (_input.NewKeyState.Contains(Keys.Up))
            {
                _humanplayer.Location.Y -= playerspeed;
                _humanplayer.Orientation = OSESpriteOrientation.Up;
            }
            else
            if (_input.NewKeyState.Contains(Keys.Down))
            {
                _humanplayer.Location.Y += playerspeed;
                _humanplayer.Orientation = OSESpriteOrientation.Down;
            }

            // You must call update to update sprite information
            _humanplayer.CheckForHit(_map);
            _humanplayer.Update();
        }


        public void UpdateMapEditor(GameTime gameTime)
        {
            // Update the cursor location from the mouse position
            _defaultcursor.Location = new OSELocation2D(_input.NewMouseState);

            _buildmenu.Update(_defaultcursor);

            if (_buildmenu.SelectedItem != null && _input.NewMouseState.LeftButton == ButtonState.Pressed)
            {
                if (_buildmenu.SelectedItem.Action == "Exit")
                {
                    Exit();
                }
                if (_buildmenu.SelectedItem.Action == "Play")
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
                _map.Draw(_spriteBatch);
                _humanplayer.Draw(_spriteBatch);
                _scorelabel.Draw(_spriteBatch);
            }
 
            else

            // Draw the Map Editor
            if (_mode == 1)
            {
                // Draw the menu
                _buildmenu.Draw(_spriteBatch);

                // Draw the cursor last, so that it is on top
                _defaultcursor.Draw(_spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}

using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ObjectSongEngine;

namespace ApocaLand
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Game
    {
        SpriteBatch _spriteBatch;

        OSECursor _defaultcursor;
        private OSEMenu _mapbuildmenu;
        SpriteFont _menufont;

        Int32 _mode;

        public Main() 
        {
            new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            _mode = 1;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _menufont = Content.Load<SpriteFont>("Arial");

          
            // Load the Default OSE Cursor
            _defaultcursor = new OSECursor(this, new OSESize2D(32,32), new OSELocation2D(0,0));
            _defaultcursor.LoadTexture(Content, "OSEContent/CrossHair32x32");
            _defaultcursor.DrawHitBox = true;

            _mapbuildmenu = new OSEMenu(this, _menufont);
            _mapbuildmenu.AddItem("File", "test1", 0);
            _mapbuildmenu.AddItem("Exit", "Exit", 1);

            //_map = new OrthoMap2D(2, 2);

            //_blacktile = new Tile(graphics.GraphicsDevice, 128, 64);
            //_blacktile.LoadTexture(Content, "TileBlack");

            //_sandtile = new Tile(graphics.GraphicsDevice, 128, 64);
            //_sandtile.LoadTexture(Content, "TileSand");

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
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();

            Keys[] state = Keyboard.GetState().GetPressedKeys();

            MouseState mousestate = Mouse.GetState();

            if (state.Contains(Keys.M) && state.Contains(Keys.RightControl))
                _mode = 1;

            if (state.Contains(Keys.P) && state.Contains(Keys.RightControl))
                _mode = 0;


            if (_mode == 0)
            {
                UpdateRunTime();
            }

            if (_mode == 1)
            {
                UpdateMapEditor(gameTime, mousestate);
            }


            base.Update(gameTime);
        }


        public void UpdateRunTime()
        {

        }


        public void UpdateMapEditor(GameTime gameTime, MouseState mouseState)
        {
            // Update the cursor location from the mouse position
            _defaultcursor.Location.Update(Mouse.GetState());

            _mapbuildmenu.Update(_defaultcursor);

            if (_mapbuildmenu.SelectedItem != null && mouseState.LeftButton == ButtonState.Pressed)
            {
                if (_mapbuildmenu.SelectedItem.Action == "Exit")
                {
                    this.Exit();
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
                //spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
                //_blacktile.Draw(spriteBatch, new OSELocation2D(0,0));
                //_blacktile.Draw(spriteBatch, new OSELocation2D(128, 0));
                //_sandtile.Draw(spriteBatch, new OSELocation2D(64, 32));
                //_sandtile.Draw(spriteBatch, new OSELocation2D(192, 32));
                //spriteBatch.End();
            }


            // Draw the Map Editor
            if (_mode == 1)
            {
                _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
                //_blacktile.Draw(spriteBatch, new OSELocation2D(0, 0));
                //_sandtile.Draw(spriteBatch, new OSELocation2D(0, 64));
                

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

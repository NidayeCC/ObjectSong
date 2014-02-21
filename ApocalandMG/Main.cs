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
        SpriteBatch _spriteBatch;

        [UsedImplicitly] internal GraphicsDeviceManager Graphics;

        OSECursor _defaultcursor;
        private OSEMenu _mapbuildmenu;
        SpriteFont _menufont;

        Int32 _mode;

        public Main() 
        {
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            _mode = 1;
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
            _defaultcursor = new OSECursor(new OSESize2D(32,32), new OSELocation2D(0,0));
            _defaultcursor.LoadTexture(GraphicsDevice, Content, "OSEContent/CrossHair32x32");
            _defaultcursor.EnableHitBox(GraphicsDevice);

            // Offset the hitbox to the middle of the cursor rectangle and make it very small
            _defaultcursor.Hitbox.Size.Height = 2;
            _defaultcursor.Hitbox.Size.Width = 2;
            _defaultcursor.Hitbox.Offset = new OSELocation2D(15,15);

            //Build a menu for the Play and Exit functions
            _mapbuildmenu = new OSEMenu(_menufont);
            _mapbuildmenu.Location = new OSELocation2D(10,10);
            _mapbuildmenu.AddItem("Play", "Play", 0);
            _mapbuildmenu.AddItem("Exit", "Exit", 1);
            _mapbuildmenu.EnableHitBox(GraphicsDevice);

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

            if (state.Contains(Keys.Escape))
                _mode = 1;

    
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

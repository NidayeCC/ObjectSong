using Microsoft.Xna.Framework.Input;

namespace ObjectSongEngineMG
{
    public class OSEInput
    {
        private Keys[] _keystate;
        private Keys[] _oldkeystate;

        private MouseState _mousestate;
        private MouseState _oldmousestate;


        public Keys[] NewKeyState
        {
            get
            {
                return _keystate;
            }
        }


        public Keys[] OldKeyState
        {
            get
            {
                return _oldkeystate;
            }
        }


        public MouseState NewMouseState
        {
            get
            {
                return _mousestate;
            }

        }


        public MouseState OldMouseState
        {
            get
            {
                return _oldmousestate;
            }

        }


        public OSEInput()
        {
            GetNewState();
        }


        public void Update()
        {
            SaveOldState();
            GetNewState();
        }


        public void SaveOldState()
        {
            _oldkeystate = _keystate;
            _oldmousestate = _mousestate;
        }


        public void GetNewState()
        {
            _keystate = Keyboard.GetState().GetPressedKeys();
            _mousestate = Mouse.GetState();
        }

    }
}

namespace ObjectSongEngineMG
{
    public class OSEPlayObject : OSESprite
    {
        private OSEAttributeList _attributes;
 

        public OSEAttributeList Attributes
        {
            get
            {
                return _attributes;
            }
            set
            {
                _attributes = value;
            }
        }


        public OSEPlayObject(OSESize2D size, OSELocation2D location) :base(size, location)
        {
            _attributes = new OSEAttributeList();
        }

    }
}

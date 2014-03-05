using System.Collections.Generic;
namespace ObjectSongEngineMG
{
    public class OSEPlayObject : OSESprite
    {
        private OSEAttributeList _attributes;
        private bool _obstacle;


        public bool IsObstacle
        {
            get
            {
                return _obstacle;
            }
            set
            {
                _obstacle = value;
            }
        }


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


        public bool CheckForHit(OSEPlayObject target)
        {
            if (base.CheckForHit(target as OSESprite))
            {
                _location.Copy(_oldlocation);
                return true;
            }
            else
            {
                _oldlocation.Copy(_location);
                return false;
            }
        }


        public bool CheckForHit(OSEMap map)
        {
            foreach (var item in map.Items)
            {
                if (base.CheckForHit(item as OSESprite))
                {
                    if (item.IsObstacle)
                    {
                        _location.Copy(_oldlocation);
                    }
                    return true;
                }
                else
                {
                    _oldlocation.Copy(_location);
                }
            }
            return false;
        }


    }
}

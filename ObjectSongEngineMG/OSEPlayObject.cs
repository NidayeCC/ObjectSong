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


        public OSEPlayObject()
            : base()
        {
            _attributes = new OSEAttributeList();
            IsObstacle = false;
        }


        public OSEPlayObject(OSESize2D size, OSELocation2D location) :base(size, location)
        {
            _attributes = new OSEAttributeList();
            IsObstacle = false;
        }


        public bool CheckForHit(OSEPlayObject target)
        {
            if (this.IsObstacle && target.IsObstacle)
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
            _oldlocation.Copy(_location);
            return false;
        }


        public bool CheckForHit(OSEMap map)
        {
            foreach (var item in map.Items)
            {
                if (this.IsObstacle && item.IsObstacle)
                {
                    if (base.CheckForHit(item as OSESprite))
                    {
                       _location.Copy(_oldlocation);
                        return true;
                    }
                    else
                    {

                    }
                }
                else
                {
                    
                }
            }
            _oldlocation.Copy(_location);
            return false;
        }


    }
}

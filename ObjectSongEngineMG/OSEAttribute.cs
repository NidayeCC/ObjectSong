using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectSongEngineMG
{
    public class OSEAttribute : IEquatable<OSEAttribute>
    {
        private string _key;
        private string _value;
        private Guid _id;


        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }


        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }


        public Guid ID
        {
            get
            {
                return _id;
            }
            internal set
            {
                _id = value;
            }
        }


        public OSEAttribute()
        {
            _id = Guid.NewGuid();
        }


        public bool Equals(OSEAttribute other)
        {
            if (other == null)
                return false;
            return (this.ID.Equals(other.ID));
        }
    }
}

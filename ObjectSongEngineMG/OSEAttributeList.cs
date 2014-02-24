using System;
using System.Collections.Generic;

namespace ObjectSongEngineMG
{
    public class OSEAttributeList
    {
        private readonly List<OSEAttribute> _items;

     


        public List<OSEAttribute> Items
        {
            get
            {
                return _items;
            }
        }


        public OSEAttributeList()
        {
            _items = new List<OSEAttribute>();
        }



        public void Add(string key, string value)
        {
            var item = new OSEAttribute
            {Key = key, Value = value};

            _items.Add(item);
        }


        public void Delete(Guid itemID)
        {
            if(_items.Exists(x => x.ID == itemID))
            {
                 _items.Remove(new OSEAttribute {ID = itemID});
            }
        }


        public void Update(Guid itemID, string newValue)
        {
            if(_items.Exists(x => x.ID == itemID))
            {
                OSEAttribute item;
                if((item = _items.Find(x => x.ID == itemID)) != null)
                {
                    item.Value = newValue;
                }
            }
        }


        public OSEAttribute Find(string itemKey)
        {
            OSEAttribute item;
            if((item = _items.Find(x => x.Key == itemKey)) != null)
            {
                return item;
            }
            return null;
        }


        public string GetValue(string itemKey)
        {
            OSEAttribute item;
            if ((item = _items.Find(x => x.Key == itemKey)) != null)
            {
                return item.Value;
            }
            return null;
        }


        public OSEAttribute Find(Guid itemID)
        {
// ReSharper disable RedundantAssignment
            OSEAttribute item = null;
            if((item = _items.Find(x => x.ID == itemID)) != null)
            {
                return item;
            }
            return null;
        }
    }
}

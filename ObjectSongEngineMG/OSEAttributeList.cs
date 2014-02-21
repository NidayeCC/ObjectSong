using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectSongEngineMG
{
    public class OSEAttributeList
    {
        private List<OSEAttribute> _items;

        public List<OSEAttribute> Items
        {
            get
            {
                return _items;
            }
        }


        public void Add(string Key, string Value)
        {
            OSEAttribute item = new OSEAttribute();
            item.Key = Key;
            item.Value = Value;
            _items.Add(item);
        }


        public void Delete(Guid itemID)
        {
            if(_items.Exists(x => x.ID == itemID))
            {
                 _items.Remove(new OSEAttribute(){ID = itemID});
            }
        }


        public void Update(Guid itemID, string newValue)
        {
            if(_items.Exists(x => x.ID == itemID))
            {
                OSEAttribute item = null;
                if((item = _items.Find(x => x.ID == itemID)) != null)
                {
                    item.Value = newValue;
                }
            }
        }


        public OSEAttribute Find(string itemKey)
        {
            OSEAttribute item = null;
            if((item = _items.Find(x => x.Key == itemKey)) != null)
            {
                return item;
            }
            return null;
        }


        public OSEAttribute Find(Guid itemID)
        {
            OSEAttribute item = null;
            if((item = _items.Find(x => x.ID == itemID)) != null)
            {
                return item;
            }
            return null;
        }
    }
}

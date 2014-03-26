using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ObjectSongEngineMG
{
    public class OSEMap
    {
        public List<OSEPlayObject> Items;
        private bool playobjexists;
        private OSEPlayObject playobj;
        private bool playobjshowhitbox;
        private GraphicsDevice device;
        private ContentManager content;


        public OSEMap(GraphicsDevice _device, ContentManager _content)
        {
            Items = new List<OSEPlayObject>();
            playobjexists = false;
            playobj = null;
            device = _device;
            content = _content;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var item in Items)
            {
                item.Draw(spriteBatch);
            }
        }


        public bool LoadFromFile(string fileName)
        {
  

            if(File.Exists(fileName))
            {
                XmlTextReader reader = new XmlTextReader(fileName);

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "WallSegment")
                    {
                        // If there is already a playobject being filled, then write it so we can start a new one
                        WritePlayObject();

                        playobj = new OSEPlayObject();
                        playobjexists = true;

                        if (reader.HasAttributes)
                        {
                            while (reader.MoveToNextAttribute())
                            {
                                if (reader.Name == "XLocation")
                                {
                                    playobj.Location.X = Convert.ToInt32(reader.Value);
                                }
                                if (reader.Name == "YLocation")
                                {
                                    playobj.Location.Y = Convert.ToInt32(reader.Value);
                                }
                                if (reader.Name == "Width")
                                {
                                    playobj.Size.Width = Convert.ToInt32(reader.Value);
                                }
                                if (reader.Name == "Height")
                                {
                                    playobj.Size.Height = Convert.ToInt32(reader.Value);
                                }
                                if (reader.Name == "TextureName")
                                {
                                    playobj.LoadTexture(device, content, "walltile16x16");
                                }
                                if (reader.Name == "IsObstacle")
                                {
                                    playobj.IsObstacle = Convert.ToBoolean(reader.Value);
                                }
                                if (reader.Name == "ShowHitBox")
                                {
                                    playobjshowhitbox = Convert.ToBoolean(reader.Value);
                                }
                            }
                        }
                        reader.MoveToElement();
                    }
                }

                // Write the last playobject
                WritePlayObject();
                return true;
            }
            return false;
        }


        protected void WritePlayObject()
        {
            if (playobjexists)
            {
                playobj.Visible = true;
                playobj.CreateHitBox(device);
                playobj.Hitbox.Visible = playobjshowhitbox;
                Items.Add(playobj);
                playobj = null;
                playobjexists = false;
                playobjshowhitbox = false;
            }
        }
    }

    }


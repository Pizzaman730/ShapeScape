using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SquarePlatformer
{
    public class ObjectTexture
    {
        public string name { get; set; }
        public TextureInfo[] textures { get; set; }
        public Dictionary<string, TextureInfo> GetTexturesAsDictionary()
        {
            Dictionary<string, TextureInfo> dict = new();
            foreach (TextureInfo info in textures)
            {
                dict.Add(info.name, info);
            }
            return dict;
        }
    }
}
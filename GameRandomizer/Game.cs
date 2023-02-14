using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace GameRandomizer
{
    class Game
    {
        public bool success { get; set; }
        public Data data { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Game game = (Game)obj;
            return data == null || game.data == null ? false : data.steam_appid == ((Game) obj).data.steam_appid;
        }
    }

    public class Data
    {
        public string type { get; set; }
        public string name { get; set; }
        public int steam_appid { get; set; }
        public string header_image { get; set; }
        public Category[] categories { get; set; }
        public Genre[] genres { get; set; }
        public string background { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public class Genre
    {
        public string id { get; set; }
        public string description { get; set; }
    }
}

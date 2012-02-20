using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Kirby
{
    class PropertyReader
    {
        TextReader Reader;
        string Root;

        public PropertyReader(string root)
        {
            Root = root;
        }

        public Dictionary<String, String> ReadPropertieFile(String filename)
        {
            Reader = new StreamReader(Root + filename);            
            string line;
            string[] splitline;
            Dictionary<String,String> props = new Dictionary<String,String>();
            while ((line = Reader.ReadLine()) != null)
            {
                char [] reg = {' '};
                splitline = line.Split(reg, 2);
                props.Add(splitline[0], splitline[1]);
            }
            return props;

        }
    }
}

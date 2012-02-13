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
        TextReader reader;

        public PropertyReader()
        {
            
        }

        public Dictionary<String, String> ReadStageProperties(String stagename)
        {
            reader = new StreamReader(stagename);            
            string line;
            string[] splitline;
            Dictionary<String,String> stageprops = new Dictionary<String,String>();
            while ((line = reader.ReadLine()) != null)
            {
                char [] reg = {' '};
                splitline = line.Split(reg, 2);
                stageprops.Add(splitline[0], splitline[1]);
            }
            return stageprops;

        }
    }
}

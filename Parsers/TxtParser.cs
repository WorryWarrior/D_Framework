using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace D_Framework
{
    public class TxtParser : IParser
    {
        public List<string> Parse(string path)
        {
            string line;
            List<string> lines = new List<string>();

            using (StreamReader fileSR = new StreamReader(path))
            {
                while ((line = fileSR.ReadLine()) != null)
                {
                    lines.Add(line);
                }

            }

            return lines;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace D_Framework
{
    public class TxtParser : IParser<string>
    {
        public IEnumerable<string> Parse(string path)
        {
            string line;

            using StreamReader fileSR = new StreamReader(path);
            while ((line = fileSR.ReadLine()) != null)
            {
                yield return line;
            }
        }
    }
}
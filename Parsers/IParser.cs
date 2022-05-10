using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    public interface IParser 
    {
        public List<string> Parse(string path); 
    }
}
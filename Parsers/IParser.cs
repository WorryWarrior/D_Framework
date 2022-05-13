using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    public interface IParser 
    {
        public List<string> Parse(string path); 
    }
}
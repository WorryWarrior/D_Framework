using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Framework
{
    public interface IParser<T>
    {
        public IEnumerable<T> Parse(string path); 
    }
}
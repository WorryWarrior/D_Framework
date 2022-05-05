using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace D_Utilities
{
    public interface IFactory<T>
    {
        public T GetElement(System.Enum elementType);
    }
}

using UnityEngine;

namespace Lingdar77.Functional.Structures
{

    [System.Serializable]
    public struct RangedData<Type> where Type : unmanaged
    {
        public Type min;
        public Type max;

        public RangedData(Type min, Type max)
        {
            this.max = max;
            this.min = min;
        }

        public override string ToString()
        {
            return "min: " + min + ", max: " + max;
        }
    }


}
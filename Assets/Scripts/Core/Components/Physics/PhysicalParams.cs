using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Core.Physics
{
    [Serializable]
    public class PhysicalDragData
    {
        [FormerlySerializedAs("drag")] public float linear;
        [FormerlySerializedAs("angularDrag")] public float angular;


        public PhysicalDragData(float linear, float angular)
        {
            this.linear = linear;
            this.angular = angular;
        }
    }
}
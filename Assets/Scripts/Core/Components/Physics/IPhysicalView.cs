using Core.Physics;
using System;


namespace Core.Components.Physics
{
    public interface IPhysicalView : IView<(Guid, PhysicalData), CollideData>
    {
        public Guid Id();
    }
}
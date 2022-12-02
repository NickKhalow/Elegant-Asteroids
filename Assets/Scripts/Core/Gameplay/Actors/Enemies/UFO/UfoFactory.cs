using Core.Components;
using Core.Components.Physics;
using Core.Gameplay.Actors.UFO;
using Core.Gameplay.Identity;
using Core.Gameplay.Spawners.IImpulses;
using Core.Physics;
using Core.Uitls;
using UnityEngine;


namespace Core.Gameplay.Actors.Enemies.UFO
{
    public class UfoFactory : IFactory<Ufo>
    {
        private readonly IFactory<Ufo> factory;


        public UfoFactory(IFactory<Ufo> factory)
        {
            this.factory = factory;
        }


        public UfoFactory(
            IdentityPool identityPool,
            UfoParams ufoParams,
            IFactory<IPhysicalView> physicalViewFactory,
            Vector2 boundaries,
            ITarget player,
            IImpulse impulse
        )
        {
            factory = new SimpleFactory<Ufo>(() =>
            {
                var physical = new Physical(
                    impulse.Value(),
                    boundaries,
                    physicalViewFactory.New()
                );
                return new Ufo(
                    identityPool,
                    ufoParams,
                    physical,
                    player
                );
            });
        }


        public Ufo New()
        {
            return factory.New();
        }
    }
}
using Core.Components.Physics;
using Core.Gameplay.Identity;
using Core.Gameplay.Spawners.IImpulses;
using Core.Physics;
using Core.Uitls;
using UnityEngine;


namespace Core.Gameplay.Actors.Asteroids
{
    public class AsteroidsFactory : IFactory<Asteroid>
    {
        private readonly IFactory<Asteroid> factory;


        public AsteroidsFactory(IFactory<Asteroid> factory)
        {
            this.factory = factory;
        }


        public AsteroidsFactory(
            IdentityPool identityPool,
            IFactory<IPhysicalView> physicalViewFactory,
            Vector2 boundaries,
            IImpulse impulse)
        {
            factory = new SimpleFactory<Asteroid>(() =>
            {
                return new Asteroid(
                    identityPool,
                    new Physical(
                        impulse.Value(),
                        boundaries,
                        physicalViewFactory.New()
                    )
                );
            });
        }


        public Asteroid New()
        {
            return factory.New();
        }
    }
}
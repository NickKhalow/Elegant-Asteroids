using Core.Gameplay.Guns.Projectile;
using Core.Gameplay.Guns.Projectiles;
using Core.Uitls;
using UnityEngine;
using View.Projectiles;


namespace View.Factory
{
    public class ProjectileViewFactory : MonoBehaviour, IFactory<IProjectileView>
    {
        [SerializeField] private ProjectileView projectileViewPrefab = null!;


        public IProjectileView New()
        {
            return Instantiate(projectileViewPrefab, transform)!;
        }
    }
}
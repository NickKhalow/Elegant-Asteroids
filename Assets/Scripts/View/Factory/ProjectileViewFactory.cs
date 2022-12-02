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
        private ObjectPool<ProjectileView> pool = null!;


        private void Awake()
        {
            projectileViewPrefab.EnsureNotNull();
            pool = new ObjectPool<ProjectileView>(
                () =>
                {
                    var view = Instantiate(projectileViewPrefab, transform)!;
                    view.Disposed += () => pool.Return(view);
                    return view;
                },
                view => view.gameObject.SetActive(false),
                view => view.gameObject.SetActive(true)
            );
        }


        public IProjectileView New()
        {
            return pool.New();
        }
    }
}
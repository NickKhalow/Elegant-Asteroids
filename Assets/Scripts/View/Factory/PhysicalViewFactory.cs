using Core.Components.Physics;
using Core.Uitls;
using UnityEngine;
using View.Actors;


namespace View.Factory
{
    public class PhysicalViewFactory : MonoBehaviour, IFactory<IPhysicalView>
    {
        [SerializeField] private GenericPhysicalView viewPrefab = null!;
        private ObjectPool<GenericPhysicalView> pool = null!;


        private void Awake()
        {
            viewPrefab.EnsureNotNull();
            pool = new ObjectPool<GenericPhysicalView>(
                () =>
                {
                    var view = Instantiate(viewPrefab, transform)!;
                    return view;
                },
                view => view.gameObject.SetActive(false),
                view => view.gameObject.SetActive(true)
            );
        }


        public IPhysicalView New()
        {
            return pool.New();
        }
    }
}
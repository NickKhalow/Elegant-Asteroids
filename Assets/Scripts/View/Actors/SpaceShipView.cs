using Core.Components;
using Core.Components.Physics;
using Core.Gameplay.Actors.Ship;
using Core.Physics;
using Core.Uitls;
using System;
using UnityEngine;
using View.Guns;


namespace View.Actors
{
    [RequireComponent(typeof(GenericPhysicalView))]
    public class SpaceShipView : MonoBehaviour, IView<SpaceShipData>
    {
        [SerializeField] private GunView laser;
        [SerializeField] private GunView gun;
        private GenericPhysicalView physicalView;


        public GunView Laser => laser;


        public GunView Gun => gun;


        public IPhysicalView PhysicalView => physicalView;


        public event Action<CollideData>? Event;


        private void Awake()
        {
            physicalView = GetComponent<GenericPhysicalView>().EnsureNotNull();
            physicalView.Event += data => Event?.Invoke(data);
        }


        public Guid Id()
        {
            return physicalView.Id();
        }


        public void Render(SpaceShipData data)
        {
            physicalView.Render(data.physicalData);
            //ignore
        }


        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
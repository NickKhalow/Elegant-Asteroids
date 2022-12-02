using Core.Components;
using Core.Components.Physics;
using Core.Physics;
using System;
using UnityEngine;


namespace View.Actors
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class GenericPhysicalView : MonoBehaviour, IPhysicalView
    {
        private Guid id;


        public event Action<CollideData>? Event;


        private void Awake()
        {
            id = Guid.NewGuid();
        }


        public void Render(PhysicalData data) { }


        public void Render((Guid, PhysicalData) data)
        {
            id = data.Item1;
            transform.position = data.Item2.coordinates;
            transform.rotation = Quaternion.Euler(0, 0, data.Item2.angle);
        }


        public void Dispose()
        {
            //TODO object pooling
            Destroy(gameObject);
        }


        public Guid Id()
        {
            return id;
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider!.TryGetComponent(out IPhysicalView physicalView))
            {
                Debug.Log($"Collide View {physicalView!.Id()} {physicalView.GetType().FullName}");
                Event?.Invoke(new CollideData(physicalView!.Id()));
            }
            else
            {
                throw new InvalidOperationException($"Not supported collider {col.collider.name}");
            }
        }
    }
}
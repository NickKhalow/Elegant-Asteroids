using Core.Gameplay.Guns;
using Core.Gameplay.Guns.Ammos;
using Core.Uitls;
using System;
using UnityEngine;


namespace View.Guns
{
    public class GunView : MonoBehaviour, IGunView
    {
        [SerializeField] private Transform barrelOutput = null!;


        private void Awake()
        {
            barrelOutput.EnsureNotNull();
        }


        public void Render(GunData data)
        {
            //data.
            //ignore
        }


        public void Dispose()
        {
            Destroy(gameObject);
        }


        public Vector2 BarrelPosition()
        {
            return barrelOutput.position;
        }


        public float BarrelRotation()
        {
            return barrelOutput.rotation.eulerAngles.z;
        }
    }
}
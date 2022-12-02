using Core.Components;
using Core.Gameplay.Actors.Ship;
using Core.Gameplay.Guns.Cooldowns;
using Core.Uitls;
using DefaultNamespace;
using System;
using TMPro;
using UnityEngine;


namespace View.Ui
{
    //[RequireComponent(typeof())]
    public class SpaceShipUi : MonoBehaviour, IView<SpaceShipData>
    {
        [SerializeField] private float refreshRate = 0.1f;
        private ICooldown cooldown;
        private TMP_Text text = null!;


        private void Awake()
        {
            text = GetComponent<TMP_Text>().EnsureNotNull();
            cooldown = new Cooldown(refreshRate);
        }


        private void Update()
        {
            cooldown.Tick(Time.deltaTime);
        }


        public void Render(SpaceShipData data)
        {
            if (cooldown.NextReady())
            {
                text.SetText(
                    $"Coordinates: {data.physicalData.coordinates:00}\nAngle: {data.physicalData.angle:0.00}\nSpeed: {data.physicalData.speed.magnitude:0.00}\n{data.gunData.ammo}");
            }
        }


        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}
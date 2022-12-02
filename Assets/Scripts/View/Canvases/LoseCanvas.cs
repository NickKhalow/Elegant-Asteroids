using Core.Uitls;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Game.Canvases
{
    public class LoseCanvas : MonoBehaviour
    {
        [SerializeField] private Button nextGameButton = null!;
        [SerializeField] private TMP_Text text = null!;


        

        public event Action? RequestNextGame;


        private void Awake()
        {
            nextGameButton.EnsureNotNull().onClick.AddListener(() => RequestNextGame?.Invoke());
            text.EnsureNotNull();
        }


        public void Show(int score)
        {
            text.SetText($"Your Result: {score}");
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
using TMPro;
using UnityEngine;

namespace UI
{
    public class Score : MonoBehaviour
    {
        public static Score Instance;

        [SerializeField] private TMP_Text _scoreText;
        
        private int _scoreValue;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance == this) 
                Destroy(gameObject);
        }

        public void AddScore(int mergeValue)
        {
            if (mergeValue < 0) return;
            
            _scoreValue += mergeValue;
            
            _scoreText.text = _scoreValue.ToString();
        }
    }
}
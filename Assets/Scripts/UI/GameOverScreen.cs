using General;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private GameOverChecker _gameOverChecker;
        [SerializeField] private GameObject _cubeHandlers;
        [SerializeField] private CanvasGroup _gameOverScreen;
        [SerializeField] private CanvasGroup _scoreScreen;
        [SerializeField] private CanvasGroup _timerContainer;
        [SerializeField] private TMP_Text _timerText;

        private void OnEnable()
        {
            _gameOverChecker.OnGameOver += ShowScreen;
            _gameOverChecker.OnTimeLeftChanged += UpdateTimer;
            _gameOverChecker.OnTimerStarted += ShowTimer;
            _gameOverChecker.OnTimerStopped += HideTimer;
        }

        private void OnDisable()
        {
            _gameOverChecker.OnGameOver -= ShowScreen;
            _gameOverChecker.OnTimeLeftChanged -= UpdateTimer;
            _gameOverChecker.OnTimerStarted -= ShowTimer;
            _gameOverChecker.OnTimerStopped -= HideTimer;
        }

        private void ShowScreen()
        {
            _cubeHandlers.SetActive(false);
            _scoreScreen.alpha = 0f;
            _gameOverScreen.alpha = 1f;
        }
        
        private void UpdateTimer(float timeLeft)
        {
            _timerText.text = $"Time to lose: {Mathf.CeilToInt(timeLeft)}";
        }
        
        private void ShowTimer()
        {
            _timerContainer.alpha = 1f;
        }

        private void HideTimer()
        {
            _timerContainer.alpha = 0f;
        }
    }
}
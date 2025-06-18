using System;
using System.Collections;
using Cube;
using UnityEngine;

namespace General
{
    public class GameOverChecker : MonoBehaviour
    {
        [SerializeField] private float _timeToLeft;
        
        private float _timer;
        private Coroutine _gameOverCoroutine;
        
        public event Action OnGameOver;
        public event Action<float> OnTimeLeftChanged;
        public event Action OnTimerStarted;
        public event Action OnTimerStopped;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit) &&
                !cubeUnit.IsMainCube)
            {
                if (_gameOverCoroutine == null)
                {
                    _gameOverCoroutine = StartCoroutine(GameOverTimer());
                    OnTimerStarted?.Invoke();
                } 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit) &&
                !cubeUnit.IsMainCube)
            {
                if (_gameOverCoroutine != null)
                {
                    StopCoroutine(_gameOverCoroutine);
                    _gameOverCoroutine = null;
                    _timer = 0f;
                    OnTimeLeftChanged?.Invoke(_timeToLeft);
                    OnTimerStopped?.Invoke();
                }
            }
        }

        private IEnumerator GameOverTimer()
        {
            while (_timeToLeft > _timer)
            {
                OnTimeLeftChanged?.Invoke(_timeToLeft - _timer);
                yield return new WaitForSeconds(1f);
                _timer++;
            }
            
            OnGameOver?.Invoke();
            OnTimerStopped?.Invoke();
        }
    }
}
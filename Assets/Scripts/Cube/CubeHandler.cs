using System;
using System.Collections;
using Handlers;
using UnityEngine;

namespace Cube
{
    public class CubeHandler : MonoBehaviour
    {
        [SerializeField] protected InputHandler _inputHandler;
        [SerializeField] private CubeSpawner _cubeSpawner;

        protected CubeUnit CubeUnit;
        protected Vector3 TouchPosition;
        
        private void OnEnable()
        {
            _cubeSpawner.OnNewCubeSpawned += OnCubeSpawned;
            _inputHandler.OnPressStarted += OnPressStarted;
            _inputHandler.OnPressCanceled += OnPressCanceled;
        }

        private void OnDisable()
        {
            _cubeSpawner.OnNewCubeSpawned -= OnCubeSpawned;
            _inputHandler.OnPressStarted -= OnPressStarted;
            _inputHandler.OnPressCanceled -= OnPressCanceled;
        }

        private void OnCubeSpawned(CubeUnit newCube)
        {
            CubeUnit = newCube;
        }

        protected virtual void OnPressStarted()
        {
            if (CubeUnit == null) return;

            StartCoroutine(DelayedPressStart());
        }

        private IEnumerator DelayedPressStart()
        {
            yield return null;
            
            if (_inputHandler.ClickedUI) yield break;
            
            _inputHandler.OnPerformedPointer += OnPerformedPointer;
        }

        protected virtual void OnPerformedPointer()
        {
            if (CubeUnit == null) return;
            
            TouchPosition = _inputHandler.GetTouchPosition(CubeUnit.transform);
        }
        
        protected virtual void OnPressCanceled()
        {
            _inputHandler.OnPerformedPointer -= OnPerformedPointer;
        }
    }
}
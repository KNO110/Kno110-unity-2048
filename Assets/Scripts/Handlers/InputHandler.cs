using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Handlers
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        
        private TouchScreenAction _screenTouch;

        public event Action OnPressStarted;
        public event Action OnPerformedPointer;
        public event Action OnPressCanceled;
        
        public bool ClickedUI { get; private set; }

        private void Awake()
        {
            _screenTouch = new TouchScreenAction();
            
            _screenTouch.TouchScreen.PressScreen.started += _ => OnPressStarted?.Invoke();
            _screenTouch.TouchScreen.PressScreen.canceled += _ => OnPressCanceled?.Invoke();
            _screenTouch.TouchScreen.TouchPosition.performed += _ => OnPerformedPointer?.Invoke();
        }

        private void OnEnable()
        {
            _screenTouch.TouchScreen.Enable();
        }

        private void LateUpdate()
        {
            ClickedUI = EventSystem.current.IsPointerOverGameObject();
        }

        private void OnDisable()
        {
            _screenTouch.TouchScreen.Disable();
        }

        public Vector3 GetTouchPosition(Transform cubeTransform)
        {
            var depth = Vector3.Distance(_mainCamera.transform.position, cubeTransform.position);
            var screenPosition = _screenTouch.TouchScreen.TouchPosition.ReadValue<Vector2>();
            
            return _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, depth));
        }
    }
}
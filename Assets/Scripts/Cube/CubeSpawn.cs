using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Handlers;
using UnityEngine.EventSystems;

namespace Cube
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private CubeThrowers _cubeThrower;
        [SerializeField] private CubeUnit _cubePrefab;
        [SerializeField] private Transform _spawnPoint;

        private CubeUnit _activeCube;
        private Coroutine _waitForStopCoroutine;
        
        private List<CubeUnit> _cubeUnits = new List<CubeUnit>();
        
        public event Action<CubeUnit> OnNewCubeSpawned;

        private void Start()
        {
            _cubeUnits.Add(SpawnCube(_cubePrefab));
        }

        private void OnEnable()
        {
            _cubeThrower.OnCubeThrowed += OnCubeThrowed;
        }

        private void OnDisable()
        {
            _cubeThrower.OnCubeThrowed -= OnCubeThrowed;
        }

        private CubeUnit SpawnCube(CubeUnit cubeUnit)
        {
            var newCube = Instantiate(cubeUnit, _spawnPoint.position, Quaternion.identity, transform);
            _activeCube = newCube;
            
            _activeCube.gameObject.layer = _activeCube.CubeUnitData.MainCubeLayer;
            _activeCube.SetMainCube(true);
            _activeCube.CubeViewer.SetCubeView();
        
            OnNewCubeSpawned?.Invoke(_activeCube);

            return _activeCube;
        }

        private void OnCubeThrowed(CubeUnit thrownCube)
        {
            if (_inputHandler.ClickedUI) return;
            
            _activeCube = null;
            
            if (_waitForStopCoroutine != null)
                StopCoroutine(_waitForStopCoroutine);

            _waitForStopCoroutine = StartCoroutine(WaitForCubeToStop(thrownCube));
        }

        private IEnumerator WaitForCubeToStop(CubeUnit cube)
        {
            const float threshold = 0.1f;
            const float delay = 0.1f;
            const float timeout = 3f;

            var cubeRigidbody = cube.Rigidbody;
            var timer = 0f;
            
            while (cubeRigidbody != null && cubeRigidbody.linearVelocity.sqrMagnitude > threshold)
            {
                yield return new WaitForSeconds(delay);
                
                timer += delay;

                if (timer >= timeout)
                {
                    break;
                }
            }

            cube.CubeMerger.enabled = true;
            
            TakeCubeFromPool(cube);
        }

        private void TakeCubeFromPool(CubeUnit cube)
        {
            for (int i = 0; i < _cubeUnits.Count; i++)
            { 
                var cubeUnit = _cubeUnits[i];
                
                if (!_cubeUnits[i].gameObject.activeSelf)
                {
                    ResetCube(cubeUnit);

                    cubeUnit.gameObject.SetActive(true);
                    cubeUnit.CubeMerger.enabled = false;  
                    
                    cubeUnit.SetMainCube(true);
                    cubeUnit.CubeViewer.SetCubeView();
                    
                    _activeCube = cubeUnit;
                    
                    OnNewCubeSpawned?.Invoke(cubeUnit);
                    
                    return;
                }
            }
            
            _cubeUnits.Add(SpawnCube(cube));
        }

        private void ResetCube(CubeUnit cubeUnit)
        {
            cubeUnit.Rigidbody.linearVelocity = Vector3.zero;
            cubeUnit.Rigidbody.angularVelocity = Vector3.zero;
            cubeUnit.transform.position = _spawnPoint.position;
            cubeUnit.transform.rotation = Quaternion.identity;
        }

        public void SpawnBonusCube(CubeUnit bonusCube)
        {
            if (_waitForStopCoroutine != null)
                StopCoroutine(_waitForStopCoroutine);

            if (_activeCube != null)
            {
                _activeCube.gameObject.SetActive(false);
                _activeCube = null;   
            }
            
            SpawnCube(bonusCube);
        }
    }
}
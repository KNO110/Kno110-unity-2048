using System;
using System.Collections;
using System.Collections.Generic;
using Handlers;
using UnityEngine;

namespace Cube
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private CubeThrowers _cubeThrower;
        [SerializeField] private CubeUnit _cubePrefab;
        [SerializeField] private CubeUnit _blackHolePrefab;
        [SerializeField] private Transform _spawnPoint;

        private CubeUnit _activeCube;
        private Coroutine _waitForStopCoroutine;
        private readonly List<CubeUnit> _cubeUnits = new List<CubeUnit>();

        public event Action<CubeUnit> OnNewCubeSpawned;

        private void Start()
        {
            var first = SpawnCube(_cubePrefab);
            _cubeUnits.Add(first);
        }

        private void OnEnable()
        {
            _cubeThrower.OnCubeThrowed += OnCubeThrowed;
        }

        private void OnDisable()
        {
            _cubeThrower.OnCubeThrowed -= OnCubeThrowed;
        }

        private CubeUnit SpawnCube(CubeUnit prefab)
        {
            if (_waitForStopCoroutine != null)
                StopCoroutine(_waitForStopCoroutine);

            if (_activeCube != null)
            {
                _activeCube.gameObject.SetActive(false);
                _activeCube = null;
            }

            var newCube = Instantiate(prefab, _spawnPoint.position, Quaternion.identity, transform);
            _activeCube = newCube;

            var data = newCube.CubeUnitData;
            newCube.gameObject.layer = data.MainCubeLayer;
            newCube.SetMainCube(true);

            if (newCube.CubeViewer != null)
                newCube.CubeViewer.SetCubeView();
            if (newCube.CubeMerger != null)
                newCube.CubeMerger.enabled = false;

            OnNewCubeSpawned?.Invoke(newCube);
            return newCube;
        }

        private void OnCubeThrowed(CubeUnit thrownCube)
        {
            if (_inputHandler.ClickedUI)
                return;

            if (thrownCube.GetComponent<BlackHole>() != null)
            {
                if (_waitForStopCoroutine != null)
                    StopCoroutine(_waitForStopCoroutine);

                _activeCube = null;
                _waitForStopCoroutine = StartCoroutine(SpawnNextFrame());
                return;
            }

            _activeCube = null;

            if (_waitForStopCoroutine != null)
                StopCoroutine(_waitForStopCoroutine);

            _waitForStopCoroutine = StartCoroutine(WaitForCubeToStop(thrownCube));
        }

        private IEnumerator SpawnNextFrame()
        {
            yield return null;
            var next = SpawnCube(_cubePrefab);
            _cubeUnits.Add(next);
            _waitForStopCoroutine = null;
        }

        private IEnumerator WaitForCubeToStop(CubeUnit cube)
        {
            const float threshold = 0.1f;
            const float delay = 0.1f;
            const float timeout = 3f;
            var rb = cube.Rigidbody;
            var timer = 0f;

            while (rb != null && rb.linearVelocity.sqrMagnitude > threshold)
            {
                yield return new WaitForSeconds(delay);
                timer += delay;
                if (timer >= timeout)
                    break;
            }

            if (cube.CubeMerger != null)
                cube.CubeMerger.enabled = true;

            TakeCubeFromPool(cube);
            _waitForStopCoroutine = null;
        }

        private void TakeCubeFromPool(CubeUnit cube)
        {
            foreach (var cu in _cubeUnits)
            {
                if (!cu.gameObject.activeSelf)
                {
                    cu.Rigidbody.linearVelocity = Vector3.zero;
                    cu.Rigidbody.angularVelocity = Vector3.zero;
                    cu.transform.position = _spawnPoint.position;
                    cu.transform.rotation = Quaternion.identity;

                    cu.gameObject.SetActive(true);
                    if (cu.CubeMerger != null)
                        cu.CubeMerger.enabled = false;
                    cu.SetMainCube(true);
                    if (cu.CubeViewer != null)
                        cu.CubeViewer.SetCubeView();
                    _activeCube = cu;
                    OnNewCubeSpawned?.Invoke(cu);
                    return;
                }
            }

            var spawned = SpawnCube(cube);
            _cubeUnits.Add(spawned);
            _waitForStopCoroutine = null;
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

            var spawned = SpawnCube(bonusCube);
            _cubeUnits.Add(spawned);
        }

        public void SpawnBlackHole()
        {
            if (_waitForStopCoroutine != null)
                StopCoroutine(_waitForStopCoroutine);

            if (_activeCube != null)
            {
                _activeCube.gameObject.SetActive(false);
                _activeCube = null;
            }

            SpawnCube(_blackHolePrefab);
        }
    }
}

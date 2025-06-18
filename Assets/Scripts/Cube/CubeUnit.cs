using Cube.Merger;
using Cube.SO;
using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeUnit : MonoBehaviour
    {
        [Header("Данные куба")]
        [SerializeField] private CubeUnitSo _cubeUnitData;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CubeMerger _cubeMerger;
        [SerializeField] private CubeViewer _cubeViewer;
        [SerializeField] private CubeSfx _cubeSfx;

        private bool _isMainCube;
        private int _cubeNumber;

        public CubeUnitSo CubeUnitData => _cubeUnitData;
        public Rigidbody Rigidbody => _rigidbody;
        public CubeMerger CubeMerger => _cubeMerger;
        public CubeViewer CubeViewer => _cubeViewer;
        public bool IsMainCube => _isMainCube;
        public int CubeNumber => _cubeNumber;

        private void Reset()
        {
            _rigidbody   = GetComponent<Rigidbody>();
            _cubeMerger  = GetComponent<CubeMerger>();
            _cubeViewer  = GetComponent<CubeViewer>();
            _cubeSfx     = GetComponent<CubeSfx>();
        }

        private void Awake()
        {
            if (_rigidbody == null)   _rigidbody   = GetComponent<Rigidbody>();
            if (_cubeMerger == null)  _cubeMerger  = GetComponent<CubeMerger>();
            if (_cubeViewer == null)  _cubeViewer  = GetComponent<CubeViewer>();
            if (_cubeSfx == null)     _cubeSfx     = GetComponent<CubeSfx>();
        }

        public void SetMainCube(bool isMainCube)
        {
            _isMainCube = isMainCube;
        }

        public void SetCubeNumber(int cubeNumber)
        {
            if (cubeNumber < 2) return;
            _cubeNumber = cubeNumber;
        }
    }
}

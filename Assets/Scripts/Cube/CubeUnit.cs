using Cube.Merger;
using Cube.SO;
using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeUnit : MonoBehaviour
    {
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
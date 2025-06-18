using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cube
{
    public class CubeViewer : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private List<TMP_Text> _texts;

        private void OnEnable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged += SetMergedCubeView;
        }

        private void OnDisable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged += SetMergedCubeView;
        }

        private void SetMergedCubeView(int cubeNumber, Vector3 position)
        {
            _cubeUnit.SetCubeNumber(cubeNumber);
            
            foreach (var tmpText in _texts)
            {
                tmpText.text = cubeNumber.ToString();
            }
            
            var cubeColor = _cubeUnit.CubeUnitData.CubeColor(cubeNumber);
            _meshRenderer.material.color = cubeColor;
        }

        public void SetCubeView()
        {
            _cubeUnit.SetCubeNumber(_cubeUnit.CubeUnitData.CubeNumber());

            foreach (var tmpText in _texts)
            {
                tmpText.text = _cubeUnit.CubeNumber.ToString();
            }
            
            var cubeColor = _cubeUnit.CubeUnitData.CubeColor(_cubeUnit.CubeNumber);
            _meshRenderer.material.color = cubeColor;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cube.SO
{
    [CreateAssetMenu(fileName = "New GameCube", menuName = "GameCube", order = 0)]
    public class CubeUnitSo : ScriptableObject
    {
        [SerializeField] private int _mainCubeLayer = 6;
        [SerializeField] private int _cubeOnBoardLayer = 7;
        [SerializeField] private List<Color> _colors;
        [SerializeField] private List<int> _chances;
        
        public int MainCubeLayer => _mainCubeLayer;
        public int CubeOnBoardLayer => _cubeOnBoardLayer;
        
        public Color CubeColor(int cubeNumber)
        {
            var colorIndex = (int)Mathf.Log(cubeNumber, 2) - 1;
            
            return _colors[colorIndex];
        }
        
        public int CubeNumber()
        {
            var roll = Random.Range(0, 100);
            var cumulative = 0;

            for (int i = 0; i < _chances.Count; i++)
            {
                cumulative += _chances[i];
                if (roll < cumulative)
                    return (int)Mathf.Pow(2, i + 1);
            }
            
            return (int)Mathf.Pow(2, _chances.Count);
        }
    }
}
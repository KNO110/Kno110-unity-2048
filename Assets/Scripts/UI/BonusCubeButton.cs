using Cube;
using Cube.Merger;
using UnityEngine;

namespace UI
{
    public class BonusCubeButton : UIButton
    {
        [SerializeField] private CubeUnit _bonusCube;
        [SerializeField] private CubeSpawner _cubeSpawner;
        
        protected override void OnButtonClick()
        {
            _cubeSpawner.SpawnBonusCube(_bonusCube);
        }
    }
}
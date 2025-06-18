using UnityEngine;

namespace Cube
{
    public class CubeMover : CubeHandler
    {
        protected override void OnPerformedPointer()
        {
            base.OnPerformedPointer();

            if (CubeUnit.IsMainCube)
            {
                MoveCube();
            }
        }
        
        private void MoveCube()
        {
            var clampPointerPositionX = Mathf.Clamp(TouchPosition.x, -4f, 4f);
            var newCubePosition = new Vector3(clampPointerPositionX, CubeUnit.transform.position.z, CubeUnit.transform.position.z);
                
            CubeUnit.transform.position = newCubePosition;
        }
    }
}
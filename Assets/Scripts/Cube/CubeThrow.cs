using System;
using UnityEngine;

namespace Cube
{
    public class CubeThrowers : CubeHandler
    {
        [SerializeField] private float _throwForce;

        public event Action<CubeUnit> OnCubeThrowed;

        protected override void OnPressCanceled()
        {
            if (CubeUnit == null) return;

            if (CubeUnit.IsMainCube)
            {
                ThrowCube();
            }
            
            base.OnPressCanceled();
        }
        
        private void ThrowCube()
        {
            CubeUnit.gameObject.layer = CubeUnit.CubeUnitData.CubeOnBoardLayer;
            CubeUnit.Rigidbody.linearVelocity = Vector3.forward * _throwForce;
            OnCubeThrowed?.Invoke(CubeUnit);

            CubeUnit.SetMainCube(false);
            CubeUnit = null;
        }
    }
}
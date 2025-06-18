using System;
using UI;
using UnityEngine;

namespace Cube.Merger
{
    public class BombMerger : CubeMerger
    {
        [SerializeField] private float _explosionRadius;    ///Зазначаем фигню для радиуса взрыв (мб потом сделаю ядерную приколюху
        
        public override void MergeHandle(CubeUnit self, CubeUnit other)
        {
            var impulseValue = self.Rigidbody.linearVelocity.sqrMagnitude;
            
            if (impulseValue > _minImpulseValueForMerge)
            {
                var cubes = Physics.OverlapSphere(self.transform.position, _explosionRadius);

                foreach (var cube in cubes)
                {
                    if (cube.TryGetComponent(out CubeUnit cubeUnit))
                    {
                        cubeUnit.gameObject.SetActive(false);
                        cubeUnit.CubeMerger.enabled = false;
                        
                        var mergeValue = other.CubeNumber / 2;
                        Score.Instance.AddScore(mergeValue);
                    }
                }

                self.gameObject.SetActive(false);
                self.CubeMerger.enabled = false;
                
                InvokeCubeMerged(other.CubeNumber * 2, transform.position);
                
                TossMergeCube();
            }
            else
            {
                InvokeCubeHitted(transform.position);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}

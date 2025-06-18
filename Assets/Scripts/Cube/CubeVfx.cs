using UnityEngine;

namespace Cube
{
    public class CubeVfx : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private ParticleSystem _mergeSfx;
        [SerializeField] private ParticleSystem _hitSfx;

        private void OnEnable()
        {
            if (_cubeUnit?.CubeMerger != null)
            {
                _cubeUnit.CubeMerger.OnCubeMerged += PlayVfxOnCubeMerged;
                _cubeUnit.CubeMerger.OnCubeHitted += PlayVfxOnCubeHitted;
            }
        }

        private void OnDisable()
        {
            if (_cubeUnit?.CubeMerger != null)
            {
                _cubeUnit.CubeMerger.OnCubeMerged -= PlayVfxOnCubeMerged;
                _cubeUnit.CubeMerger.OnCubeHitted -= PlayVfxOnCubeHitted;
            }
        }

        private void PlayVfxOnCubeMerged(int value, Vector3 position)
        {
            if (_mergeSfx != null)
            {
                _mergeSfx.transform.position = position;
                _mergeSfx.Play();
            }
        }

        private void PlayVfxOnCubeHitted(Vector3 position)
        {
            if (_hitSfx != null)
            {
                _hitSfx.transform.position = position;
                _hitSfx.Play();
            }
        }
    }
}
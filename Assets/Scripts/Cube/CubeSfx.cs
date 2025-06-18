using UnityEngine;

namespace Cube
{
    public class CubeSfx : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private AudioSource _mergeSfx;
        [SerializeField] private AudioSource _hitSfx;

        private void OnEnable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged += PlaySfxOnCubeMerged;
            _cubeUnit.CubeMerger.OnCubeHitted += PlaySfxOnCubeHitted;
        }

        private void OnDisable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged -= PlaySfxOnCubeMerged;
            _cubeUnit.CubeMerger.OnCubeHitted -= PlaySfxOnCubeHitted;
        }

        private void PlaySfxOnCubeMerged(int value, Vector3 position)
        {
            _mergeSfx.Play();
        }

        private void PlaySfxOnCubeHitted(Vector3 position)
        {
            _hitSfx.Play();
        }
    }
}
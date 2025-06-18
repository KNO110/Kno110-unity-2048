using UI;

namespace Cube.Merger
{
    public class UniversalMerger : CubeMerger
    {
        public override void MergeHandle(CubeUnit self, CubeUnit other)
        {
            var impulseValue = self.Rigidbody.linearVelocity.sqrMagnitude;
            
            if (impulseValue > _minImpulseValueForMerge)
            {
                self.gameObject.SetActive(false);
                self.CubeMerger.enabled = false;
                
                other.gameObject.SetActive(false);
                other.CubeMerger.enabled = false;
                
                var mergeValue = other.CubeNumber / 2;
                Score.Instance.AddScore(mergeValue);

                InvokeCubeMerged(other.CubeNumber * 2, transform.position);
                
                TossMergeCube();
            }
            else
            {
                InvokeCubeHitted(transform.position);
            }
        }
    }
}
using UI;

namespace Cube.Merger
{
    public class RegularMerger : CubeMerger
    {
        public override void MergeHandle(CubeUnit self, CubeUnit other)
        {
            var impulseValue = self.Rigidbody.linearVelocity.sqrMagnitude;
            
            if (other.CubeNumber == self.CubeNumber &&
                impulseValue > _minImpulseValueForMerge)
            {
                other.gameObject.SetActive(false);
                other.CubeMerger.enabled = false;
                
                var mergeValue = self.CubeNumber / 2;
                Score.Instance.AddScore(mergeValue);

                InvokeCubeMerged(self.CubeNumber * 2, transform.position);
                
                TossMergeCube();
            }
            else
            {
                InvokeCubeHitted(transform.position);
            }
        }
    }
}
using System.Collections;
using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
    public class BlackHole : MonoBehaviour
    {
        [Header("Настройки чёрной дыры")]
        [Tooltip("Стартовый радиус области притяжения")]
        [SerializeField] private float range = 6f;
        [Tooltip("Стартовая сила притяжения")]
        [SerializeField] private float forceStrength = 30f;
        [Tooltip("Тег объектов для притяжения")]
        [SerializeField] private string targetTag = "Cube";
        [Tooltip("Общая длительность жизни после броска, сек")]
        [SerializeField] private float lifetimeAfterThrow = 5f;
        [Tooltip("Длительность усиления перед взрывом, сек")]
        [SerializeField] private float rampUpDuration = 2f;
        [Tooltip("Радиус взрыва")]
        [SerializeField] private float explosionRadius = 7f;
        [Tooltip("Сила взрывного отталкивания")]
        [SerializeField] private float explosionForce = 30f;

        private Rigidbody _rb;
        private SphereCollider _trigger;
        private bool _thrown;

        private float _initialRange;
        private float _initialForce;

        private void Reset()
        {
            SetupComponents();
        }

        private void Awake()
        {
            SetupComponents();
        }

        private void SetupComponents()
        {
            _trigger = GetComponent<SphereCollider>();
            _trigger.isTrigger = true;
            _trigger.radius = range;

            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;

            _initialRange = range;
            _initialForce = forceStrength;
            _thrown = false;
            transform.localScale = Vector3.one;
        }

        private void Update()
        {
            if (!_thrown && !_rb.isKinematic && _rb.linearVelocity.sqrMagnitude > 0.1f)
            {
                _thrown = true;
                StartCoroutine(SequenceAfterThrow());
            }
        }

        private IEnumerator SequenceAfterThrow()
        {
            float waitTime = Mathf.Max(0f, lifetimeAfterThrow - rampUpDuration);
            yield return new WaitForSeconds(waitTime);

            float elapsed = 0f;
            while (elapsed < rampUpDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / rampUpDuration);
                forceStrength = Mathf.Lerp(_initialForce, explosionForce, t);
                float currRadius = Mathf.Lerp(_initialRange, _initialRange * 0.2f, t);
                _trigger.radius = currRadius;
                transform.localScale = Vector3.one * (currRadius / _initialRange);
                yield return null;
            }

            Explode();
            transform.localScale = Vector3.one * (explosionRadius / _initialRange);

            Destroy(gameObject);
        }

        private void Explode()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var col in hits)
            {
                if (!col.CompareTag(targetTag)) continue;
                var otherRb = col.attachedRigidbody;
                if (otherRb == null) continue;

                Vector3 dir = (col.transform.position - transform.position).normalized;
                otherRb.AddForce(dir * explosionForce, ForceMode.Impulse);
            }

            _trigger.radius = explosionRadius;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag(targetTag))
                return;

            var otherRb = other.attachedRigidbody;
            if (otherRb == null)
                return;

            Vector3 dir = (transform.position - other.transform.position).normalized;
            float dist = Vector3.Distance(transform.position, other.transform.position);
            float pull = forceStrength / Mathf.Max(dist, 0.1f);

            otherRb.AddForce(dir * pull, ForceMode.Acceleration);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            float drawRadius = (_trigger != null) ? _trigger.radius : range;
            Gizmos.DrawWireSphere(transform.position, drawRadius);
        }
    }
}

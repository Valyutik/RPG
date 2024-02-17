using UnityEngine;

namespace PlayForge_Team.RPG.Runtime
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public sealed class Enemy : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Dead = Animator.StringToHash("Dead");
        
        [SerializeField] private float speed = 3f;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float followDistance = 10f;
        [SerializeField] private float attackDistance = 1f;
        [SerializeField] private Transform target;
        [SerializeField] private LayerMask weaponLayerMask;

        private Rigidbody _rigidbody;
        private Collider _collider;
        private Animator _animator;
        private bool _isAttack;
        private bool _isDead;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (_isDead)
                return;

            _isAttack = false;
            if (CheckDistanceToTarget(attackDistance))
            {
                _isAttack = true;
            }
            else if (CheckDistanceToTarget(followDistance))
            {
                var tr = transform;
                var currentPosition = tr.position;
                var targetPosition = target.position;
                var forward = tr.forward;
                var walkingDirection = targetPosition - currentPosition;

                var turningDirection =
                    Vector3.RotateTowards(forward, walkingDirection, rotationSpeed * Time.deltaTime, 0);
                currentPosition =
                    Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
                
                tr.rotation = Quaternion.LookRotation(turningDirection);
                tr.position = currentPosition;
                
                _animator.SetFloat(Speed, 1);
            }
            else
            {
                _animator.SetFloat(Speed, 0);
            }
            _animator.SetBool(Attack, _isAttack);
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((weaponLayerMask & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
            {
                Death();
            }
        }

        private void Death()
        {
            _isDead = true;
            _animator.SetTrigger(Dead);
            _rigidbody.isKinematic = true;
            _collider.enabled = false;
        }

        private bool CheckDistanceToTarget(float distance)
        {
            return Vector3.Distance(transform.position, target.position) < distance;
        }
    }
}

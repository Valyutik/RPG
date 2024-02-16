using UnityEngine;

namespace PlayForge_Team.RPG.Runtime
{
    public sealed class Enemy : MonoBehaviour
    {
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        [SerializeField] private float speed = 3f;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float followDistance = 10f;
        [SerializeField] private float attackDistance = 1f;
        [SerializeField] private Transform target;

        private Animator _animator;
        private bool _isAttack;
        private bool _isDead;

        private void Start()
        {
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

        private bool CheckDistanceToTarget(float distance)
        {
            return Vector3.Distance(transform.position, target.position) < distance;
        }
    }
}

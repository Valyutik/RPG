using StarterAssets;
using UnityEngine;

namespace PlayForge_Team.RPG.Runtime
{
    [RequireComponent(typeof(Animator), typeof(StarterAssetsInputs))]
    public sealed class PlayerActions : MonoBehaviour
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        [SerializeField] private Collider weaponCollider;
        
        private StarterAssetsInputs _starterAssetsInputs;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
            weaponCollider.enabled = false;
        }

        private void Update()
        {
            OnAttack();
        }

        private void OnAttack()
        {
            _animator.SetBool(Attack, _starterAssetsInputs.attack);
        }
        
        private void OnAttackStart()
        {
            weaponCollider.enabled = true;
        }

        private void OnAttackFinish()
        {
            _starterAssetsInputs.attack = false;
            weaponCollider.enabled = false;
        }
    }
}

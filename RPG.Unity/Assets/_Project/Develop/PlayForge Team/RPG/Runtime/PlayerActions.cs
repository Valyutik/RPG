using JetBrains.Annotations;
using StarterAssets;
using UnityEngine;

namespace PlayForge_Team.RPG.Runtime
{
    [RequireComponent(typeof(Animator), typeof(StarterAssetsInputs))]
    public sealed class PlayerActions : MonoBehaviour
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        [SerializeField] private Collider weaponCollider;
        [SerializeField] private Skills skills;
        
        private StarterAssetsInputs _starterAssetsInputs;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
            weaponCollider.enabled = false;
        }

        private void OnEnable()
        {
            _starterAssetsInputs.OnOpenSkillsEvent += skills.OnToggleVisibilitySkillWindow;
        }

        private void OnDisable()
        {
            _starterAssetsInputs.OnOpenSkillsEvent -= skills.OnToggleVisibilitySkillWindow;
        }

        private void Update()
        {
            OnAttack();
        }

        private void OnAttack()
        {
            _animator.SetBool(Attack, _starterAssetsInputs.attack);
        }
        
        [UsedImplicitly]
        private void OnAttackStart()
        {
            weaponCollider.enabled = true;
        }

        [UsedImplicitly]
        private void OnAttackFinish()
        {
            _starterAssetsInputs.attack = false;
            weaponCollider.enabled = false;
        }
    }
}

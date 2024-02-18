using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlayForge_Team.RPG.Runtime
{
    public sealed class Skills : MonoBehaviour
    {
        [SerializeField] private int[] expToReachLevel;
        [SerializeField] private int expByEnemyKill;
        [SerializeField] private TextMeshProUGUI unallocatedSkillsText, healthText, strengthText, expText, levelText;
        [SerializeField] private Slider expSlider;
        [SerializeField] private Button healthButton, strengthButton;
        [SerializeField] private int skillPointsByLevel, healthByPoints, damageByStrength, baseHealth, baseDamage;
        [SerializeField] private GameObject swordWeapon;
        [SerializeField] private GameObject longSwordWeapon;
        
        private int _currentExp;
        private int _currentLevel = 1;
        private int _unallocatedSkillPoints;
        private int _strengthPoints;
        private int _healthPoints;

        private void Start()
        {
            gameObject.SetActive(false);
            UpdateExp();
            UpdateHealth();
            UpdateStrength();
            UpdateUnallocatedSkillsPoints();
            UpdateLevel();
            swordWeapon.SetActive(true);
            longSwordWeapon.SetActive(false);
        }

        public void PlusHealth()
        {
            _healthPoints++;
            _unallocatedSkillPoints--;
            UpdateHealth();
            UpdateUnallocatedSkillsPoints();
        }

        public void PlusStrength()
        {
            _strengthPoints++;
            _unallocatedSkillPoints--;
            UpdateStrength();
            UpdateUnallocatedSkillsPoints();
        }

        public int GetPlayerDamage()
        {
            return baseDamage + damageByStrength * _strengthPoints;
        }

        public void KillEnemy()
        {
            _currentExp += expByEnemyKill;
            UpdateExp();
        }

        public void ChangeWeapon(string nameWeapon)
        {
            switch (nameWeapon)
            {
                case "Sword":
                    swordWeapon.SetActive(true);
                    longSwordWeapon.SetActive(false);
                    break;
                case "LongSword":
                    swordWeapon.SetActive(false);
                    longSwordWeapon.SetActive(true);
                    break;
            }
        }

        private void UpdateExp()
        {
            if (_currentExp >= expToReachLevel[_currentLevel - 1])
            {
                LevelUp();
            }
            expText.text = _currentExp + " / " + expToReachLevel[_currentLevel - 1];
            expSlider.value = (float)_currentExp / expToReachLevel[_currentLevel - 1];
        }

        private void UpdateLevel()
        {
            levelText.text = "Level: " + _currentLevel;
        }

        private void UpdateUnallocatedSkillsPoints()
        {
            unallocatedSkillsText.text = "Unallocated skill points:" + _unallocatedSkillPoints;

            if (_unallocatedSkillPoints > 0)
            {
                healthButton.interactable = true;
                strengthButton.interactable = true;
            }
            else
            {
                healthButton.interactable = false;
                strengthButton.interactable = false;
            }
        }

        private void UpdateHealth()
        {
            healthText.text = _healthPoints.ToString();
        }

        private void UpdateStrength()
        {
            strengthText.text = _strengthPoints.ToString();
        }

        private void LevelUp()
        {
            _currentLevel++;
            _unallocatedSkillPoints += skillPointsByLevel;
            UpdateUnallocatedSkillsPoints();
            UpdateLevel();
        }

        public void OnToggleVisibilitySkillWindow()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}
using UnityEngine;
using TMPro;

namespace _Project
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText; // Reference to the Text component for level
        [SerializeField] private TextMeshProUGUI _experienceText; // Reference to the Text component for experience
        [SerializeField] private PlayerStats _playerStats; // Reference to the player's PlayerStats script

        private void Start()
        {
            if (_playerStats != null)
            {
                _playerStats.OnStatsChanged += UpdatePlayerStatsUI;
                // UpdatePlayerStatsUI(); // Initialize stats UI
            }
        }

        private void UpdatePlayerStatsUI()
        {
            if (_playerStats != null)
            {
                _levelText.text = $"Level: {_playerStats.Level}";
                _experienceText.text = $"Experience: {_playerStats.Experience}/{_playerStats.ExperienceToNextLevel}";
            }
        }
    }
}

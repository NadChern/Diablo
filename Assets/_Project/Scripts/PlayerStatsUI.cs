using System;
using UnityEngine;
using TMPro;

namespace _Project
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText; 
        [SerializeField] private TextMeshProUGUI _experienceText; 
        [SerializeField] private PlayerStats _playerStats; 

        private void Start()
        {
            if (_playerStats != null)
            {
                _playerStats.OnStatsChanged += UpdatePlayerStatsUI;
                UpdatePlayerStatsUI();  
            }
        }

        private void OnDestroy()
        {
            _playerStats.OnStatsChanged -= UpdatePlayerStatsUI;
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

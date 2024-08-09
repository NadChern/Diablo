using _Project;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Health _entityHealth;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private Camera _camera;
    [SerializeField] private TextMeshProUGUI _healthText;


    private void Start()
    {
        if (_entityHealth != null)
        {
            _entityHealth.OnHealthChanged += UpdateHealthUI;
            UpdateHealthUI();
        }
    }

    private void OnDestroy() => _entityHealth.OnHealthChanged -= UpdateHealthUI;

    private void Update()
    {
        if (!_isPlayer && _entityHealth != null)
        {
            // Keep the Canvas above the enemy
            transform.position = _entityHealth.transform.position + Vector3.up * 2.5f;

            // Make sure the Canvas faces the camera
            transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
        }
    }

    private void UpdateHealthUI()
    {
        if (_entityHealth != null && _healthSlider != null)
        {
            _healthSlider.maxValue = _entityHealth.MaxHealth;
            _healthSlider.value = _entityHealth.CurrentHealth;


            // Calculate health percentage
            float healthPercentage = _entityHealth.CurrentHealth
                                     / _entityHealth.MaxHealth;

            _healthText.text = $"{(int)_entityHealth.CurrentHealth}/{(int)_entityHealth.MaxHealth}";

            if (_isPlayer)
            {
                // Player color logic: green, yellow, red
                if (healthPercentage >= 0.75f)
                {
                    _fillImage.color = Color.green;
                }
                else if (healthPercentage >= 0.25f)
                {
                    _fillImage.color = Color.yellow;
                }
                else
                {
                    _fillImage.color = Color.red;
                }
            }
            else
            {
                // Enemy color logic: shades of red
                float redValue = 1f - healthPercentage;
                _fillImage.color = new Color(1f, redValue, redValue);
            }
        }
    }
}

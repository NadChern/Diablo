using _Project;
using UnityEngine;

public class HoverEffectManager : MonoBehaviour
{
    [SerializeField] private GameObject hoverEffectPrefab;
    [SerializeField] private Camera _camera;
    private GameObject _currentEffect;
    private Transform _lastHoveredObject;

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Transform hoveredTransform = hit.transform;
            IInteractable interactable =
                hoveredTransform.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (_lastHoveredObject != hoveredTransform)
                {
                    ShowEffect(hoveredTransform);
                    _lastHoveredObject = hoveredTransform;
                }
            }
            else
            {
                HideEffect();
                _lastHoveredObject = null;
            }
        }
        else
        {
            HideEffect();
            _lastHoveredObject = null;
        }
    }

    private void ShowEffect(Transform target)
    {
        if (_currentEffect != null)
        {
            Destroy(_currentEffect);
        }

        _currentEffect = Instantiate(hoverEffectPrefab, target.position,
            Quaternion.identity, target);
    }

    private void HideEffect()
    {
        if (_currentEffect != null)
        {
            Destroy(_currentEffect);
        }

        _lastHoveredObject = null;
    }
}

using UnityEngine;
using System.Collections;

public class HazardZone : MonoBehaviour
{
    [Header("Timing Settings")]
    [SerializeField] private float _activeTime = 5f;
    [SerializeField] private float _inactiveTime = 3f;
    [SerializeField] private bool _startActive = true;

    [Header("Visual Settings")]
    [SerializeField] private Material _activeMaterial;
    [SerializeField] private Material _inactiveMaterial;
    [SerializeField] private Color _warningColor = Color.yellow;
    [SerializeField] private float _warningDuration = 1f;

    private MeshRenderer _renderer;
    private Collider _collider;
    private bool _isActive;
    private Material _currentMaterial;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();

        if (_renderer != null)
        {
            _currentMaterial = _renderer.material;
        }

        _isActive = _startActive;
        StartCoroutine(HazardCycle());
    }

    private IEnumerator HazardCycle()
    {
        while (true)
        {
            if (_isActive)
            {
                ActivateHazard();
                yield return new WaitForSeconds(_activeTime - _warningDuration);

                ShowWarning();
                yield return new WaitForSeconds(_warningDuration);

                _isActive = false;
            }
            else
            {
                DeactivateHazard();
                yield return new WaitForSeconds(_inactiveTime - _warningDuration);

                ShowWarning();
                yield return new WaitForSeconds(_warningDuration);

                _isActive = true;
            }
        }
    }

    private void ActivateHazard()
    {
        if (_collider != null)
        {
            _collider.enabled = true;
        }

        if (_renderer != null && _activeMaterial != null)
        {
            _renderer.material = _activeMaterial;
        }
        else if (_renderer != null)
        {
            _renderer.material.color = Color.red;
        }
    }

    private void DeactivateHazard()
    {
        if (_collider != null)
        {
            _collider.enabled = false;
        }

        if (_renderer != null && _inactiveMaterial != null)
        {
            _renderer.material = _inactiveMaterial;
        }
        else if (_renderer != null)
        {
            _renderer.material.color = Color.gray;
        }
    }

    private void ShowWarning()
    {
        if (_renderer != null)
        {
            _renderer.material.color = _warningColor;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_isActive && other.CompareTag("Player_C"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
        }
    }
}


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
    [SerializeField] private float _blinkSpeed = 0.2f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _warningSound;
    [SerializeField] private float _warningSoundVolume = 1f;

    private MeshRenderer _renderer;
    private Collider _collider;
    private bool _isActive;
    private Coroutine _blinkCoroutine;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();

        if (_collider == null)
        {
            Debug.LogError("HazardZone : Aucun Collider trouvé !");
            return;
        }

        if (!_collider.isTrigger)
        {
            Debug.LogWarning("HazardZone : Le Collider n'est pas un Trigger ! Activation automatique...");
            _collider.isTrigger = true;
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

                yield return StartCoroutine(ShowWarning());

                _isActive = false;
            }
            else
            {
                DeactivateHazard();
                yield return new WaitForSeconds(_inactiveTime - _warningDuration);

                yield return StartCoroutine(ShowWarning());

                _isActive = true;
            }
        }
    }

    private void ActivateHazard()
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }

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

        Debug.Log("HazardZone ACTIVÉE - DANGER !");
    }

    private void DeactivateHazard()
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }

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

        Debug.Log("HazardZone DÉSACTIVÉE - Sécurisé");
    }

    private IEnumerator ShowWarning()
    {
        if (_warningSound != null)
        {
            AudioSource.PlayClipAtPoint(_warningSound, transform.position, _warningSoundVolume);
        }

        Debug.Log("HazardZone : AVERTISSEMENT - Changement imminent !");

        Color currentColor = _isActive ? Color.red : Color.gray;
        float elapsed = 0f;

        while (elapsed < _warningDuration)
        {
            if (_renderer != null)
            {
                _renderer.material.color = _warningColor;
            }

            yield return new WaitForSeconds(_blinkSpeed);

            if (_renderer != null)
            {
                _renderer.material.color = currentColor;
            }

            yield return new WaitForSeconds(_blinkSpeed);

            elapsed += _blinkSpeed * 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"HazardZone : Collision détectée avec {other.gameObject.name} (Tag: {other.tag})");

        if (_isActive && other.CompareTag("Player_C"))
        {
            Debug.Log("HazardZone : Le joueur touche la zone active - MORT !");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
            else
            {
                Debug.LogError("HazardZone : PlayerHealth non trouvé sur le joueur !");
            }
        }
        else if (!_isActive)
        {
            Debug.Log("HazardZone : Zone inactive - Pas de dégâts");
        }
    }

    void OnTriggerStay(Collider other)
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

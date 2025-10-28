using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform _respawnPoint;
    private Rigidbody _rb;
    private Vector3 _initialPosition;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        if (_respawnPoint != null)
        {
            _initialPosition = _respawnPoint.position;
        }
        else
        {
            _initialPosition = transform.position;
        }
    }

    public void Die()
    {
        Respawn();
    }

    private void Respawn()
    {
        transform.position = _initialPosition;

        if (_rb != null)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        Debug.Log("Player respawned!");
    }
}

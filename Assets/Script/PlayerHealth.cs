using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private Rigidbody _rb;
    private Vector3 _initialPosition;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        if (_spawnPoint != null)
        {
            _initialPosition = _spawnPoint.position;
        }
        else
        {
            _initialPosition = transform.position;
        }
    }

    public void Die()
    {
        Debug.Log("Le joueur meurt !");
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

        Debug.Log("Le joueur respawn !");
    }
}

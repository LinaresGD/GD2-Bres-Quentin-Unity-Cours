using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private Rigidbody _rb;
    private Vector3 _initialPosition;
    private Player_Collect _playerCollect;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerCollect = GetComponent<Player_Collect>();

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

        if (_playerCollect != null)
        {
            _playerCollect.ResetCrystals();
        }

        Debug.Log("Le joueur respawn !");
    }
}

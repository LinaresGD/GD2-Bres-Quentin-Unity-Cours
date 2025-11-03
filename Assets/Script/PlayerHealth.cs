using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private bool _useGameOver = true;

    private Rigidbody _rb;
    private Vector3 _initialPosition;
    private Player_Collect _playerCollect;
    private GameTimer _gameTimer;
    private int _currentLives;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerCollect = GetComponent<Player_Collect>();
        _gameTimer = FindFirstObjectByType<GameTimer>();
        _currentLives = _maxLives;

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

        if (_useGameOver)
        {
            GameOverScreen.ShowGameOver("Vous avez été touché par un ennemi !");
        }
        else
        {
            _currentLives--;

            if (_currentLives <= 0)
            {
                GameOverScreen.ShowGameOver("Vous n'avez plus de vies !");
            }
            else
            {
                Respawn();
            }
        }
    }

    public void DieByTimeout()
    {
        Debug.Log("Le temps est écoulé !");
        GameOverScreen.ShowGameOver("Le temps est écoulé !");
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

        if (_gameTimer != null)
        {
            _gameTimer.ResetTimer();
        }

        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.RespawnAllCollectibles();
        }

        Debug.Log($"Le joueur respawn ! Vies restantes : {_currentLives}");
    }

    public int GetCurrentLives()
    {
        return _currentLives;
    }
}

using UnityEngine;

public class TimeBonus : MonoBehaviour
{
    [Header("Bonus Settings")]
    [SerializeField] private float _bonusTime = 15f;
    [SerializeField] private float _rotationSpeed = 50f;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private float _soundVolume = 1f;

    private GameTimer _gameTimer;

    void Start()
    {
        _gameTimer = FindFirstObjectByType<GameTimer>();

        if (_gameTimer == null)
        {
            Debug.LogError("GameTimer non trouvé dans la scène !");
        }

        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.RegisterCollectible(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_C"))
        {
            if (_gameTimer != null)
            {
                _gameTimer.AddTime(_bonusTime);
                Debug.Log($"+{_bonusTime} secondes !");
            }

            if (_collectSound != null)
            {
                AudioSource.PlayClipAtPoint(_collectSound, transform.position, _soundVolume);
            }

            gameObject.SetActive(false);
        }
    }
}

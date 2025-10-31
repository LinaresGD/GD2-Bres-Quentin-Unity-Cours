using UnityEngine;

public class TimeBonus : MonoBehaviour
{
    [Header("Bonus Settings")]
    [SerializeField] private float _bonusTime = 15f;
    [SerializeField] private float _rotationSpeed = 50f;

    private GameTimer _gameTimer;

    void Start()
    {
        _gameTimer = FindFirstObjectByType<GameTimer>();

        if (_gameTimer == null)
        {
            Debug.LogError("GameTimer non trouvé dans la scène !");
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

            Destroy(gameObject);
        }
    }
}

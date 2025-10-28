using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string _nextLevelName = "Level_2";
    [SerializeField] private float _openSpeed = 2f;
    [SerializeField] private Vector3 _openOffset = new Vector3(0, 5, 0);
    [SerializeField] private bool _requiresKey = true;

    private bool _isOpen = false;
    private bool _playerNearby = false;
    private Vector3 _closedPosition;
    private Vector3 _openPosition;
    private Player_Collect _playerCollect;

    void Start()
    {
        _closedPosition = transform.position;
        _openPosition = _closedPosition + _openOffset;
    }

    void Update()
    {
        if (_isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, _openPosition, Time.deltaTime * _openSpeed);
        }

        if (_playerNearby && _playerCollect != null)
        {
            if (_requiresKey && _playerCollect.HasKey())
            {
                OpenDoor();
            }
            else if (!_requiresKey)
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_C"))
        {
            _playerNearby = true;
            _playerCollect = other.GetComponent<Player_Collect>();

            if (_requiresKey && !_playerCollect.HasKey())
            {
                Debug.Log("Vous avez besoin de la clé pour ouvrir cette porte !");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player_C"))
        {
            _playerNearby = false;
            _playerCollect = null;
        }
    }

    private void OpenDoor()
    {
        if (!_isOpen)
        {
            _isOpen = true;
            Debug.Log("La porte s'ouvre !");
            Invoke(nameof(LoadNextLevel), 2f);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(_nextLevelName);
    }
}

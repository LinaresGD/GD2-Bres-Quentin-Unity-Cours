using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    private static VictoryScreen _instance;

    [Header("Victory UI")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private TMP_Text _victoryText;
    [SerializeField] private Image _victoryImage;

    [Header("Settings")]
    [SerializeField] private string _victoryMessage = "YOU WIN!";
    [SerializeField] private bool _freezeGameOnVictory = false;
    [SerializeField] private float _delayBeforeNextLevel = 3f;

    private string _nextLevelName;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        if (_victoryPanel != null)
        {
            _victoryPanel.SetActive(false);
        }
    }

    public static void ShowVictory(string nextLevelName = "")
    {
        if (_instance != null)
        {
            _instance.DisplayVictory(nextLevelName);
        }
        else
        {
            Debug.LogError("VictoryScreen : Pas trouvé !");
        }
    }

    private void DisplayVictory(string nextLevelName)
    {
        if (_victoryPanel != null)
        {
            _victoryPanel.SetActive(true);
        }

        if (_victoryText != null)
        {
            _victoryText.text = _victoryMessage;
        }

        if (_freezeGameOnVictory)
        {
            Time.timeScale = 0f;
        }

        Debug.Log("VICTOIRE !");

        _nextLevelName = nextLevelName;

        if (!string.IsNullOrEmpty(_nextLevelName))
        {
            Invoke(nameof(LoadNextLevel), _delayBeforeNextLevel);
        }
    }

    private void LoadNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_nextLevelName);
    }

    public static void HideVictory()
    {
        if (_instance != null && _instance._victoryPanel != null)
        {
            _instance._victoryPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

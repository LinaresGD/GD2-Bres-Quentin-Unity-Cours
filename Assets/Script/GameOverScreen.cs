using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    private static GameOverScreen _instance;
    public static GameOverScreen Instance => _instance;

    [Header("Game Over UI")]
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _reasonText;

    [Header("Settings")]
    [SerializeField] private string _gameOverMessage = "GAME OVER";
    [SerializeField] private bool _freezeGameOnGameOver = true;

    private string _currentSceneName;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }

        _currentSceneName = SceneManager.GetActiveScene().name;
    }

    public static void ShowGameOver(string reason = "")
    {
        if (_instance != null)
        {
            _instance.DisplayGameOver(reason);
        }
        else
        {
            Debug.LogError("GameOverScreen : Instance non trouvée !");
        }
    }

    private void DisplayGameOver(string reason)
    {
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }

        if (_gameOverText != null)
        {
            _gameOverText.text = _gameOverMessage;
        }

        if (_reasonText != null && !string.IsNullOrEmpty(reason))
        {
            _reasonText.text = reason;
        }

        if (_freezeGameOnGameOver)
        {
            Time.timeScale = 0f;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log($"GAME OVER : {reason}");
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_currentSceneName);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Quitter le jeu");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public static void HideGameOver()
    {
        if (_instance != null && _instance._gameOverPanel != null)
        {
            _instance._gameOverPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

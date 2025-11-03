using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string _firstLevelName = "Dev_Map";

    [Header("Panels")]
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _settingsPanel;

    void Start()
    {
        ShowMainMenu();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_firstLevelName);
    }

    public void OpenSettings()
    {
        if (_mainMenuPanel != null)
        {
            _mainMenuPanel.SetActive(false);
        }

        if (_settingsPanel != null)
        {
            _settingsPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        ShowMainMenu();
    }

    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void ShowMainMenu()
    {
        if (_mainMenuPanel != null)
        {
            _mainMenuPanel.SetActive(true);
        }

        if (_settingsPanel != null)
        {
            _settingsPanel.SetActive(false);
        }
    }
}

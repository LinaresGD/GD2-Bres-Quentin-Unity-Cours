using System;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text _crystalCountText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _objectiveText;

    private void Start()
    {
        UpdateCrystalCount(0, 3);
        UpdateTimer(60f);
        HideObjectiveMessage();
    }

    public void UpdateCrystalCount(int current, int needed)
    {
        _crystalCountText.text = $"Cristaux : {current}/{needed}";
    }

    public void UpdateTimer(float timeRemaining)
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        _timerText.text = $"Temps : {minutes:00}:{seconds:00}";

        if (timeRemaining <= 10f && timeRemaining > 0f)
        {
            _timerText.color = Color.red;
        }
        else
        {
            _timerText.color = Color.white;
        }
    }

    public void ShowObjectiveMessage(string message)
    {
        if (_objectiveText != null)
        {
            _objectiveText.text = message;
            _objectiveText.gameObject.SetActive(true);
            Debug.Log($"[UIController] Message affiché : {message}");
        }
        else
        {
            Debug.LogError("[UIController] _objectiveText est null !");
        }
    }

    public void HideObjectiveMessage()
    {
        if (_objectiveText != null)
        {
            _objectiveText.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Player_Collect.OnCrystalCountChanged += UpdateCrystalCount;
        Player_Collect.OnKeyCollected += OnKeyCollected;
        GameTimer.OnTimerUpdate += UpdateTimer;
        Debug.Log("[UIController] Événements souscrits");
    }

    private void OnDisable()
    {
        Player_Collect.OnCrystalCountChanged -= UpdateCrystalCount;
        Player_Collect.OnKeyCollected -= OnKeyCollected;
        GameTimer.OnTimerUpdate -= UpdateTimer;
    }

    private void OnKeyCollected()
    {
        Debug.Log("[UIController] OnKeyCollected appelé !");
        ShowObjectiveMessage("Trouve la boule de cristal et enfuis-toi !");
    }
}

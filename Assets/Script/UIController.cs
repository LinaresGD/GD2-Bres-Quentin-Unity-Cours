using System;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _crystalCountText;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        UpdateCrystalCount(0, 3);
        UpdateTimer(60f);
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

    private void OnEnable()
    {
        Player_Collect.OnCrystalCountChanged += UpdateCrystalCount;
        GameTimer.OnTimerUpdate += UpdateTimer;
    }

    private void OnDisable()
    {
        Player_Collect.OnCrystalCountChanged -= UpdateCrystalCount;
        GameTimer.OnTimerUpdate -= UpdateTimer;
    }
}

using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;

public class Player_Collect : MonoBehaviour
{
    [SerializeField] private ScoreDatase _scoreData;
    [SerializeField] private UIController _uiController;

    public static Action<int> OnTargetCollected;
    public void UpdateScore(int value)
    {
        _scoreData.ScoreValue = Mathf.Clamp(_scoreData.ScoreValue+ value, 0, _scoreData.ScoreValue+ value);
        //_uiController.UpdateScore(_scoreData.ScoreValue);
        OnTargetCollected?.Invoke(_scoreData.ScoreValue);
    }
}

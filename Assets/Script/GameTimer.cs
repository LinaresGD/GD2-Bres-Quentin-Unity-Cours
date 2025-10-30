using UnityEngine;
using System;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 60f;
    [SerializeField] private PlayerHealth _playerHealth;

    private float _timeRemaining;
    private bool _isRunning = true;

    public static Action<float> OnTimerUpdate;
    public static Action OnTimerExpired;

    void Start()
    {
        _timeRemaining = _timeLimit;
    }

    void Update()
    {
        if (!_isRunning) return;

        _timeRemaining -= Time.deltaTime;

        OnTimerUpdate?.Invoke(_timeRemaining);

        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            TimeExpired();
        }
    }

    private void TimeExpired()
    {
        _isRunning = false;
        OnTimerExpired?.Invoke();

        Debug.Log("Temps écoulé ! Le joueur perd.");

        if (_playerHealth != null)
        {
            _playerHealth.Die();
        }

        ResetTimer();
    }

    public void ResetTimer()
    {
        _timeRemaining = _timeLimit;
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public float GetTimeRemaining()
    {
        return _timeRemaining;
    }
}

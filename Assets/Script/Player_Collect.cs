using UnityEngine;
using System;

public class Player_Collect : MonoBehaviour
{
    [SerializeField] private ScoreDatase _scoreData;
    [SerializeField] private UIController _uiController;
    [SerializeField] private int _crystalsNeeded = 3;
    [SerializeField] private GameObject _keySphere;

    private int _crystalsCollected = 0;
    private bool _hasKey = false;

    public static Action<int> OnTargetCollected;
    public static Action<int, int> OnCrystalCountChanged;
    public static Action OnKeyCollected;

    void Start()
    {
        if (_keySphere != null)
        {
            _keySphere.SetActive(false);
        }
    }

    public void UpdateScore(int value)
    {
        _scoreData.ScoreValue = Mathf.Clamp(_scoreData.ScoreValue + value, 0, _scoreData.ScoreValue + value);
        OnTargetCollected?.Invoke(_scoreData.ScoreValue);
    }

    public void AddCrystal()
    {
        _crystalsCollected++;
        OnCrystalCountChanged?.Invoke(_crystalsCollected, _crystalsNeeded);

        Debug.Log($"Cristaux collectés: {_crystalsCollected}/{_crystalsNeeded}");

        if (_crystalsCollected >= _crystalsNeeded)
        {
            ActivateKey();
        }
    }

    private void ActivateKey()
    {
        if (_keySphere != null && !_hasKey)
        {
            _keySphere.SetActive(true);
            Debug.Log("La clé est apparue !");
        }
    }

    public void CollectKey()
    {
        _hasKey = true;
        OnKeyCollected?.Invoke();
        Debug.Log("Clé collectée ! Le timer s'arrête.");

        GameTimer gameTimer = FindFirstObjectByType<GameTimer>();
        if (gameTimer != null)
        {
            gameTimer.StopTimer();
        }
    }

    public bool HasKey()
    {
        return _hasKey;
    }

    public void ResetCrystals()
    {
        _crystalsCollected = 0;
        _hasKey = false;

        if (_keySphere != null)
        {
            _keySphere.SetActive(false);
        }

        OnCrystalCountChanged?.Invoke(_crystalsCollected, _crystalsNeeded);
        Debug.Log("Cristaux réinitialisés à 0.");
    }
}

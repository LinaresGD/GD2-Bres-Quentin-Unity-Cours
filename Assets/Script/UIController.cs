using System;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _crystalCountText;

    private void Start()
    {
        UpdateCrystalCount(0, 3);
    }

    public void UpdateCrystalCount(int current, int needed)
    {
        _crystalCountText.text = $"Cristaux : {current}/{needed}";
    }

    private void OnEnable()
    {
        Player_Collect.OnCrystalCountChanged += UpdateCrystalCount;
    }

    private void OnDisable()
    {
        Player_Collect.OnCrystalCountChanged -= UpdateCrystalCount;
    }
}

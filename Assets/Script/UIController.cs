using System;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private void Start()
    {
        UpdateScore(0);
    }
    
    public void UpdateScore(int NewScore)
    {
        _scoreText.text = "Score : " + NewScore.ToString();
        //  _scoreText.text = $"Score : {NewScore.ToString()}; methode plus sur
    }


    private void OnEnable()
    {
       Player_Collect.OnTargetCollected += UpdateScore; 
    }

    private void OnDisable()
    {
        Player_Collect.OnTargetCollected -= UpdateScore;
    }
}    
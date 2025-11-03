using System;
using UnityEngine;

public class Target_Hard_Script : MonoBehaviour
{
    public static Action OnCrystalCollected;

    void Start()
    {
        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.RegisterCollectible(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player_Collect>() != null)
        {
            other.gameObject.GetComponent<Player_Collect>().AddCrystal();

            OnCrystalCollected?.Invoke();

            gameObject.SetActive(false);
        }
    }
}

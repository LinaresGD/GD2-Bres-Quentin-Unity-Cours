using System;
using UnityEngine;

public class Target_Hard_Script : MonoBehaviour
{
    public static Action OnCrystalCollected;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player_Collect>() != null)
        {
            other.gameObject.GetComponent<Player_Collect>().AddCrystal();

            OnCrystalCollected?.Invoke();

            Destroy(gameObject);
        }
    }
}

using System;
using Unity.VisualScripting;
using UnityEngine;

public class Target_Soft_Script : MonoBehaviour
{
    [SerializeField] private int _targetValue = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Collect>() != null)
        {
            other.gameObject.GetComponent<Player_Collect>().UpdateScore(_targetValue);
            Destroy(gameObject);
        }

        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
}
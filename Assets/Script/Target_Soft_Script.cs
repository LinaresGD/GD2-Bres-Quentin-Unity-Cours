using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Target_Soft_Script : MonoBehaviour
{
    [SerializeField] private int _targetValue = 1;
    [SerializeField] private float _shadowDuration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Collect>() != null)
        {
            other.gameObject.GetComponent<Player_Collect>().UpdateScore(_targetValue);
            ToggleVisibility(false);
            StartCoroutine(ShadowTimerControl());
        }
    }

    private void ToggleVisibility(bool newVisibility)
    {
        GetComponent<MeshRenderer>().enabled = newVisibility;
        GetComponent<Collider>().enabled = newVisibility;
    }

    private IEnumerator ShadowTimerControl()
    {
        yield return new WaitForSeconds(_shadowDuration);
        ToggleVisibility(true);
    }
}

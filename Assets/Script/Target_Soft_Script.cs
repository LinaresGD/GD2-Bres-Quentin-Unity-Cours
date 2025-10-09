using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Target_Soft_Script : MonoBehaviour
{
    [SerializeField] private int _targetValue = 1;
    [SerializeField] private float _shadowDuration = 3f;
    private float _shadowTimer = 0f;
    private  bool _isInShadow = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player_Collect>() != null)
        {
            other.gameObject.GetComponent<Player_Collect>().UpdateScore(_targetValue);
            ToggleVisibility(false);
            _isInShadow = true;
            StartCoroutine(routine: ShadowTimerControl());
            //Instantiate(particleSystem,transform.position,transform.rotation);



            //Destroy(gameObject);
            //Todo : Hide Target
            //Todo : Start Timer
        }

        
    }

    private void ToggleVisibility(bool newVisibility)
    {
        GetComponent<MeshRenderer>().enabled = newVisibility;
        GetComponent<Collider>().enabled = newVisibility;
    }

    /*private void Update()
    {
        if (_isInShadow)
        {
            _shadowTimer += Time.deltaTime;
            if(_shadowTimer >= _shadowDuration)
            {
                ToggleVisibility(true);
                _shadowTimer = 0f;
                // Todo Show Target
                // Todo: Stop Timer
            }
        }
        // TODO : Tymer by deltatime
    
        // Todo : Timer by coroutine
        }*/

    private IEnumerator ShadowTimerControl()
    {   
        //yield return new
        yield return new WaitForSeconds(_shadowDuration);
        ToggleVisibility(true);
    }
    
    
}
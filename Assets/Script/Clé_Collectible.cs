using UnityEngine;

public class Clé_Collectible : MonoBehaviour
{
    [SerializeField] private int _keyValue = 5;
    [SerializeField] private float _rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Player_Collect>() != null)
        {
            other.gameObject.GetComponent<Player_Collect>().UpdateScore(_keyValue);
            other.gameObject.GetComponent<Player_Collect>().CollectKey();
            Destroy(gameObject);
        }
    }
}

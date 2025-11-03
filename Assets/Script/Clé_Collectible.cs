using UnityEngine;

public class Clé_Collectible : MonoBehaviour
{
    [SerializeField] private int _keyValue = 5;
    [SerializeField] private float _rotationSpeed = 50f;

    void Start()
    {
        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.RegisterCollectible(gameObject);
        }
    }

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
            gameObject.SetActive(false);
        }
    }
}

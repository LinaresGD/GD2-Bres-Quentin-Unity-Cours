using UnityEngine;

public class BaseCollectible : MonoBehaviour
{
    void Start()
    {
        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.RegisterCollectible(gameObject);
        }
    }
}

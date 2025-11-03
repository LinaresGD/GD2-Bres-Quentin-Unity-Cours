using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    private static CollectibleManager _instance;
    public static CollectibleManager Instance => _instance;

    private readonly List<CollectibleData> _collectibles = new List<CollectibleData>();

    private class CollectibleData
    {
        public GameObject GameObject;
        public Vector3 InitialPosition;
        public Quaternion InitialRotation;
        public bool WasActive;

        public CollectibleData(GameObject go)
        {
            GameObject = go;
            InitialPosition = go.transform.position;
            InitialRotation = go.transform.rotation;
            WasActive = go.activeSelf;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public void RegisterCollectible(GameObject collectible)
    {
        if (!_collectibles.Exists(c => c.GameObject == collectible))
        {
            _collectibles.Add(new CollectibleData(collectible));
        }
    }

    public void RespawnAllCollectibles()
    {
        foreach (var data in _collectibles)
        {
            if (data.GameObject != null)
            {
                data.GameObject.SetActive(true);
                data.GameObject.transform.position = data.InitialPosition;
                data.GameObject.transform.rotation = data.InitialRotation;
            }
        }

        Debug.Log($"{_collectibles.Count} collectibles respawnés !");
    }
}

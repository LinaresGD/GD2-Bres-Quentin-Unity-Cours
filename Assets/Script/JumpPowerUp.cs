using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    private const float AUTO_JUMP_DURATION = 3f;
    private const float JUMP_INTERVAL = 0.5f;

    [Header("Power-Up Settings")]
    [SerializeField] private float _rotationSpeed = 100f;

    [Header("Visual Settings")]
    [SerializeField] private Color _powerUpColor = Color.green;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip _collectSound;
    [SerializeField] private float _soundVolume = 1f;

    private MeshRenderer _meshRenderer;
    private bool _hasBeenCollected = false;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        if (_meshRenderer != null && _meshRenderer.material != null)
        {
            _meshRenderer.material.color = _powerUpColor;
        }

        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.RegisterCollectible(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);

        float bounce = Mathf.Sin(Time.time * 3f) * 0.3f;
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + bounce * Time.deltaTime,
            transform.position.z
        );
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[JumpPowerUp] OnTriggerEnter détecté avec: {other.gameObject.name}, Tag: {other.tag}");

        if (_hasBeenCollected)
        {
            Debug.Log("[JumpPowerUp] Power-up déjà collecté, ignoré");
            return;
        }

        if (other.CompareTag("Player_C"))
        {
            Debug.Log("[JumpPowerUp] Tag Player_C détecté !");
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                Debug.Log("[JumpPowerUp] PlayerMovement trouvé, activation des sauts automatiques...");
                bool activated = playerMovement.EnableAutoJump(AUTO_JUMP_DURATION, JUMP_INTERVAL);

                if (activated)
                {
                    _hasBeenCollected = true;

                    if (_collectSound != null)
                    {
                        AudioSource.PlayClipAtPoint(_collectSound, transform.position, _soundVolume);
                    }

                    Debug.Log($"Power-Up Jump collecté ! Sauts automatiques pendant {AUTO_JUMP_DURATION}s !");
                    gameObject.SetActive(false);
                }
                else
                {
                    Debug.Log("Un power-up est déjà actif !");
                }
            }
            else
            {
                Debug.LogWarning("[JumpPowerUp] PlayerMovement non trouvé sur le GameObject !");
            }
        }
        else
        {
            Debug.Log($"[JumpPowerUp] Tag incorrect: {other.tag} (attendu: Player_C)");
        }
    }
}

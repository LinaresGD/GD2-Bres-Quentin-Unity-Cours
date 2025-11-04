using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ChasingEnemy : MonoBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float _moveSpeed = 6f;
    [SerializeField] private float _detectionRange = 15f;
    [SerializeField] private float _stopDistance = 1.5f;

    [Header("Rotation")]
    [SerializeField] private float _rotationSpeed = 5f;

    private Rigidbody _rb;
    private Transform _playerTransform;
    private bool _isPlayerInRange = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.linearDamping = 2f;

        GameObject player = GameObject.FindGameObjectWithTag("Player_C");
        if (player != null)
        {
            _playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("[ChasingEnemy] Joueur non trouvé avec le tag 'Player_C' !");
        }
    }

    void Update()
    {
        if (_playerTransform == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        _isPlayerInRange = distanceToPlayer <= _detectionRange;

        if (_isPlayerInRange && distanceToPlayer > _stopDistance)
        {
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            directionToPlayer.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (_playerTransform == null || _rb == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        if (_isPlayerInRange && distanceToPlayer > _stopDistance)
        {
            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            directionToPlayer.y = 0;

            Vector3 velocity = directionToPlayer * _moveSpeed;
            velocity.y = _rb.linearVelocity.y;
            _rb.linearVelocity = velocity;
        }
        else if (distanceToPlayer <= _stopDistance)
        {
            Vector3 velocity = _rb.linearVelocity;
            velocity.x = 0;
            velocity.z = 0;
            _rb.linearVelocity = velocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player_C"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Die();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stopDistance);
    }
}

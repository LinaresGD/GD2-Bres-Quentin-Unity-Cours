using UnityEngine;

public class EnemyCube : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Vector3 _moveDirection = Vector3.forward;
    [SerializeField] private float _changeDirectionInterval = 3f;

    private float _changeTimer = 0f;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb != null)
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        _moveDirection = _moveDirection.normalized;
    }

    void Update()
    {
        _changeTimer += Time.deltaTime;

        if (_changeTimer >= _changeDirectionInterval)
        {
            ChangeDirection();
            _changeTimer = 0f;
        }
    }

    void FixedUpdate()
    {
        if (_rb != null)
        {
            Vector3 velocity = _moveDirection * _moveSpeed;
            velocity.y = _rb.linearVelocity.y;
            _rb.linearVelocity = velocity;
        }
    }

    private void ChangeDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        _moveDirection = new Vector3(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad),
            0f,
            Mathf.Sin(randomAngle * Mathf.Deg2Rad)
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player_C"))
        {
            KillPlayer(collision.gameObject);
        }
        else
        {
            ChangeDirection();
        }
    }

    private void KillPlayer(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Die();
        }
    }
}



using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private float _horizontalMovement;
    private float _verticalMovement;
    private Vector3 _movement;
    private Vector3 _grappinDirection;
    private Vector3 _grappinHit;

    [SerializeField] private float _speed = 10f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.linearDamping = 5f;
    }

    void Update()
    {
        _horizontalMovement = 0f;
        _verticalMovement = 0f;

        if (Input.GetKey(KeyCode.Z))
            _verticalMovement = 1f;

        if (Input.GetKey(KeyCode.S))
            _verticalMovement = -1f;

        if (Input.GetKey(KeyCode.Q))
            _horizontalMovement = -1f;

        if (Input.GetKey(KeyCode.D))
            _horizontalMovement = 1f;

        _movement = new Vector3(_horizontalMovement, 0, _verticalMovement);

        if (_movement.sqrMagnitude > 0.1f)
        {
            GrappinUpdateDirection(_movement);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            TryThrowGrappin();
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            ThrowGrappin();
        }
    }

    void FixedUpdate()
    {
        if (_rb != null)
        {
            Vector3 velocity = _movement.normalized * _speed;
            velocity.y = _rb.linearVelocity.y;
            _rb.linearVelocity = velocity;
        }
    }

    private void GrappinUpdateDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.1f)
        {
            _grappinDirection = direction;
        }
    }

    private void TryThrowGrappin()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _grappinDirection, out hit, 100f))
        {
            _grappinHit = hit.point + hit.normal * 1.5f;
        }
    }

    private void ThrowGrappin()
    {
        transform.position = _grappinHit;
        _grappinDirection = Vector3.zero;
    }
}

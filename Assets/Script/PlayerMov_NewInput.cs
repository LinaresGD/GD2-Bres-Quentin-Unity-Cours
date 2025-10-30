using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector2 _moveInput;
    private Vector3 _movement;
    private Vector3 _grappinDirection;
    private Vector3 _grappinHit;
    private bool _isGrappinPressed = false;

    [SerializeField] private float _speed = 10f;
    [SerializeField] private InputActionAsset _inputActions;

    private InputAction _moveAction;
    private InputAction _grappinAction;

    void Awake()
    {
        if (_inputActions != null)
        {
            _moveAction = _inputActions.FindActionMap("Player").FindAction("Move");
            _grappinAction = _inputActions.FindActionMap("Player").FindAction("Grappin");
        }
    }

    void OnEnable()
    {
        _moveAction?.Enable();
        _grappinAction?.Enable();
    }

    void OnDisable()
    {
        _moveAction?.Disable();
        _grappinAction?.Disable();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.linearDamping = 5f;
    }

    void Update()
    {
        if (_moveAction != null)
        {
            _moveInput = _moveAction.ReadValue<Vector2>();
            _movement = new Vector3(_moveInput.x, 0, _moveInput.y);

            if (_movement.sqrMagnitude > 0.1f)
            {
                GrappinUpdateDirection(_movement);
            }
        }

        if (_grappinAction != null)
        {
            if (_grappinAction.WasPressedThisFrame())
            {
                TryThrowGrappin();
            }

            if (_grappinAction.WasReleasedThisFrame())
            {
                ThrowGrappin();
            }
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


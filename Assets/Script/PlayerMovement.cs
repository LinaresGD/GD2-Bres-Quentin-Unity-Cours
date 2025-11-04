using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private float _horizontalMovement;
    private float _verticalMovement;
    private Vector3 _movement;
    private Vector3 _grappinDirection;
    private Vector3 _grappinHit;
    private bool _isGrappling;
    private bool _isGrounded;
    private bool _canJump = false;
    private bool _isPowerUpActive = false;
    private bool _isAutoJumping = false;

    [Header("Movement")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _grappinSpeed = 20f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundCheckDistance = 0.3f;

    [Header("Input")]
    [SerializeField] private InputActionReference _jumpAction;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.linearDamping = 5f;
        _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;

        if (_jumpAction != null)
        {
            _jumpAction.action.Enable();
            _jumpAction.action.performed += OnJump;
        }

        Debug.Log($"[PlayerMovement] Initialisé. Tag: {gameObject.tag}, GroundLayer: {_groundLayer.value}");
    }

    void OnDestroy()
    {
        if (_jumpAction != null)
        {
            _jumpAction.action.performed -= OnJump;
        }
    }

    void Update()
    {
        _horizontalMovement = 0f;
        _verticalMovement = 0f;

        if (Input.GetKey(KeyCode.W))
            _verticalMovement = 1f;

        if (Input.GetKey(KeyCode.S))
            _verticalMovement = -1f;

        if (Input.GetKey(KeyCode.A))
            _horizontalMovement = -1f;

        if (Input.GetKey(KeyCode.D))
            _horizontalMovement = 1f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

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

        CheckGrounded();
    }

    void FixedUpdate()
    {
        if (_rb != null)
        {
            if (_isGrappling)
            {
                Vector3 direction = (_grappinHit - transform.position).normalized;
                float distance = Vector3.Distance(transform.position, _grappinHit);

                if (distance > 0.5f)
                {
                    _rb.linearVelocity = direction * _grappinSpeed;
                }
                else
                {
                    _isGrappling = false;
                    _rb.linearVelocity = Vector3.zero;
                }
            }
            else
            {
                Vector3 velocity = _movement.normalized * _speed;
                velocity.y = _rb.linearVelocity.y;
                _rb.linearVelocity = velocity;
            }
        }
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);
    }

    private void Jump()
    {
        if (!_canJump && !_isAutoJumping)
        {
            Debug.Log("Vous devez collecter un Power-Up pour sauter !");
            return;
        }

        if (_isGrounded && !_isGrappling)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void AutoJump()
    {
        Debug.Log($"[AutoJump] Tentative de saut. Grounded: {_isGrounded}, Grappling: {_isGrappling}");

        if (_isGrounded && !_isGrappling)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            Debug.Log("Saut automatique exécuté !");
        }
        else
        {
            Debug.Log($"[AutoJump] Saut impossible. Grounded: {_isGrounded}, Grappling: {_isGrappling}");
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    public bool EnableJump(float duration)
    {
        if (_isPowerUpActive)
        {
            return false;
        }

        StopAllCoroutines();
        StartCoroutine(JumpPowerUpCoroutine(duration));
        return true;
    }

    public bool EnableAutoJump(float duration, float jumpInterval)
    {
        Debug.Log($"[EnableAutoJump] Appelé. PowerUpActive: {_isPowerUpActive}");

        if (_isPowerUpActive)
        {
            Debug.Log("[EnableAutoJump] Un power-up est déjà actif");
            return false;
        }

        Debug.Log($"[EnableAutoJump] Démarrage de la coroutine. Durée: {duration}s, Intervalle: {jumpInterval}s");
        StopAllCoroutines();
        StartCoroutine(AutoJumpCoroutine(duration, jumpInterval));
        return true;
    }

    private IEnumerator JumpPowerUpCoroutine(float duration)
    {
        _isPowerUpActive = true;
        _canJump = true;
        Debug.Log($"Power-Up activé ! Vous pouvez sauter pendant {duration}s !");

        yield return new WaitForSeconds(duration);

        _canJump = false;
        _isPowerUpActive = false;
        Debug.Log("Power-Up terminé ! Vous ne pouvez plus sauter.");
    }

    private IEnumerator AutoJumpCoroutine(float duration, float jumpInterval)
    {
        _isPowerUpActive = true;
        _isAutoJumping = true;
        Debug.Log($"[AutoJumpCoroutine] Power-Up de sauts automatiques activé pendant {duration}s !");

        float elapsedTime = 0f;
        int jumpCount = 0;

        while (elapsedTime < duration)
        {
            jumpCount++;
            Debug.Log($"[AutoJumpCoroutine] Saut #{jumpCount}, temps écoulé: {elapsedTime:F2}s/{duration}s");
            AutoJump();
            yield return new WaitForSeconds(jumpInterval);
            elapsedTime += jumpInterval;
        }

        _isAutoJumping = false;
        _isPowerUpActive = false;
        Debug.Log($"[AutoJumpCoroutine] Power-Up de sauts automatiques terminé ! Total de sauts: {jumpCount}");
    }

    public bool CanJump()
    {
        return _canJump;
    }

    public bool IsPowerUpActive()
    {
        return _isPowerUpActive;
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
        _isGrappling = true;
        _grappinDirection = Vector3.zero;
    }
}


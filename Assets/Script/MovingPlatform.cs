using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private Vector3 _moveDirection = Vector3.right;
    [SerializeField] private float _moveDistance = 5f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private bool _smoothMovement = true;

    [Header("Pause Settings")]
    [SerializeField] private bool _pauseAtEnds = false;
    [SerializeField] private float _pauseDuration = 1f;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _journeyLength;
    private float _startTime;
    private bool _movingToTarget = true;
    private bool _isPaused = false;
    private float _pauseTimer = 0f;

    void Start()
    {
        _startPosition = transform.position;
        _targetPosition = _startPosition + (_moveDirection.normalized * _moveDistance);
        _journeyLength = Vector3.Distance(_startPosition, _targetPosition);
        _startTime = Time.time;
    }

    void FixedUpdate()
    {
        if (_isPaused)
        {
            _pauseTimer -= Time.fixedDeltaTime;
            if (_pauseTimer <= 0f)
            {
                _isPaused = false;
                _startTime = Time.time;
                _movingToTarget = !_movingToTarget;
            }
            return;
        }

        if (_smoothMovement)
        {
            SmoothMovement();
        }
        else
        {
            LinearMovement();
        }
    }

    private void SmoothMovement()
    {
        float distCovered = (Time.time - _startTime) * _moveSpeed;
        float fractionOfJourney = distCovered / _journeyLength;

        Vector3 currentTarget = _movingToTarget ? _targetPosition : _startPosition;
        Vector3 currentStart = _movingToTarget ? _startPosition : _targetPosition;

        transform.position = Vector3.Lerp(currentStart, currentTarget, fractionOfJourney);

        if (fractionOfJourney >= 1f)
        {
            if (_pauseAtEnds)
            {
                _isPaused = true;
                _pauseTimer = _pauseDuration;
            }
            else
            {
                _startTime = Time.time;
                _movingToTarget = !_movingToTarget;
            }
        }
    }

    private void LinearMovement()
    {
        Vector3 currentTarget = _movingToTarget ? _targetPosition : _startPosition;

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, _moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            if (_pauseAtEnds)
            {
                _isPaused = true;
                _pauseTimer = _pauseDuration;
            }
            else
            {
                _movingToTarget = !_movingToTarget;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_C"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player_C"))
        {
            other.transform.SetParent(null);
        }
    }

    void OnDrawGizmos()
    {
        Vector3 start = Application.isPlaying ? _startPosition : transform.position;
        Vector3 end = start + (_moveDirection.normalized * _moveDistance);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(start, end);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(start, 0.3f);
        Gizmos.DrawWireSphere(end, 0.3f);
    }
}

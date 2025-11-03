using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _player;

    [Header("Camera Position")]
    [SerializeField] private Vector3 _offset = new Vector3(0, 3, -5);

    [Header("Camera Smoothing")]
    [SerializeField] private float _smoothSpeed = 10f;
    [SerializeField] private bool _useSmoothFollow = true;

    [Header("Camera Angle")]
    [SerializeField] private Vector3 _cameraRotation = new Vector3(15, 0, 0);

    private Vector3 _velocity = Vector3.zero;

    void Start()
    {
        if (_player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player_C");
            if (playerObj != null)
            {
                _player = playerObj.transform;
            }
        }

        transform.rotation = Quaternion.Euler(_cameraRotation);
    }

    void LateUpdate()
    {
        if (_player == null) return;

        Vector3 desiredPosition = _player.position + _offset;

        if (_useSmoothFollow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, 1f / _smoothSpeed);
        }
        else
        {
            transform.position = desiredPosition;
        }

        transform.rotation = Quaternion.Euler(_cameraRotation);
    }
}


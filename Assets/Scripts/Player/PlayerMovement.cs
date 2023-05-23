using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveHorizontalSpeed = 5f;
    [SerializeField] private float moveForwardSpeed = 5f;
    [SerializeField] private Camera mainCamera;

    private Transform _transform;

    private Vector3 _mousePreviousPosition;
    private Vector3 _mouseCurrentPosition;
    private Vector3 _newPosition;

    private float _horizontalDelta;
    private readonly float _delta = 0.01f;

    private bool _horizontalMoving;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePreviousPosition = Input.mousePosition;
            _horizontalMoving = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _horizontalMoving = false;
        }
    }

    private void Movement()
    {
        _newPosition = _transform.position + _delta * moveForwardSpeed * Vector3.forward;

        if (_horizontalMoving)
        {
            _mouseCurrentPosition = Input.mousePosition;
            _horizontalDelta = (_mouseCurrentPosition.x - _mousePreviousPosition.x) * _delta;

            _newPosition.x = (_transform.position + _horizontalDelta * _delta * moveHorizontalSpeed * Vector3.right).x;
            _newPosition.x = Mathf.Clamp(_newPosition.x, -2f, 2f);

            _mousePreviousPosition = _mouseCurrentPosition;
        }

        _transform.position = _newPosition;
    }
}

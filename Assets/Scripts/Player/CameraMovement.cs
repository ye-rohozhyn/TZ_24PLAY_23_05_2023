using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Axis moveAxis;

    private Transform _transform;
    private Vector3 _newPosition;

    private float _delta = 0.01f;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (target)
        {
            _newPosition = CalculateNewPosition();

            _transform.position = _newPosition;
        }
    }

    private Vector3 CalculateNewPosition()
    {
        Vector3 _newPosition = target.position;
        
        if (moveAxis.Equals(Axis.X))
        {
            _newPosition = Vector3.right * _newPosition.x;
        }
        else if (moveAxis.Equals(Axis.Y))
        {
            _newPosition = Vector3.up * _newPosition.y;
        }
        else if (moveAxis.Equals(Axis.Z))
        {
            _newPosition = Vector3.forward * _newPosition.z;
        }

        _newPosition += offset;
        _newPosition = Vector3.Lerp(_transform.position, _newPosition, moveSpeed * _delta);

        return _newPosition;
    }
}

public enum Axis
{
    AllAxis, X, Y, Z
}
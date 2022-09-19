using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _startPosition;
    private Vector3 _startRotation;

    private void Awake()
    {
        _startPosition = transform.position;
        _startRotation = transform.eulerAngles;
        _startPosition.x -= _player.position.x;
        _startPosition.z -= _player.position.z;
        _startPosition.y -= _player.position.y;
    }

    private void LateUpdate()
    {
        transform.position = _player.position + _startPosition;
        transform.rotation = Quaternion.Euler(_startRotation);
    }
}

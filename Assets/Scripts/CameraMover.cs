using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
        _startPosition.z = transform.position.z - _player.position.z;
        _startPosition.y -= _player.position.y;
    }

    private void LateUpdate()
    {
        transform.position = _player.position + _startPosition;
    }
}

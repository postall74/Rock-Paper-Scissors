using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(BoxCollider))]
public class ObstacleRock : Obstacle
{
    [SerializeField] private Rigidbody[] _childObjects;
    [Header("Repulse")]
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;
    [SerializeField] private float _forceTorque = 0;
    [Range(0.01f, 2.99f), SerializeField] private float _moveDuration = 1f;
    [SerializeField] private ForceMode _forceMode;
    [Header("Object information")]
    [SerializeField] private Status _status;
    [SerializeField] private BoxCollider _boxCollider;

    private readonly float _timerDisable = 0.05f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.TryGetComponent(out StatusHandler playerCurrentStatus))
        {
            if (_status.CurrentStatus == playerCurrentStatus.PlayerStatus)
            {
                for (int i = 0; i < _childObjects.Length; i++)
                {
                    _childObjects[i].isKinematic = false;
                    _childObjects[i].AddForce(_targetPosition, _forceMode);
                    _childObjects[i].AddRelativeTorque(transform.right * _forceTorque, _forceMode);

                }

                StartCoroutine(BlocksBroke());
            }
        }
    }

    #region Coroutine
    private IEnumerator BlocksBroke()
    {
        yield return new WaitForSeconds(_timerDisable);
        _boxCollider.enabled = false;

        yield return new WaitForSeconds(_moveDuration);

        for (int i = 0; i < _childObjects.Length; i++)
        {
            Destroy(_childObjects[i].gameObject);
        }
            Destroy(gameObject);
    }
    #endregion
}

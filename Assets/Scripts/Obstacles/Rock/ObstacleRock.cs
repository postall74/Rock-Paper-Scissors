using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObstacleRock : Obstacle
{
    [Header("Repulse")]
    [SerializeField] private Vector3 _targetPosition = Vector3.zero;
    [SerializeField] private Vector3 _targetRotation = Vector3.zero;
    [Range(0.01f, 2.99f), SerializeField] private float _moveDuration = 1f;
    [SerializeField] private ForceMode _forceMode;
    [Header("Object information")]
    [SerializeField] private Status _status;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && player.TryGetComponent(out StatusHandler playerCurrentStatus))
        {
            if (_status.CurrentStatus == playerCurrentStatus.PlayerStatus)
            {
               StartCoroutine(BlocksBroke());
            }
        }
    }

    #region Coroutine
    private IEnumerator BlocksBroke()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;

        _rigidbody.AddForce(_targetPosition, _forceMode);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(i).GetComponent<Rigidbody>().AddTorque(
                new Vector3(_targetRotation.x, Random.Range(-_targetRotation.y, _targetRotation.y), _targetRotation.z),
                _forceMode);
            transform.GetChild(i).GetComponent<Rigidbody>().AddForce(_targetPosition, _forceMode);
        }

        yield return new WaitForSeconds(0.05f);
        _boxCollider.enabled = false;

        yield return new WaitForSeconds(_moveDuration);
        Destroy(gameObject);
    }
    #endregion
}

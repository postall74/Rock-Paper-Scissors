using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObstacleScissorsRope : Obstacle
{
    [Header("Repulse")]
    [SerializeField] private float _pushForceToObstacle = 650f;
    [SerializeField] private float _waitTime = 1f;
    [Header("Object information")]
    [SerializeField] private Status _status;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;

    private ObiRope _rope;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && player.TryGetComponent(out StatusHandler playerCurrentStatus))
        {
            if (_status.CurrentStatus == playerCurrentStatus.PlayerStatus)
            {
                if (transform.TryGetComponent(out _rope))
                {
                    StartCoroutine(RopeBroke());
                }
            }
        }
    }

    private IEnumerator RopeBroke()
    {
        _rigidbody.isKinematic = false;

        _boxCollider.enabled = false;
        _rope.AddForce(new Vector3(1, 0, 0) * _pushForceToObstacle, ForceMode.Acceleration);
        _rope.Tear(_rope.elements[(int)((_rope.elements.Count - 1) / 2)]);
        _rope.RebuildConstraintsFromElements();
        _rigidbody.AddForce(Vector3.forward * _pushForceToObstacle, ForceMode.Acceleration);

        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }
}
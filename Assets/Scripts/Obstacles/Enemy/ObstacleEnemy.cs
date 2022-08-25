using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObstacleEnemy : Enemy
{
    [Header("Repulse")]
    [SerializeField] private float _pushForceToEnemy = 650f;
    [SerializeField] private float _pushForceToPlayer = 15f;
    [SerializeField] private float _pushForceToPlayerUp = 3f;
    [SerializeField] private float _waitTime = 1f;
    [Header("Enemy information")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && player.TryGetComponent(out StatusHandler playerCurrentStatus))
        {
            if (playerCurrentStatus.TryWin(playerCurrentStatus, this.GetStatus()))
            {
                StartCoroutine(MoveOneByOne(player.GetComponent<Rigidbody>(), _rigidbody));
            }
        }
    }

    private IEnumerator MoveOneByOne(Rigidbody player, Rigidbody enemy)
    {
        player.AddForce(Vector3.forward * _pushForceToPlayer, ForceMode.VelocityChange);
        player.AddForce(Vector3.up * _pushForceToPlayerUp, ForceMode.VelocityChange);
        _boxCollider.enabled = false;
        yield return new WaitForSeconds(_waitTime);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.forward * _pushForceToEnemy, ForceMode.Acceleration);
        yield return null;
    }
}

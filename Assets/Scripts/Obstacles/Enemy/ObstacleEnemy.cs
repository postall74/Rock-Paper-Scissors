using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObstacleEnemy : Enemy
{
    [Header("Repulse")]
    [SerializeField] private float _pushForceToEnemy = 2f;
    [SerializeField] private float _waitTimeEnemy = 0.5f;
    [SerializeField] private float _pushForceToPlayer = 2f;
    [SerializeField] private float _waitTimePlayer = 1f;
    [Header("Enemy information")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    [Header("UI info")]
    [SerializeField] private GameObject _buttonPlanel;

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
        //player.AddForce(Vector3.forward * _pushForceToPlayer, ForceMode.VelocityChange);
        //player.AddForce(Vector3.up * _pushForceToPlayerUp, ForceMode.VelocityChange);
        player.DOJump(transform.position - new Vector3(0, 0, 3), _pushForceToPlayer, 0, _waitTimePlayer);
        _boxCollider.enabled = false;
        yield return new WaitForSeconds(_waitTimePlayer);
        transform.DOJump(new Vector3(0, 0, 200), _pushForceToEnemy, 0, _waitTimeEnemy);
        yield return null;
    }
}

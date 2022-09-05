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
    [SerializeField] private float _jumpPower = 2f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _playerJumpPower = 2f;
    [SerializeField] private float _playerDuration = 1f;
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
                StartCoroutine(PushEnemy(player.GetComponent<Rigidbody>(), _rigidbody));
            }
        }
    }

    private IEnumerator PushEnemy(Rigidbody player, Rigidbody enemy)
    {
        _buttonPlanel.GetComponent<Animator>().Play("Hide button panel");
        player.DOJump(transform.position - new Vector3(0, 0, 3), _playerJumpPower, 0, _playerDuration);
        _boxCollider.enabled = false;
        yield return new WaitForSeconds(_playerDuration);
        transform.DOJump(new Vector3(0, 0, 200), _jumpPower, 0, _duration);
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObstacleScissorsCloth : Obstacle
{
    [Header("Repulse")]
    [SerializeField] private float _pushForceToObstacle = 650f;
    [Header("Object information")]
    [SerializeField] private Status _status;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    [Header("Chield info")]
    [SerializeField] private GameObject _leftPart;
    [SerializeField] private Transform _leftPartTransform;
    [SerializeField] private GameObject _rightPart;
    [SerializeField] private Transform _rightPartTransform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && player.TryGetComponent(out StatusHandler playerCurrentStatus))
        {
            if (_status.CurrentStatus == playerCurrentStatus.PlayerStatus)
            {
                if (transform.GetComponent<Cloth>())
                {
                    StartCoroutine(ClothBroke());
                }
            }
        }
    }

    private IEnumerator ClothBroke()
    {
        _boxCollider.enabled = false;
        _leftPart.SetActive(true);
        _rightPart.SetActive(true);
        _leftPartTransform.SetParent(null);
        _rightPartTransform.SetParent(null);

        yield return null;
        Destroy(gameObject);
    }
}

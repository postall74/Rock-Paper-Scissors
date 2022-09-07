using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class ObstacleScissors : Obstacle
{
    [Header("Object information")]
    [SerializeField] private Status _status;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BoxCollider _boxCollider;
    [Header("Children info")]
    [SerializeField] private GameObject _leftPart;
    [SerializeField] private Transform _leftPartTransform;
    [SerializeField] private GameObject _rightPart;
    [SerializeField] private Transform _rightPartTransform;
    [SerializeField] private GameObject _freeCloth;
    [SerializeField] private Transform _freeClothTransform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Player player) && player.TryGetComponent(out StatusHandler playerCurrentStatus))
        {
            if (_status.CurrentStatus == playerCurrentStatus.PlayerStatus)
            {
                if (transform.GetComponent<Cloth>())
                    StartCoroutine(ClothBroke());
            }
            else if (_status.CurrentStatus != playerCurrentStatus.PlayerStatus)
            {
                if (transform.GetComponent<Cloth>() && _freeCloth != null && _freeClothTransform != null)
                    StartCoroutine(ShakeCloth());
            }
        }
    }

    private IEnumerator ShakeCloth()
    {
        _freeCloth.SetActive(true);
        yield return null;
       gameObject.SetActive(false);
    }

    private IEnumerator ClothBroke()
    {
        _boxCollider.enabled = false;
        _leftPart.SetActive(true);
        _rightPart.SetActive(true);
        _leftPartTransform.SetParent(null);
        _rightPartTransform.SetParent(null);
        yield return null;

        if (_freeCloth != null && _freeClothTransform != null)
        {
            Destroy(_freeCloth.gameObject);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);

    }
}

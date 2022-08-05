using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Obstacle : MonoBehaviour
{
    [Header("Player model")]
    [SerializeField] private GameObject _model;
    [Header("Repulse")]
    [SerializeField] private float _pushForceToObstacle = 375f;
    [SerializeField] private float _pushForceToPlayer = 15f;
    [SerializeField] private float _waitTime;
    [Header("Animation broken")]
    [SerializeField] private string _animationName;
    [Header("Status")]
    private Status _status;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private ObiRope _rope;
    private Cloth _cloth;

    private void Awake()
    {
        _status = GetComponent<Status>();
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<Player>(out Player player))
        {
            if (player.TryGetComponent<Status>(out Status playerCurrentStatus))
            {
                if (_status.Condition != playerCurrentStatus.Condition)
                {
                    player.GetComponent<Rigidbody>().AddForce(Vector3.back * _pushForceToPlayer, ForceMode.VelocityChange);
                    _model.GetComponent<Animator>().SetTrigger("Jump");
                    //Debug.DrawLine(transform.position, player.transform.position, Color.red);
                }
                else
                {
                    if (transform.TryGetComponent<Animator>(out Animator animator))
                    {
                        Debug.Log("Animaton");
                        StartCoroutine(AnimationBroke(_animationName));
                    }
                    else if (transform.TryGetComponent(out _rope))
                    {
                        Debug.Log("Rope");
                        StartCoroutine(RopeBroke());
                    }
                    else if (transform.TryGetComponent(out _cloth))
                    {
                        Debug.Log("Cloth");
                        StartCoroutine(ClothBroke());
                    }
                    else
                    {
                        Debug.Log("Bloks");
                        StartCoroutine(BlocksBroke());
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Player>(out Player player))
        {
            if (player.TryGetComponent<Status>(out Status playerCurrentStatus))
            {
                if (_status.Condition != playerCurrentStatus.Condition)
                {
                    player.GetComponent<Rigidbody>().AddForce(Vector3.back * _pushForceToPlayer, ForceMode.VelocityChange);
                    _model.GetComponent<Animator>().SetTrigger("Jump");
                    //Debug.DrawLine(transform.position, player.transform.position, Color.red);
                }
                else
                {
                    Debug.Log("Door");
                    StartCoroutine(DoorBroke());
                }
            }
        }
    }

    IEnumerator RopeBroke()
    {
        _rigidbody.isKinematic = false;

        _boxCollider.enabled = false;
        _rope.AddForce(new Vector3(1,0,0) * _pushForceToObstacle, ForceMode.Acceleration);
        _rope.Tear(_rope.elements[(int)((_rope.elements.Count - 1) / 2)]);
        _rope.RebuildConstraintsFromElements();
        _rigidbody.AddForce(Vector3.forward * _pushForceToObstacle, ForceMode.Acceleration);

        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }

    IEnumerator ClothBroke()
    {
        _boxCollider.enabled = false;
        _cloth.externalAcceleration = new Vector3(5, 5, 0);

        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }

    IEnumerator DoorBroke()
    {
        _rigidbody.isKinematic = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).DetachChildren();
        }

        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().useGravity = true;
        transform.GetComponent<Rigidbody>().AddForce(Vector3.forward * _pushForceToObstacle, ForceMode.Acceleration);

        yield return new WaitForSeconds(.05f);
        _boxCollider.enabled = false; 

        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }

    IEnumerator BlocksBroke()
    {
        _rigidbody.isKinematic = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
            transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.forward * _pushForceToObstacle, ForceMode.Acceleration);
        }

        yield return new WaitForSeconds(.05f);
        _boxCollider.enabled = false;

        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }

    IEnumerator AnimationBroke(string animationName)
    {
        _rigidbody.isKinematic = true;
        transform.GetComponent<Animator>().Play(animationName);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(_waitTime);
        Destroy(gameObject);
    }
}

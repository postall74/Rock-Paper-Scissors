using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CenterOfMass : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _centerOfMass;

    private void Update()
    {
        _rigidbody.centerOfMass = _centerOfMass;
        _rigidbody.WakeUp();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * _centerOfMass, .5f);
    }
}

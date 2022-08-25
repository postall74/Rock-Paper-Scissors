using UnityEngine;

[RequireComponent(typeof(Collider))]
public class LatePlatform : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EarlyPlatform : MonoBehaviour
{
    [SerializeField] private Collider _perfectPlatform;
    [SerializeField] private Collider _latePlatform;
    [SerializeField] private Collider _collider;

    public void DisableAllPlatformColliders()
    {
        _perfectPlatform.enabled = false;
        _latePlatform.enabled = false;
        _collider.enabled = false;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
}
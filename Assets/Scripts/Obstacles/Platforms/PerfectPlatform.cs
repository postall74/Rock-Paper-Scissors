using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PerfectPlatform : MonoBehaviour 
{
    [SerializeField] private Collider _latePlatform;
    [SerializeField] private Collider _earlyPlatform;
    [SerializeField] private Collider _collider;

    public void DisableAllPlatformColliders()
    {
        _latePlatform.enabled = false;
        _earlyPlatform.enabled = false;
        _collider.enabled = false;
    }

    public void DisableCollider()
    {
        _collider.enabled = false;
    }
}

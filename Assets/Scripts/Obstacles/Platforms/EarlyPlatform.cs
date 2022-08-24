using UnityEngine;

public class EarlyPlatform : MonoBehaviour
{
    [SerializeField] private Collider _perfectPlatform;
    [SerializeField] private Collider _latePlatform;

    public void DisableAllPlatformColliders()
    {
        _perfectPlatform.enabled = false;
        _latePlatform.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public void DisableCollider()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
using UnityEngine;

public class PerfectPlatform : MonoBehaviour 
{
    [SerializeField] private Collider _latePlatform;
    [SerializeField] private Collider _earlyPlatform;

    public void DisableAllPlatformColliders()
    {
        _latePlatform.enabled = false;
        _earlyPlatform.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public void DisableCollider()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }
}

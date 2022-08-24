using UnityEngine;

public class LatePlatform : MonoBehaviour
{
    [SerializeField] private Collider _perfectPlatform;
    [SerializeField] private Collider _earlyPlatform;

    public void DisableCollider()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }
}

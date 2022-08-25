using UnityEngine;

[RequireComponent(typeof(StatusHandler))]
[RequireComponent(typeof(EventsHandler))]
public class Player : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Obstacle obstacle))
        {
            if (obstacle.GetComponent<Status>().CurrentStatus == gameObject.GetComponent<StatusHandler>().PlayerStatus)
            {
                gameObject.GetComponent<StatusHandler>().ResetChangeStatus();
                gameObject.GetComponent<EventsHandler>().ResetMessageShow();
            }
        }
    }
}

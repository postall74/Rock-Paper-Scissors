using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Obstacle obstacle))
        {
            if (obstacle.GetComponent<Status>().CurrentStatus == gameObject.GetComponent<StatusHandler>().PlayerStatus)
            {
                gameObject.GetComponent<StatusHandler>().ResetChangeStatus();
                gameObject.GetComponent<EventsHandler>().ResetMessageShow();
            }
        }
    }
}

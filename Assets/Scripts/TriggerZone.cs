using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [System.Serializable]
    public class TriggerEvent : UnityEvent<Collider> { }


    private static Color color = new Color(1.0f, 0.5f, 0.0f, 0.5f);

    public bool TriggeredByPlayer = false;
    public bool TriggeredByHumans = false;

    public TriggerEvent OnEnter;
    public TriggerEvent OnExit;


    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Collider collider = GetComponent<Collider>();
        if (collider is BoxCollider)
        {
            Gizmos.DrawCube(collider.bounds.center, collider.bounds.size);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string triggerTag = other.gameObject.tag;
        if (TriggeredByPlayer && triggerTag == Tags.Player || 
            TriggeredByHumans && triggerTag == Tags.Human)
        {
            OnEnter.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string triggerTag = other.gameObject.tag;
        if (TriggeredByPlayer && triggerTag == Tags.Player ||
            TriggeredByHumans && triggerTag == Tags.Human)
        {
            OnExit.Invoke(other);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectOnTrigger : MonoBehaviour
{
    public Transform ObjectToMove;
    public Transform MoveFromPosition;
    public Transform MoveToPosition;
    public bool StartAtFromPosition;
    public float TravelTime;

    private bool Running;
    private float Delta;

    private void Awake()
    {
        if (StartAtFromPosition)
        {
            ObjectToMove.position = MoveFromPosition.position;
        }
    }

    void Update ()
    {
        if (Running)
        {
            ObjectToMove.position = Vector3.Lerp(MoveFromPosition.position, MoveToPosition.position, Delta / TravelTime);
            Delta += Time.deltaTime;

            if (Delta >= TravelTime)
            {
                ObjectToMove.position = MoveToPosition.position;
                Running = false;
            }
        }
	}

    public void Trigger()
    {
        Running = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(MoveFromPosition.position, 0.5f);
        Gizmos.DrawSphere(MoveToPosition.position, 0.5f);

        Gizmos.DrawLine(MoveFromPosition.position, MoveToPosition.position);
    }
}

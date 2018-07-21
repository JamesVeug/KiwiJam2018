using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public Transform TopStartingPoint;
    public Transform BottomStartingPoint;
    public Transform TopClimbPoint;
    public Transform BottomClimbPoint;

    public void PlayerAttach(Collider collider)
    {
        PlayerMovement[] movements = collider.GetComponentsInParent<PlayerMovement>();
        if (movements.Length == 0)
        {
            Debug.LogError("No ladders for gameObject " + collider.gameObject.name);
        }
        else
        {
            movements[0].AttachLadder(this);
        }
    }

    public void PlayerDetach(Collider collider)
    {
        PlayerMovement[] movements = collider.GetComponentsInParent<PlayerMovement>();
        if (movements.Length == 0)
        {
            Debug.LogError("No ladders for gameObject " + collider.gameObject.name);
        }
        else
        {
            movements[0].DetachLadder(this);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 playerSize = new Vector3(1, 2, 1);

        Gizmos.color = new Color(1, 0, 0, 0.25f);
        Gizmos.DrawCube(BottomStartingPoint.position + Vector3.Scale(playerSize / 2, Vector3.up), playerSize);
        Gizmos.DrawCube(TopStartingPoint.position + Vector3.Scale(playerSize / 2, Vector3.up), playerSize);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(BottomStartingPoint.position, BottomStartingPoint.position);
        Gizmos.DrawLine(BottomClimbPoint.position, TopClimbPoint.position);
        Gizmos.DrawLine(TopClimbPoint.position, TopStartingPoint.position);
    }

}

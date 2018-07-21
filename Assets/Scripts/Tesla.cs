using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tesla : MonoBehaviour
{
    public TriggerZone TriggerZone;
    public Transform SnapHumanPosition;

    public void OnTriggered(Collider collider)
    {
        Human human = collider.transform.parent.parent.GetComponent<Human>();

        human.ToggleAI(false);
        human.TogglePhysics(false);
        human.transform.position = SnapHumanPosition.position;
        human.animator.SetBool("IsBeingElectricuted", true);
    }
}

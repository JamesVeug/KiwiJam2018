using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tesla : MonoBehaviour
{
    public TriggerZone TriggerZone;
    public Transform SnapHumanPosition;
    public LineRenderer Lightening;

    public UnityEvent OnTrigger;

    private void Awake()
    {
        Lightening.enabled = false;
    }

    public void OnTriggered(Collider collider)
    {
        Human human = collider.transform.parent.parent.GetComponent<Human>();

        human.ToggleAI(false);
        human.TogglePhysics(false);
        human.transform.position = SnapHumanPosition.position;
        human.animator.SetBool("IsBeingElectricuted", true);
        Lightening.enabled = true;

        if(OnTrigger != null)
        {
            OnTrigger.Invoke();
        }
    }
}

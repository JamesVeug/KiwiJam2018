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
        Human human = collider.GetComponent<Human>();

        human.ToggleAI(false);
        
        human.transform.position = SnapHumanPosition.position;
        human.animator.SetTrigger("Electrocuting");
        Lightening.enabled = true;

        if(OnTrigger != null)
        {
            OnTrigger.Invoke();
        }
    }
}

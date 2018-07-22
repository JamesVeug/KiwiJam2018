using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableZone : MonoBehaviour
{
    public float FallTime = 5.0f;
    public Transform FallArea;
    public Animator FallAnimation;
    public Transform AttachTransform;

    /*private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Human human = GameObject.Find("Human").GetComponent<Human>();
            human.PushableSection = this;
            human.Push();
        }
    }*/

    public void OnHumanEntered(Collider collider)
    {
        Human[] humans = collider.GetComponentsInParent<Human>();
        humans[0].PushableSection = this;
    }

    public void OnHumanExited(Collider collider)
    {
        Human[] humans = collider.GetComponentsInParent<Human>();
        humans[0].PushableSection = this;
    }

    public void PlayPushAnimation(Transform humanTransform)
    {
        humanTransform.parent = AttachTransform;

        FallAnimation.SetTrigger("Fall");
    }
}

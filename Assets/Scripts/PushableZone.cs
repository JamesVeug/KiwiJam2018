using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableZone : MonoBehaviour
{
    public float FallTime = 5.0f;
    public Transform FallArea;
    public Animator FallAnimation;
    public Transform AttachTransform;

    public void OnHumanEntered(Collider collider)
    {
        Human[] humans = collider.GetComponentsInParent<Human>();

        FallAnimation = humans[0].GetComponentInChildren<Animator>();
        humans[0].PushableSection = this;
    }

    public void OnHumanExited(Collider collider)
    {
        Human[] humans = collider.GetComponentsInParent<Human>();

        FallAnimation = null;
        humans[0].PushableSection = this;
    }

    public IEnumerator PlayPushAnimation(Transform humanTransform)
    {
        humanTransform.parent = AttachTransform;

        RestartZone.Active = false;
        FallAnimation.SetTrigger("Pushed2");

        yield return new WaitForSeconds(1);
        SceneChanging.Instance.PlaySFXManDeath();
    }
}

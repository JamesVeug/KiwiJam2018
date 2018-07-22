﻿using System.Collections;
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

    public void PlayPushAnimation(Transform humanTransform)
    {
        humanTransform.parent = AttachTransform;

        FallAnimation.SetTrigger("Fall");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableZone : MonoBehaviour
{
    public float FallTime = 5.0f;
    public Transform FallArea;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Human human = GameObject.Find("Human").GetComponent<Human>();
            human.PushableSection = this;
            human.Push();
        }
    }

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

    public IEnumerator PlayPushAnimation(Transform humanTransform)
    {
        Vector3 startPosition = humanTransform.position;
        Vector3 endPosition = FallArea.position;

        Quaternion startRotation = humanTransform.rotation;
        Quaternion endRotation = FallArea.rotation;

        float distance = (endPosition - startPosition).magnitude;

        float delta = 0.0f;
        while (delta < FallTime)
        {
            humanTransform.position = Vector3.Lerp(startPosition, endPosition, delta);
            humanTransform.rotation = Quaternion.Slerp(startRotation, endRotation, delta);
            delta += Time.deltaTime;
            yield return null;
        }

        humanTransform.position = endPosition;
        yield return null;
    }
}

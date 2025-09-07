using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour
{
    [SerializeField]
    protected KeyCode targetKeycode;

    protected Transform targetTransform;

    protected float minViewportDiff = 0.05f;
    protected float maxViewportDiff = 0.5f;

    protected Vector3 initialMousePosition;
    protected Vector3 updatedMousePosition;


    private void Awake()
    {
        this.targetTransform = GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(this.targetKeycode))
        {
            StartCoroutine(this.ManipulateTarget());
        }
    }

    protected virtual IEnumerator ManipulateTarget()
    {
        yield return null;
    }

    protected float GetCurrentViewportDiff()
    {
        Vector3 initialMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 updatedMousePosition = initialMousePosition;

        float currentViewportDiff = Mathf.Clamp(Vector3.Distance(updatedMousePosition, initialMousePosition), 0.0f, this.maxViewportDiff);

        if (currentViewportDiff > this.minViewportDiff)
        {
            currentViewportDiff = Mathf.Clamp(Vector3.Distance(updatedMousePosition, initialMousePosition), 0.0f, this.maxViewportDiff);
        }

        return currentViewportDiff;
    }

    protected Vector3 GetDiffVector()
    {
        return (updatedMousePosition - initialMousePosition);
    }

    protected float GetNormalizedX()
    {
        return (Mathf.Abs(this.GetDiffVector().x / this.maxViewportDiff));
    }

    protected float GetNormalizedY()
    {
        return (Mathf.Abs(this.GetDiffVector().y / this.maxViewportDiff));
    }
}

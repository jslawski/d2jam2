using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour
{
    [SerializeField]
    protected KeyCode targetKeycode;
    [SerializeField]
    protected Transform bodyPartTransform;

    protected Transform targetTransform;    

    protected float minViewportDiff = 0.05f;
    protected float maxViewportDiff = 0.5f;

    protected Vector3 initialMousePosition;
    protected Vector3 updatedMousePosition;

    protected Vector3 startingPosition;

    private void Awake()
    {
        this.targetTransform = GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(this.targetKeycode))
        {
            this.startingPosition = this.targetTransform.position;

            StartCoroutine(this.ManipulateTarget());
        }
        /*
        else if (Input.GetKey(this.targetKeycode) == false && this.bodyPartTransform != null)
        {
            this.targetTransform.position = this.bodyPartTransform.position;
        }
        */
    }

    protected virtual IEnumerator ManipulateTarget()
    {
        yield return null;
    }

    protected float GetCurrentViewportDiff()
    {
        float currentViewportDiff = Mathf.Clamp(Vector3.Distance(this.updatedMousePosition, this.initialMousePosition), 0.0f, this.maxViewportDiff);

        if (currentViewportDiff > this.minViewportDiff)
        {
            currentViewportDiff = Mathf.Clamp(Vector3.Distance(this.updatedMousePosition, this.initialMousePosition), 0.0f, this.maxViewportDiff);
        }

        return currentViewportDiff;
    }

    protected float GetNormalizedViewportDiff()
    {
        return (this.GetCurrentViewportDiff() / this.maxViewportDiff);
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

    protected Vector3 GetPositionExtentsInDiffVectorDirection(float maxX, float maxY)
    {
        Vector3 positionExtents = this.startingPosition;

        Vector3 incrementalVector = this.GetDiffVector().normalized;

        //Debug.LogError("StartingPosition: " + this.startingPosition);
        //Debug.LogError("Incremental Vector: " + incrementalVector);

        if (incrementalVector.magnitude <= this.minViewportDiff)
        {
            return positionExtents;
        }
                
        while (Mathf.Abs(positionExtents.x) < Mathf.Abs(maxX) && Mathf.Abs(positionExtents.y) < Mathf.Abs(maxY))
        {
            positionExtents += incrementalVector;
        }
        
        if (positionExtents.x > maxX)
        {
            positionExtents.x = maxX;
        }
        if (positionExtents.y > maxY)
        {
            positionExtents.y = maxY;
        }

        return new Vector3(positionExtents.x, positionExtents.y, 0.0f);
    }
}

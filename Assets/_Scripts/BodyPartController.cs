using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartController : MonoBehaviour
{    
    public KeyCode targetKeycode;

    protected Transform targetTransform;

    protected float minViewportDiff = 0.00f;
    protected float maxViewportDiff = 0.5f;

    protected Vector3 initialMousePosition;
    protected Vector3 updatedMousePosition;

    private Vector3 _startingPosition;
    private Vector3 _startingRotation;
    [SerializeField]
    private Transform _bodyPartTransform;

    public bool isReplay = false;

    private void Awake()
    {
        this.targetTransform = GetComponent<Transform>();
        this._startingPosition = this.targetTransform.position;
        this._startingRotation = this.targetTransform.rotation.eulerAngles;
    }

    protected virtual void Update()
    {
        if (this.isReplay == true)
        {
            return;
        }
    
        if (Input.GetKeyDown(this.targetKeycode))
        {
            StartCoroutine(this.ManipulateTarget());
        }
    }

    public virtual void SimulateReplay()
    { 
    
    }

    protected virtual IEnumerator ManipulateTarget()
    {
        yield return null;
    }

    public Vector3 GetDiffVector()
    {
        if (this.isReplay == true)
        {
            return ReplaySimulator.instance.GetReplayDiffVector(this.targetKeycode);
        }
    
        if (Input.GetKey(this.targetKeycode) == true)
        {
            return this.GetClampedDiffVector(updatedMousePosition - initialMousePosition);
        }

        return Vector3.zero;
    }

    private Vector3 GetClampedDiffVector(Vector3 unclampedVector)
    {
        float clampedX = unclampedVector.x;
        float clampedY = unclampedVector.y;
    
        if (Mathf.Abs(unclampedVector.x) > this.maxViewportDiff)
        {
            clampedX = Mathf.Clamp(unclampedVector.x, -this.maxViewportDiff, this.maxViewportDiff);
        }

        if (Mathf.Abs(unclampedVector.y) > this.maxViewportDiff)
        {
            clampedY = Mathf.Clamp(unclampedVector.y, -this.maxViewportDiff, this.maxViewportDiff);
        }

        return new Vector3(clampedX, clampedY, 0.0f);
    }

    protected float GetNormalizedX()
    {
        if (this.isReplay == true)
        {
            return ReplaySimulator.instance.GetReplayNormalizedX(this.targetKeycode, this.maxViewportDiff);
        }
    
        return (Mathf.Abs(this.GetDiffVector().x / this.maxViewportDiff));
    }

    protected float GetNormalizedY()
    {
        if (this.isReplay == true)
        {
            return ReplaySimulator.instance.GetReplayNormalizedY(this.targetKeycode, this.maxViewportDiff);
        }

        return (Mathf.Abs(this.GetDiffVector().y / this.maxViewportDiff));
    }

    public void ResetBodyPart()
    {
        this._bodyPartTransform.position = this._startingPosition;
        this._bodyPartTransform.rotation = Quaternion.Euler(this._startingRotation);
        this.targetTransform.position = this._startingPosition;
    }

    public void SetInitialTransform(Vector3 initialPosition, Vector3 initialRotation)
    {
        this.targetTransform.position = initialPosition;
        this.targetTransform.rotation = Quaternion.Euler(initialRotation);
    }
}

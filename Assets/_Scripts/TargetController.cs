using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LimbSide { Left = -1, Right = 1 }

public class TargetController : BodyPartController
{
    [SerializeField]
    private float _maxXYDistance = 5.0f;

    [SerializeField]
    private float _minZDistance = 0.0f;
    [SerializeField]
    private float _maxZDistance = 5.0f;

    [SerializeField]
    private LimbSide _limbSide;
    
    protected override IEnumerator ManipulateTarget()
    {
        yield return new WaitForEndOfFrame();
    
        this.initialMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        this.updatedMousePosition = initialMousePosition;

        while (Input.GetKey(this.targetKeycode))
        {
            //Calculate XY Position
            float newX = this.CalculateXPosition();
            float newY = this.CalculateYPosition();
            float newZ = this.CalculateZPosition();

            this.targetTransform.position = new Vector3(newX, newY, newZ);

            yield return new WaitForEndOfFrame();

            this.updatedMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private float CalculateXPosition()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXYDistance, this.GetNormalizedX());
        
        if (this.GetDiffVector().x > 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateYPosition()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXYDistance, this.GetNormalizedY());

        if (this.GetDiffVector().y < 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateZPosition()
    {
        //Calculate Z Position
        if (this._limbSide == LimbSide.Left)
        {
            if (this.GetDiffVector().x > 0.0f)
            {
                return Mathf.Lerp(this._minZDistance, this._maxZDistance, this.GetNormalizedX());

            }
            else if (this.GetDiffVector().x < 0.0f)
            {
                return Mathf.Lerp(this._minZDistance, this._maxZDistance, this.GetNormalizedX());
            }
        }
        else if (this._limbSide == LimbSide.Right)
        {
            if (this.GetDiffVector().x > 0.0f)
            {
                return Mathf.Lerp(this._minZDistance, this._maxZDistance, this.GetNormalizedX());

            }
            else if (this.GetDiffVector().x < 0.0f)
            {
                return Mathf.Lerp(this._minZDistance, this._maxZDistance, this.GetNormalizedX());
            }
        }

        return this.targetTransform.position.z;
    }

    public override void SimulateReplay()
    {
        float newX = this.CalculateXPosition();
        float newY = this.CalculateYPosition();
        float newZ = this.CalculateZPosition();

        this.targetTransform.position = new Vector3(newX, newY, newZ);
    }
}

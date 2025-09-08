using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipsController : BodyPartController
{
    private float _maxXPosition = 0.4f;
    private float _minYPosition = 1.3f;
    private float _maxYPosition = 1.86f;
    private float _maxZPosition = 0.5f;

    protected override IEnumerator ManipulateTarget()
    {
        yield return new WaitForEndOfFrame();

        this.initialMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        this.updatedMousePosition = initialMousePosition;

        while (Input.GetKey(this.targetKeycode))
        {
            float xPosition = this.CalculateXPosition();
            float yPosition = this.CalculateYPosition();
            float zPosition = this.CalculateZPosition();

            this.targetTransform.position = new Vector3(xPosition, yPosition, zPosition);

            yield return new WaitForEndOfFrame();

            updatedMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private float CalculateXPosition()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXPosition, this.GetNormalizedX());

        if (this.GetDiffVector().x > 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateYPosition()
    {
        float diffAmount = Mathf.Lerp(this._maxYPosition, this._minYPosition, this.GetNormalizedY());
        
        return diffAmount;
    }

    private float CalculateZPosition()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxZPosition, this.GetNormalizedY());

        if (this.GetDiffVector().y > 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    public override void SimulateReplay()
    {
        float xPosition = this.CalculateXPosition();
        float yPosition = this.CalculateYPosition();
        float zPosition = this.CalculateZPosition();

        this.targetTransform.position = new Vector3(xPosition, yPosition, zPosition);
    }
}

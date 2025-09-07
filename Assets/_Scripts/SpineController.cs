using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineController : BodyPartController
{
    private float _maxXDistance = 0.5f;
    private float _maxXRotation = 40.0f;
    private float _maxZRotation = 15.0f;

    protected override IEnumerator ManipulateTarget()
    {
        yield return new WaitForEndOfFrame();

        this.initialMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        this.updatedMousePosition = initialMousePosition;

        while (Input.GetKey(this.targetKeycode))
        {
            float xPosition = this.CalculateXPosition();
            float xRotation = this.CalculateXRotation();
            float zRotation = this.CalculateZRotation();

            this.targetTransform.position = new Vector3(xPosition, this.targetTransform.position.y, this.targetTransform.position.z);
            this.targetTransform.rotation = Quaternion.Euler(-xRotation, 0.0f, zRotation);

            yield return new WaitForEndOfFrame();

            updatedMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private float CalculateXPosition()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXDistance, this.GetNormalizedX());

        if (this.GetDiffVector().x > 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateXRotation()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXRotation, this.GetNormalizedY());

        if (this.GetDiffVector().y < 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateZRotation()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxZRotation, this.GetNormalizedX());

        if (this.GetDiffVector().x < 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }
}

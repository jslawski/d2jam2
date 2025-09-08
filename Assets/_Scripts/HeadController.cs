using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : BodyPartController
{
    [SerializeField]
    private float _maxXAngle;
    [SerializeField]
    private float _maxZAngle;

    protected override IEnumerator ManipulateTarget()
    {
        yield return new WaitForEndOfFrame();

        this.initialMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        this.updatedMousePosition = initialMousePosition;

        while (Input.GetKey(this.targetKeycode))
        {
            //I'm too lazy to actually figure out what the proper names for these are right now.
            //I just know it works and I'm moving on.
            float zAngle = this.CalculateXAngle();
            float xAngle = this.CalculateZAngle();

            this.targetTransform.rotation = Quaternion.Euler(-xAngle, 0.0f, zAngle);

            yield return new WaitForEndOfFrame();

            updatedMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private float CalculateXAngle()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxZAngle, this.GetNormalizedX());

        if (this.GetDiffVector().x < 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateZAngle()
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXAngle, this.GetNormalizedY());

        if (this.GetDiffVector().y < 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    public override void SimulateReplay()
    {
        float zAngle = this.CalculateXAngle();
        float xAngle = this.CalculateZAngle();

        this.targetTransform.rotation = Quaternion.Euler(-xAngle, 0.0f, zAngle);
    }
}

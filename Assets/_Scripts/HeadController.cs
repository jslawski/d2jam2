using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    [SerializeField]
    private KeyCode _targetKeycode;

    [SerializeField]
    private float _maxXAngle;
    [SerializeField]
    private float _maxZAngle;

    private Transform _targetTransform;

    private float _minViewportDiff = 0.05f;
    private float _maxViewportDiff = 0.5f;

    private void Awake()
    {
        this._targetTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(this._targetKeycode))
        {
            StartCoroutine(this.ManipulateTarget());
        }
    }

    private IEnumerator ManipulateTarget()
    {
        yield return new WaitForEndOfFrame();

        Vector3 initialMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 updatedMousePosition = initialMousePosition;

        while (Input.GetKey(this._targetKeycode))
        {
            Vector3 diffVector = (updatedMousePosition - initialMousePosition);
            float normalizedX = Mathf.Abs(diffVector.x / this._maxViewportDiff);
            float normalizedY = Mathf.Abs(diffVector.y / this._maxViewportDiff);

            //I'm too lazy to actually figure out what the proper names for these are right now.
            //I just know it works and I'm moving on.
            float zAngle = this.CalculateXAngle(diffVector.normalized, normalizedX);
            float xAngle = this.CalculateZAngle(diffVector.normalized, normalizedY);

            this._targetTransform.rotation = Quaternion.Euler(-xAngle, 0.0f, zAngle);

            yield return new WaitForEndOfFrame();

            updatedMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private float GetCurrentViewportDiff()
    {
        Vector3 initialMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 updatedMousePosition = initialMousePosition;

        float currentViewportDiff = Mathf.Clamp(Vector3.Distance(updatedMousePosition, initialMousePosition), 0.0f, this._maxViewportDiff);

        if (currentViewportDiff > this._minViewportDiff)
        {
            currentViewportDiff = Mathf.Clamp(Vector3.Distance(updatedMousePosition, initialMousePosition), 0.0f, this._maxViewportDiff);
        }

        return currentViewportDiff;
    }

    private float CalculateXAngle(Vector3 diffVector, float normalizedX)
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxZAngle, normalizedX);

        if (diffVector.x < 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateZAngle(Vector3 diffVector, float normalizedY)
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXAngle, normalizedY);

        if (diffVector.y < 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }
}

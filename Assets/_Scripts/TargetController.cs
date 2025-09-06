using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LimbSide { Left = -1, Right = 1 }

public class TargetController : MonoBehaviour
{
    [SerializeField]
    private KeyCode _targetKeycode;

    [SerializeField]
    private float _maxXYDistance = 5.0f;
    private Vector3 _maxDistanceVector;

    [SerializeField]
    private float _minZDistance = 0.0f;
    [SerializeField]
    private float _maxZDistance = 5.0f;

    private Transform _targetTransform;

    private float _minViewportDiff = 0.05f;
    private float _maxViewportDiff = 0.5f;

    [SerializeField]
    private LimbSide _limbSide;

    private void Awake()
    {
        this._targetTransform = GetComponent<Transform>();
        this._maxDistanceVector = new Vector3(this._maxXYDistance, this._maxXYDistance, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

//        Debug.LogError("Initial Mouse Position: \n" + "ScreenSpace: " + Input.mousePosition + "\n" + "ViewportSpace: " + initialMousePosition);

        while (Input.GetKey(this._targetKeycode))
        {
            float currentViewportDiff = Mathf.Clamp(Vector3.Distance(updatedMousePosition, initialMousePosition), 0.0f, this._maxViewportDiff);

            if (currentViewportDiff > this._minViewportDiff)
            {                 
                currentViewportDiff = Mathf.Clamp(Vector3.Distance(updatedMousePosition, initialMousePosition), 0.0f, this._maxViewportDiff);
            }
            Vector3 diffVector = (updatedMousePosition - initialMousePosition);
            float normalizedX = Mathf.Abs(diffVector.x / this._maxViewportDiff);
            float normalizedY = Mathf.Abs(diffVector.y / this._maxViewportDiff);

            //Calculate XY Position
            float newX = this.CalculateXPosition(diffVector.normalized, normalizedX);
            float newY = this.CalculateYPosition(diffVector.normalized, normalizedY);
            float newZ = this.CalculateZPosition(diffVector.normalized, normalizedX);

            this._targetTransform.position = new Vector3(newX, newY, newZ);

            yield return new WaitForEndOfFrame();

            updatedMousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
    }

    private float CalculateXPosition(Vector3 normalizedDirection, float normalizedX)
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXYDistance, normalizedX);
        
        if (normalizedDirection.x > 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateYPosition(Vector3 normalizedDirection, float normalizedY)
    {
        float diffAmount = Mathf.Lerp(0.0f, this._maxXYDistance, normalizedY);

        if (normalizedDirection.y < 0.0f)
        {
            diffAmount *= -1.0f;
        }

        return diffAmount;
    }

    private float CalculateZPosition(Vector3 normalizedDirection, float normalizedX)
    {
        //Calculate Z Position
        if (this._limbSide == LimbSide.Left)
        {
            if (normalizedDirection.x > 0.0f)
            {
                return Mathf.Lerp(this._minZDistance, this._maxZDistance, normalizedX);

            }
            else if (normalizedDirection.x < 0.0f)
            {
                return Mathf.Lerp(this._minZDistance, this._maxZDistance, normalizedX);
            }
        }
        else if (this._limbSide == LimbSide.Right)
        {
            if (normalizedDirection.x > 0.0f)
            {
                return Mathf.Lerp(this._minZDistance, this._maxZDistance, normalizedX);

            }
            else if (normalizedDirection.x < 0.0f)
            {
                return Mathf.Lerp(this._minZDistance, this._maxZDistance, normalizedX);
            }
        }

        return this._targetTransform.position.z;
    }
}

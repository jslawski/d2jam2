using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManipulator : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _ragdollRigidbody;
    
    private Transform _manipulatorTransform;

    private void Awake()
    {
        this._manipulatorTransform = GetComponent<Transform>();       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Lerp to these instead of setting them instantaneously
        this._ragdollRigidbody.gameObject.transform.position = this._manipulatorTransform.position;
        this._ragdollRigidbody.gameObject.transform.rotation = this._manipulatorTransform.rotation;
    }
}

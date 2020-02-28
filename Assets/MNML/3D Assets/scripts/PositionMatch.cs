using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PositionMatch : MonoBehaviour
{

    public Vector3 position;

#if UNITY_EDITOR
    void Reset() {
        position = transform.position;
    }

    void OnValidate() {
        if(transform.position != position) {
            UnityEditor.Undo.RecordObject(transform, "change position via variable.");
            transform.position = position;
        }
    }   

    void Update () {
        if(transform.position != position) {
            UnityEditor.Undo.RecordObject(this, "change position via Transform.");
            position = transform.position;
        }
    }
#endif
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Antarsoft
public class CameraFollow : MonoBehaviour{
    [SerializeField] Transform toFollow = default;
    [SerializeField] Vector3 offset = default;
    [SerializeField] Vector3 minValue = default;
    [SerializeField] Vector3 maxValue = default;

    private void FixedUpdate() {
        Follow();
    }

    private void Follow(){
        Vector3 targetPosition = toFollow.position + offset;
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
            offset.z
        );

        transform.position = boundPosition;
    }
}

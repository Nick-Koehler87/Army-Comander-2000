using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform Target;
    public Transform orientation;
    public Transform self;

    void Start() {
        
    }
    /*
    public float Speed = 10f;
    public Vector3 Offset;
    void LateUpdate() {
        // Compute the position the object will reach
        Vector3 desiredPosition = Target.rotation * (Target.position + Offset);

        // Compute the direction the object will look at
        Vector3 desiredDirection = Vector3.Project(Target.forward, (Target.position - desiredPosition).normalized);

        // Rotate the object
        self.rotation = Quaternion.Slerp(self.rotation, Quaternion.LookRotation(desiredDirection), Time.deltaTime * Speed);

        // Place the object to "compensate" the rotation
        self.position = Target.position - self.forward * Offset.magnitude;
    }
    */
    /*
    public float speed = 0.1f; //speed in degrees per second?

    void Update () {
            Quaternion rotation1 = Quaternion.Euler(cameraPosition.position);
            Quaternion rotation2 = Quaternion.Euler(self.position);
            StartCoroutine(RotateOverTime(rotation1, rotation2, 1f / speed));
        }
 
    IEnumerator RotateOverTime (Quaternion originalRotation, Quaternion finalRotation, float duration) {
        if (duration > 0f) {
            float startTime = Time.time;
            float endTime = startTime + duration;
            self.rotation = originalRotation;
            yield return null;
            while (Time.deltaTime < endTime) {
                float progress = (Time.deltaTime - startTime) / duration;
                // progress will equal 0 at startTime, 1 at endTime.
                self.rotation = Quaternion.Slerp(originalRotation, finalRotation, progress);
                yield return null;
            }
        }
        self.rotation = finalRotation;
    }
    */

    // Start is called before the first frame update
    
    
    // Update is called once per frame
    void Update()
    {
        self.position = cameraPosition.position;
        self.rotation = cameraPosition.rotation;
    
    }
    
    
}

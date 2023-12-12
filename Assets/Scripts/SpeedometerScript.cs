using UnityEngine;
using UnityEngine.UI;

public class SpeedometerScript : MonoBehaviour
{
    public RectTransform needleTransform; // Reference to the needle's RectTransform
    private const float maxSpeedAngle = -20; // The angle of the needle at max speed
    private const float zeroSpeedAngle = 220; // The angle of the needle at zero speed

    private const float maxSpeed = 289.68f; // Maximum speed in km/h (equivalent to 180 mph)


    void Start(){
        //Set needle to 0 
        needleTransform.eulerAngles = new Vector3(0, 0, 220);

    }
    public void UpdateNeedle(float speed)
    {
        // Ensure that speed does not exceed maxSpeed
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        // Map the speed to the angle range
        needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation(speed));
    }

    private float GetSpeedRotation(float speed)
    {
        float totalAngleSize = zeroSpeedAngle - maxSpeedAngle;
        float speedNormalized = speed / maxSpeed;

        return zeroSpeedAngle - speedNormalized * totalAngleSize;
    }
}

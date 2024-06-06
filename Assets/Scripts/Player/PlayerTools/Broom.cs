using UnityEngine;

public class Broom : SwingingTool
{
    private const float StartRotationZ = -60f;
    private const float TargetRotationZ = 60f;

    public void ActivateAndSwoopBroom(float oneSwingAndBackDuration)
    {
        Vector3 startRotation = new Vector3(0f, 0f, StartRotationZ);
        Vector3 targetRotationForBroom = new Vector3(0f, 0f, TargetRotationZ);

        ActivateAndSwing(startRotation, targetRotationForBroom, oneSwingAndBackDuration, SFXType.BroomSweep);
    }

    public void DisableBroom()
    {
        DisableSwingingTool();
    }
}
using UnityEngine;

public class Broom : SwingingTool
{
    private const float StartRotationZ = -80f; // Magic number for start rotation
    private const float TargetRotationZ = 80f; // Magic number for target rotation

    public void ActivateAndSwoopBroom(float oneSwingAndBackDuration)
    {
        Vector3 startRotation = new Vector3(0f, 0f, StartRotationZ);
        Vector3 targetRotationForBroom = new Vector3(0f, 0f, TargetRotationZ);

        ActivateAndSwing(startRotation, targetRotationForBroom, oneSwingAndBackDuration, SFXType.BroomSweep);
    }

    public void DisableBroom()
    {
        Disable();
    }
}
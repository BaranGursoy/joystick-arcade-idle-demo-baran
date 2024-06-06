using UnityEngine;

public class Sword : SwingingTool
{
    private const float StartRotationY = -80f;
    private const float TargetRotationY = 80f;
    public void ActivateAndSwingSword(float oneSwingAndBackDuration)
    {
        Vector3 startRotation = new Vector3(0f, StartRotationY, 0f);
        Vector3 targetRotationForSword = new Vector3(0f, TargetRotationY, 0f);

        ActivateAndSwing(startRotation, targetRotationForSword, oneSwingAndBackDuration, SFXType.Swoosh);
    }

    public void DisableSword()
    {
        DisableSwingingTool();
    }
}
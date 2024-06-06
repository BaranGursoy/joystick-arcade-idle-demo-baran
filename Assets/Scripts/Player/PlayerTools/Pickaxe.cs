using UnityEngine;

public class Pickaxe : SwingingTool
{
    private const float TargetRotationX = 90f;
    public void ActivateAndSwingPickaxe(float oneSwingAndBackDuration)
    {
        Vector3 startRotation = Vector3.zero;
        Vector3 targetRotationForPickaxe = new Vector3(TargetRotationX, 0f, 0f);

        ActivateAndSwing(startRotation, targetRotationForPickaxe, oneSwingAndBackDuration, null);
    }

    public void DisablePickaxe()
    {
        DisableSwingingTool();
    }
}
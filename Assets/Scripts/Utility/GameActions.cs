using UnityEngine.Events;

public static class GameActions
{
    public static UnityAction PlayerTouchedSword;
    public static UnityAction EnemyDied;
    public static UnityAction<SFXType> PlaySfxAction;
    public static UnityAction PlayEnemyHitSfx;
    public static UnityAction BloodSplatCreated;
    public static UnityAction BloodSplatCleaned;
    public static UnityAction GameFinished;
    public static UnityAction ShakeCamera;
    public static UnityAction StopShakingCamera;
}

using UnityEngine.Events;

public static class GameActions
{
    public static UnityAction PlayerTouchedSword;
    public static UnityAction EnemyDied;
    public static UnityAction<SFXType> PlaySfxAction;
    public static UnityAction BloodSplatCreated;
    public static UnityAction BloodSplatCleaned;
    public static UnityAction GameFinished;
}

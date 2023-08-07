public static class Constants
{
    public enum EnemyState
    {
        IDLE,
        HIDING,
        PATROLLING,
        MOVE
    }

    static public int ENEMY_MAX_HEALTH = 3;
    static public int ENEMY_HEAD_DAMAGE = 2;
    static public int ENEMY_BODY_DAMAGE = 1;    

    static public float CLAMP_ROTATION = 60f;
    static public float CAM_SENSIVITY = 5f;
    

}
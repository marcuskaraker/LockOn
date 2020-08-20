using UnityEngine;

public abstract class EnemyBehaviour : ScriptableObject
{
    public abstract void ExecuteBehaviour(EnemyController enemyController);
}

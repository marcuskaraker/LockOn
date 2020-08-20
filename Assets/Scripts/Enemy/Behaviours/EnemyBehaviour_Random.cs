using UnityEngine;

[CreateAssetMenu(fileName = "EB_", menuName = "EnemyBehaviour/EB_Random")]
public class EnemyBehaviour_Random : EnemyBehaviour
{
    public override void ExecuteBehaviour(EnemyController enemyController)
    {
        enemyController.Movement.direction = Random.insideUnitCircle.normalized;
    }
}
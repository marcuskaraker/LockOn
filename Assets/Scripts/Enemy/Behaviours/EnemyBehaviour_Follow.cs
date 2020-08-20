using UnityEngine;

[CreateAssetMenu(fileName = "EB_", menuName = "EnemyBehaviour/EB_Follow")]
public class EnemyBehaviour_Follow : EnemyBehaviour
{
    public override void ExecuteBehaviour(EnemyController enemyController)
    {
        enemyController.Movement.direction = (GameManager.Instance.Player.transform.position - enemyController.transform.position).normalized;
    }
}

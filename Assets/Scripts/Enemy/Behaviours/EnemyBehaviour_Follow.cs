using UnityEngine;

[CreateAssetMenu(fileName = "EB_", menuName = "EnemyBehaviour/EB_Follow")]
public class EnemyBehaviour_Follow : EnemyBehaviour
{
    public bool followOffsetTarget;

    [Header("Charge")]
    public bool chargeWhenClose;
    public float minDistanceToTarget = 0.5f;
    public float chargeDuration = 2f;
    public float chargeSpeedMultiplier = 2f;

    public override void ExecuteBehaviour(EnemyController enemyController)
    {
        Vector2 playerPos = GameManager.Instance.Player ? (Vector2)GameManager.Instance.Player.transform.position : Vector2.zero;
        Vector2 target = followOffsetTarget ? playerPos + enemyController.targetOffset : playerPos;

        if (chargeWhenClose
            && !enemyController.isCharging 
            && Vector2.Distance(enemyController.transform.position, target) < minDistanceToTarget)
        {
            // Close enough, start to charge.
            enemyController.Charge(
                chargeDuration, 
                (playerPos - (Vector2)enemyController.transform.position).normalized, 
                chargeSpeedMultiplier
            );
        }
        else if (!enemyController.isCharging || !chargeWhenClose)
        {
            enemyController.Movement.direction = (target - (Vector2)enemyController.transform.position).normalized;
        }
    }
}
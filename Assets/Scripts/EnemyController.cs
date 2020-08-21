using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour;
    public Vector2 targetOffsetMinMax = new Vector2(0.5f, 1.5f);

    [System.NonSerialized] public bool isCharging;
    [System.NonSerialized] public Vector2 chargeDirection;
    [System.NonSerialized] public Vector2 targetOffset;

    private float speedBeforeCharge;

    public Movement Movement { get; private set; }
    public Shooter Shooter { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Shooter = GetComponent<Shooter>();

        targetOffset = Random.insideUnitCircle.normalized * Random.Range(targetOffsetMinMax.x, targetOffsetMinMax.y); 
    }

    private void Update()
    {
        enemyBehaviour.ExecuteBehaviour(this);
    }

    public void Charge(float chargeTime, Vector2 chargeDirection, float speedMultiplier)
    {
        StartCoroutine(DoCharge(chargeTime, chargeDirection, speedMultiplier));
    }

    private IEnumerator DoCharge(float chargeTime, Vector2 chargeDirection, float speedMultiplier)
    {
        isCharging = true;
        this.chargeDirection = chargeDirection;
        Movement.direction = chargeDirection;

        speedBeforeCharge = Movement.speed;
        Movement.speed *= speedMultiplier;

        yield return new WaitForSeconds(chargeTime);

        Movement.speed = speedBeforeCharge;
        isCharging = false;
    } 
}

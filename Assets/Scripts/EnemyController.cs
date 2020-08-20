using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour;

    public Movement Movement { get; private set; }
    public Shooter Shooter { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<Movement>();
        Shooter = GetComponent<Shooter>();      
    }

    private void Update()
    {
        enemyBehaviour.ExecuteBehaviour(this);
    }
}

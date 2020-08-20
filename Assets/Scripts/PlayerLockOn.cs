using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
    public GameObject lockOnTarget;
    public GameObject targetVisuals;
    public LayerMask hitMask;

    public Vector2 lastHitTargetPos;

    public bool changedLockOnTarget;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 50f, hitMask);
        if (hit.collider != null)
        {
            changedLockOnTarget = lockOnTarget != hit.transform.gameObject;

            lockOnTarget = hit.transform.gameObject;
            lastHitTargetPos = lockOnTarget.transform.position;
        }
        else
        {
            lockOnTarget = GameManager.Instance.GetEnemyClosestToPosition(lastHitTargetPos);
        }

        if (changedLockOnTarget)
        {
            targetVisuals.transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }

    private void LateUpdate()
    {
        if (lockOnTarget)
        {
            targetVisuals.SetActive(true);
            targetVisuals.transform.position = lockOnTarget.transform.position;
            targetVisuals.transform.localScale = Vector3.Lerp(targetVisuals.transform.localScale, Vector3.one, Time.deltaTime * 7f);
        }
        else
        {
            targetVisuals.SetActive(false);
        }
    }
}

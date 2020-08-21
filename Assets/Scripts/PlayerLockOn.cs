using UnityEngine;

public class PlayerLockOn : MonoBehaviour
{
    public GameObject lockOnTarget;
    public GameObject targetVisuals;
    public LayerMask hitMask;

    public bool changedLockOnTarget;

    private void Update()
    {
        LockOnCheck();

        if (changedLockOnTarget)
        {
            targetVisuals.transform.localScale = new Vector3(2f, 2f, 2f);
            changedLockOnTarget = false;
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

    public void LockOnCheck()
    {
        if (lockOnTarget == null)
        {
            lockOnTarget = GameManager.Instance.GetEnemyClosestToPosition(transform.position);
            changedLockOnTarget = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            lockOnTarget = GameManager.Instance.GetEnemyClosestToPosition(transform.position, lockOnTarget);
            changedLockOnTarget = true;
        }
    }
}

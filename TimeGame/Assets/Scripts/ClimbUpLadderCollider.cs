using UnityEngine;

public class ClimbUpLadderCollider : MonoBehaviour
{
    public Ladder ladder;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
          ladder.SetCanClimbTower(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ladder.SetCanClimbTower(false);
        }
    }
}

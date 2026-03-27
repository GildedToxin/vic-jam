using UnityEngine;

public class MazeWallCollision : MonoBehaviour
{
    private InvisableMaze invisableMaze;

    private void Awake()
    {
        invisableMaze = GetComponentInParent<InvisableMaze>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("Player hit a wall!");
            invisableMaze.WallCollison();
        }
    }
}
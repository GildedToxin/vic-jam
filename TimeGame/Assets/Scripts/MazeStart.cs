using UnityEngine;

public class MazeStart : MonoBehaviour
{
    [SerializeField] private InvisableMaze invisableMaze; // Reference to the maze walls GameObject

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            invisableMaze.ResetMaze();
        }
    }
}

using UnityEngine;

public class MazeEnd : MonoBehaviour
{
   [SerializeField] InvisableMaze invisableMaze; // Reference to the maze walls GameObject
    private void OnTriggerEnter(Collider other)
    {
        if (invisableMaze.GetCanCompleteRoom())
        {
            print("Player won!");
        }
    }
}

using UnityEngine;

public class InvisableMaze : MonoBehaviour
{
    [SerializeField] private bool canCompleteRoom;

    public void WallCollison()
    {
        canCompleteRoom = false; // Set the flag to indicate that the player has collided with a wall
    }
    public void ResetMaze()
    {
        canCompleteRoom = true; // Reset the flag when the player enters the start area of the maze
    }

    public bool GetCanCompleteRoom()
    {
        return canCompleteRoom;
    }
}

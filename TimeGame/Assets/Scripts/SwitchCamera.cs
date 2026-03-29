using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera freeLookCam;
    public CinemachineCamera interactCam;

    public CinemachineCamera clockCam;
    public CinemachineCamera room2Cam;

    [ContextMenu("Switch To Interact Cam")]

    public void Start()
    {
               SwitchToFreeLook();
    }
   public void SwitchToInteractCam()
    {
        interactCam.Priority = 30;
        freeLookCam.Priority = 10;
    }

    [ContextMenu ("Switch to other")]
   public void SwitchToFreeLook()
    {
        interactCam.Priority = 10;
        clockCam.Priority = 10;
        room2Cam.Priority = 10;
        freeLookCam.Priority = 30;
    }
    public void SwitchToClock()
    {
        clockCam.Priority = 30;
        freeLookCam.Priority = 10;
    }

    public void SwitchToSideRoom()
    {

   
        room2Cam.Priority = 30;
        freeLookCam.Priority = 10;
    }
}
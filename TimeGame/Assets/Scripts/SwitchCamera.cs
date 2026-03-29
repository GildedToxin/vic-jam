using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera freeLookCam;
    public CinemachineCamera interactCam;

    public CinemachineCamera clockCam;

    [ContextMenu("Switch To Interact Cam")]

    public void Start()
    {
               SwitchToFreeLook();
    }
   public void SwitchToInteractCam()
    {
        interactCam.Priority = 30;
        freeLookCam.Priority = 10;
        clockCam.Priority = 10;
    }

    [ContextMenu ("Switch to other")]
   public void SwitchToFreeLook()
    {
        interactCam.Priority = 10;
        clockCam.Priority = 10;
        freeLookCam.Priority = 30;
    }
    public void SwitchToClock()
    {
        interactCam.Priority = 10;
        clockCam.Priority = 30;
        freeLookCam.Priority = 10;
    }
}
using Unity.Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera freeLookCam;
    public CinemachineCamera interactCam;

    [ContextMenu("Switch To Interact Cam")]
   public void SwitchToInteractCam()
    {
        interactCam.Priority = 30;
        freeLookCam.Priority = 10;
    }

    [ContextMenu ("Switch to other")]
   public void SwitchToFreeLook()
    {
        interactCam.Priority = 10;
        freeLookCam.Priority = 30;
    }
}
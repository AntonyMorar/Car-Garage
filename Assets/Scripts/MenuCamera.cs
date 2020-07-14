using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MenuCamera : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras; //Front //Top //Antena

    private void OnEnable()
    {
        TabGroup.OnTabChangeTrigger += ChangePriority;
    }

    private void OnDisable()
    {
        TabGroup.OnTabChangeTrigger -= ChangePriority;
    }

    private void ChangePriority(ItemType itemType)
    {
        foreach (CinemachineVirtualCamera cam in virtualCameras)
        {
            cam.Priority = 0;
        }

        switch (itemType)
        {
            case (ItemType.Front):
                virtualCameras[0].Priority = 5;
                break;
            case (ItemType.Color):
                virtualCameras[1].Priority = 5;
                break;
            case (ItemType.Topper):
                virtualCameras[1].Priority = 5;
                break;
            case (ItemType.Antena):
                virtualCameras[2].Priority = 5;
                break;
            default:
                break;
        }
    }
}

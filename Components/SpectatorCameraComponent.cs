using UnityEngine;
using MelonLoader;
using System;

namespace SpectatorCamera
{
    public class SpectatorCameraComponent : MonoBehaviour
    {
        Transform playerHead;
        Camera spectatorCamera;

        void Start()
        {
            spectatorCamera = GetComponent<Camera>();
            
            spectatorCamera.stereoTargetEye = StereoTargetEyeMask.None;
            spectatorCamera.fieldOfView = 120f;
            spectatorCamera.targetDisplay = 0;
            spectatorCamera.depth = 100;
            spectatorCamera.nearClipPlane = 0.01f;

            foreach (Camera camera in FindObjectsOfType<Camera>())
            {
                if (camera.name == "Hmd") playerHead = camera.transform;
            }
            
            if (playerHead == null) MelonLogger.Log("Player head not found!", ConsoleColor.Red);
            if (spectatorCamera == null) MelonLogger.Log("Camera not found!", ConsoleColor.Red);
        }

        void LateUpdate()
        {
            if (playerHead == null)
            {
                playerHead = VrPlayerAvatar.instance.Head;
            }
            else if (spectatorCamera == null) MelonLogger.Log("Camera not found!", ConsoleColor.Red);
            else
            {
                transform.position = Vector3.Lerp(transform.position, playerHead.position, 0.9f * Time.unscaledDeltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, playerHead.rotation, 50f * Time.unscaledDeltaTime);
            }
        }
    }
}
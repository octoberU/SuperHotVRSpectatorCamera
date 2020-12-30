using Harmony;
using MelonLoader;
using SpectatorCamera;
using System;
using System.Reflection;
using UnityEngine;

[assembly: AssemblyVersion(SpectatorCameraMod.VERSION)]
[assembly: AssemblyFileVersion(SpectatorCameraMod.VERSION)]
[assembly: MelonGame("SUPERHOT_Team", "SUPERHOT_VR")]
[assembly: MelonInfo(typeof(SpectatorCameraMod), "Spectator Camera", SpectatorCameraMod.VERSION, "octo", "https://github.com/octoberU/SuperHotVRSpectatorCamera")]

public class SpectatorCameraMod : MelonMod
{
    public const string VERSION = "0.1.1";

    static bool objectCreated = false;

    [HarmonyPatch(typeof(VrPlayerAvatar), "Awake", new Type[0])]
    private static class VrPlayerAvatar_Awake
    {
        private static void Postfix(VrPlayerAvatar __instance)
        {
            if (objectCreated) return;
            MelonLogger.Log("Starting SpectatorCam");

            var spectatorCamObject = new GameObject("SpectatorCamera", new Type[]
            {
                typeof(SpectatorCameraComponent),
                typeof(Camera)
            }
            );
            spectatorCamObject.GetComponent<SpectatorCameraComponent>().enabled = true;
            
            spectatorCamObject.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            GameObject.DontDestroyOnLoad(spectatorCamObject);
            objectCreated = true;
        }
    }
}
﻿using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine.InputSystem;
using FPSSpectate.Patches;

namespace FPSSpectate
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class FPSSpectate : BaseUnityPlugin
    {
        public const string modGUID = "5Bit.FPSSpectate";
        public const string modName = "FPSSpectate";
        public const string modVersion = "1.0.2";

        public readonly Harmony harmony = new Harmony(modGUID);

        public static FPSSpectate Instance;
        public static ConfigEntry<Key> fpsKeyBind;

        public static ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            ConfigEntry<Key> fpsKeyBind = Config.Bind("Settings", "Keybind", Key.V, "Which key to press to switch between third and first person.");
            ConfigEntry<bool> defaultViewConfig = Config.Bind("Settings", "Default to first person", true, "Whether or not to default to first person when spectating");
            ConfigEntry<float> spectateOffset = Config.Bind("Settings", "Spectate Offset", 1.5f, "Offset of the camera for spectate");
            FPSSpectatePatch.firstPerson = defaultViewConfig.Value; 
            FPSSpectatePatch.SPECTATE_OFFSET = spectateOffset.Value; 

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);
            harmony.PatchAll();
        }
    }
}

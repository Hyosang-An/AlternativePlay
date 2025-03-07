﻿using AlternativePlay.Models;
using HarmonyLib;

namespace AlternativePlay.HarmonyPatches
{
    /// <summary>
    /// Patches the <see cref="NoteBasicCutInfoHelper.GetBasicCutInfo"/> method so that the
    /// check for saber type always succeeds.  This patch is <see cref="Priority.VeryHigh"/>
    /// since it is purposely set on and off by the user to play One Color or Spear.
    /// </summary>
    [HarmonyPatch(nameof(NoteBasicCutInfoHelper.GetBasicCutInfo))]
    [HarmonyPatch(typeof(NoteBasicCutInfoHelper))]
    [HarmonyPriority(Priority.VeryHigh)]
    internal class OneColorSaberPatch
    {
        private static void Postfix(ref bool saberTypeOK)
        {
            var config = Configuration.instance.ConfigurationData;
            if (config.PlayMode == PlayMode.BeatSpear ||
                // Allow multicutting if playing a one handed style
                (config.PlayMode == PlayMode.BeatFlail && (config.LeftFlailMode == BeatFlailMode.None || config.RightFlailMode == BeatFlailMode.None)) || 
                (Configuration.instance.ConfigurationData.OneColor && BS_Utils.Plugin.LevelData.Mode != BS_Utils.Gameplay.Mode.Multiplayer))
            {
                // Always allow saber hits from any type for OneColor or Spear
                saberTypeOK = true;
            }
        }
    }
}

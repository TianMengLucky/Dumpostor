using System;
using HarmonyLib;

namespace Dumpostor;

[HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.Awake))]
internal static class EntrypointPatch
{
    public static event Action Initialized;

    public static void Postfix(AmongUsClient __instance)
    {
        if (!AmongUsClient.Instance.Equals(__instance))
        {
            return;
        }

        Initialized?.Invoke();
    }
}

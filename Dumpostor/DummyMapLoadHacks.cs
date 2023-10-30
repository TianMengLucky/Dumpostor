using HarmonyLib;
using TMPro;
using UnityEngine;

namespace Dumpostor;

[HarmonyPatch]
internal sealed class DummyMapLoadHacks
{
    public static void Initialize()
    {
        TranslationController.Instance.Awake();
        DumpostorPlugin.EnglishLanguageUnit = new LanguageUnit(TranslationController.Instance.Languages[SupportedLangs.English]);

        var go = new GameObject();
        var shadowQuad = go.AddComponent<MeshRenderer>();
        shadowQuad.material = new Material(Shader.Find("Unlit/ShadowShader"));
        HudManager.Instance.ShadowQuad = shadowQuad;

        HudManager.Instance.Chat = go.AddComponent<ChatController>();
        HudManager.Instance.GameSettings = go.AddComponent<TextMeshPro>();

        Camera.main!.gameObject.AddComponent<FollowerCamera>();

        go.AddComponent<NormalGameManager>();
    }

    [HarmonyPatch(typeof(ChatController), nameof(ChatController.ForceClosed))]
    [HarmonyPrefix]
    public static bool SkipForceClosed() => false;

    [HarmonyPatch(typeof(ChatController), nameof(ChatController.SetVisible))]
    [HarmonyPrefix]
    public static bool SkipSetVisible() => false;
}

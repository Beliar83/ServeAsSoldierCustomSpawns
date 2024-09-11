using CustomSpawns.Dialogues;
using CustomSpawns.Utils;
using HarmonyLib;

namespace ServeAsSoldierCustomSpawns.Patches;

[HarmonyPatch(typeof(DialogueConditionInterpretor))]
public static class DialogueConditionInterpretorPatches
{
    [HarmonyPatch("HasPartyID")]
    [HarmonyPrefix]
    private static bool HasPartyIDPrefix(out bool __result, DialogueParams param, string ID)
    {
        // ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        __result = param.AdversaryParty?.Party?.MobileParty != null &&
                   CampaignUtils.IsolateMobilePartyStringID(param.AdversaryParty.Party.MobileParty) == ID;
        // ReSharper restore ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        return false;
    }

    [HarmonyPatch("PartyIsInFaction")]
    [HarmonyPrefix]
    private static bool PartyIsInFactionPrefix(out bool __result, DialogueParams param, string factionID)
    {
        // ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        __result = param.AdversaryParty?.MapFaction != null && param.AdversaryParty.MapFaction.StringId == factionID;
        // ReSharper restore ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        return false;
    }

    [HarmonyPatch("IsFriendly")]
    [HarmonyPrefix]
    private static bool IsFriendlyPrefix(out bool __result, DialogueParams param)
    {
        // ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        __result = !param.AdversaryParty?.MapFaction?.IsAtWarWith(param.PlayerParty?.MapFaction) ?? false;
        // ReSharper restore ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        return false;
    }

    [HarmonyPatch("IsHostile")]
    [HarmonyPrefix]
    private static bool IsIsHostilePrefix(out bool __result, DialogueParams param)
    {
        // ReSharper disable ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        __result = param.AdversaryParty?.MapFaction?.IsAtWarWith(param.PlayerParty?.MapFaction) ?? false;
        // ReSharper restore ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        return false;
    }
}

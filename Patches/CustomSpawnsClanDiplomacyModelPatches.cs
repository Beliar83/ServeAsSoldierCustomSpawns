using System.Reflection;
using System.Runtime.CompilerServices;
using CustomSpawns.Diplomacy;
using HarmonyLib;
using ServeAsSoldier;
using TaleWorlds.CampaignSystem;

namespace ServeAsSoldierCustomSpawns.Patches;

[HarmonyPatch(typeof(CustomSpawnsClanDiplomacyModel))]
public class CustomSpawnsClanDiplomacyModelPatches
{
    private static Test Test => Campaign.Current.GetCampaignBehavior<Test>();
    
    [HarmonyPatch(nameof(CustomSpawnsClanDiplomacyModel.IsWarDeclarationPossible))]
    [HarmonyPrefix]
    public static bool IsWarDeclarationPossiblePrefix(IFaction? attacker, IFaction? warTarget, CustomSpawnsClanDiplomacyModel __instance, out bool __result, MethodBase __originalMethod)
    {
        if (Test.followingHero is null)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            __result = IsWarDeclarationPossible(__instance, attacker, warTarget);
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        else
        {
            __result = warTarget is not { StringId: "player_faction" } ? 
                IsWarDeclarationPossible(__instance, attacker, warTarget) : 
                Test.followingHero.MapFaction.IsAtWarWith(attacker);
        }    
        
        return false;
    }
    
    [HarmonyReversePatch]
    [HarmonyPatch(nameof(CustomSpawnsClanDiplomacyModel.IsWarDeclarationPossible))]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static bool IsWarDeclarationPossible(CustomSpawnsClanDiplomacyModel instance, IFaction? attacker, IFaction? warTarget)
    {
        return instance.IsWarDeclarationPossible(attacker, warTarget);
    }
    
    [HarmonyPatch(nameof(CustomSpawnsClanDiplomacyModel.IsPeaceDeclarationPossible))]
    [HarmonyPrefix]
    public static bool IsPeaceDeclarationPossiblePrefix(IFaction? attacker, IFaction? peaceTarget, CustomSpawnsClanDiplomacyModel __instance, out bool __result, MethodBase __originalMethod)
    {
        if (Test.followingHero is null)
        {
#pragma warning disable CS8601 // Possible null reference assignment.
            __result = IsPeaceDeclarationPossible(__instance, attacker, peaceTarget);
#pragma warning restore CS8601 // Possible null reference assignment.
        }
        else
        {
            __result = peaceTarget is not { StringId: "player_faction" } ? 
                IsPeaceDeclarationPossible(__instance, attacker, peaceTarget) : 
                !Test.followingHero.MapFaction.IsAtWarWith(attacker);
        }    
        
        return false;
    }
    
    [HarmonyReversePatch]
    [HarmonyPatch(nameof(CustomSpawnsClanDiplomacyModel.IsPeaceDeclarationPossible))]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static bool IsPeaceDeclarationPossible(CustomSpawnsClanDiplomacyModel instance, IFaction? attacker, IFaction? peaceTarget)
    {
        return instance.IsPeaceDeclarationPossible(attacker, peaceTarget);
    }      
}

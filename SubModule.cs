using System.Reflection;
using HarmonyLib;
using TaleWorlds.MountAndBlade;


namespace ServeAsSoldierCustomSpawns;

public class SubModule : MBSubModuleBase
{
    protected override void OnSubModuleLoad()
    {
        base.OnSubModuleLoad();
        new Harmony("ServeAsSoldierCustomSpawns").PatchAll(Assembly.GetExecutingAssembly());
    }
}
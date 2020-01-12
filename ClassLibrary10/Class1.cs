using System;
using Harmony;
using KSerialization;

namespace ClassLibrary10
{
    public sealed class Foo : KMonoBehaviour
    {
        public Random random = new Random();
        [Serialize] public string Target { get; set; }
    }

    [HarmonyPatch(typeof(Game), "OnSpawn")]
    public static class Class1
    {
        public static void Postfix()
        {
            var foo = Immigration.Instance.GetComponent<Foo>();
            Debug.Log("从存档里读取的值");
            Debug.Log(foo.Target);
//            if (!foo.Target.IsNullOrWhiteSpace()) return;
            foo.Target = "foo_" + foo.random.Next();
            Debug.Log($"赋值{foo.Target}");
        }
    }


    [HarmonyPatch(typeof(SaveGame), "OnPrefabInit")]
    public static class SaveGame_OnPrefabInit_Patch
    {
        internal static void Postfix(SaveGame __instance)
        {
            Debug.Log("SaveGame_OnPrefabInit_Patch");
            var gameObject = __instance.gameObject;
            if (gameObject == null)
                return;
            //gameObject.AddOrGet<Foo>();
            var foo = __instance.FindOrAddComponent<Foo>();
        }
    }
}
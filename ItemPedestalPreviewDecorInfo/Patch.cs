using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ItemPedestalPreviewDecorInfo
{
    public class Hook
    {
        public static ReceptacleSideScreen Holder { get; set; }

        public static void SetText(LocText text, string value)
        {
            if (Holder is IncubatorSideScreen || Holder is PlanterSideScreen)
            {
                text.SetText(value);
                return;
            }

            var selectedDepositObjectTag = Traverse.Create(Holder).Field("selectedDepositObjectTag").GetValue<Tag>();
            var prefab = Assets.GetPrefab(selectedDepositObjectTag);
            var decor = 5f;
            if (prefab != null)
            {
                var decorProvider = prefab.GetComponent<DecorProvider>();
                decor = decorProvider != null ? decorProvider.baseDecor : 0;
            }

            var tail = string.Format(" (+{0})", Mathf.Max(decor * 2, 5));

            text.SetText(value + tail);
        }
    }

    [HarmonyPatch(typeof(ReceptacleSideScreen), "UpdateState")]
    public class ReceptacleSideScreenUpdateStatePatch
    {
        public static void Prefix(ReceptacleSideScreen __instance)
        {
            Hook.Holder = __instance;
        }

        public static void Postfix()
        {
            Hook.Holder = null;
        }

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var method = AccessTools.Method(typeof(Hook), nameof(Hook.SetText));
            var find = false;
            foreach (var instruction in instructions)
            {
                if (instruction.ToString() == "ldfld LocText subtitleLabel")
                {
                    find = true;
                }

                if (find && instruction.ToString() == "callvirt Void SetText(System.String)")
                {
                    instruction.operand = method;
                    find = false;
                }

                yield return instruction;
            }
        }
    }
}
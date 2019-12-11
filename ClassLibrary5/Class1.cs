using Harmony;
using Klei.AI;
using UnityEngine;

namespace ClassLibrary5
{
    [HarmonyPatch(typeof(ItemPedestal), "OnSpawn")]
    public class Class1
    {
        public static void Postfix(
            SingleEntityReceptacle ___receptacle
        )
        {
            Debug.Log("基座：" + ___receptacle.Occupant);

            if (___receptacle.Occupant != null)
            {
                var prefab = ___receptacle.Occupant;
                foreach (var attributeInstance in prefab.GetAttributes())
                {
                    Debug.Log(attributeInstance);
                }

                var attribute = Db.Get().BuildingAttributes.Decor.Lookup(prefab);
                if (attribute != null)
                {
                    Debug.Log("装饰度：" + attribute.GetTotalValue());
                }
            }
        }
    }

    //[HarmonyPatch(typeof(SingleEntityReceptacle))]
    public class Class2
    {
    }

//    [HarmonyPatch(typeof(ReceptacleSideScreen), "GetTitle")]
    public class Class3
    {
        public static void Postfix(
            ReceptacleSideScreen __instance,
            LocText ___subtitleLabel,
            string __result)
        {
            Debug.Log("展台的名字？ -> " + __instance);
            Debug.Log("展台的名字？ -> " + ___subtitleLabel.text);
            Debug.Log("展台的名字？ -> " + __result);
        }
    }

//    [HarmonyPatch(typeof(ReceptacleSideScreen), "Initialize")]
    public class Class4
    {
        public static void Prefix()
        {
            Debug.Log("Initialize");
        }
    }

//    [HarmonyPatch(typeof(ReceptacleSideScreen), "UpdateState")]
    public class Class5
    {
        public static void Prefix(object data)
        {
            Debug.Log("UpdateState" + (data == null));
        }

        public static void Postfix(
            string ___subtitleStringSelect,
            LocText ___subtitleLabel,
            Tag ___selectedDepositObjectTag,
            SingleEntityReceptacle ___targetReceptacle
        )
        {
            Debug.Log("UpdateState -> " + ___subtitleStringSelect);
            Debug.Log("UpdateState -> " + ___subtitleLabel);
            Debug.Log("UpdateState -> " + ___subtitleLabel.text);
            if (___targetReceptacle != null)
            {
                Debug.Log("UpdateState -> " + ___targetReceptacle.Occupant.GetProperName());
            }

            Debug.Log("UpdateState -> " + ___selectedDepositObjectTag);
            Debug.Log("UpdateState -> " + ___selectedDepositObjectTag.Name);
        }
    }

    [HarmonyPatch(typeof(ReceptacleSideScreen), "ToggleClicked")]
    public class Class6
    {
        public static void Postfix(
            Tag ___selectedDepositObjectTag
        )
        {
            Debug.Log("选择的是：" + ___selectedDepositObjectTag);
            var prefab = Assets.GetPrefab(___selectedDepositObjectTag);
            Debug.Log("选择的是：" + prefab);
            var decor = 5f;
            if (prefab != null)
            {
                var decorProvider = prefab.GetComponent<DecorProvider>();
                decor = decorProvider != null ? decorProvider.baseDecor : 0;
            }
            
            Debug.Log("装饰度: " + Mathf.Max(decor * 2, 5));
        }
    }
}
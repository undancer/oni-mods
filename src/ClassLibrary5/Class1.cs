﻿using HarmonyLib;
using undancer.Commons;
using UnityEngine;

namespace ClassLibrary5
{
    [HarmonyPatch(typeof(LoadScreen), "OnPrefabInit")]
    public static class Class1
    {
        public static void Postfix(
            KButton ___loadButton
        )
        {
            var openButton = Util.KInstantiateUI<KButton>(
                ___loadButton.gameObject,
                ___loadButton.transform.parent.gameObject,
                true
            );
            var p = openButton.transform.position;
            openButton.transform.position = new Vector3(p.x - 130, p.y);
            openButton.name = "OpenButton";
            openButton.GetComponentInChildren<LocText>().text = "Open...";
            openButton.onClick += delegate
            {
                var url = Util.RootFolder();
                FinderUtils.OpenInFileBrowser(url);
            };
        }
    }
}
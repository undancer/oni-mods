using System.Linq;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace undancer.ScheduleShift
{
    [HarmonyPatch(typeof(ScheduleScreen), "OnAddScheduleClick")]
    public static class ScheduleScreenOnAddScheduleClickPatch
    {
        public static bool Prefix()
        {
            var schedule = ScheduleManager.Instance.GetSchedules().LastOrDefault();
            ScheduleManager.Instance.AddSchedule(
                schedule != null ? schedule.GetGroups() : Db.Get().ScheduleGroups.allGroups);
            return false;
        }
    }

    [HarmonyPatch(typeof(ScheduleScreenEntry), nameof(ScheduleScreenEntry.Setup))]
    public static class ScheduleScreenEntrySetupPatch
    {
        private static GameObject _buttonsHolderPanel;

        private static bool SetParentAndLayer(GameObject child, GameObject parent)
        {
            if (parent == null) return true;
            child.transform.SetParent(parent.transform, false);
            SetLayerRecursively(child, parent.layer);
            return false;
        }

        private static void SetLayerRecursively(GameObject go, int layer)
        {
            go.layer = layer;
            var transform = go.transform;
            for (var index = 0; index < transform.childCount; ++index)
                SetLayerRecursively(transform.GetChild(index).gameObject, layer);
        }

        public static bool Prefix(
            DialogPanel ___optionsPanel,
            KButton ___optionsButton,
            Schedule schedule)
        {
            var button = ___optionsPanel.GetComponent<HierarchyReferences>().GetReference<KButton>("ResetButton");

            var panel = DefaultControls.CreatePanel(new DefaultControls.Resources());
            panel.name = "ButtonsHolder";
            panel.GetComponent<Image>().color = Color.clear;
            if (!SetParentAndLayer(panel, ___optionsButton.gameObject.transform.parent.gameObject))
                SetLayerRecursively(panel, LayerMask.NameToLayer("UI"));

            var orAddComponent = panel.FindOrAddComponent<GridLayoutGroup>();
            orAddComponent.cellSize = new Vector2(24, 24);
            orAddComponent.spacing = new Vector2(5, 0);
            orAddComponent.childAlignment = TextAnchor.UpperCenter;

            _buttonsHolderPanel = panel;

            var leftButton = Util.KInstantiateUI<KButton>(button.gameObject, _buttonsHolderPanel.gameObject, true);
            leftButton.name = "LeftShiftButton";
            leftButton.GetComponentInChildren<LocText>().text = "<";
            leftButton.GetComponentInChildren<ToolTip>().ClearMultiStringTooltip();
            leftButton.onClick += schedule.LeftShift;


            var rightButton = Util.KInstantiateUI<KButton>(button.gameObject, _buttonsHolderPanel.gameObject, true);
            rightButton.name = "RightShiftButton";
            rightButton.GetComponentInChildren<LocText>().text = ">";

            rightButton.GetComponentInChildren<ToolTip>().ClearMultiStringTooltip();
            rightButton.onClick += schedule.RightShift;

            return true;
        }
    }
}
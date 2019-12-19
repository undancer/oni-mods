using System.Collections.Generic;
using Harmony;

namespace undancer.NotificationManager
{
//    [HarmonyPatch(typeof(Notifier), nameof(Notifier.Add))]
    public class Class1
    {
        public static void Prefix(
            Notification notification,
            string suffix)
        {
//            Debug.Log("Add");
//            Debug.Log(notification);

            if (notification is MessageNotification message)
            {
                Debug.Log("MSG");
            }
            Debug.Log(notification.titleText);

            Debug.Log("suffix: " + suffix);
        }
    }

//    [HarmonyPatch(typeof(NotificationScreen),"Update")]
    public class Class2
    {

        public static void Postfix(
         List<Notification> ___pendingNotifications,
         List<Notification> ___notifications
            
            )
        {
            Debug.Log("UPDATE !!");
            foreach (var notification in ___pendingNotifications)
            {
                Debug.Log("PENDING " + notification);

            }
            foreach (var notification in ___notifications)
            {
                Debug.Log("NOTIFIC " + notification);

            }
        }
        
    }

//    [HarmonyPatch(typeof(NotificationScreen),"OnSpawn")]
    public class Class3
    {
        public static void Postfix(
            List<Notification> ___pendingNotifications,
            List<Notification> ___notifications
            )
        {
            Debug.Log("OnSpawn !!");
            foreach (var notification in ___pendingNotifications)
            {
                Debug.Log("PENDING " + notification.titleText);
            }
            foreach (var notification in ___notifications)
            {
                Debug.Log("NOTIFIC " + notification.titleText);
            }
        }
    }

    [HarmonyPatch(typeof(NotificationScreen),"AddNotification")]
    public class Class4
    {
        public static void Prefix(
            NotificationScreen __instance,
            Notification notification
            )
        {          
            Debug.Log("ADD " + notification);
            Debug.Log("ADD title -> " + notification.titleText);
            Debug.Log("ADD type -> " + notification.Type);
            Debug.Log("ADD group -> " + notification.Group.HashValue);
            Debug.Log("ADD tooltip ->" + notification.ToolTip);
            Debug.Log("ADD tooltip_data -> " + notification.tooltipData);
            Debug.Log("ADD expires -> " + notification.expires);
            Debug.Log("ADD delay -> " + notification.Delay);
            Debug.Log("ADD click -> " + notification.customClickCallback);
            Debug.Log("ADD click_data -> " + notification.customClickData);
            Debug.Log("ADD focus -> " + notification.clickFocus);
            Debug.Log("");
        }
    }

    [HarmonyPatch(typeof(NotificationScreen),"SortNotifications")]
    public class Class5
    {
        public static bool Prefix(
            NotificationScreen __instance,
            List<Notification> ___notifications
            )
        {

            Debug.Log("排序？");
            return false;

        }
    }
}
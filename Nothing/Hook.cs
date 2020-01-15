namespace Nothing
{

    public class Hook
    {
        public static bool AlwaysTrue()
        {
            Debug.Log("DistributionPlatform.Initialized");
            Debug.Log("================================");
            Debug.Log(DistributionPlatform.Initialized);
            Debug.Log("================================");
            return true;
        }
    }
}
using KSerialization;

namespace undancer.SelectLastCarePackage
{
    public class ImmigrantScreenContext : KMonoBehaviour
    {
        public bool Skip { get; set; }
        [Serialize] public CarePackageInfo LastSelectedCarePackageInfo { get; set; }
    }
}
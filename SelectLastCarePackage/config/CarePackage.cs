namespace undancer.SelectLastCarePackage.config
{
    public class CarePackage
    {
        public string id { get; set; }
        public float quantity { get; set; }


        public CarePackage()
        {
        }

        public CarePackage(string carePackageId, float carePackageQuantity)
        {
            id = carePackageId;
            quantity = carePackageQuantity;
        }
    }
}
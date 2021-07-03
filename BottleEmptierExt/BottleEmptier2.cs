using KSerialization;
using UnityEngine;

namespace undancer.BottleEmptierExt
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class BottleEmptier2 : BottleEmptier, IUserControlledCapacity
    {
        [Serialize] 
        private float userMaxCapacity = float.PositiveInfinity;

        public virtual float UserMaxCapacity
        {
            get => Mathf.Min(userMaxCapacity, GetComponent<Storage>().capacityKg * 1000f);
            set => userMaxCapacity = value;
        }

        public float AmountStored => GetComponent<Storage>().MassStored();


        public float MinCapacity => 0.0f;

        public float MaxCapacity => GetComponent<Storage>().capacityKg * 1000f;


        public bool WholeValues => false;

        public LocString CapacityUnits => GameUtil.GetCurrentMassUnit(true);
    }
}
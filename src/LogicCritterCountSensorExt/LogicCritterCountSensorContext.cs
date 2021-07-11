namespace undancer.LogicCritterCountSensorExt
{
    public class LogicCritterCountSensorContext : KMonoBehaviour
    {
        public bool Initialed { get; set; }
        public bool CountCreatures { get; set; }
        public bool CountEggs { get; set; }

        protected override void OnPrefabInit()
        {
            base.OnPrefabInit();

//            Debug.Log("初始化 ！！");

            if (!Initialed)
            {
                CountCreatures = true;
                CountEggs = true;
                Initialed = true;
            }
        }

        public int GetState()
        {
            var state = 0;
            if (CountCreatures) state += 1;

            if (CountEggs) state += 2;

            return state;
        }
    }
}
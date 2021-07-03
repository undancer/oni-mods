namespace CodexProbe.Patch
{
//    [HarmonyPatch(typeof(CodexEntryGenerator), nameof(CodexEntryGenerator.GenerateFoodEntries))]
    public static class GenerateFoodEntries
    {
        public static void Postfix()
        {
            Debug.Log("GenerateFoodEntries");
            /*foreach (var info in FOOD.FOOD_TYPES)
            {
                var name = info.Id;
                var sprite = Def.GetUISprite(info.ConsumableId).first;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/FOOD", name.ToUpper()},
                    sprite = sprite,
                    color = Color.white
                });
            }*/

            var list = new[]
            {
                RotPileConfig.ID,
            };

            foreach (var item in list)
            {
                var name = item;
                var go = ((Tag) item).Prefab();

                var tuple = Def.GetUISprite(go);
                var sprite = tuple.first;
                var color = tuple.second;

                ImageUtils.SaveImage(new Image
                {
                    prefixes = new[] {"ASSETS/FOOD", name.ToUpper()},
                    sprite = sprite,
                    color = color,
                });
            }
        }
    }
}
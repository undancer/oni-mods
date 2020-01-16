// Decompiled with JetBrains decompiler
// Type: RefinementPlus.Methods
// Assembly: RefinementPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B692DF77-975B-476A-9AF9-C249F16125AB
// Assembly location: /Users/undancer/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Steam/1803983415/RefinementPlus.dll

using System.Collections.Generic;
using STRINGS;

namespace RefinementPlus
{
    internal class Methods : RefinementPlusData
    {
        public void burnCloth()
        {
            var dictionary = new Dictionary<string, string>
            {
                {
                    CoolVestConfig.ID,
                    EQUIPMENT.PREFABS.COOL_VEST.NAME
                },
                {
                    FunkyVestConfig.ID,
                    EQUIPMENT.PREFABS.FUNKY_VEST.NAME
                },
                {
                    WarmVestConfig.ID,
                    EQUIPMENT.PREFABS.WARM_VEST.NAME
                }
            };
            var format = "Burn old {0}";
            foreach (var keyValuePair in dictionary)
            {
                var ingredients = new[]
                {
                    new ComplexRecipe.RecipeElement((Tag) keyValuePair.Key, 1f)
                };
                var results = new[]
                {
                    new ComplexRecipe.RecipeElement(
                        SimHashes.Carbon.CreateTag()
                        , 3f
                    )
                };
                recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(
                        KilnConfig.ID
                        , ingredients, results),
                    ingredients, results)
                {
                    time = 55f,
                    description = string.Format(format, keyValuePair.Value),
                    fabricators = new List<Tag>
                    {
                        TagManager.Create(KilnConfig.ID)
                    },
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
                }.id);
            }
        }

        public void burnRot()
        {
            var tag = SimHashes.Carbon.CreateTag();
            var str = "Burn rotten food";
            var ingredients = new[]
            {
                new ComplexRecipe.RecipeElement((Tag) RotPileConfig.ID, 100f)
            };
            var results = new[]
            {
                new ComplexRecipe.RecipeElement(tag, 50f)
            };
            recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(KilnConfig.ID, ingredients, results),
                ingredients, results)
            {
                time = 35f,
                description = str,
                fabricators = new List<Tag>
                {
                    TagManager.Create(KilnConfig.ID)
                },
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
            }.id);
        }

        public void clayToSlime()
        {
            var tag1 = SimHashes.Clay.CreateTag();
            var tag2 = SimHashes.ToxicSand.CreateTag();
            var tag3 = SimHashes.SlimeMold.CreateTag();
            var format = "Mixes {0} with {1} to create {2}";
            var ingredients = new[]
            {
                new ComplexRecipe.RecipeElement(tag1, 200f),
                new ComplexRecipe.RecipeElement(tag2, 200f)
            };
            var results = new[]
            {
                new ComplexRecipe.RecipeElement(tag3, 400f)
            };
            recipesIDs.Add(new ComplexRecipe(
                ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                ingredients, results)
            {
                time = 100f,
                description = string.Format(format, tag1, tag2, tag3),
                fabricators = new List<Tag>
                {
                    TagManager.Create(RockCrusherConfig.ID)
                },
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
            }.id);
        }

        public void limeCrushing()
        {
            var dictionary = new Dictionary<string[], float[]>
            {
                {
                    new string[] {EggShellConfig.ID, MISC.TAGS.EGGSHELL},
                    new[] {5f, 5f}
                },
                {
                    new string[]
                    {
                        BabyCrabShellConfig.ID,
                        ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME
                    },
                    new[] {1f, 5f}
                },
                {
                    new string[]
                    {
                        CrabShellConfig.ID,
                        ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME
                    },
                    new[] {1f, 10f}
                }
            };
            var tag = SimHashes.Lime.CreateTag();
            var format = "Crushes {0} into {1}";
            foreach (var keyValuePair in dictionary)
            {
                var ingredients = new[]
                {
                    new ComplexRecipe.RecipeElement((Tag) keyValuePair.Key[0], keyValuePair.Value[0])
                };
                var results = new[]
                {
                    new ComplexRecipe.RecipeElement(tag, keyValuePair.Value[1])
                };
                recipesIDs.Add(new ComplexRecipe(
                    ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                    ingredients, results)
                {
                    time = 40f,
                    description = string.Format(format, keyValuePair.Key[1], tag),
                    fabricators = new List<Tag>
                    {
                        TagManager.Create(RockCrusherConfig.ID)
                    },
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
                }.id);
            }
        }

        public void fossilCrushing()
        {
            var tag1 = SimHashes.Fossil.CreateTag();
            var tag2 = SimHashes.Lime.CreateTag();
            var tag3 = SimHashes.Sand.CreateTag();
            var format = "Crushes {0} into {1} and {2}";
            var ingredients = new[]
            {
                new ComplexRecipe.RecipeElement(tag1, 100f)
            };
            var results = new[]
            {
                new ComplexRecipe.RecipeElement(tag2, 5f),
                new ComplexRecipe.RecipeElement(tag3, 95f)
            };
            recipesIDs.Add(new ComplexRecipe(
                ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                ingredients, results)
            {
                time = 40f,
                description = string.Format(format, tag1, tag2, tag3),
                fabricators = new List<Tag>
                {
                    TagManager.Create(RockCrusherConfig.ID)
                },
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
            }.id);
        }

        public void maficToRegolith()
        {
            var tag1 = SimHashes.MaficRock.CreateTag();
            var tag2 = SimHashes.Regolith.CreateTag();
            var format = "Crushes {0} into {1}";
            var ingredients = new[]
            {
                new ComplexRecipe.RecipeElement(tag1, 100f)
            };
            var results = new[]
            {
                new ComplexRecipe.RecipeElement(tag2, 80f)
            };
            recipesIDs.Add(new ComplexRecipe(
                ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                ingredients, results)
            {
                time = 80f,
                description = string.Format(format, tag1, tag2),
                fabricators = new List<Tag>
                {
                    TagManager.Create(RockCrusherConfig.ID)
                },
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
            }.id);
        }

        public void mineralsCrushing()
        {
            var dictionary = new Dictionary<Tag, Tag>
            {
                {
                    SimHashes.Katairite.CreateTag(),
                    SimHashes.IronOre.CreateTag()
                },
                {
                    SimHashes.Granite.CreateTag(),
                    SimHashes.Wolframite.CreateTag()
                },
                {
                    SimHashes.IgneousRock.CreateTag(),
                    SimHashes.IronOre.CreateTag()
                },
                {
                    SimHashes.Obsidian.CreateTag(),
                    SimHashes.IronOre.CreateTag()
                },
                {
                    SimHashes.SandStone.CreateTag(),
                    SimHashes.Cuprite.CreateTag()
                },
                {
                    SimHashes.SedimentaryRock.CreateTag(),
                    SimHashes.GoldAmalgam.CreateTag()
                }
            };
            var tag = SimHashes.Sand.CreateTag();
            var format = "Crushes {0} into {1} and a bit of {2}";
            foreach (var keyValuePair in dictionary)
            {
                var ingredients = new[]
                {
                    new ComplexRecipe.RecipeElement(keyValuePair.Key, 100f)
                };
                var results = new[]
                {
                    new ComplexRecipe.RecipeElement(tag, 90f),
                    new ComplexRecipe.RecipeElement(keyValuePair.Value, 10f)
                };
                recipesIDs.Add(new ComplexRecipe(
                    ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                    ingredients, results)
                {
                    time = 50f,
                    description = string.Format(format, keyValuePair.Key, tag, keyValuePair.Value),
                    fabricators = new List<Tag>
                    {
                        TagManager.Create(RockCrusherConfig.ID)
                    },
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
                }.id);
            }
        }

        public void oresToRefined()
        {
            var dictionary = new Dictionary<Tag, Tag>
            {
                {
                    SimHashes.AluminumOre.CreateTag(),
                    SimHashes.Aluminum.CreateTag()
                },
                {
                    SimHashes.Cuprite.CreateTag(),
                    SimHashes.Copper.CreateTag()
                },
                {
                    SimHashes.GoldAmalgam.CreateTag(),
                    SimHashes.Gold.CreateTag()
                },
                {
                    SimHashes.IronOre.CreateTag(),
                    SimHashes.Iron.CreateTag()
                },
                {
                    SimHashes.Wolframite.CreateTag(),
                    SimHashes.Tungsten.CreateTag()
                }
            };
            var tag = SimHashes.Sand.CreateTag();
            var format = "Crushes {0} into {1} and {2}";
            foreach (var keyValuePair in dictionary)
            {
                var ingredients = new[]
                {
                    new ComplexRecipe.RecipeElement(keyValuePair.Key, 100f)
                };
                var results = new[]
                {
                    new ComplexRecipe.RecipeElement(tag, 50f),
                    new ComplexRecipe.RecipeElement(keyValuePair.Value, 50f)
                };
                recipesIDs.Add(new ComplexRecipe(
                    ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                    ingredients, results)
                {
                    time = 50f,
                    description = string.Format(format, keyValuePair.Key, tag, keyValuePair.Value),
                    fabricators = new List<Tag>
                    {
                        TagManager.Create(RockCrusherConfig.ID)
                    },
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
                }.id);
            }
        }

        public void refinedToOres()
        {
            var dictionary = new Dictionary<Tag, Tag>
            {
                {
                    SimHashes.Aluminum.CreateTag(),
                    SimHashes.AluminumOre.CreateTag()
                },
                {
                    SimHashes.Copper.CreateTag(),
                    SimHashes.Cuprite.CreateTag()
                },
                {
                    SimHashes.Gold.CreateTag(),
                    SimHashes.GoldAmalgam.CreateTag()
                },
                {
                    SimHashes.Iron.CreateTag(),
                    SimHashes.IronOre.CreateTag()
                },
                {
                    SimHashes.Tungsten.CreateTag(),
                    SimHashes.Wolframite.CreateTag()
                }
            };
            var tag = SimHashes.Sand.CreateTag();
            var format = "Mixes {0} and {1} into {2}";
            foreach (var keyValuePair in dictionary)
            {
                var ingredients = new[]
                {
                    new ComplexRecipe.RecipeElement(keyValuePair.Key, 50f),
                    new ComplexRecipe.RecipeElement(tag, 50f)
                };
                var results = new[]
                {
                    new ComplexRecipe.RecipeElement(keyValuePair.Value, 100f)
                };
                recipesIDs.Add(new ComplexRecipe(
                    ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                    ingredients, results)
                {
                    time = 50f,
                    description = string.Format(format, keyValuePair.Key, tag, keyValuePair.Value),
                    fabricators = new List<Tag>
                    {
                        TagManager.Create(RockCrusherConfig.ID)
                    },
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
                }.id);
            }
        }

        public void organicToFert()
        {
            var stringList = new List<string>
            {
                MushBarConfig.ID,
                BasicForagePlantConfig.ID,
                RotPileConfig.ID
            };
            var floatList = new List<float>
            {
                0.5f,
                0.5f,
                500f
            };
            var tag1 = SimHashes.ToxicSand.CreateTag();
            var tag2 = SimHashes.Fertilizer.CreateTag();
            var index = 0;
            var format = "Mixes {0} with {1} to create {2}";
            foreach (var str in stringList)
            {
                var ingredients = new[]
                {
                    new ComplexRecipe.RecipeElement((Tag) str, floatList[index]),
                    new ComplexRecipe.RecipeElement(tag1, 1000f)
                };
                var results = new[]
                {
                    new ComplexRecipe.RecipeElement(tag2, 1500f)
                };
                recipesIDs.Add(new ComplexRecipe(
                    ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                    ingredients, results)
                {
                    time = 120f,
                    description = string.Format(format, str, tag1, tag2),
                    fabricators = new List<Tag>
                    {
                        TagManager.Create(RockCrusherConfig.ID)
                    },
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
                }.id);
                ++index;
            }
        }

        public void saltCrushing()
        {
            var tag1 = SimHashes.Salt.CreateTag();
            var tag2 = TableSaltConfig.ID.ToTag();
            var tag3 = SimHashes.Sand.CreateTag();
            var num = 5E-05f;
            var format = "Crushes {0} into {1} and some {2}";
            var ingredients = new[]
            {
                new ComplexRecipe.RecipeElement(tag1, 100f)
            };
            var results = new[]
            {
                new ComplexRecipe.RecipeElement(tag2, 100f * num),
                new ComplexRecipe.RecipeElement(tag3, (float) (100.0 * (1.0 - num)))
            };
            recipesIDs.Add(new ComplexRecipe(
                ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                ingredients, results)
            {
                time = 120f,
                description = string.Format(format, tag1, tag2, tag3),
                fabricators = new List<Tag>
                {
                    TagManager.Create(RockCrusherConfig.ID)
                },
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
            }.id);
        }

        public void seedsToCoal()
        {
            var dictionary = new Dictionary<string, string>
            {
                {
                    ForestTreeConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.WOOD_TREE.NAME
                },
                {
                    SwampLilyConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.SWAMPLILY.NAME
                },
                {
                    PrickleGrassConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.PRICKLEGRASS.NAME
                },
                {
                    PrickleFlowerConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.PRICKLEFLOWER.NAME
                },
                {
                    BulbPlantConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.BULBPLANT.NAME
                },
                {
                    SaltPlantConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.SALTPLANT.NAME
                },
                {
                    MushroomPlantConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.MUSHROOMPLANT.NAME
                },
                {
                    GasGrassConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.GASGRASS.NAME
                },
                {
                    CactusPlantConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.CACTUSPLANT.NAME
                },
                {
                    BasicSingleHarvestPlantConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.BASICSINGLEHARVESTPLANT.NAME
                },
                {
                    LeafyPlantConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.LEAFYPLANT.NAME
                },
                {
                    BeanPlantConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.BEAN_PLANT.NAME
                },
                {
                    SpiceVineConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.SPICE_VINE.NAME
                },
                {
                    ColdWheatConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.COLDWHEAT.NAME
                },
                {
                    EvilFlowerConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.EVILFLOWER.NAME
                },
                {
                    BasicFabricMaterialPlantConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.BASICFABRICMATERIALPLANT.NAME
                },
                {
                    SeaLettuceConfig.ID + "Seed",
                    CREATURES.SPECIES.SEEDS.SEALETTUCE.NAME
                },
                {
                    ColdBreatherConfig.SEED_ID,
                    CREATURES.SPECIES.SEEDS.COLDBREATHER.NAME
                }
            };
            var tag = SimHashes.Carbon.CreateTag();
            var format = "Burns {0} to convert them into {1}";
            foreach (var keyValuePair in dictionary)
            {
                var ingredients = new[]
                {
                    new ComplexRecipe.RecipeElement((Tag) keyValuePair.Key, 1f),
                    new ComplexRecipe.RecipeElement(tag, 50f)
                };
                var results = new[]
                {
                    new ComplexRecipe.RecipeElement(tag, 800f)
                };
                recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(KilnConfig.ID, ingredients, results),
                    ingredients, results)
                {
                    time = 35f,
                    description = string.Format(format, keyValuePair.Value,
                        ElementLoader.FindElementByHash(SimHashes.Carbon).name),
                    fabricators = new List<Tag>
                    {
                        TagManager.Create(KilnConfig.ID)
                    },
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
                }.id);
            }
        }

        public void snowCone()
        {
            var tag1 = SimHashes.Ice.CreateTag();
            var tag2 = SimHashes.Snow.CreateTag();
            var ingredients = new[]
            {
                new ComplexRecipe.RecipeElement(tag1, 100f)
            };
            var results = new[]
            {
                new ComplexRecipe.RecipeElement(tag2, 100f)
            };
            recipesIDs.Add(new ComplexRecipe(
                ComplexRecipeManager.MakeRecipeID(RockCrusherConfig.ID, ingredients, results),
                ingredients, results)
            {
                time = 10f,
                description = string.Format(BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, tag1.ProperName(),
                    tag2.ProperName()),
                fabricators = new List<Tag>
                {
                    TagManager.Create(RockCrusherConfig.ID)
                },
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
            }.id);
        }
    }
}
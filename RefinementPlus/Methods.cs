// Decompiled with JetBrains decompiler
// Type: RefinementPlus.Methods
// Assembly: RefinementPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B692DF77-975B-476A-9AF9-C249F16125AB
// Assembly location: /Users/undancer/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Steam/1803983415/RefinementPlus.dll

using STRINGS;
using System.Collections.Generic;

namespace RefinementPlus
{
  internal class Methods : RefinementPlusData
  {
    public void burnCloth()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          "Cool_Vest",
          (string) EQUIPMENT.PREFABS.COOL_VEST.NAME
        },
        {
          "Funky_Vest",
          (string) EQUIPMENT.PREFABS.FUNKY_VEST.NAME
        },
        {
          "Warm_Vest",
          (string) EQUIPMENT.PREFABS.WARM_VEST.NAME
        }
      };
      Tag tag = SimHashes.Carbon.CreateTag();
      string format = "Burn old {0}";
      foreach (KeyValuePair<string, string> keyValuePair in dictionary)
      {
        ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement((Tag) keyValuePair.Key, 1f)
        };
        ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement(tag, 3f)
        };
        RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Kiln", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
        {
          time = 55f,
          description = string.Format(format, (object) keyValuePair.Value),
          fabricators = new List<Tag>()
          {
            TagManager.Create("Kiln")
          },
          nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
        }.id);
      }
    }

    public void burnRot()
    {
      Tag tag = SimHashes.Carbon.CreateTag();
      string str = "Burn rotten food";
      ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement((Tag) "RotPile", 100f)
      };
      ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement(tag, 50f)
      };
      RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Kiln", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
      {
        time = 35f,
        description = str,
        fabricators = new List<Tag>()
        {
          TagManager.Create("Kiln")
        },
        nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
      }.id);
    }

    public void clayToSlime()
    {
      Tag tag1 = SimHashes.Clay.CreateTag();
      Tag tag2 = SimHashes.ToxicSand.CreateTag();
      Tag tag3 = SimHashes.SlimeMold.CreateTag();
      string format = "Mixes {0} with {1} to create {2}";
      ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
      {
        new ComplexRecipe.RecipeElement(tag1, 200f),
        new ComplexRecipe.RecipeElement(tag2, 200f)
      };
      ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement(tag3, 400f)
      };
      RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
      {
        time = 100f,
        description = string.Format(format, (object) tag1, (object) tag2, (object) tag3),
        fabricators = new List<Tag>()
        {
          TagManager.Create("RockCrusher")
        },
        nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
      }.id);
    }

    public void limeCrushing()
    {
      Dictionary<string[], float[]> dictionary = new Dictionary<string[], float[]>()
      {
        {
          new string[2]{ "EggShell", (string) MISC.TAGS.EGGSHELL },
          new float[2]{ 5f, 5f }
        },
        {
          new string[2]
          {
            "BabyCrabShell",
            (string) ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME
          },
          new float[2]{ 1f, 5f }
        },
        {
          new string[2]
          {
            "CrabShell",
            (string) ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME
          },
          new float[2]{ 1f, 10f }
        }
      };
      Tag tag = SimHashes.Lime.CreateTag();
      string format = "Crushes {0} into {1}";
      foreach (KeyValuePair<string[], float[]> keyValuePair in dictionary)
      {
        ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement((Tag) keyValuePair.Key[0], keyValuePair.Value[0])
        };
        ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement(tag, keyValuePair.Value[1])
        };
        RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
        {
          time = 40f,
          description = string.Format(format, (object) keyValuePair.Key[1], (object) tag),
          fabricators = new List<Tag>()
          {
            TagManager.Create("RockCrusher")
          },
          nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
        }.id);
      }
    }

    public void fossilCrushing()
    {
      Tag tag1 = SimHashes.Fossil.CreateTag();
      Tag tag2 = SimHashes.Lime.CreateTag();
      Tag tag3 = SimHashes.Sand.CreateTag();
      string format = "Crushes {0} into {1} and {2}";
      ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement(tag1, 100f)
      };
      ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[2]
      {
        new ComplexRecipe.RecipeElement(tag2, 5f),
        new ComplexRecipe.RecipeElement(tag3, 95f)
      };
      RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
      {
        time = 40f,
        description = string.Format(format, (object) tag1, (object) tag2, (object) tag3),
        fabricators = new List<Tag>()
        {
          TagManager.Create("RockCrusher")
        },
        nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
      }.id);
    }

    public void maficToRegolith()
    {
      Tag tag1 = SimHashes.MaficRock.CreateTag();
      Tag tag2 = SimHashes.Regolith.CreateTag();
      string format = "Crushes {0} into {1}";
      ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement(tag1, 100f)
      };
      ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement(tag2, 80f)
      };
      RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
      {
        time = 80f,
        description = string.Format(format, (object) tag1, (object) tag2),
        fabricators = new List<Tag>()
        {
          TagManager.Create("RockCrusher")
        },
        nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
      }.id);
    }

    public void mineralsCrushing()
    {
      Dictionary<Tag, Tag> dictionary = new Dictionary<Tag, Tag>()
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
      Tag tag = SimHashes.Sand.CreateTag();
      string format = "Crushes {0} into {1} and a bit of {2}";
      foreach (KeyValuePair<Tag, Tag> keyValuePair in dictionary)
      {
        ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement(keyValuePair.Key, 100f)
        };
        ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[2]
        {
          new ComplexRecipe.RecipeElement(tag, 90f),
          new ComplexRecipe.RecipeElement(keyValuePair.Value, 10f)
        };
        RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
        {
          time = 50f,
          description = string.Format(format, (object) keyValuePair.Key, (object) tag, (object) keyValuePair.Value),
          fabricators = new List<Tag>()
          {
            TagManager.Create("RockCrusher")
          },
          nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
        }.id);
      }
    }

    public void oresToRefined()
    {
      Dictionary<Tag, Tag> dictionary = new Dictionary<Tag, Tag>()
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
      Tag tag = SimHashes.Sand.CreateTag();
      string format = "Crushes {0} into {1} and {2}";
      foreach (KeyValuePair<Tag, Tag> keyValuePair in dictionary)
      {
        ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement(keyValuePair.Key, 100f)
        };
        ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[2]
        {
          new ComplexRecipe.RecipeElement(tag, 50f),
          new ComplexRecipe.RecipeElement(keyValuePair.Value, 50f)
        };
        RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
        {
          time = 50f,
          description = string.Format(format, (object) keyValuePair.Key, (object) tag, (object) keyValuePair.Value),
          fabricators = new List<Tag>()
          {
            TagManager.Create("RockCrusher")
          },
          nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
        }.id);
      }
    }

    public void refinedToOres()
    {
      Dictionary<Tag, Tag> dictionary = new Dictionary<Tag, Tag>()
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
      Tag tag = SimHashes.Sand.CreateTag();
      string format = "Mixes {0} and {1} into {2}";
      foreach (KeyValuePair<Tag, Tag> keyValuePair in dictionary)
      {
        ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
        {
          new ComplexRecipe.RecipeElement(keyValuePair.Key, 50f),
          new ComplexRecipe.RecipeElement(tag, 50f)
        };
        ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement(keyValuePair.Value, 100f)
        };
        RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
        {
          time = 50f,
          description = string.Format(format, (object) keyValuePair.Key, (object) tag, (object) keyValuePair.Value),
          fabricators = new List<Tag>()
          {
            TagManager.Create("RockCrusher")
          },
          nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
        }.id);
      }
    }

    public void organicToFert()
    {
      List<string> stringList = new List<string>()
      {
        "MushBar",
        "BasicForagePlant",
        "RotPile"
      };
      List<float> floatList = new List<float>()
      {
        0.5f,
        0.5f,
        500f
      };
      Tag tag1 = SimHashes.ToxicSand.CreateTag();
      Tag tag2 = SimHashes.Fertilizer.CreateTag();
      int index = 0;
      string format = "Mixes {0} with {1} to create {2}";
      foreach (string str in stringList)
      {
        ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
        {
          new ComplexRecipe.RecipeElement((Tag) str, floatList[index]),
          new ComplexRecipe.RecipeElement(tag1, 1000f)
        };
        ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement(tag2, 1500f)
        };
        RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
        {
          time = 120f,
          description = string.Format(format, (object) str, (object) tag1, (object) tag2),
          fabricators = new List<Tag>()
          {
            TagManager.Create("RockCrusher")
          },
          nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
        }.id);
        ++index;
      }
    }

    public void saltCrushing()
    {
      Tag tag1 = SimHashes.Salt.CreateTag();
      Tag tag2 = TableSaltConfig.ID.ToTag();
      Tag tag3 = SimHashes.Sand.CreateTag();
      float num = 5E-05f;
      string format = "Crushes {0} into {1} and some {2}";
      ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement(tag1, 100f)
      };
      ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[2]
      {
        new ComplexRecipe.RecipeElement(tag2, 100f * num),
        new ComplexRecipe.RecipeElement(tag3, (float) (100.0 * (1.0 - (double) num)))
      };
      RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
      {
        time = 120f,
        description = string.Format(format, (object) tag1, (object) tag2, (object) tag3),
        fabricators = new List<Tag>()
        {
          TagManager.Create("RockCrusher")
        },
        nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
      }.id);
    }

    public void seedsToCoal()
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          "ForestTreeSeed",
          (string) CREATURES.SPECIES.SEEDS.WOOD_TREE.NAME
        },
        {
          "SwampLilySeed",
          (string) CREATURES.SPECIES.SEEDS.SWAMPLILY.NAME
        },
        {
          "PrickleGrassSeed",
          (string) CREATURES.SPECIES.SEEDS.PRICKLEGRASS.NAME
        },
        {
          "PrickleFlowerSeed",
          (string) CREATURES.SPECIES.SEEDS.PRICKLEFLOWER.NAME
        },
        {
          "BulbPlantSeed",
          (string) CREATURES.SPECIES.SEEDS.BULBPLANT.NAME
        },
        {
          "SaltPlantSeed",
          (string) CREATURES.SPECIES.SEEDS.SALTPLANT.NAME
        },
        {
          "MushroomSeed",
          (string) CREATURES.SPECIES.SEEDS.MUSHROOMPLANT.NAME
        },
        {
          "GasGrassSeed",
          (string) CREATURES.SPECIES.SEEDS.GASGRASS.NAME
        },
        {
          "CactusPlantSeed",
          (string) CREATURES.SPECIES.SEEDS.CACTUSPLANT.NAME
        },
        {
          "BasicSingleHarvestPlantSeed",
          (string) CREATURES.SPECIES.SEEDS.BASICSINGLEHARVESTPLANT.NAME
        },
        {
          "LeafyPlantSeed",
          (string) CREATURES.SPECIES.SEEDS.LEAFYPLANT.NAME
        },
        {
          "BeanPlantSeed",
          (string) CREATURES.SPECIES.SEEDS.BEAN_PLANT.NAME
        },
        {
          "SpiceVineSeed",
          (string) CREATURES.SPECIES.SEEDS.SPICE_VINE.NAME
        },
        {
          "ColdWheatSeed",
          (string) CREATURES.SPECIES.SEEDS.COLDWHEAT.NAME
        },
        {
          "EvilFlowerSeed",
          (string) CREATURES.SPECIES.SEEDS.EVILFLOWER.NAME
        },
        {
          "BasicFabricMaterialPlantSeed",
          (string) CREATURES.SPECIES.SEEDS.BASICFABRICMATERIALPLANT.NAME
        },
        {
          "SeaLettuceSeed",
          (string) CREATURES.SPECIES.SEEDS.SEALETTUCE.NAME
        },
        {
          "ColdBreatherSeed",
          (string) CREATURES.SPECIES.SEEDS.COLDBREATHER.NAME
        }
      };
      Tag tag = SimHashes.Carbon.CreateTag();
      string format = "Burns {0} to convert them into {1}";
      foreach (KeyValuePair<string, string> keyValuePair in dictionary)
      {
        ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[2]
        {
          new ComplexRecipe.RecipeElement((Tag) keyValuePair.Key, 1f),
          new ComplexRecipe.RecipeElement(tag, 50f)
        };
        ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
        {
          new ComplexRecipe.RecipeElement(tag, 800f)
        };
        RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Kiln", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
        {
          time = 35f,
          description = string.Format(format, (object) keyValuePair.Value, (object) ElementLoader.FindElementByHash(SimHashes.Carbon).name),
          fabricators = new List<Tag>()
          {
            TagManager.Create("Kiln")
          },
          nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
        }.id);
      }
    }

    public void snowCone()
    {
      Tag tag1 = SimHashes.Ice.CreateTag();
      Tag tag2 = SimHashes.Snow.CreateTag();
      ComplexRecipe.RecipeElement[] ingredients = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement(tag1, 100f)
      };
      ComplexRecipe.RecipeElement[] results = new ComplexRecipe.RecipeElement[1]
      {
        new ComplexRecipe.RecipeElement(tag2, 100f)
      };
      RefinementPlusData.recipesIDs.Add(new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", (IList<ComplexRecipe.RecipeElement>) ingredients, (IList<ComplexRecipe.RecipeElement>) results), ingredients, results)
      {
        time = 10f,
        description = string.Format((string) BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, (object) tag1.ProperName(), (object) tag2.ProperName()),
        fabricators = new List<Tag>()
        {
          TagManager.Create("RockCrusher")
        },
        nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult
      }.id);
    }
  }
}

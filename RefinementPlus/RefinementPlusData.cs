// Decompiled with JetBrains decompiler
// Type: RefinementPlus.RefinementPlusData
// Assembly: RefinementPlus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B692DF77-975B-476A-9AF9-C249F16125AB
// Assembly location: /Users/undancer/Library/Application Support/unity.Klei.Oxygen Not Included/mods/Steam/1803983415/RefinementPlus.dll

using System.Collections.Generic;

namespace RefinementPlus
{
    public class RefinementPlusData
    {
        public static List<string> recipesIDs = new List<string>();

        public static List<string> getRecipesIDs()
        {
            return recipesIDs;
        }

        protected static void AddRecipe(
            string fabricator,
            ComplexRecipe.RecipeElement[] inputs,
            ComplexRecipe.RecipeElement[] outputs,
            float time, string description, ComplexRecipe.RecipeNameDisplay nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult)
        {
            recipesIDs.Add(new ComplexRecipe(
                    ComplexRecipeManager.MakeRecipeID(fabricator, inputs, outputs), inputs, outputs
                )
                {
                    time = time,
                    description = description,
                    fabricators = new List<Tag> {TagManager.Create(fabricator)},
                    nameDisplay = nameDisplay
                }.id
            );
        }
    }
}
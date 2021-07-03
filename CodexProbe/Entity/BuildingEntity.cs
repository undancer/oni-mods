using System.Linq;

namespace CodexProbe.Entity
{
    public class BuildingEntity
    {
        public string Id { get; set; }
        
        public string Category { get; set; }

        public string Tag { get; set; }

        public string[] Tags { get; set; }
        
        
        public int WidthInCells { get; set; }
        public int HeightInCells { get; set; }

        public bool Floodable { get; set; }
        public bool Disinfectable { get; set; }
        public bool Entombable { get; set; }
        public bool Replaceable { get; set; }
        public bool Overheatable { get; set; }
        public bool Repairable { get; set; }
        public bool Breakable { get; set; }
        public bool Upgradeable { get; set; }

        public BuildingEntity(BuildingDef def ,string category)
        {
            Id = def.PrefabID;
            Category = category;
            Tag = def.Tag.Name;
            if (def.ReplacementTags != null)
            {
                Tags = def.ReplacementTags.Select(tag => tag.Name).ToArray();
            }

            WidthInCells = def.WidthInCells;
            HeightInCells = def.HeightInCells;
            
            Floodable = def.Floodable;
            Disinfectable = def.Disinfectable;
            Entombable = def.Entombable;
            Repairable = def.Repairable;
            Replaceable = def.Replaceable;
            Overheatable = def.Overheatable;
            Breakable = def.Breakable;
            Upgradeable = def.Upgradeable;
            
        }
    }
}
using System.Linq;
using JetBrains.Annotations;
using static CodexProbe.Jobs.GenerateElementEntriesJob;

namespace CodexProbe.Entity
{
    public class ElementEntity
    {
        //STRINGS.ELEMENTS.ALUMINUM.NAME
        //STRINGS.ELEMENTS.ALUMINUM.DESC

        public ElementEntity()
        {
        }

        private ElementEntity(Element element)
        {
            Id = element.id.ToString();
//            tag = element.tag.Name;
            Tags = element.oreTags.Select(tag => tag.Name).ToArray();
            if (element.highTempTransitionTarget != 0)
            {
                highTemp = element.highTemp;
                highTempTransitionTarget = element.highTempTransitionTarget.ToString();
            }

            if (element.lowTempTransitionTarget != 0)
            {
                lowTemp = element.lowTemp;
                lowTempTransitionTarget = element.lowTempTransitionTarget.ToString();
            }

            State = GetStateString(element.state);
        }

        public string Id { get; set; }

//        public string tag { get; set; }

        public string[] Tags { get; set; }

        public string State { get; set; }

        [CanBeNull] public float? highTemp { get; set; }

        [CanBeNull] public string highTempTransitionTarget { get; set; }

        [CanBeNull] public float? lowTemp { get; set; }

        [CanBeNull] public string lowTempTransitionTarget { get; set; }

        public static implicit operator ElementEntity(Element element)
        {
            return new ElementEntity(element);
        }
    }
}
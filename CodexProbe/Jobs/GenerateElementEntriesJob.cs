using System.Collections.Generic;
using System.IO;
using System.Text;
using CodexProbe.Entity;
using Newtonsoft.Json;
using UnityEngine;

namespace CodexProbe.Jobs
{
    public class GenerateElementEntriesJob : BaseJob
    {
        public static string GetStateString(Element.State state)
        {
            switch (state & Element.State.Solid)
            {
                case Element.State.Solid:
                    return Element.State.Solid.ToString();
                case Element.State.Liquid:
                    return Element.State.Liquid.ToString();
                case Element.State.Gas:
                    return Element.State.Gas.ToString();
                default:
                    return Element.State.Vacuum.ToString();
            }
        }

        public static void GenerateElementEntries()
        {
            var elements = new List<ElementEntity>();

            foreach (var element in ElementLoader.elements)
            {
                if (element.disabled) continue;

                var name = element.id.ToString();
                var state = GetStateString(element.state);

                elements.Add(element);


                Tuple<Sprite, Color> tuple;

                switch (element.id)
                {
                    case SimHashes.Void:
                        tuple = new Tuple<Sprite, Color>(Assets.GetSprite("ui_elements-void"), Color.white);
                        break;
                    case SimHashes.Vacuum:
                        tuple = new Tuple<Sprite, Color>(Assets.GetSprite("ui_elements-vacuum"), Color.white);
                        break;
                    default:
                        tuple = Def.GetUISprite(element);
                        break;
                }

                var sprite = tuple.first;
                var color = tuple.second;

                SaveImage(new[] {"ASSETS/ELEMENTS", state.ToUpper(), name.ToUpper()}, sprite, color);
            }

            WriteJson(new[] {"json", "elements"}, elements);
        }
    }
}
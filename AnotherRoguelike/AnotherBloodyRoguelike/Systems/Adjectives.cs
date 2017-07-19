using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Systems
{
    public class Adjectives
    {
        public static string AdjGen()
        {
            List<string> adjList;
            adjList = new List<string>() {"Active", "Adorable", "Angry", "Aware",
            "Blushing", "Busy", "Bubbly", "Bouncy", "Bony",
            "Calculating", "Coarse", "Creamy", "Curvy", "Curly",
            "Damp", "Delicious", "Dizzy", "Doting", "Dooting", "Dear",
            "Edible", "Entire", "Excitable",
            "Faraway", "Feisty", "Forked", "Fuzzy",
            "Healthy", "Handsome", "Hefty", "Hungry",
            "Impish", "Itchy", "Infatuated",
            "Jaunty", "Jagged", "Jumpy",
            "Keen", "Knotty", "Kind",
            "Lanky", "Leafy", "Lumpy", "Liquid",
            "Milky", "Meaty", "Metallic", "Moist", "Mushroomy",
            "Naughty", "Nifty", "Nutritious",
            "Oily", "Organic", "Other", "Overcooked",
            "Peppery", "Polite", "Prickly", "Plump",
            "Questionable", "Quaint", "Quixotic",
            "Raw", "Rogueish", "Rigid", "Rosy",
            "Sandy", "Slushy", "Soupy", "Sparkling", "Svelte",
            "Thrifty", "Tart", "Tasty",
            "Utter", "Usable", "Upbeat",
            "Valorious", "Vital", "Vivacious","Viscous",
            "Wavy", "Wry", "Woozy", "Wee",
            "Yawning", "Yummy",
            "Zesty"};
            Random gen = new Random();
            return (adjList.ElementAt(gen.Next(0, adjList.Count)));
        }
    }
}

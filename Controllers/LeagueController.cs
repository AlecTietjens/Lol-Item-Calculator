using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace lol_item_calculator.Controllers
{
    [Route("api/[controller]")]
    public class LeagueCalculatorController : Controller
    {
        private static string apiKey = ""; // insert API key here
        private static Uri baseUri = new Uri("https://na1.api.riotgames.com");
        private static Uri itemsUri = new Uri(baseUri, "lol/static-data/v3/items?api_key=" + apiKey + "&tags=all"); // request all tags
        private static Uri championsUri = new Uri(baseUri, "/lol/static-data/v3/champions?api_key=" + apiKey + "&tags=all");

        private static HttpClient Client = new HttpClient();

        // currently this method pulls the data from a json file made by the UpdateItems method
        [HttpGet("[action]")]
        public async Task<ItemList> Items() {
            string json = await System.IO.File.ReadAllTextAsync("./items.json");

            ItemList items = JsonConvert.DeserializeObject<ItemList>(json);

            return items;
        }

        [HttpGet("[action]")]
        public async Task<ChampionList> ChampionAsync() {
            string json = await System.IO.File.ReadAllTextAsync("./champions.json");

            ChampionList champions = JsonConvert.DeserializeObject<ChampionList>(json);

            return champions;
        }

        // right now this method just gets the items from the league API and puts them into a json file
        [HttpGet("[action]")]
        public async void UpdateItems() {
            HttpResponseMessage response = await Client.GetAsync(itemsUri);

            if (response.StatusCode.Equals(HttpStatusCode.OK)) {
                HttpContent responseContent = response.Content;

                string content = await responseContent.ReadAsStringAsync();

                await System.IO.File.WriteAllTextAsync("./items.json", content);
            }
        }


        // right now this method just gets the champions from the league API and puts them into a json file
        [HttpGet("[action]")]
        public async void UpdateChampions() {
            HttpResponseMessage response = await Client.GetAsync(championsUri);

            if (response.StatusCode.Equals(HttpStatusCode.OK)) {
                HttpContent responseContent = response.Content;

                string content = await responseContent.ReadAsStringAsync();

                await System.IO.File.WriteAllTextAsync("./champions.json", content);
            }
        }

        // item models
        public class ItemList {
            public IDictionary<string, Item> Data { get; set; }
            public string Version { get; set; }
            public IEnumerable<ItemTree> Tree { get; set; }
            public IEnumerable<Group> Groups { get; set; }
            public string Types { get; set; }
        }

        public class ItemTree {
            public string Header { get; set; }
            public IEnumerable<string> Tags { get; set; }
        }

        public class Item {
            public Gold Gold { get; set; }
            public string Plaintext { get; set; }
            public bool HideFromAll { get; set; }
            public bool InStore { get; set; }
            public IEnumerable<string> Into { get; set; }
            public int ID { get; set; }
            public InventoryDataStats Stats { get; set; }
            public string Colloq { get; set; }
            public IDictionary<string, bool> Maps { get; set; }
            public int SpecialRecipe { get; set; }
            public Image Image { get; set; }
            public string Description { get; set; }
            public IEnumerable<string> Tags { get; set; }
            public IDictionary<string, string> Effect { get; set; }
            public string RequiredChampion { get; set; }
            public IEnumerable<string> From { get; set; }
            public string Group { get; set; }
            public bool ConsumedOnFull { get; set; }
            public string SanitizedDescription { get; set; }
            public int Depth { get; set; }
            public int Stacks { get; set; }
        }

        public class Gold {
            public int Sell { get; set; }
            public int Total { get; set; }
            public int Base { get; set; }
            public bool Purchasable { get; set; }
        }

        public class InventoryDataStats {
            public double PercentCritDamageMod { get; set; }
            public double PercentSpellBlockMod { get; set; }
            public double PercentMovementSpeedMod { get; set; }
            public double FlatSpellBlockMod { get; set; }
            public double FlatCritDamageMod { get; set; }
            public double FlatEnergyPoolMod { get; set; }
            public double PercentLifeStealMod { get; set; }
            public double FlatMPPoolMod { get; set; }
            public double FlatMovementSpeedMod { get; set; }
            public double PercentAttackSpeedMod { get; set; }
            public double FlatBlockMod { get; set; }
            public double PercentBlockMod { get; set; }
            public double FlatEnergyRegenMod { get; set; }
            public double PercentSpellVampMod { get; set; }
            public double FlatMPRegenMod { get; set; }
            public double PercentDodgeMod { get; set; }
            public double FlatAttackSpeedMod { get; set; }
            public double FlatArmorMod { get; set; }
            public double FlatHPRegenMod { get; set; }
            public double PercentMagicDamageMod { get; set; }
            public double PercentMPPoolMod { get; set; }
            public double FlatMagicDamageMod { get; set; }
            public double PercentMPRegenMod { get; set; }
            public double PercentPhysicalDamageMod { get; set; }
            public double FlatPhysicalDamageMod { get; set; }
            public double PercentHPPoolMod { get; set; }
            public double PercentArmorMod { get; set; }
            public double PercentCritChanceMod { get; set; }
            public double PercentEXPBonus { get; set; }
            public double FlatHPPoolMod { get; set; }
            public double FlatCritChanceMod { get; set; }
            public double FlatEXPBonus { get; set; }
        }

        public class Group {
            public string MaxGroupOwnable { get; set; }
            public string Key { get; set; }
        }

        // champion models
        public class ChampionList {
            public IDictionary<string, string> Keys { get; set; }
            public IDictionary<string, Champion> Data { get; set; }
            public string Version { get; set; }
            public string Type { get; set; }
            public string Format { get; set; }
        }

        public class Champion {
            public Info Info { get; set; }
            public IEnumerable<string> EnemyTips { get; set; }
            public Stats Stats { get; set; }
            public string Name { get; set; }
            public string Title { get; set; }
            public Image Image { get; set; }
            public IEnumerable<string> Tags { get; set; }
            public string ParType { get; set; }
            public IEnumerable<Skin> Skins { get; set; }
            public Passive Passive { get; set; }
            public IEnumerable<Recommended> Recommended { get; set; }
            public IEnumerable<string> AllyTips { get; set; }
            public string Key { get; set; }
            public string Lore { get; set; }
            public int ID { get; set; }
            public string Blurb { get; set; }
            public IEnumerable<ChampionSpell> Spells { get; set; }
        }

        public class Info {
            public int Difficulty { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
            public int Magic { get; set; }
        }

        public class Stats {
            public double ArmorPerLevel { get; set; }
            public double HyperLevel { get; set; }
            public double AttackDamage { get; set; }
            public double MPPerLevel { get; set; }
            public double AttackSpeedOffset { get; set; }
            public double Armor { get; set; }
            public double HP { get; set; }
            public double HPRegenPerLevel { get; set; }
            public double SpellBlock { get; set; }
            public double AttackRange { get; set; }
            public double MoveSpeed { get; set; }
            public double AttackDamagePerLevel { get; set; }
            public double MPRegenPerLevel { get; set; }
            public double MP { get; set; }
            public double SpellBlockPerLevel { get; set; }
            public double Crit { get; set; }
            public double MPRegen { get; set; }
            public double AttackSpeedPerLevel { get; set; }
            public double HPRegen { get; set; }
            public double CritPerLevel { get; set; }
        }

        public class Skin { 
            public int Num { get; set; }
            public string Name { get; set; }
            public int ID { get; set; }
        }

        public class Passive {
            public Image Image { get; set; }
            public string SanitizedDescription { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public class Recommended {
            public string Map { get; set; }
            public IEnumerable<Block> Blocks { get; set; }
            public string Champion { get; set; }
            public string Title { get; set; }
            public bool Priority { get; set; }
            public string Mode { get; set; }
            public string Type { get; set; }
        }

        public class Block {
            public IEnumerable<BlockItem> Items { get; set; }
            public bool RecMath { get; set; }
            public string Type { get; set; }
        }

        public class BlockItem {
            public int Count { get; set; }
            public int ID { get; set; }
        }

        public class ChampionSpell {
            public string CooldownBurn { get; set; }
            public string Resource { get; set; }
            public LevelTip LevelTip { get; set; }
            public IEnumerable<SpellVars> Vars { get; set; }
            public string CostType { get; set; }
            public Image Image { get; set; }
            public string SanitizedDescription { get; set; }
            public string SanitizedTooltip { get; set; }
            public IEnumerable<object> Effect { get; set; } // this field is a list of list of double
            public string Tooltip { get; set; }
            public int MaxRank { get; set; }
            public string CostBurn { get; set; }
            public string RangeBurn { get; set; }
            public object Range { get; set; } // this field is either a list of int or the string 'self' for spells that target one's own champion
            public IEnumerable<double> Cooldown { get; set; }
            public IEnumerable<int> Cost { get; set; }
            public string Key { get; set; }
            public string Description { get; set; }
            public IEnumerable<string> EffectBurn { get; set; }
            public IEnumerable<Image> AltImages { get; set; }
            public string Name { get; set; }
        }

        public class LevelTip {
            public IEnumerable<string> Effect { get; set; }
            public IEnumerable<string> Label { get; set; }
        }

        public class SpellVars {
            public string RanksWith { get; set; }
            public string Dyn { get; set; }
            public string Link { get; set; }
            public IEnumerable<double> CoEff { get; set; }
            public string Key { get; set; }
        }

        // shared models
        public class Image {
            public string Full { get; set; }
            public string Group { get; set; }
            public string Sprite { get; set; }
            public int H { get; set; }
            public int W { get; set; }
            public int Y { get; set; }
            public int X { get; set; }
        }
    }
}

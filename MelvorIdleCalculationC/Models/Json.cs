using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SelectList = System.Web.Mvc.SelectList;

namespace MelvorIdleCalculationC.Models
{
    public class Json
    {
    }

    public class MonsterResult
    {

        public int ID { get; set; }
        public string SelectedDungeon { get; set; }
        public Array DungeonOption { get; set; }
        public string SelectedMonster { get; set; }
        public Array MonsterOption { get; set; }
        public int MaxHit { get; set; }
        public string HitType { get; set; }
    }

    public class CalculationResult
    {
        public string AutoEat { get; set; }
        public string EquipType { get; set; }
        public int HitPoints { get; set; }
        public int DamageReduction { get; set; }
        public int MaxHit { get; set; }
        public string HitType { get; set; }

        public bool Result { get; set; }
        public double DrMultiplyer { get; set; }
        public int TotalDamageReduction { get; set; }
        public double MaxHitTolerance { get; set; }
        public double MonsterMaxDmg { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using SelectList = System.Web.Mvc.SelectList;

namespace MelvorIdleCalculationC.Models
{
    public class MonsterStatsList
    {
        public int? ID { get; set; }
        public string SelectedDungeon { get; set; }
        public SelectList DungeonOption { get; set; }
        public string SelectedMonster { get; set; }
        public SelectList MonsterOption { get; set; }
        public int MaxHit { get; set; }       
        public string HitType { get; set; }
        public int Sort { get; set; }

        public string AutoEat { get; set; }
        public SelectList AutoEatOption { get; set; }
        public int HitPoints { get; set; }
        public string EquipType { get; set; }
        public SelectList EquipTypeOption { get; set; }
        public int DamageReduction { get; set; }

        public bool Result { get; set; }
        public double DrMultiplyer { get; set; }
        public int TotalDamageReduction { get; set; }
        public float MaxHitTolerance { get; set; }
        public float MonsterMaxDmg { get; set; }
    }  
    
    public class EditListModel
    {
        public List<MonsterStatsList> MonstersList { get; set; }

       #region Filter
        public string Dungeon { get; set; }
        public string Monster { get; set; }
        #endregion

        public bool Scroll { get; set; }
        public byte SortBy { get; set; }

        public SelectList DungeonOptions { get; set; }
        public SelectList MonsterOptions { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MelvorIdleCalculationC.Models;

namespace MelvorIdleCalculationC.Controllers
{
    public class JsonController : Controller
    {
        public JsonResult GetMonster(int id, string dungeon, string monster, int maxHit, string hitType)
        {
            MonsterStatsList m = new MonsterStatsList();
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            SelectLists l = new SelectLists();
            MonsterResult ret = new MonsterResult();

            if (dungeon == "" && monster == "0")
            {
                m = IHandler.GetItemList().FirstOrDefault();
                dungeon = m.SelectedDungeon;
            }            
            if (dungeon != "" && monster == "")
            {
                m = IHandler.GetItemList().Find(f => f.ID == int.Parse(dungeon));
                dungeon = m.SelectedDungeon;
            }
            if ((dungeon == "0" || dungeon == "") && monster != "")
            {
                if (monster == "0")
                {
                    m = IHandler.GetItemList().Find(f => f.MaxHit == maxHit && f.HitType == hitType);
                    dungeon = m.SelectedDungeon;
                }
                else
                {
                    m = IHandler.GetItemList().Find(f => f.ID == int.Parse(monster));
                    monster = m.SelectedMonster;
                    dungeon = m.SelectedDungeon;
                }
            }
            if (dungeon != "" && (monster == "" || monster == "0"))
            {
                ret.ID = id;
                ret.SelectedDungeon = dungeon;
                ret.SelectedMonster = "";
                ret.MaxHit = 0;
                ret.HitType = "-";
            }
            else if (monster != "" && dungeon != "" && monster != "0")
            {
                m = IHandler.GetItemList(). Find(f => f.SelectedMonster == monster);
                ret.ID = m.ID.Value;
                ret.SelectedDungeon = m.SelectedDungeon;
                ret.SelectedMonster = m.SelectedMonster;
                ret.MaxHit = m.MaxHit;
                ret.HitType = m.HitType;
            }            
            else if (dungeon == "" && monster == "")
            {
                m = IHandler.GetItemList().FirstOrDefault();
                ret.ID = m.ID.Value;
                ret.SelectedDungeon = m.SelectedDungeon;
                ret.SelectedMonster = m.SelectedMonster;
                ret.MaxHit = m.MaxHit;
                ret.HitType = m.HitType;
            }
            else
            {

            }
            ret.DungeonOption = l.GetDungeonOptionRemoved(ret.SelectedDungeon).ToArray();
            ret.MonsterOption = l.GetMonsterOption(ret.SelectedDungeon, ret.SelectedMonster).ToArray();

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCalculation(string autoEat, string equipType, int hitPoints, int damageReduction, int maxHit, string hitType)
        {
            CalculationResult ret = new CalculationResult();
            switch (equipType)
            {
                case "0":
                    ret.EquipType = "Attack";
                    break;
                case "1":
                    ret.EquipType = "Ranged";
                    break;
                case "2":
                    ret.EquipType = "Magic";
                    break;
            }
            ret.HitPoints = hitPoints;
            ret.DamageReduction = damageReduction;
            ret.MaxHit = maxHit;
            ret.HitType = hitType;

            #region DamageReduction
            switch (ret.EquipType)
            {
                case "Attack":
                    if (hitType == "Attack")
                    {
                        ret.DrMultiplyer = 1;
                    }
                    else if (hitType == "Ranged")
                    {
                        ret.DrMultiplyer = 1.25;
                    }
                    else if (hitType == "Magic")
                    {
                        ret.DrMultiplyer = 0.85;
                    }
                    break;
                case "Ranged":
                    if (hitType == "Attack")
                    {
                        ret.DrMultiplyer = 0.85;
                    }
                    else if (hitType == "Ranged")
                    {
                        ret.DrMultiplyer = 1;
                    }
                    else if (hitType == "Magic")
                    {
                        ret.DrMultiplyer = 1.25;
                    }
                    break;
                case "Magic":
                    if (hitType == "Attack")
                    {
                        ret.DrMultiplyer = 1.25;
                    }
                    else if (hitType == "Ranged")
                    {
                        ret.DrMultiplyer = 0.85;
                    }
                    else if (hitType == "Magic")
                    {
                        ret.DrMultiplyer = 1;
                    }
                    break;
            }
            double totalDR = damageReduction * ret.DrMultiplyer;
            ret.TotalDamageReduction = (int)Math.Floor(totalDR);
            #endregion
            #region AutoEat
            double ae = 0;
            switch (autoEat)
            {
                case "0":
                    ae = 0;
                    break;
                case "1":
                    ae = 0.2;
                    break;
                case "2":
                    ae = 0.3;
                    break;
                case "3":
                    ae = 0.4;
                    break;
            }
            string aeString = ae.ToString();
            if (aeString != "0")
            {
                ret.AutoEat = aeString.Replace(',', '.');
            }
            #endregion

            double MaxHitTolerance = (int)(hitPoints * ae);
            double MonsterMaxDmg = maxHit * (1 - (ret.TotalDamageReduction * 0.01));
            if (MaxHitTolerance > MonsterMaxDmg)
            {
                ret.Result = true;
            }
            else if (MaxHitTolerance <= MonsterMaxDmg)
            {
                ret.Result = false;
            }

            ret.MaxHitTolerance = Math.Round(MaxHitTolerance, 2);
            ret.MonsterMaxDmg = Math.Round(MonsterMaxDmg, 2);

            return Json(ret, JsonRequestBehavior.AllowGet);
        }
    }
}
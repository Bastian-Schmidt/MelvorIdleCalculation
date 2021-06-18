using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MelvorIdleCalculationC.Models;

namespace MelvorIdleCalculationC.Controllers
{        
    public class HomeController : Controller
    {
        MelvorIdleCalculationDataContext DC = new MelvorIdleCalculationDataContext();
        public ActionResult Index(int? id)
        {
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            SelectLists l = new SelectLists();
            MonsterStatsList m = new MonsterStatsList();
          
            if (id != null)
            {
                m = IHandler.GetItemList().Find(f => f.ID == id);
                m.DungeonOption = new SelectList(l.GetDungeonOptionRemoved(m.SelectedDungeon), "ID", "Title");
                m.MonsterOption = new SelectList(l.GetMonsterOption(m.SelectedDungeon, m.SelectedMonster), "ID", "Title");
                m.AutoEatOption = new SelectList(l.GetAutoEatList(), "ID", "Title");
                m.EquipTypeOption = new SelectList(l.GetEquipTypeList(), "ID", "Title");
                return View(m);
            }
            m = IHandler.GetItemList().FirstOrDefault();
            m.DungeonOption = new SelectList(l.GetDungeonOptionRemoved(m.SelectedDungeon), "ID", "Title");
            m.MonsterOption = new SelectList(l.GetMonsterOption(m.SelectedDungeon, m.SelectedMonster), "ID", "Title");
            m.AutoEatOption = new SelectList(l.GetAutoEatList(), "ID", "Title");
            m.EquipTypeOption = new SelectList(l.GetEquipTypeList(), "ID", "Title");                        

            return View(m);
        }                  

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MonsterStatsList iList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MonsterStatsDBHandler MonsterStatsHandler = new MonsterStatsDBHandler();
                    MonsterStatsHandler.InsertItem(iList);
                }
                return RedirectToAction("Create");
            }
            catch { return View(); }
        }

        public ActionResult EditList(string dungeon, string monster, byte? sort)
        {
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            SelectLists l = new SelectLists();
            EditListModel m = new EditListModel();

            m.Dungeon = dungeon;
            m.Monster = monster;

            m.DungeonOptions = new SelectList(l.GetDungeonOption(true), "ID", "Title");
            m.MonsterOptions = new SelectList(l.GetAllMonsterOption(true), "ID", "Title");

            //m.MonstersList = IHandler.GetItemList().OrderBy(o => o.Sort).ThenBy(o2 => o2.SelectedMonster).ToList();
            m.MonstersList = DC.Monsters.OrderBy(o => o.Sort).ThenBy(o1 => o1.Monster1).Select(s => new MonsterStatsList).ToList();
            if (!string.IsNullOrEmpty(m.Dungeon) && m.Dungeon != "0")
            {                
                dungeon = l.GetDungeonName(int.Parse(dungeon));                                                             
                m.MonstersList = m.MonstersList.FindAll(f => f.SelectedDungeon == dungeon);
            }
            if (!string.IsNullOrEmpty(m.Monster) && m.Monster != "0")
            {               
                monster = l.GetMonsterName(int.Parse(monster));
                m.MonstersList = m.MonstersList.FindAll(f => f.SelectedMonster == monster);
            }  
            
            if (sort.HasValue)
            {
                switch (sort)
                {
                    case 0:
                        m.MonstersList = m.MonstersList.OrderBy(o => o.SelectedDungeon).ToList();
                        break;
                    case 1:
                        m.MonstersList = m.MonstersList.OrderBy(o => o.SelectedMonster).ToList();
                        break;
                    case 2:
                        m.MonstersList = m.MonstersList.OrderBy(o => o.MaxHit).ToList();
                        break;
                    case 3:
                        m.MonstersList = m.MonstersList.OrderBy(o => o.HitType).ToList();
                        break;
                }
            }
            
            if (m.MonstersList.Count() > 4)
            {
                m.Scroll = true;
            }                        

            return View(m);
        }

        [HttpPost]
        public ActionResult EditList(EditListModel m)
        {
            return RedirectToAction("EditList", new { dungeon = m.Dungeon, monster = m.Monster, sort = m.SortBy });
        }
        
        public ActionResult Edit(int? id)
        {
            MonsterStatsDBHandler MonsterStatsHandler = new MonsterStatsDBHandler();
            MonsterStatsList m = new MonsterStatsList();

            if (id != null)
            {
                m = MonsterStatsHandler.GetItemList().Find(itemmodel => itemmodel.ID == id);
            }
            else
            {
                m = MonsterStatsHandler.GetItemList().FirstOrDefault();
            }

            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(int id, MonsterStatsList iList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MonsterStatsDBHandler MonsterStatsHandler = new MonsterStatsDBHandler();
                    MonsterStatsHandler.UpdateItem(iList);
                }
                return RedirectToAction("Edit");
            }
            catch { return View(); }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                MonsterStatsDBHandler MonsterStatsHandler = new MonsterStatsDBHandler();
                if (MonsterStatsHandler.DeleteItem((int)id))
                {
                    ViewBag.AlertMsg = "Monster Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch { return View(); }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MelvorIdleCalculationC.Models;

namespace MelvorIdleCalculationC
{
    public class SelectLists
    {
        public List<SOption> GetDungeonOptionRemoved(string dungeon)
        {
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            List<MonsterStatsList> Mlist = IHandler.GetItemList().OrderBy(o => o.Sort).ThenBy(o2 => o2.SelectedMonster).ToList();
            List<SOption> list = new List<SOption>();            
            int i = 0;                                  
            foreach (var e in Mlist)
            {
                if (list.Where(w => w.Title == e.SelectedDungeon).Count() == 0 && e.SelectedDungeon != dungeon)
                {
                    list.Insert(i, new SOption(e.ID.ToString(), e.SelectedDungeon));
                    i++;
                }
            }
            return list;
        }

        public List<SOption> GetDungeonOption(bool emptyEntry)
        {
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            List<MonsterStatsList> Mlist = IHandler.GetItemList().OrderBy(o => o.Sort).ThenBy(o2 => o2.SelectedMonster).ToList(); ;
            List<SOption> list = new List<SOption>();
            int i = 0;
            if (emptyEntry)
            {
                i = 1;
                list.Insert(0, new SOption("0", "-"));
            }
            foreach (var e in Mlist)
            { 
                if (list.Where(w => w.Title == e.SelectedDungeon).Count() == 0)
                {
                    list.Insert(i, new SOption(e.ID.ToString(), e.SelectedDungeon));
                    i++;
                }                              
            }
            return list;
        }

        public List<SOption> GetMonsterOption(string selectedDungeon, string monster)
        {
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            List<MonsterStatsList> Mlist = IHandler.GetItemList().FindAll(f => f.SelectedDungeon == selectedDungeon).OrderBy(o => o.Sort).ThenBy(o2 => o2.SelectedMonster).ToList(); ;
            List<SOption> list = new List<SOption>();
            int i = 1;
            list.Insert(0, new SOption("0", "-"));
            foreach (var e in Mlist)
            {
                if (e.SelectedMonster != monster)
                {
                    list.Insert(i, new SOption(e.ID.ToString(), e.SelectedMonster));
                    i++;
                }
            }            
            return list;
        }

        public List<SOption> GetAllMonsterOption(bool emptyEntry)
        {
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            List<MonsterStatsList> Mlist = IHandler.GetItemList().OrderBy(o => o.Sort).ThenBy(o2 => o2.SelectedMonster).ToList(); ;                        
            List<SOption> list = new List<SOption>();
            int i = 0;
            if (emptyEntry)
            {
                i = 1;
                list.Insert(0, new SOption("0", "-"));
            }            
            foreach (var e in Mlist)
            {                
                    list.Insert(i, new SOption(e.ID.ToString(), e.SelectedMonster));
                    i++;            
            }
            return list;
        }

        public List<SOption> GetEmptyList()
        {
            List<SOption> list = new List<SOption>();
            list.Insert(0, new SOption("", "-"));
            return list;
        }

        public List<SOption> GetAutoEatList()
        {
            List<SOption> list = new List<SOption>();
            list.Insert(0, new SOption("0", "0"));
            list.Insert(1, new SOption("1", "1"));
            list.Insert(2, new SOption("2", "2"));
            list.Insert(3, new SOption("3", "3"));
            return list;
        }

        public List<SOption> GetEquipTypeList()
        {
            List<SOption> list = new List<SOption>();
            list.Insert(0, new SOption("0", "Attack"));
            list.Insert(1, new SOption("1", "Ranged"));
            list.Insert(2, new SOption("2", "Magic"));
            return list;
        }

        public string GetDungeonName(int id)
        {
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            string dungeon;

            dungeon = IHandler.GetItemList().Find(f => f.ID == id).SelectedDungeon;

            return dungeon;
        }

        public string GetMonsterName(int id)
        {
            MonsterStatsDBHandler IHandler = new MonsterStatsDBHandler();
            string monster;

            monster = IHandler.GetItemList().Find(f => f.ID == id).SelectedMonster;

            return monster;
        }
    }

    public class SOption
    {
        public SOption(string id, string title)
        {
            this.ID = id;
            this.Title = title;
        }
        public string ID { get;  set; }
        public string Title { get; set; }
    }
}
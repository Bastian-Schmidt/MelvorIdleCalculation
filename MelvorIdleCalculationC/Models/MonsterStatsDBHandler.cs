using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MelvorIdleCalculationC.Models
{
    public class MonsterStatsDBHandler
    {
        private SqlConnection con;
        private void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["MonsterStatsListConString"].ToString();
            con = new SqlConnection(constring);
        }

        // 1. ********** Insert Item **********
        public bool InsertItem(MonsterStatsList iList)
        {
            Connection();
            string query = "INSERT INTO MonsterStatsList VALUES('" + iList.SelectedDungeon + "','" + iList.SelectedMonster + "','" + iList.MaxHit + "','" + iList.HitType + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // 2. ********** Get All Item List **********
        public List<MonsterStatsList> GetItemList()
        {
            Connection();
            List<MonsterStatsList> iList = new List<MonsterStatsList>();

            string query = "SELECT * FROM MonsterStatsList";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new MonsterStatsList
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    SelectedDungeon = Convert.ToString(dr["Dungeon"]),
                    SelectedMonster = Convert.ToString(dr["Monster"]),
                    MaxHit = Convert.ToInt32(dr["MaxHit"]),
                    HitType = Convert.ToString(dr["HitType"]),
                    Sort = Convert.ToInt32(dr["Sort"])
                });
            }
            return iList;
        }

        // 3. ********** Update Item Details **********
        public bool UpdateItem(MonsterStatsList iList)
        {
            Connection();
            string query = "UPDATE MonsterStatsList SET Dungeon = '" + iList.SelectedDungeon + "', Monster = '" + iList.SelectedMonster + "', MaxHit = " + iList.MaxHit + ", HitType = '" + iList.HitType + "' WHERE ID = " + iList.ID;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        // 4. ********** Delete Item **********
        public bool DeleteItem(int id)
        {
            Connection();
            string query = "DELETE FROM MonsterStatsList WHERE ID = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}
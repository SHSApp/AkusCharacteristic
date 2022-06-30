using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace SHSApp_Char
{
    class Settings
    {
        private OleDbConnection Conn = null;
        //public static string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=settings.mdb;";
        public static string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=settings.mdb;";
        private DataTable SVO = null;
        private DataTable Personal = null;
        private DataTable Options = null;
        private DataTable Dosug = null;
        private DataTable AutoNO = null;
        private int kruzhkiCount = 0;

        public DataTable Kruzhki
        {
            get
            {
                return Dosug;
            }

            set
            {
                Dosug = value;
            }
        }

        public int KruzhkiCount
        {
            get
            {
                return kruzhkiCount;
            }

            set
            {
                kruzhkiCount = value;
            }
        }

        public Settings()
        {
            Conn = new OleDbConnection(ConnectionString);
        }

        public void LoadTables()
        {
            if (Conn != null)
            {
                try
                {
                    Conn.Open();
                    SVO = new DataTable();
                    Personal = new DataTable();
                    Options = new DataTable();
                    Dosug = new DataTable();
                    AutoNO = new DataTable();
                    OleDbCommand oCmd = Conn.CreateCommand();
                    oCmd.CommandText = "SELECT * FROM SVO ORDER BY ID";
                    SVO.Load(oCmd.ExecuteReader());
                    oCmd.CommandText = "SELECT * FROM Personal WHERE Отдел LIKE 'ОВРО' ORDER BY ID";
                    Personal.Load(oCmd.ExecuteReader());
                    oCmd.CommandText = "SELECT * FROM Options ORDER BY ID";
                    Options.Load(oCmd.ExecuteReader());
                    oCmd.CommandText = "SELECT * FROM NachOtr ORDER BY ID";
                    AutoNO.Load(oCmd.ExecuteReader());
                    oCmd.CommandText = "SELECT * FROM Kruzhki ORDER BY ID";
                    Dosug.Load(oCmd.ExecuteReader());
                    kruzhkiCount = Dosug.Rows.Count;
                    Conn.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public void SetKruzhokState(int id, bool check)
        {
            if (kruzhkiCount > id) Dosug.Rows[id][3] = check;
        }
        
        public bool GetKruzhokState(int id)
        {
            if (kruzhkiCount > id) return (bool)Dosug.Rows[id][3];
            else return false;
        }

        public string GetSelectedKruzhki()
        {
            string res = "";
            foreach (DataRow dr in Dosug.Rows)
            {
                if ((bool)dr[3])
                {
                    if (res != "") res += ", ";
                    res += dr[2].ToString();
                }
            }
            return res;
        }

        public DataTable GetSVO(string nomer)
        {
            var svo = from row in SVO.AsEnumerable() where row.Field<string>("Отряд") == nomer select row;
            return svo.CopyToDataTable();
        }

        public string GetAutoNachOtrFIO(string NomOtr)
        {
            var notr = from row in AutoNO.AsEnumerable() where row.Field<string>("Номер отряда") == NomOtr select row.Field<string>("ФИО");
            return notr.First();
        }

        public string GetAutoNachOtrZvanie(string NomOtr)
        {
            var notr = from row in AutoNO.AsEnumerable() where row.Field<string>("Номер отряда") == NomOtr select row.Field<string>("Звание");
            return notr.First();
        }

        public string GetParameterByID(int ID)
        {
            return Options.Rows[ID].ItemArray[2].ToString();
        }

        public string GetParameterByName(string name)
        {
            foreach (DataRow dr in Options.Rows)
            {
                if (dr[1].ToString().ToUpper() == name.ToUpper())
                    return dr[2].ToString();
            }
            return "";
        }

        public string GetOption(string name)
        {
            var opt = from row in Options.AsEnumerable() where row.Field<string>("Параметр") == name select row.Field<string>("Значение");
            return opt.First();
        }

        public void FillHeadersByID(ComboBox cb, int ID)
        {
            switch (ID)
            {
                case 0:
                    cb.Items.Add("Определить автоматически");
                    for (int i = 0; i < Personal.Rows.Count; i++)
                    {
                        cb.Items.Add(Tools.FirstToUpper(Personal.Rows[i][3].ToString()) + " вн. сл. - " + Personal.Rows[i][1].ToString());                        
                    }
                    cb.SelectedIndex = 0;
                    break;
                case 1:
                    for (int i = 0; i < Options.Rows.Count; i++)
                    {
                        cb.Items.Add(Options.Rows[i].ItemArray[1]);
                        cb.SelectedIndex = 0;
                    }
                    break;
            }               
        }

        public string GetPersonalDoljnostByID(int ID, bool Sklonenie)
        {
            if (!Sklonenie)
            {
                return Personal.Rows[ID].ItemArray[2].ToString();
            }
            else
            {
                return Personal.Rows[ID].ItemArray[5].ToString();
            }
        }

        public string GetPersonalZvanieByID(int ID, bool Sklonenie)
        {
            if (!Sklonenie)
            {
                return Personal.Rows[ID].ItemArray[3].ToString();
            }
            else
            {
                return Personal.Rows[ID].ItemArray[6].ToString();
            }
        }

        public string GetPersonalFIOByID(int ID, bool Sklonenie)
        {
            if (!Sklonenie)
            {
                return Personal.Rows[ID].ItemArray[1].ToString();
            }
            else
            {
                return Personal.Rows[ID].ItemArray[4].ToString();
            }
        }

        public string GetPersonalFullNameByID(int ID, bool Sklonenie)
        {
            if (!Sklonenie)
            {
                return Personal.Rows[ID].ItemArray[2].ToString() + " " 
                    + Personal.Rows[ID].ItemArray[3].ToString() + " внутренней службы " + Personal.Rows[ID].ItemArray[1].ToString();
            }
            else
            {
                return Personal.Rows[ID].ItemArray[5].ToString() + " "
                    + Personal.Rows[ID].ItemArray[6].ToString() + " внутренней службы " + Personal.Rows[ID].ItemArray[4].ToString();
            }
        }



    }
}

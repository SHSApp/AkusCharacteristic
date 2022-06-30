using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FSLib.Declension;

namespace SHSApp_Char
{

    class AkusConnect
    {
        private OleDbConnection Conn = null;
        private DataTable result = null;
        private DataTable sudimosti = null;
        private DataTable vzisk = null;
        private DataTable pooshren = null;
        private DataTable profuch = null;
        private DataTable uslovia = null;
        private DataTable iski = null;
        private DataTable ucheba = null;
        private int pooshrenCount = 0;
        private int vziskCount = 0;
        private int profuchCount = 0;
        private int usloviaCount = 0;
        private int sudimostCount = 0;
        private int iskiCount = 0;
        private int uchebaCount = 0;
        private int zaTrudCount = 0;
        private int zaVospitCount = 0;
        private int zaUchebuCount = 0;
        private int vziskDoVstupCount = 0;
        private int vziskDeystvCount = 0;
        private int vziskSnyatoCount = 0;
        private int vziskPogashenoCount = 0;
        private bool flagPorchaImushesva = false;
        private bool flagKurenie = false;
        private bool flagNevypolnenieTrebovaniy = false;
        private bool flagNetaktichnoePovedenie = false;
        private bool flagKonfliktnost = false;
        private bool flagNarushenieFormyOdezhdy = false;
        private bool flagZapravkaNePoObrazcu = false;
        private bool flagZanaveshivanie = false;
        private bool flagOtkazOtRaboty = false;
        private bool flagOtkazOtRaboty106 = false;
        private bool flagZlostniyNarushitel = false;
        private PriznanieViny flagPriznanieViny = PriznanieViny.PriznalChastichno;
        private Otnoshenie106 flagOtnoshenieK106 = Otnoshenie106.UdevletvoritelnoTrebKontrol;
        private Trudoustroysvo trudoustroystvo = Trudoustroysvo.NeTrudoustroenNeHochet;
        private KMM flagUchastieVKMM = KMM.NeUchastvuet;
        private CharType flagCharType = CharType.PoUmolchaniyu;
        private Vyvod vyvodSimple = Vyvod.Udovletvoritelno;
        private SudSupport flagSudSupport = SudSupport.Yes;
        private VidSuda flagVidSuda = VidSuda.UDO;
        private string dataPriznaniyaZlostnym = "";
        private string statiaZlostniy = "";
        private string itemZlostniy = "";
        private string itemOtnosheniePrest = "";
        private double summaIskaUgolDelo = 0;
        private double pogashenoUgolDelo = 0;
        private double pogashenoProchie = 0;
        private double summaIskaProchie = 0;
        private static string razmerAlimentov = "";
        private bool flagAlimenty = false;
        private bool flagIskUgolDelo = false;
        private bool flagIskProchie = false;
        private bool flagOkonchenoUgolDelo = false;
        private bool flagOkonchenoProchie = false;
        private double summaIskaOkonchUgolDelo = 0;
        private double summaIskaOkonchProchie = 0;
        private string okonchIskUgolDelo = "";
        private string okonchIskProch = "";
        private bool flagSamoobrazovanie = true;
        private bool flagUchebaShkola = false;
        private bool flagUchebaPU = false;
        private string professiyaPU = "";
        private bool flagKruzhkiOtryada = true;
        private string kruzhki = "";
        private bool harakSpokoystvie = true;
        private bool harakUravnoveshen = true;
        private bool harakStabilen = true;
        private bool harakKonflikten = false;
        private bool flagAntisanitSost = false;
        private bool flagNestabilnoe = false;

        private int distiplCoeff = 3;

        public static string ConnectionString = "Provider=VFPOLEDB.1;Data Source=C:\\IK\\Database\\;Password=\"\";Collating Sequence=MACHINE;";

        public string Kruzhki
        {
            get
            {
                return kruzhki;
            }

            set
            {
                kruzhki = value;
            }
        }

        public bool FlagKruzhkiOtryada
        {
            get
            {
                return flagKruzhkiOtryada;
            }

            set
            {
                flagKruzhkiOtryada = value;
            }
        }

        public bool FlagSamoobrazovanie
        {
            get
            {
                return flagSamoobrazovanie;
            }

            set
            {
                flagSamoobrazovanie = value;
            }
        }

        public Trudoustroysvo Trudoustroystvo
        {
            get
            {
                return trudoustroystvo;
            }

            set
            {
                trudoustroystvo = value;
            }
        }

        public VidSuda FlagVidSuda
        {
            get
            {
                return flagVidSuda;
            }

            set
            {
                flagVidSuda = value;
            }
        }

        public Otnoshenie106 FlagOtnoshenieK106
        {
            get
            {
                return flagOtnoshenieK106;
            }

            set
            {
                flagOtnoshenieK106 = value;
            }
        }

        public KMM FlagUchastieVKMM
        {
            get
            {
                return flagUchastieVKMM;
            }

            set
            {
                flagUchastieVKMM = value;
            }
        }

        public CharType FlagCharType
        {
            get
            {
                return flagCharType;
            }

            set
            {
                flagCharType = value;
            }
        }

        public Vyvod VyvodSimple
        {
            get
            {
                return vyvodSimple;
            }

            set
            {
                vyvodSimple = value;
            }
        }

        public SudSupport FlagSudSupport
        {
            get
            {
                return flagSudSupport;
            }

            set
            {
                flagSudSupport = value;
            }
        }

        public bool HarakSpokoystvie
        {
            get
            {
                return harakSpokoystvie;
            }

            set
            {
                harakSpokoystvie = value;
            }
        }

        public bool HarakUravnoveshen
        {
            get
            {
                return harakUravnoveshen;
            }

            set
            {
                harakUravnoveshen = value;
            }
        }

        public bool HarakStabilen
        {
            get
            {
                return harakStabilen;
            }

            set
            {
                harakStabilen = value;
            }
        }

        public bool HarakKonflikten
        {
            get
            {
                return harakKonflikten;
            }

            set
            {
                harakKonflikten = value;
            }
        }

        public int VziskCount
        {
            get
            {
                return vziskCount;
            }
        }

        public int PooshrenCount
        {
            get
            {
                return pooshrenCount;
            }
        }

        public void SetDataBasePath(string path)
        {
            ConnectionString = "Provider=VFPOLEDB.1;Data Source=" + path + ";Password=\"\";Collating Sequence=MACHINE;";
        }

        public void ClearValues()
        {
            result.Reset();
            pooshren.Reset();
            vzisk.Reset();
            profuch.Reset();
            uslovia.Reset();
            sudimosti.Reset();
            iski.Reset();
            ucheba.Reset();
            pooshrenCount = 0;
            vziskCount = 0;
            profuchCount = 0;
            usloviaCount = 0;
            sudimostCount = 0;
            zaTrudCount = 0;
            zaVospitCount = 0;
            zaUchebuCount = 0;
            iskiCount = 0;
            uchebaCount = 0;
            vziskDoVstupCount = 0;
            vziskDeystvCount = 0;
            vziskSnyatoCount = 0;
            vziskPogashenoCount = 0;
            flagPorchaImushesva = false;
            flagKurenie = false;
            flagNevypolnenieTrebovaniy = false;
            flagNetaktichnoePovedenie = false;
            flagKonfliktnost = false;
            flagNarushenieFormyOdezhdy = false;
            flagZapravkaNePoObrazcu = false;
            flagZanaveshivanie = false;
            flagOtkazOtRaboty = false;
            flagOtkazOtRaboty106 = false;
            flagZlostniyNarushitel = false;
            flagPriznanieViny = PriznanieViny.PriznalChastichno;
            flagOtnoshenieK106 = Otnoshenie106.UdevletvoritelnoTrebKontrol;
            flagVidSuda = VidSuda.UDO;
            dataPriznaniyaZlostnym = "";
            statiaZlostniy = "";
            itemZlostniy = "";
            itemOtnosheniePrest = "";
            summaIskaUgolDelo = 0;
            pogashenoUgolDelo = 0;
            pogashenoProchie = 0;
            summaIskaProchie = 0;
            razmerAlimentov = "";
            flagAlimenty = false;
            flagIskUgolDelo = false;
            flagIskProchie = false;
            flagOkonchenoUgolDelo = false;
            flagOkonchenoProchie = false;
            okonchIskUgolDelo = "";
            okonchIskProch = "";
            summaIskaOkonchUgolDelo = 0;
            summaIskaOkonchProchie = 0;
            trudoustroystvo = Trudoustroysvo.NeTrudoustroenNeHochet;
            flagUchebaShkola = false;
            flagUchebaPU = false;
            professiyaPU = "";
            flagSamoobrazovanie = true;
            flagKruzhkiOtryada = true;
            kruzhki = "";
            harakSpokoystvie = true;
            harakUravnoveshen = true;
            harakStabilen = true;
            harakKonflikten = false;
            flagUchastieVKMM = KMM.NeUchastvuet;
            flagCharType = CharType.PoUmolchaniyu;
            vyvodSimple = Vyvod.Udovletvoritelno;
            flagSudSupport = SudSupport.Yes;
            flagAntisanitSost = false;
            flagNestabilnoe = false;
        }

        public void Execute(string ID)
        {
            if (Conn != null)
            {
                try
                {
                    Conn.ConnectionString = ConnectionString;
                    Conn.Open();
                    ClearValues();
                    OleDbCommand oCmd = Conn.CreateCommand();
                    if (oCmd.Parameters.Count == 0) oCmd.Parameters.Add(new OleDbParameter("itemperson", OleDbType.WChar));
                    oCmd.Parameters["itemperson"].Value = ID;
                    oCmd.CommandText = "SELECT itemperson, votnprest FROM Data\\charakt WHERE itemperson = ?";
                    result.Load(oCmd.ExecuteReader());
                    if (result.Rows.Count > 0) itemOtnosheniePrest = result.Rows[0][1].ToString().TrimEnd(' ');
                    result.Reset();
                    oCmd.CommandText = "SELECT itemperson, name, itemme FROM Data\\_pr15 WHERE itemperson = ?";
                    result.Load(oCmd.ExecuteReader());
                    foreach (DataRow dr in result.Rows)
                    {
                        if (dr[1].ToString().ToUpper().Contains("ЗНУПОН") || 
                            dr[1].ToString().ToUpper().Contains("ЗЛОСТН") || 
                            dr[1].ToString().ToUpper().Contains("116"))
                        {
                            flagZlostniyNarushitel = true;
                            itemZlostniy = dr[2].ToString().TrimEnd(' ');
                        }
                    }
                    result.Reset();
                    oCmd.CommandText = "SELECT proper(vfamily), vname, vlastname, vdatar, vosustatia, vsroklet, " +
                        "vsrokmes, vsrokdney, vnachsroka, vkonecsrok, vnomotr, vdataprib, votkudauch, " +
                        "vdatavstup, vnation, vmestorgos, vmestorobl, vmestorgor, vmestoradr, vgrajdanst, vmestopgos, vmestopobl, " +
                        "vmestopgor, vmestopadr, vsemia, vobrazov, vrabota, vdoljnost, vosudata, vsudname, vsrokudo, vdataudo, vsrokposel, " +
                        "vdataposel, change, data1_ch, vsrok_pr, d1_pr, vfotofas, vdataotudo, vdataotpos, data2_ch, d2_pr, vfotoprof FROM Data\\card WHERE itemperson = ?";                    
                    result.Load(oCmd.ExecuteReader());
                    result.Columns[10].MaxLength = 10;
                    oCmd.CommandText = "SELECT cast(iif(empty(vdataob), evl(vdataob, NULL), ctod(substr(vdataob, 4, 2) + \".\" + substr(vdataob, 1, 2) + \".\" + substr(vdataob, 7, 4))) as date) as vdataob1, vdataob, vzachto, vvidpoosh FROM Data\\pooshren WHERE pooshren.ItemPerson = ? ORDER BY vdataob1";
                    pooshren.Load(oCmd.ExecuteReader());
                    pooshren.Columns[3].MaxLength = 100;
                    pooshrenCount = pooshren.Rows.Count;
                    oCmd.CommandText = "SELECT cast(iif(empty(vdata), evl(vdata, NULL), ctod(substr(vdata, 4, 2) + \".\" + substr(vdata, 1, 2) + \".\" + substr(vdata, 7, 4))) as date) as vdata1, vdata, vprichiny, vvid, vsrok, vdatasnajt, itemme FROM Data\\distipl WHERE distipl.ItemPerson = ? ORDER BY vdata1";
                    vzisk.Load(oCmd.ExecuteReader());
                    vzisk.Columns[2].MaxLength = 200; vzisk.Columns[3].MaxLength = 250; vzisk.Columns[5].MaxLength = 250;
                    vziskCount = vzisk.Rows.Count;
                    oCmd.CommandText = "SELECT cast(iif(empty(vdatapost), evl(vdatapost, NULL), ctod(substr(vdatapost, 4, 2) + \".\" + substr(vdatapost, 1, 2) + \".\" + substr(vdatapost, 7, 4))) as date) as vdatapost1, vdatapost, vdatasnyat, vkategory FROM Data\\profuch WHERE profuch.ItemPerson = ? ORDER BY vdatapost1";
                    profuch.Load(oCmd.ExecuteReader());
                    profuch.Columns[3].MaxLength = 350;
                    profuchCount = profuch.Rows.Count;
                    oCmd.CommandText = "SELECT cast(iif(empty(vdata), evl(vdata, NULL), ctod(substr(vdata, 4, 2) + \".\" + substr(vdata, 1, 2) + \".\" + substr(vdata, 7, 4))) as date) as vdata1, vdata, vuslov FROM Data\\uslovia WHERE ItemPerson = ? ORDER BY vdata1";
                    uslovia.Load(oCmd.ExecuteReader());
                    uslovia.Columns[2].MaxLength = 220;
                    usloviaCount = uslovia.Rows.Count;
                    oCmd.CommandText = "SELECT itemperson, vnomsudim, vdataosu, vstatosuj, vkemosuj, vsroklet, " +
                        "vsrokmes, vsrokdney, vprimechan FROM Data\\sudimost WHERE ItemPerson = ? ORDER BY vnomsudim";
                    sudimosti.Load(oCmd.ExecuteReader());
                    sudimosti.Columns[3].MaxLength = 400;
                    sudimosti.Columns[4].MaxLength = 200;
                    sudimostCount = sudimosti.Rows.Count;
                    oCmd.CommandText = "SELECT itemperson, vosnovan, vuder, valimenty, vosnovprek FROM Data\\isplist WHERE itemperson = ?";
                    iski.Load(oCmd.ExecuteReader());
                    iski.Columns[1].MaxLength = 250;
                    iski.Columns[4].MaxLength = 300;
                    iskiCount = iski.Rows.Count;
                    oCmd.CommandText = "SELECT itemperson, vdokum, vkemvydan FROM Data\\docum WHERE itemperson = ?";
                    ucheba.Load(oCmd.ExecuteReader());
                    ucheba.Columns[1].MaxLength = 200;
                    uchebaCount = ucheba.Rows.Count;
                    Conn.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        public string GetRandomZekID()
        {
            if (Conn != null)
            {
                try
                {
                    Conn.ConnectionString = ConnectionString;
                    Conn.Open();
                    DataTable res = new DataTable();
                    Random ran = new Random();
                    int s = ran.Next(1, 19);
                    OleDbCommand oCmd = Conn.CreateCommand();
                    oCmd.CommandText = "SELECT itemperson FROM Data\\card WHERE vpr_osv == 1 or vubylpost == 1 or vumer == 1 and vsroklet == ?";
                    oCmd.Parameters.Add(new OleDbParameter("vsroklet", OleDbType.Numeric));
                    oCmd.Parameters["vsroklet"].Value = s;
                    res.Load(oCmd.ExecuteReader());
                    Conn.Close();
                    if (res.Rows.Count > 0) return res.Rows[ran.Next(0, res.Rows.Count - 1)][0].ToString();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return "";
        }

        public void Analyze()
        {
            AnalizVziskaniy();
            AnalizPooshreniy();
            AnalizPriznaniyaViny();
            AnalizIskov();
            AnalizObucheniya();
            AnalizPovedeniya();
        }

        private void AnalizVziskaniy()
        {
            DateTime d1 = Convert.ToDateTime(GetFieldByID(FieldID.DataVstupleniaPrigovora));
            bool pogaslo = false;
            for (int i = 0; i < vziskCount; i++)
            {
                DateTime d2 = Convert.ToDateTime(vzisk.Rows[i][1].ToString());
                if (d2 < d1)
                {
                    vzisk.Rows[i][5] = "До вступления приговора в законную силу";
                    vziskDoVstupCount++;
                }
            }
            d1 = DateTime.Today;
            for (int i = vziskCount - 1; i > vziskDoVstupCount - 1; i--)
            {
                DateTime d2 = Convert.ToDateTime(vzisk.Rows[i][1].ToString());
                if (vzisk.Rows[i][5].ToString().TrimEnd(' ') != "")
                {
                    vzisk.Rows[i][5] = "Снято " + vzisk.Rows[i][5].ToString().TrimEnd(' ');
                    vziskSnyatoCount++;
                    pogaslo = true;
                }
                else
                {
                    if ((d2.AddYears(1) > d1) && (!pogaslo))
                    {
                        vzisk.Rows[i][5] = "Действующее";
                        vziskDeystvCount++;
                    }
                    else
                    {
                        vzisk.Rows[i][5] = "Погашено";
                        vziskPogashenoCount++;
                        pogaslo = true;
                    }
                }
                d1 = d2.AddYears(-1);
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("порча")) flagPorchaImushesva = true;
                if ((vzisk.Rows[i][2].ToString().ToLower().Contains("курение")) ||
                    (vzisk.Rows[i][2].ToString().ToLower().Contains("курил"))) flagKurenie = true;
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("требований")) flagNevypolnenieTrebovaniy = true;
                if ((vzisk.Rows[i][2].ToString().ToLower().Contains("нетактичн")) ||
                    (vzisk.Rows[i][2].ToString().ToLower().Contains("обращение"))) flagNetaktichnoePovedenie = true;
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("конфликт")) flagKonfliktnost = true;
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("одежды")) flagNarushenieFormyOdezhdy = true;
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("заправ")) flagZapravkaNePoObrazcu = true;
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("занаве")) flagZanaveshivanie = true;
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("работы")) flagOtkazOtRaboty = true;
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("106") ||
                    vzisk.Rows[i][2].ToString().ToLower().Contains("хоз") ||
                    vzisk.Rows[i][2].ToString().ToLower().Contains("благоуст")) flagOtkazOtRaboty106 = true;
                if (vzisk.Rows[i][2].ToString().ToLower().Contains("антисанит")) flagAntisanitSost = true;
                if ((flagZlostniyNarushitel) && (vzisk.Rows[i][6].ToString().TrimEnd(' ') == itemZlostniy))
                {
                    dataPriznaniyaZlostnym = vzisk.Rows[i][1].ToString();
                    if (vzisk.Rows[i][2].ToString().ToLower().Contains("запрещен") ||
                        vzisk.Rows[i][2].ToString().ToLower().Contains("неповиновен") ||
                        vzisk.Rows[i][2].ToString().ToLower().Contains("отказ") ||
                        vzisk.Rows[i][2].ToString().ToLower().Contains("телефон"))
                        statiaZlostniy = "по части 1 статьи 116 УИК РФ";
                    else statiaZlostniy = "по части 2 статьи 116 УИК РФ";
                }
            }
        }

        private void AnalizPriznaniyaViny()
        {
            if (itemOtnosheniePrest != "")
            {
                flagPriznanieViny = PriznanieViny.Priznal;
                if (itemOtnosheniePrest.ToLower().Contains("не призн")) flagPriznanieViny = PriznanieViny.NePriznal;
                if (itemOtnosheniePrest.ToLower().Contains("частично")) flagPriznanieViny = PriznanieViny.PriznalChastichno;
            }
        }

        private void AnalizPooshreniy()
        {
            for (int i = 0; i < pooshrenCount; i++)
            {
                if (pooshren.Rows[i][2].ToString().ToLower().Contains("труд")) zaTrudCount++;
                if (pooshren.Rows[i][2].ToString().ToLower().Contains("воспит") ||
                    pooshren.Rows[i][2].ToString().ToLower().Contains("соревнов") ||
                    pooshren.Rows[i][2].ToString().ToLower().Contains("обществ")) zaVospitCount++;
                if (pooshren.Rows[i][2].ToString().ToLower().Contains("учеб") ||
                    pooshren.Rows[i][2].ToString().ToLower().Contains("обучен")) zaUchebuCount++;
            }
        }

        private void AnalizIskov()
        {
            foreach (DataRow dr in iski.Rows)
            {
                if (dr[1].ToString().Contains("уголовн") || dr[1].ToString().Contains("штраф"))
                {
                    flagIskUgolDelo = true;
                    summaIskaUgolDelo += Convert.ToDouble(dr[3]);
                    if (dr[4].ToString().ToLower().Contains("расчет") || dr[4].ToString().ToLower().Contains("расчёт")) pogashenoUgolDelo += Convert.ToDouble(dr[3]);
                    dr[4] = Tools.ConvertOkonchIska(dr[4].ToString());
                    if (dr[4].ToString().Contains("прекращ") || dr[4].ToString().Contains("оконч") || dr[4].ToString().Contains("отозв"))
                    {
                        flagOkonchenoUgolDelo = true;
                        summaIskaOkonchUgolDelo += Convert.ToDouble(dr[3]);
                        okonchIskUgolDelo += "Исполнительное производство по иску на сумму " + dr[3].ToString().Trim(' ') +
                            Tools.GetDecl(Convert.ToInt32(dr[3]), " рубль ", " рубля ", " рублей ") + dr[4].ToString() + ". ";
                    }
                }
                else if (dr[1].ToString().Contains("алимент"))
                {
                    flagAlimenty = true;
                    if (Convert.ToString(dr[2]).Trim(' ') == "0")
                    {
                        razmerAlimentov = Convert.ToString(dr[3]).Trim(' ');
                        razmerAlimentov += " " +Tools.GetDecl((int)Convert.ToDouble(razmerAlimentov), "рубль", "рубля", "рублей");
                    }
                    else razmerAlimentov = Convert.ToString(dr[2]).Trim(' ') + "% от заработной платы";
                }
                else
                {
                    flagIskProchie = true;
                    summaIskaProchie += Convert.ToDouble(dr[3]);
                    if (dr[4].ToString().ToLower().Contains("расчет") || dr[4].ToString().ToLower().Contains("расчёт")) pogashenoProchie += Convert.ToDouble(dr[3]);
                    dr[4] = Tools.ConvertOkonchIska(dr[4].ToString());
                    if (dr[4].ToString().Contains("прекращ") || dr[4].ToString().Contains("оконч") || dr[4].ToString().Contains("отозв"))
                    {
                        flagOkonchenoProchie = true;
                        summaIskaOkonchProchie += Convert.ToDouble(dr[3]);
                        okonchIskProch += "Исполнительное производство по иску на сумму " + dr[3].ToString().Trim(' ') + 
                            Tools.GetDecl(Convert.ToInt32(dr[3]), " рубль ", " рубля ", " рублей ") + dr[4].ToString() + ". ";
                    }
                }
            }
        }

        private void AnalizObucheniya()
        {
            foreach (DataRow dr in ucheba.Rows)
            {
                string doc = dr[1].ToString().ToLower();
                if (doc.Contains("аттест") && doc.Contains("средн") && doc.Contains("общем"))
                {
                    flagUchebaShkola = true;
                }
                if (doc.Contains("свидет") && doc.Contains("квалифик"))
                {
                    flagUchebaPU = true;
                    string prof = dr[2].ToString().ToLower();
                    if (professiyaPU != "") professiyaPU += ", ";
                    if (prof.Contains("повар")) professiyaPU += "повара 3 разряда";
                    if (prof.Contains("кочег")) professiyaPU += "машиниста (кочегара) котельной 2 разряда";
                    if (prof.Contains("подсоб")) professiyaPU += "подсобного рабочего 2 разряда";
                    if (prof.Contains("столяр")) professiyaPU += "столяра 2 разряда";
                    if (prof.Contains("пожарн")) professiyaPU += "пожарного 4 разряда";
                }
            }
        }

        private void AnalizPovedeniya()
        {
            bool flag1 = false;
            bool flag2 = false;
            int nest = 0;
            for (int i = 0; i < vziskCount; i++)
            {
                flag1 = vzisk.Rows[i][5].ToString().ToLower().Contains("снят");
                if (flag1 && !flag2) nest++;
                flag2 = flag1;
            }
            if (nest > 0 && vziskSnyatoCount > 4) flagNestabilnoe = true;
            if (nest > 1 && (vziskCount - vziskDoVstupCount) < 12) flagNestabilnoe = true;
            if ((vziskCount - vziskDoVstupCount) >= 12 && nest > 2) flagNestabilnoe = true;

            int Coef = pooshrenCount * 12 - vziskPogashenoCount * 12 - vziskSnyatoCount * 9 - vziskDoVstupCount * 3 - vziskDeystvCount * 18;

            if (vziskDeystvCount > 0)
            {
                vyvodSimple = Vyvod.Otritsatelno;
                flagSudSupport = SudSupport.NoDeystvVzisk;
                if (Coef < 0) flagOtnoshenieK106 = Otnoshenie106.Neudovletvoritelno;
                if (Coef >= 0) flagOtnoshenieK106 = Otnoshenie106.UdevletvoritelnoTrebKontrol;
                if (Coef >= 36) flagOtnoshenieK106 = Otnoshenie106.Udovletvoritelno;
                if (Coef >= 48) flagOtnoshenieK106 = Otnoshenie106.Dobrosovestno;
            }
            else
            {
                DateTime dks = Convert.ToDateTime(GetFieldByID(FieldID.KonecSroka));
                bool bolshSrok = (DateTime.Today.AddYears(5) < dks);
                int vz = vziskCount - vziskDoVstupCount;
                
                if (flagZlostniyNarushitel || flagOtkazOtRaboty) Coef -= 240;
                if (Coef >= 48)
                {
                    vyvodSimple = Vyvod.Polozhitelno;
                    flagSudSupport = SudSupport.Yes;
                    if (bolshSrok) flagSudSupport = SudSupport.NoBolshoyostatok;
                    if (flagNestabilnoe) flagSudSupport = SudSupport.NoNestabiloePovedenie;
                    if (vz >= 15) flagSudSupport = SudSupport.NoMnogoNarusheniy;
                }
                else
                {
                    vyvodSimple = Vyvod.Udovletvoritelno;
                    if (Coef < 24) flagSudSupport = SudSupport.NoCelyIspravleniya;
                    if (bolshSrok) flagSudSupport = SudSupport.NoBolshoyostatok;
                    if (flagNestabilnoe) flagSudSupport = SudSupport.NoNestabiloePovedenie;
                    if (vz >= 12 && pooshrenCount <= 8) flagSudSupport = SudSupport.NoMnogoNarusheniy;
                }
            }

            string notr = GetFieldByID(FieldID.NomerOtrjada);

            if (Tools.IsPromka(notr))
            {
                if (!flagOtkazOtRaboty)
                {
                    if (zaTrudCount >= 2) trudoustroystvo = Trudoustroysvo.Trudoustroen;
                    else if (zaTrudCount > 0) trudoustroystvo = Trudoustroysvo.NeTrudoustroenRaneeRabotal;
                    else trudoustroystvo = Trudoustroysvo.NeTrudoustroenNoHochet;
                }
                else trudoustroystvo = Trudoustroysvo.NeTrudoustroenNeHochet;

                if (!flagOtkazOtRaboty106)
                {
                    if (Coef < 0) flagOtnoshenieK106 = Otnoshenie106.Neudovletvoritelno;
                    if (Coef >= 0) flagOtnoshenieK106 = Otnoshenie106.UdevletvoritelnoTrebKontrol;
                    if (Coef >= 36) flagOtnoshenieK106 = Otnoshenie106.Udovletvoritelno;
                    if (Coef >= 48) flagOtnoshenieK106 = Otnoshenie106.Dobrosovestno;
                }
                else flagOtnoshenieK106 = Otnoshenie106.PosredstvennoTrebKontrol;

                if (zaVospitCount >= 1 && Coef >= 12) flagUchastieVKMM = KMM.UchastvuetNeohotno;
                else flagUchastieVKMM = KMM.NeUchastvuet;
                if (zaVospitCount >= 3 && Coef >= 36) flagUchastieVKMM = KMM.Uchastvuet;
                
            }

            if (Tools.IsZhilka(notr))
            {
                if (zaTrudCount > 1) trudoustroystvo = Trudoustroysvo.NeTrudoustroenRaneeRabotal;
                else trudoustroystvo = Trudoustroysvo.NeTrudoustroenNeHochet;

                if (!flagOtkazOtRaboty106)
                {
                    if (Coef < 0) flagOtnoshenieK106 = Otnoshenie106.Neudovletvoritelno;
                    if (Coef > 0) flagOtnoshenieK106 = Otnoshenie106.PosredstvennoTrebKontrol;
                    if (Coef >= 36) flagOtnoshenieK106 = Otnoshenie106.UdevletvoritelnoTrebKontrol;
                    if (Coef >= 48) flagOtnoshenieK106 = Otnoshenie106.Udovletvoritelno;
                }
                else flagOtnoshenieK106 = Otnoshenie106.Neudovletvoritelno;

                if (zaVospitCount >= 2 && Coef >= 36) flagUchastieVKMM = KMM.UchastvuetNeohotno;
                else flagUchastieVKMM = KMM.NeUchastvuet;
                if (zaVospitCount >= 4 && Coef >= 48) flagUchastieVKMM = KMM.Uchastvuet;
            }            
        }

        public void FillTables(Qualif qf)
        {
            qf.OpenQualif("pc56");
            string res = "";
            string tmp = "";
            foreach (DataRow dr in vzisk.Rows)
            {
                qf.ChangeQualif("pc56");
                res = "";
                tmp = dr[2].ToString().Trim(' ');
                int IDcount = tmp.Length / 2;
                for (int i = 0; i < IDcount; i++)
                {
                    if (i > 0) { res += ", "; }
                    res += qf.GetFromQualifByID(tmp.Substring(2 * i, 2));
                }
                dr[2] = res;
                qf.ChangeQualif("pc29");
                dr[3] = qf.GetFromQualifByID(dr[3].ToString().TrimEnd(' '));
            }
            qf.ChangeQualif("pc37", true);
            foreach (DataRow dr in pooshren.Rows)
            {
                dr[3] = qf.GetFromQualifByID(dr[3].ToString().TrimEnd(' '));
            }
            if (itemOtnosheniePrest != "")
            {
                qf.ChangeQualif("pc39", true);
                itemOtnosheniePrest = qf.GetFromQualifByID(itemOtnosheniePrest);
            }
            qf.ChangeQualif("pc70", true);
            foreach (DataRow dr in uslovia.Rows)
            {
                dr[1] = dr[1].ToString().TrimEnd(' ');
                dr[2] = qf.GetFromQualifByID(dr[2].ToString().TrimEnd(' '));
            }
            qf.ChangeQualif("pc32", true);
            foreach (DataRow dr in profuch.Rows)
            {
                dr[1] = dr[1].ToString().TrimEnd(' ');
                dr[2] = dr[2].ToString().TrimEnd(' ');
                dr[3] = qf.GetFromQualifByID(dr[3].ToString().TrimEnd(' '));
            }
            qf.ChangeQualif("pc34", true);
            foreach (DataRow dr in iski.Rows) dr[1] = qf.GetFromQualifByID(dr[1].ToString());
            qf.ChangeQualif("pc36", true);
            foreach (DataRow dr in ucheba.Rows) dr[1] = qf.GetFromQualifByID(dr[1].ToString());
            qf.ChangeQualif("pc5", true);
            foreach (DataRow dr in sudimosti.Rows) dr[4] = Tools.ConvertNazvanieSuda(qf.GetFromQualifByID(dr[4].ToString().TrimEnd(' ')));
            qf.CloseOpenedQualif();
            foreach (DataRow dr in sudimosti.Rows)
            {
                if (dr[3].ToString().TrimEnd(' ').Length > 8) dr[3] = qf.GetStatiaSimpleByID(dr[3].ToString().TrimEnd(' '));
                else dr[3] = qf.GetStatiaFullByID(dr[3].ToString().TrimEnd(' '));
            }
            result.Rows[0][(int)FieldID.NomerOtrjada] = qf.GetNomerOtryada(result.Rows[0][(int)FieldID.NomerOtrjada].ToString()); 
                       
        }       

        public AkusConnect()
        {
            Conn = new OleDbConnection();
            result = new DataTable();
            pooshren = new DataTable();
            vzisk = new DataTable();
            profuch = new DataTable();
            uslovia = new DataTable();
            sudimosti = new DataTable();
            iski = new DataTable();
            ucheba = new DataTable();
        }

        public string GetFullFIO(DeclensionCase padej)
        {
            string surname = result.Rows[0][0].ToString().TrimEnd(' ');
            string name = result.Rows[0][1].ToString().TrimEnd(' ');
            string otchestvo = result.Rows[0][2].ToString().TrimEnd(' ');
            return Declension1251.GetSNPDeclension(surname, name, otchestvo, Gender.MasculineGender, padej);
        }

        public string GetFIO(DeclensionCase padej)
        {
            string i = "";
            string o = "";
            if (result.Rows[0][1].ToString().TrimEnd(' ') != "") i = result.Rows[0][1].ToString().TrimEnd(' ').Substring(0, 1).ToUpper() + ".";
            if (result.Rows[0][2].ToString().TrimEnd(' ') != "") o = result.Rows[0][2].ToString().TrimEnd(' ').Substring(0, 1).ToUpper() + ".";
            string fio = result.Rows[0][0].ToString().TrimEnd(' ');
            if (i != "") fio += " " + i;
            if (o != "") fio += o;
            return Declension1251.GetSNPDeclension(fio, Gender.MasculineGender, padej);
        }

        public string GetFieldByID(FieldID ID)
        {
            return result.Rows[0][(int)ID].ToString().TrimEnd(' ');
        }

        public string GetVziskDataByID(int ID)
        {
            return vzisk.Rows[ID][1].ToString().TrimEnd(' ');
        }

        public string GetVziskZaChtoByID(int ID)
        {
            return vzisk.Rows[ID][2].ToString().TrimEnd(' ');
        }

        public string GetVziskVidByID(int ID)
        {
            return vzisk.Rows[ID][3].ToString().TrimEnd(' ');
        }

        public string GetVziskSrokByID(int ID)
        {
            return vzisk.Rows[ID][4].ToString().TrimEnd(' ');
        }

        public string GetVziskPrimByID(int ID)
        {
            return vzisk.Rows[ID][5].ToString().TrimEnd(' ');
        }

        public string GetPooshrenDataByID(int ID)
        {
            return pooshren.Rows[ID][1].ToString().TrimEnd(' ');
        }

        public string GetPooshrenZaChtoByID(int ID)
        {
            return pooshren.Rows[ID][2].ToString().TrimEnd(' ');
        }

        public string GetPooshrenVidByID(int ID)
        {
            return pooshren.Rows[ID][3].ToString().TrimEnd(' ');
        }

        public string GetBirthday()
        {
            return result.Rows[0][3].ToString().TrimEnd(' ');
        }

        public string GetFormattedSrok()
        {
            string res = "срок ";
            string tmp = result.Rows[0][5].ToString().TrimEnd(' ');
            int i = Convert.ToInt32(tmp);
            if ((i > 10) && (i < 20))
            {
                res = res + tmp + " лет ";
            }
            else
            {
                switch (tmp.Substring(tmp.Length - 1, 1))
                {
                    case "1":
                        res = res + tmp + " год ";
                        break;
                    case "2":
                        res = res + tmp + " года ";
                        break;
                    case "3":
                        res = res + tmp + " года ";
                        break;
                    case "4":
                        res = res + tmp + " года ";
                        break;
                    default:
                        res = res + tmp + " лет ";
                        break;
                }
            }
            tmp = result.Rows[0][6].ToString().TrimEnd(' ');
            i = Convert.ToInt32(tmp);
            if (i != 0)
            {
                switch (tmp)
                {
                    case "1":
                        res = res + tmp + " месяц ";
                        break;
                    case "2":
                        res = res + tmp + " месяца ";
                        break;
                    case "3":
                        res = res + tmp + " месяца ";
                        break;
                    case "4":
                        res = res + tmp + " месяца ";
                        break;
                    default:
                        res = res + tmp + " месяцев ";
                        break;
                }
            }
            tmp = result.Rows[0][7].ToString().Trim(' ');
            i = Convert.ToInt32(tmp);
            if (i != 0)
            {
                if ((i > 10) && (i < 20))
                {
                    res = res + tmp + " дней ";
                }
                else
                {
                    switch (tmp.Substring(tmp.Length - 1, 1))
                    {
                        case "1":
                            res = res + tmp + " день ";
                            break;
                        case "2":
                            res = res + tmp + " дня ";
                            break;
                        case "3":
                            res = res + tmp + " дня ";
                            break;
                        case "4":
                            res = res + tmp + " дня ";
                            break;
                        default:
                            res = res + tmp + " дней ";
                            break;
                    }
                }
            }
            return res + "лишения свободы";
        }

        public string GetFormattedSrokSimple()
        {
            string res = "срок ";
            string tmp = result.Rows[0][5].ToString().TrimEnd(' ');
            int i = Convert.ToInt32(tmp);
            if ((i > 10) && (i < 20))
            {
                res = res + tmp + " л. ";
            }
            else
            {
                switch (tmp.Substring(tmp.Length - 1, 1))
                {
                    case "1":
                        res = res + tmp + " г. ";
                        break;
                    case "2":
                        res = res + tmp + " г. ";
                        break;
                    case "3":
                        res = res + tmp + " г. ";
                        break;
                    case "4":
                        res = res + tmp + " г. ";
                        break;
                    default:
                        res = res + tmp + " л. ";
                        break;
                }
            }
            tmp = result.Rows[0][6].ToString().TrimEnd(' ');
            i = Convert.ToInt32(tmp);
            if (i != 0)
            {
                res = res + tmp + " м. ";
            }
            tmp = result.Rows[0][7].ToString().Trim(' ');
            i = Convert.ToInt32(tmp);
            if (i != 0)
            {
                res = res + tmp + " д. ";                
            }
            return res + "л/св.";
        }

        public string GetChastSuda(VidSuda sud)
        {
            switch (sud)
            {
                case VidSuda.UDO: return Tools.GetChastUDO(GetFieldByID(FieldID.ChastUDO));
                case VidSuda.KP: return Tools.GetChastKP(GetFieldByID(FieldID.ChastPoselka));
                case VidSuda.IR: return Tools.GetChastZMN(GetFieldByID(FieldID.ChastZMN));
                case VidSuda.PR: return Tools.GetChastPR(GetFieldByID(FieldID.ChastPR));
                case VidSuda.OS: return Tools.GetChastZMN(GetFieldByID(FieldID.ChastZMN));
            }
            return Tools.GetChastUDO(GetFieldByID(FieldID.ChastUDO));
        }

        public string GetDataSuda(VidSuda sud)
        {
            switch (sud)
            {
                case VidSuda.UDO: return GetFieldByID(FieldID.DataUDO);
                case VidSuda.KP: return GetFieldByID(FieldID.DataPoselka);
                case VidSuda.IR: return GetFieldByID(FieldID.DataZMN);
                case VidSuda.PR: return GetFieldByID(FieldID.DataPR);
                case VidSuda.OS: return GetFieldByID(FieldID.DataZMN);
            }
            return GetFieldByID(FieldID.DataUDO);
        }

        public string PredlNarushSizo()
        {
            if (vziskDoVstupCount > 0) return "допустил " + Tools.SkloneniePoKolichestvu(vziskDoVstupCount, "нарушен") + " режима содержания";
            else return "нарушений режима содержания не допускал";
        }

        public string PredlPrezhnieSudimosti()
        {
            string res = "";
            if (sudimostCount > 0)
            {
                res += "Прежние судимости:" + (char)13;
                for (int i = 0; i < sudimostCount; i++)
                {
                    if (i != 0) res += (char)13;
                    res += Convert.ToString(i + 1) + ") " + sudimosti.Rows[i][2].ToString().TrimEnd(' ') + " года " +
                        sudimosti.Rows[i][4] + " по " + sudimosti.Rows[i][3];
                    if (Tools.GetFormattedSrok(sudimosti.Rows[i][5].ToString(), sudimosti.Rows[i][6].ToString(),
                        sudimosti.Rows[i][7].ToString()) != "")
                        res += " на " + Tools.GetFormattedSrok(sudimosti.Rows[i][5].ToString(), sudimosti.Rows[i][6].ToString(), sudimosti.Rows[i][7].ToString());
                    if (sudimosti.Rows[i][8].ToString().TrimEnd(' ') != "")
                         res += " " + sudimosti.Rows[i][8].ToString().TrimEnd(' ');
                    res += ".";
                }
            }
            else res = "Ранее не судим.";
            return res;
        }

        public string PredlZlostNarush()
        {
            if (flagZlostniyNarushitel)
            {
                return dataPriznaniyaZlostnym + " года признан злостным нарушителем установленного порядка отбывания наказания " +
                    statiaZlostniy;
            }
            else return "Злостным нарушителем установленного порядка отбывания наказания не признавался";
        }

        public string PredlProfUchet()
        {
            string res = "";
            if (profuchCount > 0)
            {
                foreach (DataRow dr in profuch.Rows)
                {
                    if (res != "") res += ", ";
                    res += "c " + dr[1];
                    if (dr[2].ToString().TrimEnd(' ') != "") res += " по " + dr[2] + " состоял на профилактическом учете как «";
                    else res += " состоит на профилактическом учете как «";
                    res += dr[3] + "»";
                }
                res += ".";
            }
            else res = "на профилактическом учете не состоит.";
            return Tools.FirstToUpper(res);
        }

        public string PredlProfUchetSostoit()
        {
            string res = "";
            if (profuchCount > 0)
            {
                foreach (DataRow dr in profuch.Rows)
                {
                    if (res != "") res += ", ";
                    if (dr[2].ToString().TrimEnd(' ') == "") res += "c " + dr[1] + " состоит на профилактическом учете как «" + dr[3] + "»";
                }
                if (res != "") res += ".";
            }
            else res = "на профилактическом учете не состоит.";
            if (res == "") res = "на профилактическом учете не состоит.";
            return Tools.FirstToUpper(res);
        }

        public string PredlUsloviaOtbyvania()
        {
            string res = "";
            Uslovia usl;
            foreach (DataRow dr in uslovia.Rows)
            {
                usl = Tools.CheckUslovia(dr[2].ToString());
                if (res != "")
                {
                    res += "." + (char)13;
                    if (usl == Uslovia.Obychnie) res += "C " + dr[1].ToString() +
                            " по решению административной комиссии переведен в обычные условия отбывания наказания";
                    if (usl == Uslovia.Strogie) res += "C " + dr[1].ToString() +
                            " по решению административной комиссии переведен в строгие условия отбывания наказания";
                    if (usl == Uslovia.Oblegchennie) res += "C " + dr[1].ToString() +
                            " по решению административной комиссии переведен в облегченные условия отбывания наказания";
                }
                else
                {
                    if (usl == Uslovia.Obychnie) if (usloviaCount > 1) res += "C " + dr[1].ToString() + " содержался в обычных условиях отбывания наказания";
                    else res += "C " + dr[1].ToString() + " содержится в обычных условиях отбывания наказания";
                    if (usl == Uslovia.Strogie) res += "C " + dr[1].ToString() + 
                            " содержится в строгих условиях на основании части 5 статьи 122 УИК РФ";
                    if (usl == Uslovia.Oblegchennie) res += "C " + dr[1].ToString() + 
                            " по решению административной комиссии переведен в облегченные условия отбывания наказания";
                }
            }
            if (res == "") res += "Cодержится в обычных условиях отбывания наказания.";
            else res += ".";
            return res;
        }

        public string PredlDispraktika()
        {
            string res = "";
            if (vziskCount > 0)
            {
                res += "имел " + vziskCount.ToString() + " " + Tools.GetDecl(vziskCount, "взыскание", "взыскания", "взысканий");
                if (vziskCount == vziskDoVstupCount) res += ", " + Tools.GetDecl(vziskCount, "допущенное", "допущенных") + 
                        " до вступления приговора в законную силу.";
                else
                {
                    res += " (до вступления приговора суда в законную силу - " + vziskDoVstupCount.ToString() + "), ";
                    if (vziskDeystvCount > 0) res += "из них " + vziskDeystvCount.ToString() + " " +
                            Tools.GetDecl(vziskDeystvCount,"действующее","действующих") + ".";
                    else
                    {
                        if (vziskPogashenoCount > 0 && vziskSnyatoCount > 0) res += "взыскания сняты и погашены";
                        if (vziskPogashenoCount == 0 && vziskSnyatoCount > 0) res += Tools.GetDecl(vziskSnyatoCount, "взыскание снято", "взыскания сняты");
                        if (vziskPogashenoCount > 0 && vziskSnyatoCount == 0) res += Tools.GetDecl(vziskPogashenoCount, "взыскание погашено", "взыскания погашены");
                        res += " в установленном законом порядке.";
                    }
                }
            }
            else
            {
                res += "взысканий не имел.";
            }
            res += (char)13;
            if (pooshrenCount > 0)
            {
                res += "Количество поощрений - " + pooshrenCount.ToString() + ", ";
                if (pooshrenCount > distiplCoeff) res += "стремится к их получению.";
                else res += "не стремится к их получению.";
            }
            else
            {
                res += "Поощрений не имеет и не стремится к их получению.";
            }
            return res;
        }

        public string PredlFormalniySrok()
        {
            return Tools.formalniySrok[(int)flagVidSuda] + ": " + GetChastSuda(flagVidSuda) + " – " + GetDataSuda(flagVidSuda) + " года";
        }

        public string PredlIski()
        {
            string res = "";
            if (flagIskUgolDelo)
            {
                res += "имеет иски на общую сумму " + summaIskaUgolDelo +
                    Tools.GetDecl((int)summaIskaUgolDelo, " рубль", " рубля", " рублей");
                if (pogashenoUgolDelo > 1.0)
                {
                    double ostatok = summaIskaUgolDelo - pogashenoUgolDelo - summaIskaOkonchUgolDelo;
                    if (ostatok > 1.0)
                    {
                        res += ", из которых погашено " + pogashenoUgolDelo + 
                            Tools.GetDecl((int)pogashenoUgolDelo, " рубль, ", " рубля, ", " рублей, ") + "остаток долга составляет " +
                            ostatok + Tools.GetDecl((int)ostatok, " рубль. ", " рубля. ", " рублей. ");
                    } else
                    {
                        if (flagOkonchenoUgolDelo)
                        {
                            if ((summaIskaUgolDelo - summaIskaOkonchUgolDelo) < 1.0) res += ". ";
                            if ((summaIskaUgolDelo - summaIskaOkonchUgolDelo) > 1.0) res += ", которые частично погашены. ";
                        }
                        else res += ", которые полностью погашены. ";
                    }
                } else
                {
                    if (trudoustroystvo == Trudoustroysvo.Trudoustroen) res += ", которые погашает в установленном законом порядке. ";
                    else res += ", мер по погашению которых не предпринимает. ";
                }
                if (flagOkonchenoUgolDelo) res += okonchIskUgolDelo;                    
            }
            else res += "исковых обязательств не имеет. ";

            if (flagIskProchie)
            {
                res += "Имеет иные иски на общую сумму " + summaIskaProchie + Tools.GetDecl((int)summaIskaProchie, " рубль", " рубля", " рублей");
                if (pogashenoProchie > 1.0)
                {
                    double ostatok = summaIskaProchie - pogashenoProchie - summaIskaOkonchProchie;
                    if (ostatok > 1.0)
                    {
                        res += ", из которых погашено " + pogashenoProchie +
                            Tools.GetDecl((int)pogashenoProchie, " рубль, ", " рубля, ", " рублей, ") + "остаток долга составляет " +
                            ostatok + Tools.GetDecl((int)ostatok, " рубль. ", " рубля. ", " рублей. ");
                    } else
                    {
                        if (flagOkonchenoProchie && pogashenoProchie < 1.0) res += ". ";
                        else res += ", которые полностью погашены. ";
                    }
                }
                else
                {
                    if (trudoustroystvo == Trudoustroysvo.Trudoustroen) res += ", которые погашает в установленном законом порядке. ";
                    else res += ", мер по погашению которых не предпринимает. ";
                }
                if (flagOkonchenoProchie) res += okonchIskProch;
            }

            if (flagAlimenty)
            {
                res += "Имеет алименты в размере " + razmerAlimentov;
                if (trudoustroystvo == Trudoustroysvo.Trudoustroen) res += ", которые выплачивает в установленном законом порядке. ";
                else res += ". ";
            }

            if (!flagIskUgolDelo && !flagIskProchie && !flagAlimenty)
                res += "В бухгалтерию исполнительные листы о взыскании с него денежных средств не поступали. " +
                    "На официальном сайте ФССП России информация о наличии других исполнительных листов отсутствует.";
            return res;
        }

        public string PredlUcheba()
        {
            string res = "";
            if (flagSamoobrazovanie) res += "Занимается самообразованием, читает много литературы в различных жанрах. ";
            else res += "Самообразованием не занимается. ";
            if (!flagUchebaShkola && !flagUchebaPU)
            {
                res += "В вечерней (сменной) школе № 9, ПУ № 292 при ФКУ ИК-18 ГУФСИН России по Новосибирской области не обучался";
                if (flagSamoobrazovanie) res += ". ";
                else res += " по причине отсутствия интереса к обучению. ";
            }
            if (flagUchebaShkola)
                res += "В вечерней (сменной) школе № 9 при ФКУ ИК-18 ГУФСИН России по Новосибирской области получил среднее общее образование. ";
            if (flagUchebaPU)
            {
                res += "Находясь в местах лишения свободы, получил профессиональное образование";
                if (professiyaPU != "") res += " " + professiyaPU;
                res += " в ФКПОУ № 292 при учреждении. ";
            }
            if (zaUchebuCount > 0) res += "К учебе относится добросовестно и с интересом, за что поощрялся администрацией учреждения. ";
            return res;
        }

        public string PredlTrudoustroystvo()
        {
            string res = "";
            if (trudoustroystvo == Trudoustroysvo.NeTrudoustroenNeHochet)
                res += "За период отбывания наказания в ФКУ ИК – 18 ГУФСИН России по Новосибирской области трудоустроен не был, попыток трудоустроиться не предпринимал. ";
            if (trudoustroystvo == Trudoustroysvo.NeTrudoustroenRaneeRabotal)
            {
                if (flagCharType == CharType.VSud) res += "За период отбывания наказания в ФКУ ИК – 18 ГУФСИН России по Новосибирской области был трудоустроен [Информация о трудоустройстве] ";
                if (!flagOtkazOtRaboty) res += "В настоящий время не трудоустроен в связи с отсутствием объема работ на производстве, попытки трудоустроится регулярно предпринимает";                
                if (zaTrudCount > 0 && !flagOtkazOtRaboty) res += ". Ранее был трудоустроен, к труду относился добросовестно";
                if (zaTrudCount > 1 && !flagOtkazOtRaboty) res += ", за что неоднократно поощрялся администрацией ИУ";
                else res += ". ";
            }
            if (trudoustroystvo == Trudoustroysvo.Trudoustroen)
            {
                if (flagCharType == CharType.VSud)
                    res += "За период отбывания наказания в ФКУ ИК – 18 ГУФСИН России по Новосибирской области был трудоустроен [Информация о трудоустройстве] где работает по настоящее время. ";
                else res += "В настоящий момент трудоустроен. ";
                if (!flagOtkazOtRaboty) res += "К труду относится добросовестно";
                if (zaTrudCount > 1 && !flagOtkazOtRaboty) res += ", за что неоднократно поощрялся администрацией ИУ. ";
                else res += ". ";
            }
            if (trudoustroystvo == Trudoustroysvo.NeTrudoustroenNoHochet)
            {
                 res += "За период отбывания наказания в ФКУ ИК – 18 ГУФСИН России по Новосибирской области трудоустроен не был, попытки трудоустроиться регулярно предпринимает. ";
            }
            if (flagOtkazOtRaboty) res += "Имеет случаи отказа от оплачиваемых работ, за что привлекался к дисциплинарной ответственности. ";
            return res;
        }

        public string PredlOtnoshenieK106()
        {
            string res = "";
            res += Tools.otnoshenieK106[(int)flagOtnoshenieK106] + ". ";
            if (flagOtkazOtRaboty106) res += "Имеет случаи отказа от работ по благоустройству ИУ без оплаты труда, за что привлекался к дисциплинарной ответственности. ";
            res += "К оборудованию, инструментам, сырью, материалам – относится ";
            if (flagPorchaImushesva) res += "небрежно, имеет взыскания за порчу казенного имущества, ";
            else res += "бережно, ";
            res += "правила техники безопасности, промышленной санитарии, пожарной безопасности выполняет ";
            if (flagKurenie) res += "не в полном объеме, имеет взыскания за курение в неотведенных для этого местах.";
            else res += "в полном объёме.";
            return res;
        }

        public string PredlMeryVospitHaraktera()
        {
            string res = "";
            if (flagCharType == CharType.SVO)
            {
                res += Tools.vyvod[(int)vyvodSimple] + ". ";
            }
            else
            {
                if (vziskDeystvCount > 0) res += "реагирует слабо, должные выводы для себя не делает. ";
                else res += "реагирует, должные выводы для себя делает. ";
            }            
            res += "Законные требования сотрудников администрации выполняет";
            if (flagNevypolnenieTrebovaniy) res += " не всегда, имеет взыскания за невыполнение требований сотрудников администрации. ";
            else res += ". ";
            res += "В общении с сотрудниками администрации ИУ ";
            if (flagNetaktichnoePovedenie) res += "бывает не вежлив, имеет взыскания за нетактичное поведение.";
            else res += "вежлив, тактичен.";
            return res;
        }

        public string PredlUchastieVKMM()
        {
            string res = "";
            if (flagUchastieVKMM == KMM.NeUchastvuet)
            {
                if (Tools.IsPromka(GetFieldByID(FieldID.NomerOtrjada)) && flagKruzhkiOtryada) res += "Участие в спортивных и культурно-массовых мероприятиях не принимает, состоит в кружковой работе при отряде. ";
                else res += "Участие в общественной жизни отряда, спортивных и культурно-массовых мероприятиях не принимает. ";
            }                
            if (flagUchastieVKMM == KMM.Uchastvuet)
            {
                res += "Принимает активное участие в общественной жизни отряда, спортивных и культурно-массовых мероприятиях";
                if (!flagKruzhkiOtryada && kruzhki == "") res += ". ";
                if (flagKruzhkiOtryada && kruzhki == "") res += ", состоит в кружковой работе при отряде. ";
                if (!flagKruzhkiOtryada && kruzhki != "") res += ", состоит в кружковой работе при учреждении: в " + kruzhki + ". ";
                if (flagKruzhkiOtryada && kruzhki != "") res += ", состоит в кружковой работе при отряде и учреждении: в " + kruzhki + ". ";
                if (zaVospitCount == 1) res += "Поощрялся ";
                if (zaVospitCount > 1) res += "Неоднократно поощрялся ";
                if (zaVospitCount > 0) res += "администрацией исправительного учреждения за активное участие в воспитательных мероприятиях.";
            }
            if (flagUchastieVKMM == KMM.UchastvuetNeohotno)
            {
                res += "Участие в общественной жизни отряда, спортивных и культурно-массовых мероприятиях принимает неохотно";
                if (!flagKruzhkiOtryada && kruzhki == "") res += ". ";
                if (flagKruzhkiOtryada && kruzhki == "") res += ", состоит в кружковой работе при отряде. ";
                if (!flagKruzhkiOtryada && kruzhki != "") res += ", состоит в кружковой работе при учреждении: в " + kruzhki + ". ";
                if (flagKruzhkiOtryada && kruzhki != "") res += ", состоит в кружковой работе при отряде и учреждении: в " + kruzhki + ". ";
                if (zaVospitCount > 0) res += "Поощрялся администрацией исправительного учреждения за участие в воспитательных мероприятиях.";
            }
            return res;
        }

        public string PredlHarakter()
        {
            string res = "";
            if (harakSpokoystvie) res += "спокойный, "; else res += "агрессивный, ";
            if (harakUravnoveshen) res += "уравновешенный, "; else res += "вспыльчивый, ";
            if (harakStabilen) res += "эмоционально стабильный. "; else res += "эмоционально не стабильный. ";
            res += "В среде осужденных ";
            if (!harakKonflikten) res += "уживчив, не ";
            res += "склонен проявлять агрессивность, конфликтность";
            if (flagKonfliktnost) res += ", имеет взыскание за создание конфликтной ситуации."; else res += ".";
            return res;
        }

        public string PredlVneshniyVid()
        {
            string res = "";
            if (flagNarushenieFormyOdezhdy) res += "не всегда опрятен, имеет взыскания за нарушение формы одежды, ";
            else res += "опрятен, ";
            if (flagZanaveshivanie || flagZapravkaNePoObrazcu) res += "в быту не всегда аккуратен";
            else res += "в быту аккуратен";
            if (flagZanaveshivanie) res += ", имеет взыскания за занавешивание спального места";
            if (flagZapravkaNePoObrazcu) res += ", имеет взыскания за заправку постели не по установленному образцу";
            res += ". ";
            if (flagAntisanitSost) res += "Санитарно-гигиенические нормы, установленные в ИУ, соблюдает не в полном объеме, " +
                    "имеет взыскание за антисанитарное состояние, чистоту своего спального места, прикроватной тумбочки поддерживает не всегда.";
            else res += "Санитарно-гигиенические нормы, установленные в ИУ, соблюдает, чистоту своего спального места, прикроватной тумбочки поддерживает.";
            return res;
        }

        public string PredlPriznanieViny()
        {
            string res = "";
            res += Tools.priznanieViny[(int)flagPriznanieViny];
            if (flagCharType == CharType.VSud) res += ". Отбыл более " + GetChastSuda(flagVidSuda) +" назначенного судом срока наказания";
            return res;
        }

        public string PredlVyvod()
        {
            string res = "";
            if (flagCharType == CharType.PoUmolchaniyu)
            {
                res += "администрация исправительного учреждения характеризует осужденного " + GetFullFIO(DeclensionCase.Vinit) + 
                    " " + Tools.vyvod[(int)vyvodSimple];
                if (vyvodSimple == Vyvod.Otritsatelno)
                {
                    if (vziskDeystvCount == 1) res += ", так как имеет действующее дисциплинарное взыскание";
                    if (vziskDeystvCount > 1) res += ", так как имеет действующие дисциплинарные взыскания";
                } else 
                res += "."; 
            }
            if (flagCharType == CharType.VSud)
            {
                res += "комиссия исправительного учреждения характеризует осужденного " + GetFullFIO(DeclensionCase.Vinit) +
                    " " + Tools.vyvod[(int)vyvodSimple];
                if ((int)vyvodSimple + (int)flagSudSupport <= 1 || vyvodSimple == Vyvod.Otritsatelno) res += " и считает, что ";
                else res += ", однако считает, что ";
                res += Tools.vyvodSud[(int)flagVidSuda] + " ";
                if (flagVidSuda == VidSuda.UDO || flagVidSuda == VidSuda.KP) res += Tools.support1[(int)flagSudSupport] + ".";
                else res += Tools.support2[(int)flagSudSupport] + ".";
            }
            /* if (flagCharType == CharType.NaOblegchennye)
             {
                 res += "комиссия исправительного учреждения характеризует осужденного " + GetFullFIO(DeclensionCase.Vinit) +
                    " " + Tools.vyvod[(int)vyvodSimple] + " и считает, что перевод из обычных условий содержания в облегченные ";
                 if (vyvodSimple == Vyvod.Otritsatelno)
                 {
                     if (vziskDeystvCount == 1) res += "не целесообразен, так как имеет действующее дисциплинарное взыскание.";
                     if (vziskDeystvCount > 1) res += "не целесообразен, так как имеет действующие дисциплинарные взыскания.";
                 }
                 else res += "целесообразен."; 
             } */
            if (flagCharType == CharType.SVO)
            {
                res += "осужденный " + GetFullFIO(DeclensionCase.Imenit) + " характеризуется " + Tools.vyvod[(int)vyvodSimple];
                if (vyvodSimple == Vyvod.Otritsatelno)
                {
                    if (vziskDeystvCount == 1) res += ", так как имеет действующее дисциплинарное взыскание";
                    if (vziskDeystvCount > 1) res += ", так как имеет действующие дисциплинарные взыскания";
                }
                res += ".";
            }
            return res;
        }

        public string PredlRabota()
        {
            string res = "";
            if (GetFieldByID(FieldID.GdeRabotalMesto) != "") res += GetFieldByID(FieldID.GdeRabotalMesto);
            if (GetFieldByID(FieldID.GdeRabotalDoljnost) != "")
            {
                if (res != "") res += ", ";
                res += GetFieldByID(FieldID.GdeRabotalDoljnost);
            }
            return res;
        }

        public string PredlRanee()
        {
            string res = "";
            string otkudo = result.Rows[0][(int)FieldID.DataOtkazaUDO].ToString().Trim(' ');
            string otkkp = result.Rows[0][(int)FieldID.DataOtkazaKP].ToString().Trim(' ');
            string otkzmn = result.Rows[0][(int)FieldID.DataOtkazaZMN].ToString().Trim(' ');
            string otkpr = result.Rows[0][(int)FieldID.DataOtkazaPR].ToString().Trim(' ');
            if (otkudo + otkkp + otkzmn + otkpr == "") res += "Ранее с ходатайствами не обращался";
            else res += "Ранее ";
            if (otkpr != "")
            {
                res += "обращался с ходатайством о замене неотбытой части наказания более мягким видом наказания в виде принудительных работ, ";
                res += otkpr + " - отказ";
            }
            if (otkzmn != "")
            {
                if (otkpr != "") res += ", ";
                res += "обращался с ходатайством о замене неотбытой части наказания более мягким видом наказания, " + otkzmn + " - отказ";
            }
            if (otkkp != "")
            {
                if (otkpr != "" || otkzmn != "") res += ", ";
                res += "обращался с ходатайством об изменении вида исправительного учреждения в виде перевода на колонию-поселение, " + otkkp + " - отказ";
            }
            if (otkudo != "")
            {
                if (otkpr != "" || otkzmn != "" || otkkp != "") res += ", ";
                res += "обращался с ходатайством об условно-досрочном освобождении, " + otkudo + " - отказ";
            }
            res += ".";
            return res;
        }

        public string PredlLichnayaGigiena()
        {
            string res = "";
            if (flagAntisanitSost) res += "соблюдает не в полном объеме, " +
                    "имеет взыскание за антисанитарное состояние.";
            else res += "соблюдает.";
            return res;
        }

        public string PredlVneshniyVidSVO()
        {
            string res = "";
            if (flagNarushenieFormyOdezhdy) res += "Внешний вид не всегда опрятен, имеет взыскания за нарушение формы одежды.";
            else res += "Внешне опрятен.";
            return res;
        }

        public string GetReligiya()
        {
            if (Tools.IsBubek(GetFullFIO(DeclensionCase.Imenit))) return "Мусульманин";
            Random ran = new Random();
            if (ran.Next(0, 100) > 60) return "Атеист";
            else return "Православный";
        }

        public string GetVredniePrivychki()
        {
            Random ran = new Random();
            if (ran.Next(0, 100) > 20) return "Вредные привычки - курение.";
            else return "Вредных привычек не имеет";
        }

    }
}

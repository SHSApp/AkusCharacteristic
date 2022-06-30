using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.IO;

namespace SHSApp_Char
{
    public enum FieldID
    {
        Family = 0, Name = 1, Lastname = 2, DataRozhdenia = 3, StatiaOsuzhdenia = 4, SrokLet = 5, SrokMes = 6,
        SrokDney = 7, NachaloSroka = 8, KonecSroka = 9, NomerOtrjada = 10, DataPribytiya = 11, UchrezhdeniePribytiya = 12,
        DataVstupleniaPrigovora = 13, Nation = 14, MestoRozhdeniaGosudarstvo = 15, MestoRozhdeniaOblast = 16, MestoRozhdeniaGorod = 17,
        MestoRozhdeniaAdres = 18, Grazhdanstvo = 19, MestoPropiskiGosudarstvo = 20, MestoPropiskiOblast = 21, MestoPropiskiGorod = 22,
        MestoPropiskiAdres = 23, SemeynoePolozhenie = 24, Obrazovanie = 25, GdeRabotalMesto = 26, GdeRabotalDoljnost = 27,
        DataOsuzhdeniya = 28, KemOsuzhden = 29, ChastUDO = 30, DataUDO = 31, ChastPoselka = 32, DataPoselka = 33,
        ChastZMN = 34, DataZMN = 35, ChastPR = 36, DataPR = 37, FotoFas = 38, DataOtkazaUDO = 39, DataOtkazaKP = 40,
        DataOtkazaZMN = 41, DataOtkazaPR = 42, FotoProf = 43
    }

    public enum CharType
    {
        PoUmolchaniyu = 0, VSud = 1, SVO = 2, SpravkaPV = 3, ZekSpravka = 4
    }

    public enum VidSuda
    {
        UDO = 0, KP = 1, IR = 2, PR = 3, OS = 4
    }

    public enum Uslovia
    {
        Obychnie = 0, Oblegchennie = 1, Strogie = 2
    }

    public enum Vyvod
    {
        Polozhitelno = 0, Udovletvoritelno = 1, Otritsatelno = 2
    }

    public enum Otnoshenie106
    {
        Dobrosovestno = 0, Udovletvoritelno = 1, UdevletvoritelnoTrebKontrol = 2,
        PosredstvennoTrebKontrol = 3, Neudovletvoritelno = 4
    }

    public enum PriznanieViny
    {
        Priznal = 0, PriznalChastichno = 1, NePriznal = 2
    }

    public enum Trudoustroysvo
    {
        Trudoustroen = 0, NeTrudoustroenRaneeRabotal = 1, NeTrudoustroenNoHochet = 2, NeTrudoustroenNeHochet = 3
    }

    public enum KMM
    {
        Uchastvuet = 0, UchastvuetNeohotno = 1, NeUchastvuet = 2
    }

    public enum SudSupport
    {
        Yes = 0, NoDeystvVzisk = 1, NoMnogoNarusheniy = 2, NoNestabiloePovedenie = 3, NoCelyIspravleniya = 4, NoBolshoyostatok = 5 
    }

    public static class Tools
    {
        public static string[] vyvod =
            { "положительно", "удовлетворительно", "отрицательно"};

        public static string[] formalniySrok =
            { "условно-досрочного освобождения",
            "изменения вида исправительного учреждения в виде перевода на колонию-поселение",
            "замены неотбытой части наказания более мягким видом наказания в виде исправительных работ",
            "замены неотбытой части наказания более мягким видом наказания в виде принудительных работ",
            "замены неотбытой части наказания более мягким видом наказания в виде ограничения свободы" };

        public static string[] vyvodSud =
            { "условно-досрочное освобождение от отбывания наказания",
            "изменение вида исправительного учреждения в виде перевода на колонию-поселение",
            "замена неотбытой части наказания более мягким видом наказания в виде исправительных работ",
            "замена неотбытой части наказания более мягким видом наказания в виде принудительных работ",
            "замена неотбытой части наказания более мягким видом наказания в виде ограничения свободы" };

        public static string[] support1 =
            { "целесообразно",
            "не целесообразно, так как имеются не снятые и не погашенные дисциплинарные взыскания",
            "не целесообразно, так как большую часть срока допускал нарушения",
             "не целесообразно, так как наблюдается нестабильное поведение, поощрения чередуются со взысканиями",
            "не целесообразно,  так как цели исправления не достигнуты в полном объеме",
            "не целесообразно, так как в связи с большим остатком срока отбывания наказания цели исправления были достигнуты не в полном объеме"};

        public static string[] support2 =
            { "целесообразна",
            "не целесообразна, так как имеются не снятые и не погашенные дисциплинарные взыскания",
            "не целесообразна, так как большую часть срока допускал нарушения",
             "не целесообразна, так как наблюдается нестабильное поведение, поощрения чередуются со взысканиями",
            "не целесообразна,  так как цели исправления не достигнуты в полном объеме",
            "не целесообразна, так как в связи с большим остатком срока отбывания наказания цели исправления были достигнуты не в полном объеме"};

        public static string[] priznanieViny =
            { "признал, в содеянном раскаялся", "признал частично", "не признал" };

        public static string[] otnoshenieK106 =
            { "добросовестно, контроля со стороны администрации ИУ не требует" ,
            "удовлетворительно, контроля со стороны администрации ИУ не требует",
            "удовлетворительно, требует контроля со стороны администрации ИУ",
            "посредственно, требует контроля со стороны администрации ИУ",
            "неудовлетворительно, требует постоянного контроля со стороны администрации ИУ" };

        public static string[] vidSudaSimple =
            { "УДО", "КП", "ИР", "ПР", "ОС" };

        public static string[] vidCharSimple =
            { "Обычная характеристика", "Характеристика в суд на", "Характеристика СВО", "Справка о поощрениях и взысканиях", "Справка по осужденному" };

        public static bool IsContent(string src, string part)
        {
            return src.Contains(part);
        }

        public static string FirstToUpper(string str)
        {
            if (str.Length > 0) { return char.ToUpper(str[0]) + str.Substring(1); }
            return "";
        }

        public static string MonthToString(int month)
        {
            string res = "";
            switch (month)
            {
                case 1:
                    res = "января";
                    break;
                case 2:
                    res = "февраля";
                    break;
                case 3:
                    res = "марта";
                    break;
                case 4:
                    res = "апреля";
                    break;
                case 5:
                    res = "мая";
                    break;
                case 6:
                    res = "июня";
                    break;
                case 7:
                    res = "июля";
                    break;
                case 8:
                    res = "августа";
                    break;
                case 9:
                    res = "сентября";
                    break;
                case 10:
                    res = "октября";
                    break;
                case 11:
                    res = "ноября";
                    break;
                case 12:
                    res = "декабря";
                    break;
            }
            return res;
        }

        public static string HoursToString(int hours)
        {
            string res = "";
            switch (hours)
            {
                case 0:
                    res = "00 часов";
                    break;
                case 1:
                    res = "01 час";
                    break;
                case 2:
                    res = "02 часа";
                    break;
                case 3:
                    res = "03 часа";
                    break;
                case 4:
                    res = "04 часа";
                    break;
                case 5:
                    res = "05 часов";
                    break;
                case 6:
                    res = "06 часов";
                    break;
                case 7:
                    res = "07 часов";
                    break;
                case 8:
                    res = "08 часов";
                    break;
                case 9:
                    res = "09 часов";
                    break;
                case 21:
                    res = "21 час";
                    break;
                case 22:
                    res = "22 часа";
                    break;
                case 23:
                    res = "23 часа";
                    break;
                default:
                    res = hours.ToString() + " часов";
                    break;
            }
            return res;
        }

        public static string MinutesToString(int minutes)
        {
            string res = "";
            if (minutes < 10)
            {
                res = "0" + minutes.ToString();
            }
            else
            {
                res = minutes.ToString();
            }
            if ((minutes > 10) && (minutes < 20))
            {
                res += " минут";
            }
            else
            {
                switch (minutes % 10)
                {
                    case 0:
                        res += " минут";
                        break;
                    case 1:
                        res += " минуту";
                        break;
                    case 2:
                        res += " минуты";
                        break;
                    case 3:
                        res += " минуты";
                        break;
                    case 4:
                        res += " минуты";
                        break;
                    default:
                        res += " минут";
                        break;
                }
            }
            return res;
        }

        public static string GetChastUDO(string chast)
        {
            switch (chast)
            {
                case "1":
                    return "1/3";
                case "2":
                    return "1/2";
                case "3":
                    return "2/3";
                case "4":
                    return "3/4";
                case "5":
                    return "4/5";
                default: return "3/4";
            }
        }

        public static string GetChastKP(string chast)
        {
            switch (chast)
            {
                case "1":
                    return "1/4";
                case "2":
                    return "1/3";
                case "3":
                    return "1/2";
                case "4":
                    return "2/3";
                case "5":
                    return "3/4";
                case "6":
                    return "4/5";
                default: return "2/3";
            }
        }

        public static string GetChastZMN(string chast)
        {
            switch (chast)
            {
                case "1":
                    return "1/3";
                case "2":
                    return "1/2";
                case "3":
                    return "2/3";
                case "4":
                    return "3/4";
                case "5":
                    return "4/5";
                default: return "2/3";
            }
        }

        public static string GetChastPR(string chast)
        {
            switch (chast)
            {
                case "1":
                    return "1/4";
                case "2":
                    return "1/3";
                case "3":
                    return "1/2";
                case "4":
                    return "3/4";
                case "5":
                    return "4/5";
                default: return "1/2";
            }
        }

        public static string GetSemeyniyStatus(int semya)
        {
            if (semya == 1) return "женат";
            else return "не женат";
        }

        public static string GetFormattedSrok(string years, string months, string days)
        {
            string res = "срок ";
            string tmp = years;
            int y = Convert.ToInt32(tmp);
            if (y != 0)
            {
                if ((y > 10) && (y < 20))
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
            }
            tmp = months;
            int m = Convert.ToInt32(tmp);
            if (m != 0)
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
            tmp = days;
            int d = Convert.ToInt32(tmp);
            if (d != 0)
            {
                if ((d > 10) && (d < 20))
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
            if ((y == 0) && (m == 0) && (d == 0)) return "";
            return res.Remove(res.Length - 1);// + "лишения свободы";
        }

        public static string ConvertNazvanieSuda(string sud)
        {
            sud = sud.Replace("й р/суд", "м районным судом");
            sud = sud.Replace("й р/с", "м районным судом");
            sud = sud.Replace("й г/с", "м городским судом");
            sud = sud.Replace("й районный суд", "м районным судом");
            sud = sud.Replace("й окружной военный суд", "м окружным военным судом");
            sud = sud.Replace("й областной суд", "м областным судом");
            sud = sud.Replace("Мировой судья", "Мировым судьей");
            sud = sud.Replace("Мировой суд.", "Мировым судьей");
            sud = sud.Replace("мир. суд.", "Мировым судьей");
            sud = sud.Replace("мир. судья", "Мировым судьей");
            sud = sud.Replace("Мир. ", "Мировым ");
            sud = sud.Replace("суд. уч-ка", "судебного участка");
            sud = sud.Replace("с/у", "судебного участка");
            sud = sud.Replace("й обл. суд", "м областным судом");
            sud = sud.Replace("обл.", "области");
            sud = sud.Replace("г. ", "города ");
            sud = sud.Replace("г.", "города ");
            sud = sud.Replace("р-на", "района");
            return sud;
        }

        public static string ConvertRegion(string src)
        {
            src = src.Replace("г. ", "город ");
            src = src.Replace("г.", "город ");
            src = src.Replace("р-н", "район");
            src = src.Replace("р-он", "район");
            src = src.Replace("обл.", "область");
            src = src.Replace("респ.", "Республика");
            src = src.Replace("ст-ца", "станица");
            src = src.Replace("пгт. ", "поселок городского типа ");
            src = src.Replace("пгт.", "поселок городского типа ");
            src = src.Replace("п.г.т. ", "поселок городского типа ");
            src = src.Replace("п.г.т.", "поселок городского типа ");
            src = src.Replace("п. ", "поселок ");
            src = src.Replace("п.", "поселок ");
            src = src.Replace("с. ", "село ");
            src = src.Replace("с.", "село ");
            src = src.Replace("АО", "автономный округ");
            return src;
        }

        public static string ConvertAdress(string src)
        {
            src = src.Replace("пос. ", "поселок ");
            src = src.Replace("пос.", "поселок ");
            src = src.Replace("с. ", "село ");
            src = src.Replace("с.", "село ");
            src = src.Replace("р.п. ", "рабочий поселок ");
            src = src.Replace("р.п.", "рабочий поселок ");
            src = src.Replace("гор. ", "город ");
            src = src.Replace("гор.", "город ");
            src = src.Replace("г. ", "город ");
            src = src.Replace("г.", "город ");
            src = src.Replace("обл.", "область");
            src = src.Replace("р-н", "район");
            src = src.Replace("п. ", "поселок ");
            src = src.Replace("п.", "поселок ");
            src = src.Replace("дер. ", "деревня ");
            src = src.Replace("дер.", "деревня ");
            src = src.Replace("пер. ", "переулок ");
            src = src.Replace("пер.", "переулок ");
            src = src.Replace("ст. ", "станица ");
            src = src.Replace("ст.", "станица ");
            src = src.Replace("к. ", "кишлак ");
            src = src.Replace("к.", "кишлак ");
            src = src.Replace("ул. ", "улица ");
            src = src.Replace("ул.", "улица ");
            src = src.Replace("кв. ", "квартира ");
            src = src.Replace("кв.", "квартира ");
            src = src.Replace("м/район", "микрорайон");
            if (src.Contains("д."))
            {
                if (src.IndexOf("д.") < 3)
                {
                    if (src[src.IndexOf("д.") + 2] != ' ') src = src.Insert(src.IndexOf("д.") + 2, " ");
                    src = src.Remove(src.IndexOf("д.") + 1, 1);
                    src = src.Insert(src.IndexOf("д.") + 1, "еревня");
                }
                if (src.IndexOf("д.") > 3)
                {
                    if (src[src.IndexOf("д.") + 2] != ' ') src = src.Insert(src.IndexOf("д.") + 2, " ");
                    src = src.Insert(src.IndexOf("д.") + 1, "ом");
                    src = src.Remove(src.IndexOf("дом.") + 3, 1);
                }
            }
            return src;
        }

        public static string SkloneniePoKolichestvu(int count, string koren)
        {
            string res = count.ToString() + " " + koren;
            if ((count > 10) && (count < 20))
            {
                res += "ий";
            }
            else
            {
                switch (count % 10)
                {
                    case 0:
                        res += "ий";
                        break;
                    case 1:
                        res += "ие";
                        break;
                    case 2:
                        res += "ия";
                        break;
                    case 3:
                        res += "ия";
                        break;
                    case 4:
                        res += "ия";
                        break;
                    default:
                        res += "ий";
                        break;
                }
            }
            return res;
        }

        public static string GetDecl(int num, string one, string two, string five)
        {
            int n = num % 100;
            if (n >= 5 && n <= 20)
            {
                return five;
            }
            n = num % 10;
            if (n == 1)
            {
                return one;
            }
            if (n >= 2 && n <= 4)
            {
                return two;
            }
            return five;
        }

        public static string GetDecl(int num, string one, string two)
        {
            return GetDecl(num, one, two, two);
        }

        public static Uslovia CheckUslovia(string src)
        {
            if (src.Contains("обычные")) return Uslovia.Obychnie;
            if (src.Contains("облегченные")) return Uslovia.Oblegchennie;
            if (src.Contains("строгие")) return Uslovia.Strogie;
            return Uslovia.Obychnie;
        }

        public static string ConvertOkonchIska(string src)
        {
            src = src.Trim(' ').ToLower();
            src = src.Replace("окон. ", "окончено ");
            src = src.Replace("окон.", "окончено ");
            src = src.Replace("оконч. ", "окончено ");
            src = src.Replace("оконч.", "окончено ");
            src = src.Replace("окончен. ", "окончено ");
            src = src.Replace("окончен.", "окончено ");
            src = src.Replace("оконче. ", "окончено ");
            src = src.Replace("оконче.", "окончено ");
            src = src.Replace("оконченно ", "окончено ");
            src = src.Replace("оконченно", "окончено ");
            src = src.Replace("постан. ", "постановлению ");
            src = src.Replace("постан.", "постановлению ");
            src = src.Replace("постановл. ", "постановлению ");
            src = src.Replace("постановл.", "постановлению ");
            src = src.Replace("постанов. ", "постановлению ");
            src = src.Replace("постанов.", "постановлению ");
            src = src.Replace("пост. ", "постановлению ");
            src = src.Replace("пост.", "постановлению ");
            src = src.Replace("постановлен. ", "постановлению ");
            src = src.Replace("постановлен.", "постановлению ");
            src = src.Replace("р-ну", "району");
            src = src.Replace("р-на", "района");
            src = src.Replace("отзыв ", "отозвано ");
            src = src.Replace("отзыв", "отозвано ");
            src = src.Replace("отз. ", "отозвано ");
            src = src.Replace("отз.", "отозвано ");
            src = src.Replace("отозв. ", "отозвано ");
            src = src.Replace("отозв.", "отозвано ");
            if (src.ToLower().Contains("осп"))
            {
                src = src.Remove(src.ToLower().LastIndexOf("осп") - 1);
            }
            return src;
        }

        public static bool IsPromka(string nom)
        {
            return "!2!6!7!8!9!12!".Contains("!" + nom + "!");
        }

        public static bool IsZhilka(string nom)
        {
            return "!1 (СУОН)!Карантин!3!4!5!10!11!".Contains("!" + nom + "!");
        }

        public static string SwapFIO(string fio)
        {
            return fio.Substring(fio.IndexOf(' ') + 1) + " " + fio.Remove(fio.IndexOf(' '));
        }

        public static double cton(string str, int p, int shift)
        {
            double nresult = 0;
            str = str.Replace(((char)33).ToString(), "");
            for (int k = 0; k < str.Length; k++)
            {
                nresult = nresult * p + Asc(str[k]) - shift;
            }
            return nresult;
        }

        public static int Asc(char c)
        {
            int converted = c;
            if (converted >= 0x80)
            {
                byte[] buffer = new byte[2];
                if (System.Text.Encoding.Default.GetBytes(new char[] { c }, 0, 1, buffer, 0) == 1)
                {
                    converted = buffer[0];
                }
                else
                {
                    converted = buffer[0] << 16 | buffer[1];
                }
            }
            return converted;
        }

        public static string GetFoto(string id)
        {
            string res = Convert.ToString(cton(id, 142, 34)).Substring(0,14);
            return "Foto\\" + res.Substring(0, 7) + "\\" + res + ".jpg";
        }

        public static bool IsBubek(string fio)
        {
            fio = fio.ToLower();
            return (fio.Contains("бек") || fio.Contains("мурод") || fio.Contains("жон") || fio.Contains("жан") || fio.Contains("ибро") || fio.Contains("хутд") || fio.Contains("махмуд") ||
                fio.Contains("лоев") || fio.Contains("диев") || fio.Contains("джаев") || fio.Contains("мудин") || fio.Contains("тдин") || fio.Contains("ддин") || fio.Contains("заев") ||
                fio.Contains("алеев") || fio.Contains("угли") || fio.Contains("оглы") || fio.Contains("огли") || fio.Contains("доев") || fio.Contains("берди") || fio.Contains("марда") ||
                fio.Contains("бахо") || fio.Contains("дулл") || fio.Contains("дали") || fio.Contains("хасан") || fio.Contains("абду") || fio.Contains("ходж") || fio.Contains("худж"));
        }

        public static void ToLog(string zek, CharType charType, VidSuda vidSuda)
        {
            string res = DateTime.Now.ToString() + ' ' + vidCharSimple[(int)charType] + ' ';
            if (charType == CharType.VSud) res += vidSudaSimple[(int)vidSuda] + ' ';
            res += "- " + zek + ';' + (char)13;
            File.AppendAllText(@"\\ik18srv\папкаобмена\ОПЕРА\5881\charLog.txt", res);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.ComponentModel;
using System.Data;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.IO;

namespace SHSApp_Char
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    delegate void UpdateProgressBarDelegate(DependencyProperty dp, object value);
    
    public partial class MainWindow : Window
    {
        private AkusConnect akus;
        private Settings sett;
        private Qualif qualif;
        public int Count = 0;
        public string Zeki = "";
        BackgroundWorker worker;
        private int persID;

        private void Insert(WordDocument doc, string src, string dst)
        {
            doc.SetSelectionToText(src);
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = dst;
        }

        private void ZapolnitZekSpravku(WordDocument doc)
        {
            string tempAdress = "";
            doc.SelectTable(1);
            if (akus.GetFieldByID(FieldID.FotoFas).Trim(' ') != "")
                doc.InsertPictureIntoTable(sett.GetOption("БД") + Tools.GetFoto(akus.GetFieldByID(FieldID.FotoFas)), 1, 1);
            if (akus.GetFieldByID(FieldID.FotoProf).Trim(' ') != "")
                doc.InsertPictureIntoTable(sett.GetOption("БД") + Tools.GetFoto(akus.GetFieldByID(FieldID.FotoProf)), 1, 2);
            //doc.InsertPictureIntoTable("C:\\IK\\Database\\Foto\\20210510626035.jpg", 1, 1);
            //doc.InsertPictureIntoTable("C:\\IK\\Database\\Foto\\20210510626036.jpg", 1, 2);
            Insert(doc, "[ФИО]", akus.GetFullFIO(DeclensionCase.Imenit));
            Insert(doc, "[Дата рождения]", akus.GetBirthday());
            tempAdress = qualif.GetRegionAdress(akus.GetFieldByID(FieldID.MestoRozhdeniaGosudarstvo),
                akus.GetFieldByID(FieldID.MestoRozhdeniaOblast), akus.GetFieldByID(FieldID.MestoRozhdeniaGorod));
            if (akus.GetFieldByID(FieldID.MestoRozhdeniaAdres) != "") tempAdress += ", " + Tools.ConvertAdress(akus.GetFieldByID(FieldID.MestoRozhdeniaAdres));
            Insert(doc, "[Место рождения]", tempAdress);
            Insert(doc, "[Гражданство]", qualif.GetGrajdanstvo(akus.GetFieldByID(FieldID.Grazhdanstvo)));
            tempAdress = qualif.GetRegionAdress(akus.GetFieldByID(FieldID.MestoPropiskiGosudarstvo),
                akus.GetFieldByID(FieldID.MestoPropiskiOblast), akus.GetFieldByID(FieldID.MestoPropiskiGorod));
            if (akus.GetFieldByID(FieldID.MestoPropiskiAdres) != "") tempAdress += ", " + Tools.ConvertAdress(akus.GetFieldByID(FieldID.MestoPropiskiAdres));
            Insert(doc, "[Место жительства]", tempAdress);
            Insert(doc, "[Дата осуждения]", akus.GetFieldByID(FieldID.DataOsuzhdeniya));
            Insert(doc, "[Кем осужден]", Tools.ConvertNazvanieSuda(qualif.GetFromQualif(akus.GetFieldByID(FieldID.KemOsuzhden), "pc5")));
            Insert(doc, "[Статьи]", qualif.GetStatiaFullByID(akus.GetFieldByID(FieldID.StatiaOsuzhdenia)));
            Insert(doc, "[Срок]", akus.GetFormattedSrok());
            Insert(doc, "[Начало срока]", akus.GetFieldByID(FieldID.NachaloSroka));
            Insert(doc, "[Конец срока]", akus.GetFieldByID(FieldID.KonecSroka));
            Insert(doc, "[Дата прибытия]", akus.GetFieldByID(FieldID.DataPribytiya));
            Insert(doc, "[Учреждение прибытия]", qualif.GetOtkudaPribByID(akus.GetFieldByID(FieldID.UchrezhdeniePribytiya)));
            Insert(doc, "[Профучет]", akus.PredlProfUchet());
            Insert(doc, "[Дата вступления]", akus.GetFieldByID(FieldID.DataVstupleniaPrigovora));
            Insert(doc, "[Дата УДО]", akus.GetFieldByID(FieldID.DataUDO));
            Insert(doc, "[Дата КП]", akus.GetFieldByID(FieldID.DataPoselka));
            Insert(doc, "[Часть УДО]", Tools.GetChastUDO(akus.GetFieldByID(FieldID.ChastUDO)));
            Insert(doc, "[Часть КП]", Tools.GetChastKP(akus.GetFieldByID(FieldID.ChastPoselka)));
            Insert(doc, "[Прежние судимости]", akus.PredlPrezhnieSudimosti());
            doc.ReplaceAllStrings("ИЗ-^?^?/", "СИЗО-");
            doc.ReplaceAllStrings("\"", "«");
        }

        private void ZapolnitShapkuSVO(WordDocument doc)
        {
            string tempAdress = "";
            Insert(doc, "[ФИО]", akus.GetFullFIO(DeclensionCase.Imenit));
            Insert(doc, "[Дата рождения]", akus.GetBirthday());
            tempAdress = qualif.GetRegionAdress(akus.GetFieldByID(FieldID.MestoRozhdeniaGosudarstvo),
                akus.GetFieldByID(FieldID.MestoRozhdeniaOblast), akus.GetFieldByID(FieldID.MestoRozhdeniaGorod));
            if (akus.GetFieldByID(FieldID.MestoRozhdeniaAdres) != "") tempAdress += ", " + Tools.ConvertAdress(akus.GetFieldByID(FieldID.MestoRozhdeniaAdres));
            Insert(doc, "[Место рождения]", tempAdress);
            Insert(doc, "[Гражданство]", qualif.GetGrajdanstvo(akus.GetFieldByID(FieldID.Grazhdanstvo)));
            tempAdress = qualif.GetRegionAdress(akus.GetFieldByID(FieldID.MestoPropiskiGosudarstvo),
                akus.GetFieldByID(FieldID.MestoPropiskiOblast), akus.GetFieldByID(FieldID.MestoPropiskiGorod));
            if (akus.GetFieldByID(FieldID.MestoPropiskiAdres) != "") tempAdress += ", " + Tools.ConvertAdress(akus.GetFieldByID(FieldID.MestoPropiskiAdres));
            Insert(doc, "[Место жительства]", tempAdress);
            Insert(doc, "[Семейное положение]", Tools.GetSemeyniyStatus(Convert.ToInt32(akus.GetFieldByID(FieldID.SemeynoePolozhenie))));
            Insert(doc, "[Образование]", qualif.GetObrazovanie(akus.GetFieldByID(FieldID.Obrazovanie)));
            Insert(doc, "[Место работы]", akus.PredlRabota());
            Insert(doc, "[Дата осуждения]", akus.GetFieldByID(FieldID.DataOsuzhdeniya));
            Insert(doc, "[Кем осужден]", Tools.ConvertNazvanieSuda(qualif.GetFromQualif(akus.GetFieldByID(FieldID.KemOsuzhden), "pc5")));
            Insert(doc, "[Статьи]", qualif.GetStatiaFullByID(akus.GetFieldByID(FieldID.StatiaOsuzhdenia)));
            Insert(doc, "[Срок]", akus.GetFormattedSrok());
            Insert(doc, "[Начало срока]", akus.GetFieldByID(FieldID.NachaloSroka));
            Insert(doc, "[Конец срока]", akus.GetFieldByID(FieldID.KonecSroka));
            Insert(doc, "[Прежние судимости]", akus.PredlPrezhnieSudimosti());
            Insert(doc, "[Формальный срок]", akus.PredlFormalniySrok());
            Insert(doc, "[Дата прибытия]", akus.GetFieldByID(FieldID.DataPribytiya));
            Insert(doc, "[Учреждение прибытия]", qualif.GetOtkudaPribByID(akus.GetFieldByID(FieldID.UchrezhdeniePribytiya)));          
        }

        private void ZapolnitSVO(WordDocument doc)
        {
            Insert(doc, "[Условия отбывания]", akus.PredlUsloviaOtbyvania());
            Insert(doc, "[Трудоустройство]", akus.PredlTrudoustroystvo());
            Insert(doc, "[Отношение к 106]", akus.PredlOtnoshenieK106());
            Insert(doc, "[Отношение к администрации]", akus.PredlMeryVospitHaraktera());
            Insert(doc, "[Диспрактика]", akus.PredlDispraktika());
            Insert(doc, "[Обучение]", akus.PredlUcheba());
            Insert(doc, "[Участие в КММ]", akus.PredlUchastieVKMM());
            Insert(doc, "[Религия]", akus.GetReligiya());
            Insert(doc, "[Профучет]", akus.PredlProfUchetSostoit());
            Insert(doc, "[Правила личной гигиены]", akus.PredlLichnayaGigiena());
            Insert(doc, "[Вредные привычки]", akus.GetVredniePrivychki());
            Insert(doc, "[Внешний вид]", akus.PredlVneshniyVidSVO());
            Insert(doc, "[Иски]", akus.PredlIski());
            Insert(doc, "[Признание вины]", akus.PredlPriznanieViny());
            Insert(doc, "[Вывод]", akus.PredlVyvod());
            if (persID == 0)
            {
                doc.ReplaceAllStrings("[Должность сотрудника]", "Начальник отряда ОВРО");
                doc.ReplaceAllStrings("[Звание сотрудника]", sett.GetAutoNachOtrZvanie(akus.GetFieldByID(FieldID.NomerOtrjada)));
                doc.ReplaceAllStrings("[ФИО сотрудника]", Tools.SwapFIO(sett.GetAutoNachOtrFIO(akus.GetFieldByID(FieldID.NomerOtrjada))));
            } else
            {
                doc.ReplaceAllStrings("[Должность сотрудника]", Tools.FirstToUpper(sett.GetPersonalDoljnostByID(persID - 1, false)));
                doc.ReplaceAllStrings("[Звание сотрудника]", sett.GetPersonalZvanieByID(persID - 1, false));
                doc.ReplaceAllStrings("[ФИО сотрудника]", Tools.SwapFIO(sett.GetPersonalFIOByID(persID - 1, false)));
            }            
            doc.ReplaceAllStrings("ИЗ-^?^?/", "СИЗО-");
            doc.ReplaceAllStrings("\"", "«");
        }

        private void ZapolnitChlenovSVO(WordDocument doc, DataTable SVO)
        {
            string pname = "";
            for (int i = 0; i < 4; i ++)
            {
                pname = "[Должность СВО" + (i+1).ToString() + "]";
                doc.ReplaceAllStrings(pname, Tools.FirstToUpper(SVO.Rows[i][3].ToString()));
                pname = "[Звание СВО" + (i+1).ToString() + "]";
                doc.ReplaceAllStrings(pname, SVO.Rows[i][2].ToString());
                pname = "[ФИО СВО" + (i+1).ToString() + "]";
                doc.ReplaceAllStrings(pname, Tools.SwapFIO(SVO.Rows[i][1].ToString()));
            }
        }

        private void ZapolnitSpravkuPV(WordDocument doc, int tabIndex)
        {
            doc.ReplaceAllStrings("[ФИО2]", akus.GetFullFIO(DeclensionCase.Rodit));
            if (akus.PooshrenCount == 0)
            {
                doc.ReplaceAllStrings("[ПООЩРЕНИЯ]", "ПООЩРЕНИЯ" + (char)13 + "Поощрений не имеет");
                doc.SelectTable(tabIndex);
                doc.RemoveTable();
                doc.SelectTable(tabIndex);
            }
            else
            {
                doc.ReplaceAllStrings("[ПООЩРЕНИЯ]", "ПООЩРЕНИЯ");
                doc.SelectTable(tabIndex);
                for (int i = 0; i < akus.PooshrenCount; i++)
                {
                    doc.AddRowToTable();
                    doc.SetSelectionToCell(i + 2, 1);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = (i + 1).ToString();
                    doc.SetSelectionToCell(i + 2, 2);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = akus.GetPooshrenDataByID(i);
                    doc.SetSelectionToCell(i + 2, 3);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = akus.GetPooshrenZaChtoByID(i);
                    doc.SetSelectionToCell(i + 2, 4);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = akus.GetPooshrenVidByID(i);
                }
                doc.SelectTable(tabIndex+1);
            }
            if (akus.VziskCount == 0)
            {
                doc.ReplaceAllStrings("[ВЗЫСКАНИЯ]", "ВЗЫСКАНИЯ" + (char)13 + "Взысканий не имеет");
                doc.RemoveTable();
            }
            else
            {
                doc.ReplaceAllStrings("[ВЗЫСКАНИЯ]", "ВЗЫСКАНИЯ");
                for (int i = 0; i < akus.VziskCount; i++)
                {
                    doc.AddRowToTable();
                    doc.SetSelectionToCell(i + 2, 1);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = (i + 1).ToString();
                    doc.SetSelectionToCell(i + 2, 2);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = akus.GetVziskDataByID(i);
                    doc.SetSelectionToCell(i + 2, 3);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = akus.GetVziskZaChtoByID(i);
                    doc.SetSelectionToCell(i + 2, 4);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = akus.GetVziskVidByID(i);
                    doc.SetSelectionToCell(i + 2, 5);
                    doc.Selection.Aligment = TextAligment.Center;
                    doc.Selection.Text = akus.GetVziskPrimByID(i);
                }
            }
            if (akus.FlagCharType == CharType.SpravkaPV)
            {
                if (persID == 0)
                {
                    doc.ReplaceAllStrings("[Должность сотрудника]", "Начальник отряда ОВРО");
                    doc.ReplaceAllStrings("[Звание сотрудника]", sett.GetAutoNachOtrZvanie(akus.GetFieldByID(FieldID.NomerOtrjada)));
                    doc.ReplaceAllStrings("[ФИО сотрудника]", Tools.SwapFIO(sett.GetAutoNachOtrFIO(akus.GetFieldByID(FieldID.NomerOtrjada))));
                }
                else
                {
                    doc.ReplaceAllStrings("[Должность сотрудника]", Tools.FirstToUpper(sett.GetPersonalDoljnostByID(persID - 1, false)));
                    doc.ReplaceAllStrings("[Звание сотрудника]", sett.GetPersonalZvanieByID(persID - 1, false));
                    doc.ReplaceAllStrings("[ФИО сотрудника]", Tools.SwapFIO(sett.GetPersonalFIOByID(persID - 1, false)));
                }
            }
        }
        
        private void ZapolnitShapku(WordDocument doc)
        {
            string tempAdress = "";
            doc.ReplaceAllStrings("[ФИО]", akus.GetFullFIO(DeclensionCase.Imenit));
            doc.ReplaceAllStrings("[Дата рождения]", akus.GetBirthday());
            tempAdress = qualif.GetRegionAdress(akus.GetFieldByID(FieldID.MestoRozhdeniaGosudarstvo),
                akus.GetFieldByID(FieldID.MestoRozhdeniaOblast), akus.GetFieldByID(FieldID.MestoRozhdeniaGorod));
            if (akus.GetFieldByID(FieldID.MestoRozhdeniaAdres) != "") tempAdress += ", " + Tools.ConvertAdress(akus.GetFieldByID(FieldID.MestoRozhdeniaAdres));
            doc.ReplaceAllStrings("[Место рождения]",  tempAdress);
            doc.ReplaceAllStrings("[Гражданство]", qualif.GetGrajdanstvo(akus.GetFieldByID(FieldID.Grazhdanstvo)));
            tempAdress = qualif.GetRegionAdress(akus.GetFieldByID(FieldID.MestoPropiskiGosudarstvo),
                akus.GetFieldByID(FieldID.MestoPropiskiOblast), akus.GetFieldByID(FieldID.MestoPropiskiGorod));
            if (akus.GetFieldByID(FieldID.MestoPropiskiAdres) != "") tempAdress += ", " + Tools.ConvertAdress(akus.GetFieldByID(FieldID.MestoPropiskiAdres));
            doc.ReplaceAllStrings("[Место жительства]", tempAdress);
            doc.ReplaceAllStrings("[Семейное положение]", Tools.GetSemeyniyStatus(Convert.ToInt32(akus.GetFieldByID(FieldID.SemeynoePolozhenie))));
            doc.ReplaceAllStrings("[Образование]", qualif.GetObrazovanie(akus.GetFieldByID(FieldID.Obrazovanie)));
            doc.ReplaceAllStrings("[Место работы]", akus.PredlRabota());
            doc.ReplaceAllStrings("[Дата осуждения]", akus.GetFieldByID(FieldID.DataOsuzhdeniya));
            doc.ReplaceAllStrings("[Кем осужден]", Tools.ConvertNazvanieSuda(qualif.GetFromQualif(akus.GetFieldByID(FieldID.KemOsuzhden),"pc5")));
            doc.SetSelectionToText("[Статьи]");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = qualif.GetStatiaFullByID(akus.GetFieldByID(FieldID.StatiaOsuzhdenia));
            doc.ReplaceAllStrings("[Срок]", akus.GetFormattedSrok());
            doc.ReplaceAllStrings("[Начало срока]", akus.GetFieldByID(FieldID.NachaloSroka));
            doc.ReplaceAllStrings("[Конец срока]", akus.GetFieldByID(FieldID.KonecSroka));
            doc.ReplaceAllStrings("[Формальный срок]", akus.PredlFormalniySrok());
            doc.ReplaceAllStrings("[Признание вины]", akus.PredlPriznanieViny());
            doc.ReplaceAllStrings("[Дата прибытия]", akus.GetFieldByID(FieldID.DataPribytiya));
            doc.ReplaceAllStrings("[Учреждение прибытия]", qualif.GetOtkudaPribByID(akus.GetFieldByID(FieldID.UchrezhdeniePribytiya)));
            doc.ReplaceAllStrings("[Нарушения в СИЗО]", akus.PredlNarushSizo());
            doc.ReplaceAllStrings("[Злостный нарушитель]", akus.PredlZlostNarush());
            doc.SetSelectionToText("[Диспрактика]");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlDispraktika();
            doc.ReplaceAllStrings("[Характер]", akus.PredlHarakter());
            if (persID == 0)
            {
                doc.ReplaceAllStrings("[Должность сотрудника]", "Начальник отряда ОВРО");
                doc.ReplaceAllStrings("[Звание сотрудника]", sett.GetAutoNachOtrZvanie(akus.GetFieldByID(FieldID.NomerOtrjada)));
                doc.ReplaceAllStrings("[ФИО сотрудника]", Tools.SwapFIO(sett.GetAutoNachOtrFIO(akus.GetFieldByID(FieldID.NomerOtrjada))));
            }
            else
            {
                doc.ReplaceAllStrings("[Должность сотрудника]", Tools.FirstToUpper(sett.GetPersonalDoljnostByID(persID - 1, false)));
                doc.ReplaceAllStrings("[Звание сотрудника]", sett.GetPersonalZvanieByID(persID - 1, false));
                doc.ReplaceAllStrings("[ФИО сотрудника]", Tools.SwapFIO(sett.GetPersonalFIOByID(persID - 1, false)));
            }
            doc.ReplaceAllStrings("ИЗ-^?^?/", "СИЗО-");   
            if (akus.FlagCharType == CharType.VSud)
            {
                if (akus.FlagSudSupport == SudSupport.Yes) doc.ReplaceAllStrings("[не]", "");
                else doc.ReplaceAllStrings("[не]", "не ");
                doc.ReplaceAllStrings("[ФИО сокращенное2]", akus.GetFIO(DeclensionCase.Rodit));
                doc.SetSelectionToText("[Ранее обращался]");
                doc.Selection.Aligment = TextAligment.Justify;
                doc.Selection.Text = akus.PredlRanee();
            }         
        }

        private void ZapolnitZakladki(WordDocument doc)
        {
            doc.SetSelectionToBookmark("ВнешнийВид");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlVneshniyVid();
            doc.SetSelectionToBookmark("Вывод");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlVyvod();
            doc.SetSelectionToBookmark("Иски");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlIski();
            doc.SetSelectionToBookmark("КММ");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlUchastieVKMM();
            doc.SetSelectionToBookmark("МерыВоспитательногоХарактера");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlMeryVospitHaraktera();
            doc.SetSelectionToBookmark("ОтношениеК106");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlOtnoshenieK106();
            doc.SetSelectionToBookmark("ПрежниеСудимости");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlPrezhnieSudimosti();
            doc.SetSelectionToBookmark("Профучет");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlProfUchet();
            doc.SetSelectionToBookmark("Трудоустройство");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlTrudoustroystvo();
            doc.SetSelectionToBookmark("УсловияОтбывания");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlUsloviaOtbyvania();
            doc.SetSelectionToBookmark("Учеба");
            doc.Selection.Aligment = TextAligment.Justify;
            doc.Selection.Text = akus.PredlUcheba();
            doc.ReplaceAllStrings("\"", "«");            
        }

        public MainWindow()
        {           
            InitializeComponent();            
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
        }

        private void Load_Checkboxes()
        {
            DataTable dt = sett.Kruzhki;
            if (sett.KruzhkiCount > 0)
            {
                FlagKruzhok1.Visibility = Visibility.Visible;
                FlagKruzhok1.Content = dt.Rows[0][1].ToString();
                FlagKruzhok1.IsChecked = (bool)dt.Rows[0][3];
            }
            if (sett.KruzhkiCount > 1)
            {
                FlagKruzhok2.Visibility = Visibility.Visible;
                FlagKruzhok2.Content = dt.Rows[1][1].ToString();
                FlagKruzhok2.IsChecked = (bool)dt.Rows[1][3];
            }
            if (sett.KruzhkiCount > 2)
            {
                FlagKruzhok3.Visibility = Visibility.Visible;
                FlagKruzhok3.Content = dt.Rows[2][1].ToString();
                FlagKruzhok3.IsChecked = (bool)dt.Rows[2][3];
            }
            if (sett.KruzhkiCount > 3)
            {
                FlagKruzhok4.Visibility = Visibility.Visible;
                FlagKruzhok4.Content = dt.Rows[3][1].ToString();
                FlagKruzhok4.IsChecked = (bool)dt.Rows[3][3];
            }
            if (sett.KruzhkiCount > 4)
            {
                FlagKruzhok5.Visibility = Visibility.Visible;
                FlagKruzhok5.Content = dt.Rows[4][1].ToString();
                FlagKruzhok5.IsChecked = (bool)dt.Rows[4][3];
            }
            if (sett.KruzhkiCount > 5)
            {
                FlagKruzhok6.Visibility = Visibility.Visible;
                FlagKruzhok6.Content = dt.Rows[5][1].ToString();
                FlagKruzhok6.IsChecked = (bool)dt.Rows[5][3];
            }
            if (sett.KruzhkiCount > 6)
            {
                FlagKruzhok7.Visibility = Visibility.Visible;
                FlagKruzhok7.Content = dt.Rows[6][1].ToString();
                FlagKruzhok7.IsChecked = (bool)dt.Rows[6][3];
            }
        }

        private void Set_Variables()
        {
            akus.FlagCharType = (CharType)cbVidChar.SelectedIndex;
            if (cbVyvod.SelectedIndex > 0) akus.VyvodSimple = (Vyvod)cbVyvod.SelectedIndex - 1;
            if (cbTrud.SelectedIndex > 0) akus.Trudoustroystvo = (Trudoustroysvo)cbTrud.SelectedIndex - 1;
            if (cb106.SelectedIndex > 0) akus.FlagOtnoshenieK106 = (Otnoshenie106)cb106.SelectedIndex - 1;
            if (cbVidSuda.IsEnabled) akus.FlagVidSuda = (VidSuda)cbVidSuda.SelectedIndex;
            if (cbSupport.IsEnabled && cbSupport.SelectedIndex > 0) akus.FlagSudSupport = (SudSupport)cbSupport.SelectedIndex - 1;
            akus.HarakSpokoystvie = (bool)harakSpokoyniy.IsChecked;
            akus.HarakUravnoveshen = (bool)harakUravnovesh.IsChecked;
            akus.HarakStabilen = (bool)harakStabilniy.IsChecked;
            akus.HarakKonflikten = (bool)harakKonflikten.IsChecked;
            akus.FlagSamoobrazovanie = (bool)FlagSamoobrazovanie.IsChecked;
            if (cbKMM.SelectedIndex > 0) akus.FlagUchastieVKMM = (KMM)cbKMM.SelectedIndex - 1;
            akus.FlagKruzhkiOtryada = (bool)FlagKruzhkiOtryada.IsChecked;
            akus.Kruzhki = sett.GetSelectedKruzhki();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            akus = new AkusConnect();
            sett = new Settings();
            qualif = new Qualif();
            sett.LoadTables();
            sett.FillHeadersByID(cbNO, 0);
            akus.SetDataBasePath(sett.GetOption("БД"));
            qualif.SetDataBasePath(sett.GetOption("БД"));
            Load_Checkboxes();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateProgressBarDelegate updProgress = new UpdateProgressBarDelegate(pb.SetValue);
            double value = 0;
            bool save = false;
            string path = "";
            Dispatcher.Invoke((Action)delegate { save = (bool)chbSave.IsChecked; path = savePath.Text; });
            for (int i = 0; i < Count; i++)
            {
                WordDocument wDoc = null;
                Dispatcher.Invoke((Action)delegate { go.IsEnabled = false; });
                string id = Zeki.Substring(i * 9, 9);                
                Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                akus.Execute(id);
                //MessageBox.Show(Tools.GetFoto(akus.GetFieldByID(FieldID.FotoFas)));
                Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                akus.FillTables(qualif);
                Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                akus.Analyze();
                Dispatcher.Invoke((Action)(() => { Set_Variables(); }));
                Tools.ToLog(akus.GetFullFIO(DeclensionCase.Imenit), akus.FlagCharType, akus.FlagVidSuda);
                Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                if (akus.FlagCharType == CharType.PoUmolchaniyu)
                {
                    wDoc = new WordDocument(sett.GetOption("БД") + sett.GetOption("Характеристика обычная"));
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitShapku(wDoc);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitZakladki(wDoc);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitSpravkuPV(wDoc, 2);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                }
                if (akus.FlagCharType == CharType.SpravkaPV)
                {
                    wDoc = new WordDocument(sett.GetOption("БД") + sett.GetOption("Справка ПВ"));
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });                    
                    ZapolnitSpravkuPV(wDoc, 1);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                }
                if (akus.FlagCharType == CharType.VSud)
                {
                    wDoc = new WordDocument(sett.GetOption("БД") + sett.GetOption("Характеристика в суд"));
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitShapku(wDoc);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitZakladki(wDoc);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitSpravkuPV(wDoc, 2);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                } 
                if (akus.FlagCharType == CharType.ZekSpravka)
                {
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    wDoc = new WordDocument(sett.GetOption("БД") + sett.GetOption("Справка по зеку"));
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitZekSpravku(wDoc);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });                    
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                }
                if (akus.FlagCharType == CharType.SVO)
                {
                    wDoc = new WordDocument(sett.GetOption("БД") + sett.GetOption("Характеристика СВО"));
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitShapkuSVO(wDoc);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitSVO(wDoc);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                    ZapolnitChlenovSVO(wDoc, sett.GetSVO(akus.GetFieldByID(FieldID.NomerOtrjada)));
                    ZapolnitSpravkuPV(wDoc, 6);
                    Dispatcher.Invoke(updProgress, new object[] { ProgressBar.ValueProperty, ++value });
                }
                                              
                if (save)
                {
                    if (akus.FlagCharType == CharType.ZekSpravka)
                    {
                        string fn = akus.GetFIO(DeclensionCase.Imenit) + " - справка";
                        if (!Directory.Exists(path + "\\" + akus.GetFullFIO(DeclensionCase.Imenit)))
                            Directory.CreateDirectory(path + "\\" + akus.GetFullFIO(DeclensionCase.Imenit));
                        wDoc.Save(path + "\\" + akus.GetFullFIO(DeclensionCase.Imenit) + "\\" + fn + ".doc");
                        wDoc.Close();
                    }
                    else
                    {
                        string filename = akus.GetFIO(DeclensionCase.Imenit) + " - характеристика";
                        if (akus.FlagCharType == CharType.VSud) filename += " " + Tools.vidSudaSimple[(int)akus.FlagVidSuda];
                        if (akus.FlagCharType == CharType.SVO) filename += " СВО";
                        wDoc.Save(path + "\\" + filename + ".doc");
                        wDoc.Close();
                    }
                } else wDoc.Visible = true;
                Dispatcher.Invoke((Action)delegate { App.Current.Windows[0].Activate(); });
            }
            Dispatcher.Invoke((Action)delegate { this.Close(); App.Current.Shutdown();  });
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            if (!Zeki.Contains("!!!D•ЃЂFz"))
            {
                Count = 1;
                Zeki = akus.GetRandomZekID();
            } else
            {
                Zeki = Zeki.Remove(Zeki.IndexOf("!!!D•ЃЂFz"), 9);
                Count--;
            }

            pb.Maximum = Count*8;
            pb.Value = 0;
            persID = cbNO.SelectedIndex;
            worker.RunWorkerAsync();          
        }

        private void cbVidChar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbVidChar.SelectedIndex == 1)
            {
                labelVidSuda.IsEnabled = true;
                cbVidSuda.IsEnabled = true;
                labelSupport.IsEnabled = true;
                cbSupport.IsEnabled = true;
            } else
            {
                labelVidSuda.IsEnabled = false;
                cbVidSuda.IsEnabled = false;
                labelSupport.IsEnabled = false;
                cbSupport.IsEnabled = false;
            }
        }

        private void cbKMM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbKMM.SelectedIndex == 3)
            {
                for (int i = 1; i <= 7; i++)
                {
                    CheckBox chb = (CheckBox)this.FindName("FlagKruzhok"+i.ToString());
                    chb.IsEnabled = false;
                }
                FlagKruzhkiOtryada.IsEnabled = false;
            }
            else
            {
                for (int i = 1; i <= 7; i++)
                {
                    CheckBox chb = (CheckBox)this.FindName("FlagKruzhok" + i.ToString());
                    chb.IsEnabled = true;
                }
                FlagKruzhkiOtryada.IsEnabled = true;
            }
        }

        private void FlagKruzhok_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = (CheckBox)sender;
            int id = Convert.ToInt32(chb.Name.Substring(chb.Name.Length - 1)) - 1;
            sett.SetKruzhokState(id, true);
        }

        private void FlagKruzhok_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = (CheckBox)sender;
            int id = Convert.ToInt32(chb.Name.Substring(chb.Name.Length - 1)) - 1;
            sett.SetKruzhokState(id, false);
        }

        private void btBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.DefaultDirectory = "\\\\ik18srv\\папкаобмена";
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                savePath.Text = dialog.FileName;
            }
        }

        private void chbSave_Checked(object sender, RoutedEventArgs e)
        {
            savePath.IsEnabled = true;
            btBrowse.IsEnabled = true;
        }

        private void chbSave_Unchecked(object sender, RoutedEventArgs e)
        {
            savePath.IsEnabled = false;
            btBrowse.IsEnabled = false;
        }
    }
}

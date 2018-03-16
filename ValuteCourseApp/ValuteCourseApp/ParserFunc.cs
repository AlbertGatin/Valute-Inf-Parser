using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity;

namespace ValuteCourseApp
{
    class ParserFunc
    {
        public static List<ValuteCourse> GetFromXml() //возвращает в виде списка объектов данные с xml файла
        {
            System.Net.WebClient wc = new System.Net.WebClient();//объект для перевода web страницы в строку
            XDocument xdoc = XDocument.Parse(wc.DownloadString("http://www.cbr.ru/scripts/XML_daily.asp"));//XDocument.Parse принимает строку полученную из преобразования web страницы
            return (from doc in xdoc.Descendants("Valute") // выборка требуемых данных
                          where ((string)doc.Attribute("ID")) == "R01235" || ((string)doc.Attribute("ID")) == "R01239" || ((string)doc.Attribute("ID")) == "R01035"
                    select new ValuteCourse
                          {
                              NumCode = (int)doc.Element("NumCode"),
                              CharCode = doc.Element("CharCode").Value,
                              ValuteName = doc.Element("Name").Value,
                              ValuteValue = doc.Element("Value").Value,
                          }).ToList();
        }
        public static void InsertToDB(List<ValuteCourse> valutes)//отправляет в бд принятые на вход данные
        {
            using (ValuteCourseContext db = new ValuteCourseContext())
            {
                var rows = from o in db.ValuteCourse
                           select o;
                foreach (var row in rows)
                {
                    db.ValuteCourse.Remove(row);
                }
                db.SaveChanges();
                foreach (ValuteCourse val in valutes)
                {

                    db.ValuteCourse.Add(val);
                }
                db.SaveChanges();
            }
        }

        public static List<ValuteCourse> GetFromDB() //возвращает данные из бд в виде списка объектов
        {

            List<ValuteCourse> valtoGrid = new List<ValuteCourse>();
            using (ValuteCourseContext db = new ValuteCourseContext())
            {
                var valutes = db.ValuteCourse;
                foreach (ValuteCourse val in valutes)
                    valtoGrid.Add(new ValuteCourse { NumCode = val.NumCode, CharCode =val.CharCode, ValuteName = val.ValuteName, ValuteValue =val.ValuteValue }); 

            }
            return valtoGrid;        
        }
    }
}

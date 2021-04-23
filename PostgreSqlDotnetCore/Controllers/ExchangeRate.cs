using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PostgreSqlDotnetCore.Models.Evds;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PostgreSqlDotnetCore.Controllers
{


    public class ExchangeRate : Controller
    {
        EvdsEntities db = new EvdsEntities();
        // GET: ExchangeRate
        public ActionResult Index()
        {


            var model = db.KurBilgileri.ToList();
            ViewBag.ParaBirimleri = db.ParaBirimi.ToList();

            return View(model);
        }

        // GET: ExchangeRate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ExchangeRate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExchangeRate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeRate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ExchangeRate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ExchangeRate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ExchangeRate/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateRates(string[] currencies, DateTime startDate, DateTime endDate)
        {
            try
            {

                List<string> birimler = new List<string>();
                foreach (var item in currencies)
                {
                    birimler.Add(item);
                }

                var result = getDataFromEvds(birimler, startDate, endDate);

                List<ExchangeRateInformation> data = new List<ExchangeRateInformation>();
                foreach (var item in result)
                {
                   
                   
                    foreach (JProperty prop in item.ToObject<JObject>().Properties())
                    {
                        ExchangeRateInformation data1 = new ExchangeRateInformation();
                        data1.RateDate = item["Tarih"].ToString();
                        if (prop.Name != "Tarih" && prop.Name != "UNIXTIME")
                        {
                            if (prop.Value != null && !String.IsNullOrEmpty(prop.Value.ToString()))
                            {
                                data1.CurrencyValue = Convert.ToDouble(prop.Value);

                            }
                            foreach (var birim in birimler)
                            {
                                if (prop.Name.Contains(birim))
                                {
                                    data1.CurrencyName = birim;
                                    data.Add(data1);                                   
                                }
                            }
                        }                     
                    }
                    
                }
                db.KurBilgileri.AddRange(data);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }


        public JArray getDataFromEvds(List<string> currencies, DateTime startDate, DateTime enddate)
        {
            var series = "";
            foreach (var item in currencies)
            {
                series = series + "TP.DK." + item + ".A-";
            }
            if (series.EndsWith("-"))
            {
                series = series.Remove(series.Length - 1, 1);
            }

            string sd = startDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
            string ed = enddate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

            //"series=" + series + "TP.DK.USD.A-TP.DK.EUR.A-TP.DK.CHF.A-TP.DK.GBP.A-TP.DK.JPY.A&"
            //"startDate=01-10-2017&" +
            //   "endDate=01-11-2017&" +
            var client = new RestClient("https://evds2.tcmb.gov.tr/service/evds/" +
                 "series=" + series + "&" +
                 "startDate=" + sd + "&" +
                 "endDate=" + ed + "&" +
                 "type=json" +
                 "&key=XTqLFuMNtg");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", "SessionCookie=!w8Pc2WSHuardJF3qkygjtJLA5IKF7V20azCQ1Z40H3/OBsg5HdkFPWz3z4yL7eH6hX2stj+sow16x4E=; TS013c5758=015d31d691b815e2f8e98685b57d665ba1c446688d9cfbb1723e1d867ed165542d1cbc86d3631ca721a7f516a9fc59594372badcfd0e0c916e027e091462bf81810cb7a8be");
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            var result = JsonConvert.DeserializeObject<JObject>(response.Content);
            return (JArray)result["items"];
        }
    }

    public class JsonResponseObject
    {
        public int totalCount { get; set; }
        public List<JsonResponseItems> items { get; set; }
    }
    public class JsonResponseItems
    {
        public Object result { get; set; }
    }
}

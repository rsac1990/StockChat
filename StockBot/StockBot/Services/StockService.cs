using StockBot.Models;
using System;
using System.Net.Http;

namespace StockBot.Services
{
    public class StockService
    {
        private static readonly HttpClient Client = new HttpClient();

        public static StockInfo GetStockInfo(string StockCode)
        {
            string stockAPIUrl = $"https://stooq.com/q/l/?s={StockCode}&f=sd2t2ohlcv&h&e=csv";

            using (HttpResponseMessage stockInfoResponse = Client.GetAsync(stockAPIUrl).Result)
            {
                using (HttpContent responseContent = stockInfoResponse.Content)
                {
                    var callResponse = responseContent.ReadAsStringAsync().Result;

                    if (stockInfoResponse.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        throw new ArgumentException(callResponse);
                    }

                    //remove the csv table header, and keep the values
                    var stockData = callResponse.Substring(callResponse.IndexOf(
                        Environment.NewLine, StringComparison.Ordinal) + 2);

                    var stockDataArray = stockData.Split(',');
                    if (stockDataArray[6].Contains("N/D"))
                    {
                        return null;
                    }
                    return new StockInfo()
                    {
                        Code = stockDataArray[0],
                       
                        Date = !stockDataArray[1].Contains("N/D") ?
                            Convert.ToDateTime(stockDataArray[1]) : default,
                        
                        Time = !stockDataArray[2].Contains("N/D") ?
                            Convert.ToDateTime(stockDataArray[2]).TimeOfDay : default,
                        
                        Open = !stockDataArray[3].Contains("N/D") ?
                            Convert.ToDouble(stockDataArray[3]) : default,
                        
                        High = !stockDataArray[4].Contains("N/D") ?
                            Convert.ToDouble(stockDataArray[4]) : default,
                        
                        Low = !stockDataArray[5].Contains("N/D") ?
                            Convert.ToDouble(stockDataArray[5]) : default,
                       
                        ClosingValue = !stockDataArray[6].Contains("N/D") ?
                            Convert.ToDouble(stockDataArray[6]) : default,
                        
                        Volume = !stockDataArray[7].Contains("N/D") ?
                            Convert.ToDouble(stockDataArray[7]) : default,
                    };
                }
            }

        }

    }
}

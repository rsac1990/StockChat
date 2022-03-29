using StockBot.Models;
using StockBot.Services;
using Xunit;

namespace TestProject
{
    public class StockBotTest
    {

        [Fact]
        public void StockBot_GetStock_ExistingStockCode()
        {
            string stockCode = "btc.v";
            StockInfo stockInfo =   StockService.GetStockInfo(stockCode);  

            //Validates it returns a stockinfo object and the value of the share is greater than 0
            Assert.NotNull(stockInfo);
            Assert.True(stockInfo.ClosingValue > 0);
        }

        [Fact]
        public void StockBot_GetStock_NonExistentStockCode()
        {
            string stockCode = "Hello world";
            StockInfo stockInfo = StockService.GetStockInfo(stockCode);
            Assert.Null(stockInfo);
        }

    }
}
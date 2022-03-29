using System;

namespace StockBot.Models
{
    public class StockInfo
    {      
            public string Code { get; set; }
            public DateTime Date { get; set; }
            public TimeSpan Time { get; set; }
            public double Open { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double ClosingValue { get; set; }
            public double Volume { get; set; }
        
    }
}

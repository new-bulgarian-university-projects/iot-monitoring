using System;
using System.Collections.Generic;
using System.Text;

namespace IoTMon.Models.TimeSeries
{
    public class ChartData
    {
        public ChartData(DateTime date, double value)
        {
            this.Date = date;
            this.Value = value;
        }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}

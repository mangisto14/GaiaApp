
using System;

namespace GaiaApi.Models
{
    public class OperationRecord
    {
        public int Id { get; set; }
        public double FieldA { get; set; }
        public double FieldB { get; set; }
        public string Operation { get; set; }
        public string Result { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

}

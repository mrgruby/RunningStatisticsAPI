using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RunningStatConvertedDto
    {
        public int Id { get; set; }
        public string ActivityId { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public DateTime StartTimeLocal { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double AvgSpeed { get; set; }
        public double MaxSpeed { get; set; }
        public int Steps { get; set; }
        public double Calories { get; set; }
        public double AvgHr { get; set; }
        public double MaxHr { get; set; }
        public double AvgDoubleCadence { get; set; }
        public double MaxDoubleCadence { get; set; }
        public string LocationName { get; set; }
        public int DistanceType { get; set; }
        public DateTime? InsertedTime { get; set; }
    }
}

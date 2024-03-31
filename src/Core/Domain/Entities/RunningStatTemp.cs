using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RunningStatTemp
    {
        public int Id { get; set; }
        public string ActivityId { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        /// <summary>
        /// Unix timestamp - https://www.unixtimestamp.com/index.php
        /// </summary>
        public double StartTimeLocal { get; set; }
        /// <summary>
        /// cm
        /// </summary>
        public double Distance { get; set; }
        /// <summary>
        /// Milliseconds
        /// </summary>
        public double Duration { get; set; }
        /// <summary>
        /// Value from json X 36
        /// </summary>
        public double AvgSpeed { get; set; }
        /// <summary>
        /// Value from json X 36
        /// </summary>
        public double MaxSpeed { get; set; }
        public int Steps { get; set; }

        /// <summary>
        /// KJ
        /// </summary>
        public double Calories { get; set; }
        public int AvgHr { get; set; }
        public int MaxHr { get; set; }
        public int AvgDoubleCadence { get; set; }
        public int MaxDoubleCadence { get; set; }
        public double StartLongitude { get; set; }
        public double StartLatitude { get; set; }
        public string LocationName { get; set; }
    }
}

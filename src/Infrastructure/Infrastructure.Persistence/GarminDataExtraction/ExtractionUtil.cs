using Domain.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GarminDataExtraction
{
    public static class ExtractionUtil
    {
        /// <summary>
        /// Parse JSON data file from Garmin.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<List<RunningStatTemp>> ParseJsonFile(string fileName)
        {
            var extractedData = new List<RunningStatTemp>();
            using (StreamReader r = new StreamReader(@$"D:\VSProjects\RunningStat\GarminData\{fileName}"))
            {
                string json = r.ReadToEnd();
                //Remove the first and last []
                json = json.Substring(1, json.Length - 2);
                var jsonParsedToJObject = JObject.Parse(json);
                IList<JToken> results = jsonParsedToJObject["summarizedActivitiesExport"].Children().ToList();

                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    var searchResult = result.ToObject<RunningStatTemp>();
                    extractedData.Add(searchResult);
                }

            }

            return extractedData;
        }

        //public static List<RunningStatConverted> ConvertData(List<RunningStatTemp> extractedData)
        //{
        //    var runningData = new List<RunningStatConverted>();
        //    foreach (var item in extractedData)
        //    {
        //        runningData.Add(Convert(item));
        //    }

        //    return runningData;
        //}

        /// <summary>
        /// Alot of the data comes in obscure units such as milliseconds, Unix timestamp etc.
        /// This method converts it to readable data.
        /// </summary>
        /// <param name="runningStat"></param>
        /// <returns></returns>
        public static RunningStatConverted Convert(RunningStatTemp runningStat)
        {
            var converted = new RunningStatConverted();
            converted.ActivityId = runningStat.ActivityId;
            converted.Name = runningStat.Name;
            converted.ActivityType = runningStat.ActivityType;
            converted.Duration = Math.Round(runningStat.Duration / 60000, 2);
            converted.Distance = Math.Round(runningStat.Distance / 100000, 2);
            converted.AvgSpeed = Math.Round(runningStat.AvgSpeed * 36, 2);
            converted.MaxSpeed = Math.Round(runningStat.MaxSpeed * 36, 2);
            converted.StartTimeLocal = ConvertFromUnixTimestamp(runningStat.StartTimeLocal / 1000);
            converted.Steps = runningStat.Steps;
            converted.Calories = Math.Round(runningStat.Calories / 4.184);
            converted.AvgHr = runningStat.AvgHr;
            converted.MaxHr = runningStat.MaxHr;
            converted.AvgDoubleCadence = runningStat.AvgDoubleCadence;
            converted.MaxDoubleCadence = runningStat.MaxDoubleCadence;
            converted.StartLatitude = runningStat.StartLatitude;
            converted.StartLongitude = runningStat.StartLongitude;
            converted.LocationName = runningStat.LocationName;
            converted.Id = runningStat.Id;
            converted.DistanceType = CalculateDistanceType(converted);//Calculate the distance type
            converted.InsertedTime = DateTime.Now;
            return converted;
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static int CalculateDistanceType(RunningStatConverted item)
        {
            if (item.ActivityType == "running" || item.ActivityType == "treadmill_running")
            {
                if (item.Distance < 11 && item.Distance > 10)
                {
                    item.DistanceType = 10;
                }
                else if (item.Distance < 10 && item.Distance > 9)
                {
                    item.DistanceType = 9;
                }
                else if (item.Distance < 9 && item.Distance > 8)
                {
                    item.DistanceType = 8;
                }
                else if (item.Distance < 8 && item.Distance > 7)
                {
                    item.DistanceType = 7;
                }
                else if (item.Distance < 7 && item.Distance > 6)
                {
                    item.DistanceType = 6;
                }
                else if (item.Distance < 6 && item.Distance > 5)
                {
                    item.DistanceType = 5;
                }
                else if (item.Distance < 5 && item.Distance > 4)
                {
                    item.DistanceType = 4;
                }
                else if (item.Distance < 4 && item.Distance > 3)
                {
                    item.DistanceType = 3;
                }
                else if (item.Distance < 3 && item.Distance > 2)
                {
                    item.DistanceType = 2;
                }
            }
            else
            {
                item.DistanceType = 0;
            }

            return item.DistanceType;
        }
    }
}

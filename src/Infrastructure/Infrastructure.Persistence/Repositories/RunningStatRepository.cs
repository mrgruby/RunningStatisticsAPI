using Application.Dtos;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.GarminDataExtraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RunningStatRepository : BaseRepository<RunningStatConverted>, IRunningStatRepository
    {
        private readonly RunningStatContext _context;
        public RunningStatRepository(RunningStatContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<RunningStatConverted> GetSpecific(Expression<Func<RunningStatConverted, bool>> predicate)
        {
            return _context.Set<RunningStatConverted>().Where(predicate);
        }

        /// <summary>
        /// Parse JSON data file from Garmin.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<string>> ParseGarminJsonFile(string fileName)
        {
            var message = "";
            var extractedData = await ExtractionUtil.ParseJsonFile(fileName);

            var convertedData = new List<RunningStatConverted>();
            try
            {
                if (extractedData.Any())
                {
                    foreach (var item in extractedData)
                    {
                        convertedData.Add(ExtractionUtil.Convert(item));
                    }
                }
            }
            catch (Exception e)
            {
                return new ServiceResponse<string> { Data = $"There was a problem extracting data from the Garmin file {e}" };
            }

            if (convertedData.Any())
            {
                try
                {
                    var count = 0;
                    var existing = _context.RunningStats.ToList();
                    //Check if the activity already exists in the db, if not, add the new activity - only add new stuff...
                    foreach (var item in convertedData)
                    {
                        var test = existing.Any(i => i.ActivityId == item.ActivityId);
                        if (!test)
                        {
                            _context.RunningStats.Add(item);
                            count++;
                        }
                    }

                    var x = _context.SaveChanges();
                    if (count != 0)
                        message = $"{count} activities inserted successfully!";
                    else
                        message = "Activity list up to date, no records added.";

                }
                catch (Exception e)
                {
                    return new ServiceResponse<string> { Data = $"There was a problem inserting Garmin data in the database {e}", Success = false };
                }
            }

            return new ServiceResponse<string> { Data = message };
        }
    }
}

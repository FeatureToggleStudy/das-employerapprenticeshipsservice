﻿using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SFA.DAS.LevyAggregationProvider.Worker.Model;

namespace SFA.DAS.LevyAggregationProvider.Worker.Providers
{
    public class LevyAggregator
    {
        public DestinationData BuildAggregate(SourceData input)
        {
            var clone = Clone(input.Data);

            //Build aggregate structures
            var aggregates = BuildAggregate(input.Data);

            return new DestinationData
            {
                AccountId = input.AccountId,
                Data = aggregates
            };
        }

        private List<AggregationLine> BuildAggregate(List<SourceDataItem> source)
        {
            var output = new List<AggregationLine>();

            var balance = 0.0m;

            foreach (var item in source)
            {
                balance += item.Amount;

                var existing = output.FirstOrDefault(x => x.LevyItemType == item.LevyItemType && x.Year == item.ActivityDate.Year && x.Month == item.ActivityDate.Month);

                if (existing == null)
                {
                    existing = new AggregationLine
                    {
                        Month = item.ActivityDate.Month,
                        Year = item.ActivityDate.Year,
                        LevyItemType = item.LevyItemType,
                        Amount = item.Amount,
                        Balance = balance,
                        Items = new List<AggregationLineItem>
                        {
                            MapFrom(item)
                        }
                    };
                    output.Add(existing);
                }
                else
                {
                    existing.Balance = balance;
                    existing.Items.Add(MapFrom(item));
                }
            }

            return output;
        }

        private AggregationLineItem MapFrom(SourceDataItem item)
        {
            return new AggregationLineItem
            {
                Id = item.Id,
                EmpRef = item.EmpRef,
                ActivityDate = item.ActivityDate,
                Amount = item.Amount,
                LevyItemType = item.LevyItemType
            };
        }

        private List<SourceDataItem> Clone(List<SourceDataItem> source)
        {
            var json = JsonConvert.SerializeObject(source);

            return JsonConvert.DeserializeObject<List<SourceDataItem>>(json);
        }
    }
}
using AutoMapper;
using Organizations.Business.Abstraction.Services;
using Organizations.Business.Models.DTOs.Statistic;
using Organizations.Data.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Services
{
	public class ReadDataSummarizer : IReadDataSummarizer
	{
		public ReadDataSummaryDTO CreateSummary<T>(ICollection<T> data)
		{
			return new ReadDataSummaryDTO { ReadData = data.Count };
		}
	}
}

using Organizations.Business.Models.DTOs.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Business.Abstraction.Services
{
	public interface IReadDataSummarizer
	{
		public ReadDataSummaryDTO CreateSummary<T>(ICollection<T> data);
	}
}

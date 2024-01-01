using DataImporting.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporting.Services
{
	public class IndustriesNormalizer : IIndustriesNormalizer
	{
		public IEnumerable<string>? NormalizeIndustries(string rawIndustries)
		{
			var normalizedIndustries = Enumerable.Empty<string>();

			rawIndustries = rawIndustries.Replace("\\s+", " ");

			normalizedIndustries = rawIndustries.Split(" / ").AsEnumerable();

			return normalizedIndustries;
		}
	}
}

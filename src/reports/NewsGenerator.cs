using System.Collections.Generic;

namespace OOD_24L_01180689.src.reports
{
    public class NewsGenerator : IIterator<string>
    {
        private readonly List<NewsProvider> providers;
        private readonly List<IReportable> reporters;
        private int providerIndex = 0;
        private int reporterIndex = 0;

        public NewsGenerator(List<NewsProvider> providers, List<IReportable> reporters)
        {
            this.providers = providers;
            this.reporters = reporters;
        }

        public bool HasNext()
        {
            return providerIndex < providers.Count && reporterIndex < reporters.Count;
        }

        public string Next()
        {
            var news = providers[providerIndex].Report(reporters[reporterIndex]);
            reporterIndex++;
            if (reporterIndex >= reporters.Count)
            {
                reporterIndex = 0;
                providerIndex++;
            }

            return news;
        }

        public void Reset()
        {
            providerIndex = 0;
            reporterIndex = 0;
        }
    }
}
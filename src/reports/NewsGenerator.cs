using OOD_24L_01180689.src.dataStorage;
using OOD_24L_01180689.src.dto.reports.reporters;
using OOD_24L_01180689.src.dto.reports.reporters.reporters;

namespace OOD_24L_01180689.src.reports;

public class NewsGenerator : IIterator<string>
{
    private readonly List<NewsProvider> providers;
    private readonly List<IReportable> reporters;
    private int providerIndex;
    private int reporterIndex;

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

    public static void InitializeProviders()
    {
        DataStorage.GetInstance.addNewsProvider(new Television("Abelian Television"));
        DataStorage.GetInstance.addNewsProvider(new Television("Channel TV-Tensor"));

        DataStorage.GetInstance.addNewsProvider(new Radio("Quantifier radio"));
        DataStorage.GetInstance.addNewsProvider(new Radio("Shmem radio"));

        DataStorage.GetInstance.addNewsProvider(new Newspaper("Categories Journal"));
        DataStorage.GetInstance.addNewsProvider(new Newspaper("Polytechnical Gazette"));
    }
}
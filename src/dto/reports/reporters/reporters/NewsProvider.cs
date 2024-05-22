namespace OOD_24L_01180689.src.dto.reports.reporters.reporters;

public abstract class NewsProvider
{
    public string name;

    public NewsProvider(string name)
    {
        this.name = name;
    }

    public abstract string Report(IReportable reportable);
}
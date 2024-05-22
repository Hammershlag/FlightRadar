namespace OOD_24L_01180689.src.dto.reports.reporters;

public interface IReportable
{
    void Accept(INewsVisitor visitor);
}
namespace mptc.dgc.sample.application.Interfaces
{
    public interface IImportStrategy
    {
        Task ImportAsync(Stream fileStream);
    }
}

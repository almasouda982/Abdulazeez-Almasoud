namespace University.Repositories.Base
{
    public interface IUnitOfWork
    {
        IWaitlistEntryRepository WaitlistEntryRepository { get; }
    }
}

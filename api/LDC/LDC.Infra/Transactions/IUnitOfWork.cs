namespace LDC.Infra.Transactions
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}

using System;

namespace WebReferenceSite.Mvc.Repositories.DapperWrapper
{
    public interface ITransactionScope : IDisposable
    {
        void Complete();
    }
}

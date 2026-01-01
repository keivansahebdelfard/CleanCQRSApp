using System.Threading;
using System.Threading.Tasks;

namespace MyApp.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// شروع تراکنش
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Commit تغییرات و پایان تراکنش
        /// </summary>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rollback تغییرات در صورت خطا
        /// </summary>
        Task RollbackAsync();

        /// <summary>
        /// ذخیره تغییرات بدون Commit تراکنش
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

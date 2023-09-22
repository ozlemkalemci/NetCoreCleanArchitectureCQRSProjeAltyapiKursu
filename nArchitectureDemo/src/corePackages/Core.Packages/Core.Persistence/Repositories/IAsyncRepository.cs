using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories
{
    // IAsyncRepository adlı bir arabirim (interface) tanımlanıyor.
    public interface IAsyncRepository<TEntity, TEntityId> : IQuery<TEntity>
        where TEntity : Entity<TEntityId>
    {
        // Belirli bir koşula göre bir varlık almak için kullanılır.
        Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate, // Koşulu ifade eden bir fonksiyon
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // İlişkili varlıkları içe almak için
            bool withDeleted = false, // Silinmiş varlıkları dahil etmek için
            bool enableTracking = true, // İzleme (tracking) özelliğini etkinleştirmek için
            CancellationToken cancellationToken = default // İşlemi iptal etmek için bir işaretçi
        );

        // Birden çok varlığı liste halinde almak için kullanılır.
        Task<Paginate<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null, // Koşulu belirlemek için (isteğe bağlı)
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, // Verilerin nasıl sıralanacağını belirlemek için (isteğe bağlı)
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // İlişkili varlıkları içe almak için (isteğe bağlı)
            int index = 0, // Sayfa numarası
            int size = 10, // Sayfa boyutu
            bool withDeleted = false, // Silinmiş varlıkları dahil etmek için
            bool enableTracking = true, // İzleme (tracking) özelliğini etkinleştirmek için
            CancellationToken cancellationToken = default // İşlemi iptal etmek için bir işaretçi
        );

        // Dinamik bir sorguya göre bir varlık listesi almak için kullanılır.
        Task<Paginate<TEntity>> GetListByDynamicAsync(
            DynamicQuery dynamic, // Dinamik sorgu nesnesi
            Expression<Func<TEntity, bool>>? predicate = null, // Koşulu belirlemek için (isteğe bağlı)
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // İlişkili varlıkları içe almak için (isteğe bağlı)
            int index = 0, // Sayfa numarası
            int size = 10, // Sayfa boyutu
            bool withDeleted = false, // Silinmiş varlıkları dahil etmek için
            bool enableTracking = true, // İzleme (tracking) özelliğini etkinleştirmek için
            CancellationToken cancellationToken = default // İşlemi iptal etmek için bir işaretçi
        );

        // Veri var mı yok mu sorgusu için kullanılır.
        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>>? predicate = null, // Koşulu belirlemek için (isteğe bağlı)
            bool withDeleted = false, // Silinmiş varlıkları dahil etmek için
            bool enableTracking = true, // İzleme (tracking) özelliğini etkinleştirmek için
            CancellationToken cancellationToken = default // İşlemi iptal etmek için bir işaretçi
        );

        // Bu metot, veritabanına yeni bir varlık (TEntity) eklemek için kullanılır.
        Task<TEntity> AddAsync(TEntity entity);

        // Bu metot, birden çok varlığı (ICollection<TEntity>) veritabanına eklemek için kullanılır.
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

        // Bu metot, varlık (TEntity) üzerinde yapılan değişiklikleri veritabanına kaydetmek için kullanılır.
        Task<TEntity> UpdateAsync(TEntity entity);

        // Bu metot, birden çok varlığın (ICollection<TEntity>) üzerinde yapılan değişiklikleri veritabanına kaydetmek için kullanılır.
        Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);

        // Bu metot, belirtilen varlığı (TEntity) veritabanından silmek için kullanılır.
        Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false);

        // Bu metot, birden çok varlığın (ICollection<TEntity>) veritabanından silmek için kullanılır.
        Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false);
    }
}

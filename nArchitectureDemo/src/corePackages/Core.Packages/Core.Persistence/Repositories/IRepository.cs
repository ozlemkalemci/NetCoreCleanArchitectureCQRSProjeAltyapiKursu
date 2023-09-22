using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories;

public interface IRepository<TEntity, TEntityId> : IQuery<TEntity>
        where TEntity : Entity<TEntityId>
{
    TEntity? Get(
        Expression<Func<TEntity, bool>> predicate, // Koşulu ifade eden bir fonksiyon
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // İlişkili varlıkları içe almak için
        bool withDeleted = false, // Silinmiş varlıkları dahil etmek için
        bool enableTracking = true // İzleme (tracking) özelliğini etkinleştirmek için
   );

    Paginate<TEntity> GetList(
        Expression<Func<TEntity, bool>>? predicate = null, // Koşulu belirlemek için (isteğe bağlı)
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, // Verilerin nasıl sıralanacağını belirlemek için (isteğe bağlı)
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // İlişkili varlıkları içe almak için (isteğe bağlı)
        int index = 0, // Sayfa numarası
        int size = 10, // Sayfa boyutu
        bool withDeleted = false, // Silinmiş varlıkları dahil etmek için
        bool enableTracking = true // İzleme (tracking) özelliğini etkinleştirmek için
     );

    Paginate<TEntity> GetListByDynamic(
        DynamicQuery dynamic, // Dinamik sorgu nesnesi
        Expression<Func<TEntity, bool>>? predicate = null, // Koşulu belirlemek için (isteğe bağlı)
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // İlişkili varlıkları içe almak için (isteğe bağlı)
        int index = 0, // Sayfa numarası
        int size = 10, // Sayfa boyutu
        bool withDeleted = false, // Silinmiş varlıkları dahil etmek için
        bool enableTracking = true // İzleme (tracking) özelliğini etkinleştirmek için
     );

    bool Any(
        Expression<Func<TEntity, bool>>? predicate = null, // Koşulu belirlemek için (isteğe bağlı)
        bool withDeleted = false, // Silinmiş varlıkları dahil etmek için
        bool enableTracking = true // İzleme (tracking) özelliğini etkinleştirmek için
       );

    TEntity Add(TEntity entity);

    ICollection<TEntity> AddRange(ICollection<TEntity> entities);

    TEntity Update(TEntity entity);

    ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);

    TEntity Delete(TEntity entity, bool permanent = false);

    ICollection<TEntity> DeleteRange(ICollection<TEntity> entity, bool permanent = false);

}

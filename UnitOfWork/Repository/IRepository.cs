using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Collections;

namespace UnitOfWork.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        void ChangeTable(string table);
        /// <summary>
        /// Lấy giá trị theo phân trang
        /// </summary>
        IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         int pageIndex = 0,
                                         int pageSize = 20,
                                         bool disableTracking = true,
                                         bool ignoreQueryFilters = false);
        /// <summary>
        /// Lấy giá trị theo phân trang
        /// </summary>
        Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                    int pageIndex = 0,
                                                    int pageSize = 20,
                                                    bool disableTracking = true,
                                                    CancellationToken cancellationToken = default(CancellationToken),
                                                    bool ignoreQueryFilters = false);
        /// <summary>
        /// Lấy giá trị theo phân trang
        /// </summary>
        IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                  Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  int pageIndex = 0,
                                                  int pageSize = 20,
                                                  bool disableTracking = true,
                                                  bool ignoreQueryFilters = false) where TResult : class;
        /// <summary>
        /// Lấy giá trị theo phân trang
        /// </summary>
        Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
                                                             Expression<Func<TEntity, bool>> predicate = null,
                                                             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                             int pageIndex = 0,
                                                             int pageSize = 20,
                                                             bool disableTracking = true,
                                                             CancellationToken cancellationToken = default(CancellationToken),
                                                             bool ignoreQueryFilters = false) where TResult : class;
        /// <summary>
        /// Lấy giá trị duy nhất đầu tiên
        /// </summary>
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool disableTracking = true,
                                  bool ignoreQueryFilters = false);
        /// <summary>
        /// Lấy giá trị duy nhất đầu tiên
        /// </summary>
        TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
                                           Expression<Func<TEntity, bool>> predicate = null,
                                           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                           bool disableTracking = true,
                                           bool ignoreQueryFilters = false);
        /// <summary>
        /// Lấy giá trị duy nhất đầu tiên
        /// </summary>
        Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);
        /// <summary>
        /// Lấy giá trị duy nhất đầu tiên
        /// </summary>
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true,
            bool ignoreQueryFilters = false);
        /// <summary>
        /// Dùng câu lệnh SQL
        /// </summary>
        IQueryable<TEntity> FromSql(string sql, params object[] parameters);
        /// <summary>
        /// Tìm kiếm
        /// </summary>
        TEntity Find(params object[] keyValues);
        /// <summary>
        /// Tìm kiếm bất đồng bộ
        /// </summary>
        ValueTask<TEntity> FindAsync(params object[] keyValues);
        /// <summary>
        /// Tìm kiếm bất đồng bộ
        /// </summary>
        ValueTask<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken);
        /// <summary>
        /// lấy tất cả dữ liệu
        /// </summary>
        IQueryable<TEntity> GetAll();
        /// <summary>
        /// lấy tất cả dữ liệu có điều kiện
        /// </summary>
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null,
                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                          bool disableTracking = true,
                                                          bool ignoreQueryFilters = false);
        /// <summary>
        /// Lấy tất cả giá trị
        /// </summary>
        Task<IList<TEntity>> GetAllAsync();
        /// <summary>
        /// Lấy tất cả dữ liệu Bất đồng bộ
        /// </summary>
        Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                          bool disableTracking = true,
                                                          bool ignoreQueryFilters = false);
        /// <summary>
        /// Tính Tổng
        /// </summary>
        int Count(Expression<Func<TEntity, bool>> predicate = null);
        /// <summary>
        /// Tinh tổng bất đồng bộ
        /// </summary>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        /// <summary>
        /// Số phần tử trong chuối thỏa mãn điều kiện 
        /// </summary>
        long LongCount(Expression<Func<TEntity, bool>> predicate = null);
        /// <summary>
        /// Số phần tử trong chuối thỏa mãn điều kiện
        /// </summary>
        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate = null);
        /// <summary>
        /// giá trị lớn nhất
        /// </summary>
        T Max<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);
        /// <summary>
        /// Giá trị lớn nhất bất đồng bộ
        /// </summary>
        Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);
        /// <summary>
        /// Giá trị nhỏ nhất
        /// </summary>
        T Min<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);
        /// <summary>
        /// Giá trị nhỏ nhất bất đồng bộ
        /// </summary>
        Task<T> MinAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);
        /// <summary>
        /// Tính trung bình
        /// </summary>
        decimal Average(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);
        /// <summary>
        /// Tính trung bình bất đồng bộ
        /// </summary>
        Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);
        /// <summary>
        /// Tính tổng
        /// </summary>
        decimal Sum(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);
        /// <summary>
        /// Tính tổng bất đồng bộ
        /// </summary>
        Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);
        /// <summary>
        /// Kiểm tra tồn tại
        /// </summary>
        bool Exists(Expression<Func<TEntity, bool>> selector = null);
        /// <summary>
        /// Kiểm tra tồn tại Bất đồng bộ
        /// </summary>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null);
        /// <summary>
        /// Tạo mới 1 bản ghi
        /// </summary>
        TEntity Insert(TEntity entity);
        /// <summary>
        /// Tạo mới nhiều bản ghi theo tham số mảng
        /// </summary>
        void Insert(params TEntity[] entities);
        /// <summary>
        /// Tạo mới nhiều bản ghi
        /// </summary>
        void Insert(IEnumerable<TEntity> entities);
        /// <summary>
        /// Tạo mới bản ghi
        /// </summary>
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Tạo mới nhiều bản ghi theo mảng truyền vào
        /// </summary>
        Task InsertAsync(params TEntity[] entities);
        /// <summary>
        /// Tạo mới bản ghi bất đồng bộ
        /// </summary>
        Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        void Update(TEntity entity);
        /// <summary>
        /// Cập nhật nhiều bản ghi theo mảng
        /// </summary>
        void Update(params TEntity[] entities);
        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        void Update(IEnumerable<TEntity> entities);
        /// <summary>
        /// Xóa bản ghi theo id
        /// </summary>
        void Delete(object id);
        /// <summary>
        /// Xóa một bản ghi
        /// </summary>
        void Delete(TEntity entity);
        /// <summary>
        /// Xóa nhiều bản ghi theo mảng truyền vào
        /// </summary>
        void Delete(params TEntity[] entities);
        /// <summary>
        /// Xóa nhiều bản ghi theo dạng list
        /// </summary>
        void Delete(IEnumerable<TEntity> entities);
        void ChangeEntityState(TEntity entity, EntityState state);
    }
}
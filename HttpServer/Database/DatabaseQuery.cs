using System.Reflection;
using HttpServer.Database.Tables;

namespace HttpServer.Database;

public partial class Database
{
    private Dictionary<Type, (ReaderWriterLockSlim,List<object>)> _readerWriterLocks;

    public void Get<T>(Guid id) where T : IEntity
    {
        BlockRead<T>(() =>
        {
            _readerWriterLocks[typeof(T)].Item2.FirstOrDefault(itemInList => (itemInList as IEntity).Id == id);
        });
    }

    public void Add<T>(T item) where T : IEntity
    {
        BlockWrite<T>(() =>
        {
            _readerWriterLocks[typeof(T)].Item2.Add(item);
        });
    }

    public void Remove<T>(T item) where T : IEntity
    {
        BlockWrite<T>(() =>
        {
            _readerWriterLocks[typeof(T)].Item2.Remove(item);
        });
    }

    public void Update<T>(T item) where T : IEntity
    {
        BlockWrite<T>(() =>
        {
            var itemInList = _readerWriterLocks[typeof(T)].Item2
                .FirstOrDefault(itemInList => (itemInList as IEntity).Id == item.Id);
        });
    }

    private void BlockRead<T>(Action methodToExecuteInsideThread)
    {
        _readerWriterLocks[typeof(T)].Item1.EnterReadLock();
        methodToExecuteInsideThread();
        _readerWriterLocks[typeof(T)].Item1.ExitReadLock();
    }

    private void BlockWrite<T>(Action methodToExecuteInsideThread)
    {
        _readerWriterLocks[typeof(T)].Item1.EnterWriteLock();
        methodToExecuteInsideThread();
        _readerWriterLocks[typeof(T)].Item1.ExitWriteLock();
    }

    private List<object> GetTableByType(Type entityType)
    {
        var table = this
            .GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            .FirstOrDefault(field => field.FieldType.GetGenericTypeDefinition() == typeof(List<>)
                                     && field.FieldType.GenericTypeArguments[0] == entityType) 
                    ?? throw new ArgumentException("Database List not set yet.");
        
        return table.GetValue(this) as List<object>;
    }
}
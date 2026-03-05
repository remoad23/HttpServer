using HttpServer.Database.Tables;

namespace HttpServer.Singleton;

// TODO irgendwie einen readerWriterLock für jede Liste anlegen. 
public static class DBThreadSafeHelper
{
    private static readonly ReaderWriterLockSlim _readerWriterLock;
    
    public static void Get<T>(Guid id,List<T> list) where  T : IEntity
    {
        BlockRead(() =>
        {
            var itemInList = list.FirstOrDefault(itemInList => itemInList.Id == id);
        });
    }
    
    public static void Add<T>(T item,List<T> list) where  T : IEntity
    {
        BlockWrite(() =>
        {
            list.Add(item);
        });
    }

    public static void Remove<T>(T item,List<T> list) where  T : IEntity
    {
        BlockWrite(() =>
        {
            list.Remove(item);
        });
    }

    public static void Update<T>(T item,List<T> list) where  T : IEntity
    {
        BlockWrite(() =>
        {
            var itemInList = list.FirstOrDefault(itemInList => itemInList.Id == item.Id);
        });
    }

    private static void BlockRead(Action methodToExecuteInsideThread)
    {
        _readerWriterLock.EnterReadLock();
        methodToExecuteInsideThread();
        _readerWriterLock.ExitReadLock();
    }

    private static void BlockWrite(Action methodToExecuteInsideThread)
    {
        _readerWriterLock.EnterWriteLock();
        methodToExecuteInsideThread();
        _readerWriterLock.ExitWriteLock();
    }
}
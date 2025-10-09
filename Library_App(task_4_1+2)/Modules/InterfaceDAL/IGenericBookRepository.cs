namespace Library_App_task_4_1_2_.Modules.InterfaceDAL
{
    public interface IGenericBookRepository<T> : IDisposable
    {
        ICollection<T> GetBooks();
        T? GetBookById(int searchIdid);
        bool CreateBook(T newBook);
        bool UpdateBookById(int updateId, T updatedBook);
        bool RemoveBookById(int removalId);
    }
}

namespace Library_App_task_4_1_2_.Modules.InterfaceDAL
{
    public interface IGenericBookRepository<T> : IDisposable
    {
        ICollection<T> GetBooks();
        T? GetBookById(int id);
        bool CreateBook(T book);
        bool UpdateBookById(int id,T book);
        bool RemoveBookById(int id);
    }
}

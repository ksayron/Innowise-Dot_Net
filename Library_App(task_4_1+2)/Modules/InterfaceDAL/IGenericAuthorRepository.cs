using Library_App_task_4_1_2_.Modules.Entities;

namespace Library_App_task_4_1_2_.Modules.InterfaceDAL
{
    public interface IGenericAuthorRepository<T> : IDisposable
    {
        ICollection<T> GetAuthors();
        T? GetAuthorById(int id);
        bool CreateAuthor(T author);
        bool UpdateAuthorById(int id,T author);
        bool RemoveAuthorById(int id);
    }
}

using Library_App_task_4_1_2_.Modules.Entities;

namespace Library_App_task_4_1_2_.Modules.InterfaceDAL
{
    public interface IGenericAuthorRepository<T> : IDisposable
    {
        ICollection<T> GetAuthors();
        T? GetAuthorById(int searchId);
        bool CreateAuthor(T newAuthor);
        bool UpdateAuthorById(int updateId, T updatedAuthor);
        bool RemoveAuthorById(int removalId);
    }
}

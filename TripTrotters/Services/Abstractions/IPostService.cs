using TripTrotters.Models;

namespace TripTrotters.Services.Abstractions
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll();
        Task<Post> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetAllbyUser(string UserName);
        Task<IEnumerable<Post>> GetAllbyApartment(int ApartmentID);

        bool Add(Post p);
        bool Update(Post p);
        bool Delete(Post p);
        bool Save();
    }
}

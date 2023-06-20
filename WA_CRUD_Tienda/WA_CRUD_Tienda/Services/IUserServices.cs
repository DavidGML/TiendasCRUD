using WA_CRUD_Tienda.Models.Request;
using WA_CRUD_Tienda.Models.Response;

namespace WA_CRUD_Tienda.Services
{
    public interface IUserServices
    {
        UserResponse Auth(LoginRequest model);
    }
}

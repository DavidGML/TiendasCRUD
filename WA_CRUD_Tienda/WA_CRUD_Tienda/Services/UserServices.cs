using WA_CRUD_Tienda.Models;
using WA_CRUD_Tienda.Models.Request;
using WA_CRUD_Tienda.Models.Response;
using WA_CRUD_Tienda.Utilidades;

namespace WA_CRUD_Tienda.Services
{
    public class UserServices : IUserServices
    {
        public UserResponse Auth(LoginRequest model)
        {
            UserResponse userResponse = new UserResponse();
            using (var db = new TiendasContext())
            {
                string spassword = Encriptacion.GetSHA256(model.password);
                var usuario = db.Usuarios.Where(c=>c.EmailUsuario == model.email && c.PassUsuario == model.password).FirstOrDefault();

                if(usuario == null) return null;
                userResponse.Email = usuario.EmailUsuario;
            }
            return userResponse;
        }
    }
}

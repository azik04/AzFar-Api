using Final.DAL.Repositories;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Roles;

public class RoleService : IRoleService
{
    private readonly IBaseRepository<Role> _role;

    public RoleService(IBaseRepository<Role> role)
    {
        _role = role;
    }
    public  IBaseResponse<List<Role>> GetRoles()
    {
        try
        {
            var role = _role.GetAll().ToList();
            return new BaseResponse<List<Role>>()
            {
                Data = role,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Role>>()
            {
                Description = $"[GetRoles] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}

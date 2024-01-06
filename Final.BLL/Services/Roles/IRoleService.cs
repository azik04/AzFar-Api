using Final.Domain.Entity;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Roles;

public interface IRoleService
{
    IBaseResponse<List<Role>> GetRoles();
}

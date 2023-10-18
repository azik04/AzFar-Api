using Final.DAL.Repositories;
using Final.DAL.Repositories.Stadiums;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Stadiums;

public class StadiumService : IStadiumService
{
    private readonly IBaseRepository<Stadium> _stadiumRepository;

    public StadiumService(IBaseRepository<Stadium> stadiumRepository)
    {
        _stadiumRepository = stadiumRepository;
    }
    public async Task<IBaseResponse<Stadium>> GetStadium(long id)
    {
        try
        {
            var stadium = _stadiumRepository.GetAll().SingleOrDefault(x => x.Id == id);

            var data = new Stadium()
            {
                Adress = stadium.Adress,
                Name = stadium.Name,
                Avatar = stadium.Avatar,
            };

            return new BaseResponse<Stadium>()
            {
                StatusCode = StatusCode.OK,
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<Stadium>()
            {
                Description = $"[GetStadium] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        } 
    }

        public async Task<IBaseResponse<Stadium>> CreateStadium(Stadium model, byte[] imageData)
        {
            try
            {
                var Stadium = new Stadium()
                {
                    Name = model.Name,
                    Avatar = imageData,
                    Adress = model.Adress,
                };
                await _stadiumRepository.Create(Stadium);

                return new BaseResponse<Stadium>()
                {
                    StatusCode = StatusCode.OK,
                    Data = Stadium
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Stadium>()
                {
                    Description = $"[CreateStadium] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    public async Task<IBaseResponse<bool>> DeleteStadium(long id)
    {
        try
        {
            var stadium = _stadiumRepository.GetAll().FirstOrDefault(x => x.Id == id);

            await _stadiumRepository.Delete(stadium);

            return new BaseResponse<bool>()
            {
                Data = true,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>()
            {
                Description = $"[DeleteStadium] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public IBaseResponse<List<Stadium>> GetStadiums()
    {
        try
        {
            var stadium = _stadiumRepository.GetAll().ToList();
            return new BaseResponse<List<Stadium>>()
            {
                Data = stadium,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Stadium>>()
            {
                Description = $"[GetStadiums] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

}

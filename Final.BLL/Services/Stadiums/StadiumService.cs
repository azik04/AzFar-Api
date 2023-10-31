using Final.DAL.Repositories;
using Final.DAL.Repositories.Stadiums;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using Final.Domain.ViewModel.Stadiums;
using Microsoft.AspNetCore.Http;
using static Final.Domain.Response.IBaseResponse;

namespace Final.BLL.Services.Stadiums;

public class StadiumService : IStadiumService
{
    private readonly IBaseRepository<Stadium> _stadiumRepository;
    private readonly IBaseRepository<StadiumPhotos> _stadiumPhotosRepository;
    public StadiumService(IBaseRepository<Stadium> stadiumRepository, IBaseRepository<StadiumPhotos> stadiumPhotosRepository)
    {
        _stadiumRepository = stadiumRepository;
        _stadiumPhotosRepository = stadiumPhotosRepository;
    }
    public async Task<IBaseResponse<StadiumViewModel>> GetStadium(long id)
    {
        try
        {
            var stadium =  _stadiumRepository.GetAll().FirstOrDefault(x => x.Id == id);
            var img = _stadiumPhotosRepository.GetAll().ToList().Where(x => x.StadiumId == id);

            var data = new StadiumViewModel()
            {
                Adress = stadium.Adress,
                Name = stadium.Name,
                StadiumPhotos = img.ToList()
            };

            return new BaseResponse<StadiumViewModel>()
            {
                StatusCode = StatusCode.OK,
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<StadiumViewModel>()
            {
                Description = $"[GetStadium] : {ex.Message}",
                StatusCode = StatusCode.InternalServerError
            };
        } 
    }

        public async Task<IBaseResponse<Stadium>> CreateStadium(StadiumViewModel model, IFormFile StadiumPhoto)
        {
            try
            {
                var Stadium = new Stadium()
                {
                    Name = model.Name,
                    Adress = model.Adress,
                };
                await _stadiumRepository.Create(Stadium);

                var StadiumPphoto = new StadiumPhotos()
                {
                    StadiumId = model.Id,
                    PhotoPath = StadiumPhoto.FileName
                };
                await _stadiumPhotosRepository.Create(StadiumPphoto);
                string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(StadiumPhoto.FileName);
                using (Stream stream = new FileStream("wwwroot/Files/" + fileName, FileMode.Create))
                {
                    StadiumPhoto.CopyTo(stream);
                }
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
            var img = _stadiumPhotosRepository.GetAll().ToList().Where(x => x.StadiumId == id);
            await _stadiumRepository.Delete(stadium);
            foreach (var item in img)
            {
                await _stadiumPhotosRepository.Delete(item);
            }
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
            foreach (var item in stadium)
            {
                var img = _stadiumPhotosRepository.GetAll().ToList().Where(x => x.StadiumId == item.Id);
                item.Img = img.ToList();
            }
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

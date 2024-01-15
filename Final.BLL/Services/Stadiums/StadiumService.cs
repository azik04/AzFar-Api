using Final.DAL.Repositories;
using Final.DAL.Repositories.Stadiums;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using Final.Domain.ViewModel.Stadiums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
            var firstPhotoPath = _stadiumPhotosRepository
            .GetAll()
            .Where(x => x.StadiumId == id)
            .Select(x => x.StadiumPhoto)
            .FirstOrDefault();

            var data = new StadiumViewModel()
            {
                Adress = stadium.Adress,
                Name = stadium.Name,
                StadiumPhoto = new List<IFormFile>()
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

    public async Task<IBaseResponse<Stadium>> CreateStadium(StadiumViewModel model)
    {
        try
        {
            var stadium = new Stadium()
            {
                Name = model.Name,
                Adress = model.Adress,
            };

            await _stadiumRepository.Create(stadium);

            foreach (var item in model.StadiumPhoto)
            {
                var uploadDirectory = Path.Combine("wwwroot", "upload");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(item.FileName);
                var saveFilePath = Path.Combine(uploadDirectory, fileName);
                using (var stream = new FileStream(saveFilePath, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
                var stadiumPhotoEntity = new StadiumPhotos()
                {
                    StadiumId = stadium.Id,
                    StadiumPhoto = fileName
                };

                await _stadiumPhotosRepository.Create(stadiumPhotoEntity);
            }

            return new BaseResponse<Stadium>()
            {
                StatusCode = StatusCode.OK,
                Data = stadium
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
                item.StadiumPhoto = _stadiumPhotosRepository.GetAll().Where(x => x.StadiumId == item.Id).ToList();
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

using Final.DAL.Repositories;
using Final.DAL.Repositories.Stadiums;
using Final.Domain.Entity;
using Final.Domain.Enum;
using Final.Domain.Response;
using Final.Domain.ViewModel.Stadiums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<IBaseResponse<Stadium>> CreateStadium(StadiumViewModel model, IFormFile stadiumPhoto)
    {
        try
        {
            if (model == null || stadiumPhoto == null || stadiumPhoto.Length == 0)
            {
                return new BaseResponse<Stadium>()
                {
                    Description = "Invalid input parameters",
                    StatusCode = StatusCode.InternalServerError
                };
            }

            var stadium = new Stadium()
            {
                Name = model.Name,
                Adress = model.Adress,
            };

            await _stadiumRepository.Create(stadium);

            var saveFilePath = Path.Combine("c:\\t\\", Guid.NewGuid().ToString() + Path.GetExtension(stadiumPhoto.FileName));

            // File handling
            try
            {
                using (var stream = new FileStream(saveFilePath, FileMode.Create))
                {
                    await stadiumPhoto.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Stadium>()
                {
                    Description = $"Error saving stadium photo: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }

            var stadiumPhotoEntity = new StadiumPhotos()
            {
                StadiumId = stadium.Id,
                PhotoPath = Guid.NewGuid().ToString() + Path.GetExtension(stadiumPhoto.FileName)
            };

            await _stadiumPhotosRepository.Create(stadiumPhotoEntity);

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

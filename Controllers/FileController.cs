using System;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using HelperDesk.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace HelperDesk.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _repo;
        private readonly  IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        public FileController(IFileRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._cloudinaryConfig = cloudinaryConfig;
            this._repo = repo;
            this._mapper = mapper;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _repo.GetFile(id);

            return Ok(file);
        }

        [HttpPost("add/{managamentId}")]
        public async Task<IActionResult> AddFilePosition(int managamentId, FileForCreationDto fileForCreationDto)
        {

            var file = fileForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0) 
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)                         
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            fileForCreationDto.Url = uploadResult.Url.ToString();
            fileForCreationDto.PublicId = uploadResult.PublicId;

            var tmpFile = _mapper.Map<File>(fileForCreationDto);
            tmpFile.isMain = true;
            tmpFile.ManagementId = managamentId;

            var fileId = await _repo.Add(tmpFile);
            
            return Ok(fileId);
        }
    }
}
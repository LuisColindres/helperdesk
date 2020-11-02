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

        [HttpGet]
        public async Task<IActionResult> GetFiles() 
        {
            var files = await _repo.List();

            return Ok(files);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFile(int id)
        {
            var file = await _repo.GetFile(id);

            return Ok(file);
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateFile(int id,
            [FromForm]FileForUpdateDto fileForCreationDto)
        {
            var fileDB = await _repo.GetFile(id);

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
            fileForCreationDto.Description = "Archivo Actualizado correctamente";


            var editedFile = await _repo.Edit(fileForCreationDto, id);
            
            return Ok(editedFile);

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddFilePosition(
            [FromForm]FileForCreationDto fileForCreationDto)
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

            var tmpFile = new File();
            tmpFile.Url = fileForCreationDto.Url;
            tmpFile.Description = "Archivo Cargado Correctamente";
            tmpFile.isMain = true;
            tmpFile.DateAdded = DateTime.Now;
            tmpFile.PublicId = "1";

            // var tmpFile = _mapper.Map<File>(fileForCreationDto);
            // tmpFile.isMain = true;
            // tmpFile.ManagementId = managamentId;

            var fileId = await _repo.Add(tmpFile);
            
            return Ok(fileId);
        }

        [HttpPut("delete")]
        public async Task<IActionResult> delete(FileForUpdateDto file)
        {
            var resp = await _repo.Remove(file.Id);

            return Ok();
        }
    }
}
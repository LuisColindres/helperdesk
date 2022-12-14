using System;
using System.Threading.Tasks;
using AutoMapper;
using HelperDesk.API.Data;
using HelperDesk.API.Dtos;
using HelperDesk.API.Models;
using HelperDesk.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using System.ComponentModel;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace HelperDesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly IManagamentRepository _repo;
        private readonly IFileRepository _repoFile;
        private readonly IUserRepository _repoUser;
        public readonly IEmailRepository _repoEmail;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        static bool mailSent = false;

        public ManagementController(IManagamentRepository repo, IUserRepository repoUser, IEmailRepository repoEmail, IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig, IFileRepository repoFile)
        {
            _repo = repo;
            _repoUser = repoUser;
            _repoEmail = repoEmail;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            _repoFile = repoFile;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpGet]
        public async Task<IActionResult> GetManagements()
        {
            var managaments = await _repo.List();

            return Ok(managaments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetManagement(int id)
        {
            var managament = await _repo.Get(id);

            return Ok(managament);
        }

        [HttpPost("byfilter")]
        public async Task<IActionResult> GetManagamentByFilter(ManagamentForFilterDto managament) 
        {
            var managaments = await _repo.GetManagamentByFilter(managament.DepartmentId, managament.Status);

            return Ok(managaments);
        }

        [HttpGet("byuser/{userId}")]
        public async Task<IActionResult> GetManagamentByuser(int userId)
        {
            var managaments = await _repo.GetManagamentByUser(userId);

            return Ok(managaments);
        }

        [HttpGet("tracing/byuser/{userId}")]
        public async Task<IActionResult> GetManagamentTracingByUser(int userId)
        {
            var managaments = await _repo.GetManagamentTracingByUser(userId);

            return Ok(managaments);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ManagamentForCreateDto managamentForCreateDto)
        {

            // Position position = data["position"].ToObject<Position>();
            // TicketDetail detailDto = data["detailData"].ToObject<TicketDetail>();

            // int positionId = await _repo.Add(position);

            Management management = new Management();
            management.DepartmentId = managamentForCreateDto.DepartmentId;
            management.UserCreatedId = managamentForCreateDto.UserCreatedId;
            management.AssignedUserId = managamentForCreateDto.AssignedUserId;
            management.Description = managamentForCreateDto.Description;
            management.Status = managamentForCreateDto.Status;
            management.CreatedByUserId = managamentForCreateDto.CreatedByUserId;

            var addedManagamentId = await _repo.Add(management);

            // var file = managamentForCreateDto.File;

            var user = await _repoUser.GetUser(management.UserCreatedId);
            var userAssigned = await _repoUser.GetUser(management.AssignedUserId);

            // var uploadResult = new ImageUploadResult();

            // if (file.Length > 0) 
            // {
            //     using (var stream = file.OpenReadStream())
            //     {
            //         var uploadParams = new ImageUploadParams()
            //         {
            //             File = new FileDescription(file.Name, stream)                         
            //         };

            //         uploadResult = _cloudinary.Upload(uploadParams);
            //     }
            // }

            // File tmpFile = new File();
            // tmpFile.Url = uploadResult.Url.ToString();
            // tmpFile.PublicId = uploadResult.PublicId;
            // tmpFile.Description = "Imagen de PNC No. " + addedManagamentId;
            // tmpFile.isMain = true;
            // tmpFile.ManagementId = addedManagamentId;

            // var fileId = _repoFile.Add(tmpFile);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("luiscolindres07@gmail.com", "luis596C");
            MailAddress from = new MailAddress("luiscolindres07@gmail.com", "Aseguramiento", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(user.Email);

            MailMessage message = new MailMessage(from, to);
            message.Body = "Su solicitud de PNC ha sido enviada correctamente. Pronto nos pondremos en contacto con usted";
            message.Subject = "Creaci??n de PNC";
            // client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            // string userState = "test message1";
            await client.SendMailAsync(message);

            Email clt = new Email();
            clt.Type = 1;
            clt.Message = message.Body;
            clt.Subject = message.Subject;
            clt.ForwardEmail = user.Email;
            clt.ManagamentId = addedManagamentId;
            clt.Status = 1;
            clt.CreatedByUserId = management.CreatedByUserId;

            var email = await _repoEmail.Add(clt);

            MailAddress fromCopy = new MailAddress("luiscolindres07@gmail.com", "Aseguramiento", System.Text.Encoding.UTF8);
            MailAddress toCopy = new MailAddress(userAssigned.Email);

            MailMessage messageCopy = new MailMessage(fromCopy, toCopy);
            messageCopy.Body = "Se ha creado una solicitud de Producto No Conforme";
            messageCopy.Body += "\nUsuario: " + user.Names + " " + user.LastName;
            messageCopy.Body += "\nDescripcion: " + management.Description;

            messageCopy.Subject = "Creaci??n de PNC";

            await client.SendMailAsync(messageCopy);

            Email worker = new Email();
            worker.Type = 2;
            worker.Message = messageCopy.Body;
            worker.Subject = messageCopy.Subject;
            worker.ForwardEmail = userAssigned.Email;
            worker.ManagamentId = addedManagamentId;
            worker.Status = 1;
            worker.CreatedByUserId = management.CreatedByUserId;

            var emailWorker = await _repoEmail.Add(worker);

            message.Dispose();
            
            // return StatusCode(201);
            return Ok(addedManagamentId);

            throw new Exception($"Error al crear el PNC");
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ManagamentForUpdateDto managament)
        {
            var editedManagament = await _repo.Edit(managament, id);

            return StatusCode(201);
        }

        [HttpPut("tracing/{id}")]
        public async Task<IActionResult> Tracing(int id, ManagamentForUpdateDto managament)
        {
            var editedManagament = await _repo.Edit(managament, id);

            var user = await _repoUser.GetUser(managament.UserCreatedId);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("luiscolindres07@gmail.com", "luis596C");
            MailAddress from = new MailAddress("luiscolindres07@gmail.com", "Aseguramiento", System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(user.Email);

            MailMessage message = new MailMessage(from, to);
            message.Body = "Se le ha dado respuesta a su solicitud de Producto No conforme.";
            message.Body += "\nRespuesta: " + managament.Response;
            message.Subject = "Respuesta de PNC";
            // client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

            // string userState = "test message1";
            await client.SendMailAsync(message);

            Email clt = new Email();
            clt.Type = 2;
            clt.Message = message.Body;
            clt.Subject = message.Subject;
            clt.ForwardEmail = user.Email;
            clt.ManagamentId = id;
            clt.Status = 1;
            
            var email = _repoEmail.Add(clt);

            return StatusCode(201);
        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
             String token = (string) e.UserState;

            if (e.Cancelled)
            {
                 Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                 Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            } else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }
    }
}
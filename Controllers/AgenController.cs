using AutoMapper;
using HeksaAgen.DTO;
using HeksaAgen.Model;
using HeksaAgen.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

// here we alias the type System.IO.File to IoFile so it won't conflict
using IoFile = System.IO.File;

namespace HeksaAgen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgenController : ControllerBase
    {
        private readonly AgenRepository _agenRepository;
        private readonly IMapper _mapper;

        public AgenController(AgenRepository AgenRepository, IMapper mapper)
        {
            _agenRepository = AgenRepository;
            _mapper = mapper;
        }

        [HttpGet("GetAllAgen")]
        public IActionResult GetAllAgen()
        {
            try
            {
                List<GetAllAgenDTO> agens = _mapper.Map<List<GetAllAgenDTO>>(_agenRepository.GetAllAgen());
                return Ok(agens);
            }
            catch
            {
                return StatusCode(500, "Server Error occured");
            }
        }

        [HttpGet("GetAgenById")]
        public IActionResult GetAgenById(long Id)
        {
            try
            {
                Agen agen = _agenRepository.GetAgenById(Id);
                if (agen == null)
                    return NotFound("Agen not found");
                return Ok(agen);
            }
            catch
            {
                return StatusCode(500, "Server Error occured");
            }
        }

        [HttpPost("CreateAgen")]
        public IActionResult CreateAgen([FromBody]CreateAgenDTO createAgen)
        {
            try
            {
                if (createAgen == null)
                    return BadRequest("Data should be inputed");

                if (string.IsNullOrWhiteSpace(createAgen.Name))
                    return BadRequest("Invalid Name");


                if (!IsValidEmail(createAgen.Email))
                    return BadRequest("Invalid Email Address");

#nullable enable
                Agen? agen = _agenRepository.CheckEmail(createAgen.Email);
#nullable disable

                if (agen != null)
                    return BadRequest("This email address is already used by another agen");

                bool createAgenStatus = _agenRepository.CreateAgen(_mapper.Map<Agen>(createAgen));
                if (!createAgenStatus)
                    return StatusCode(500, "Create Agen Failed");
                return StatusCode(201, "Create Agen Success");
            }
            catch
            {
                return StatusCode(500, "Server Error occured");
            }
        }

        [HttpGet("DownloadAttachment")]
        public IActionResult DownloadAttachment(string fileName)
        {
            try
            {
                string downloadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/", fileName);
                var contentType = GetMimeType(fileName);
                var memory = new MemoryStream();
                if (IoFile.Exists(downloadPath))
                {
                    var net = new System.Net.WebClient();
                    var data = net.DownloadData(downloadPath);
                    var file = IoFile.OpenRead(downloadPath);
                    var content = new System.IO.MemoryStream(data);
                    memory = content;
                }
                memory.Position = 0;
                return File(memory.ToArray(), contentType, fileName);
            }
            catch(Exception err)
            {
                return StatusCode(500, err);
            }
        }

        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        [HttpPost("UploadAttachment")]
        //[Consumes("multipart/form-data")]
        public IActionResult UploadAttachments([FromForm] IFormFile attachmentFile)
        {
            try
            {
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/", attachmentFile.FileName);
                InsertAttachmentDTO attachment = new InsertAttachmentDTO();
                attachment.FileName = attachmentFile.FileName;
                attachment.FilePath = savePath;
                attachment.FileType = attachmentFile.ContentType;
                attachment.AttachmentType = attachmentFile.ContentType;

                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    attachmentFile.CopyTo(stream);
                }

                return Ok(attachment);
            }
            catch
            {
                return StatusCode(500, "Server Error occured");
            }
        }

        [HttpDelete("DeleteAttachment")]
        public IActionResult DeleteAttachment(string attachmentFileName)
        {
            try
            {
                if (attachmentFileName == null || attachmentFileName == "")
                    return BadRequest("File name must not be null");

                string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/", attachmentFileName);

                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                using (FileStream fs = new FileStream(deletePath, FileMode.Open))
                {
                }

                if (IoFile.Exists(deletePath))
                {
                    IoFile.Delete(deletePath);
                    return StatusCode(201, "Attachment file deleted");
                } 
                else
                {
                    return NotFound("File not found");
                }            
            }
            catch (Exception err)
            {
                return StatusCode(500, err);
            }
        }

        [HttpPut("UpdateAgen")]
        public IActionResult UpdateAgen([FromBody]Agen updateAgen, long Id)
        {
            try
            {
                if (updateAgen == null)
                    return BadRequest("Data should be inputed");

                #nullable enable
                Agen? checkAgen = _agenRepository.GetAgenById(Id);
                #nullable disable
                if (checkAgen == null)
                    return BadRequest($"Agen with ID of {Id} doesn't exist");

                if (string.IsNullOrWhiteSpace(updateAgen.Name))
                    return BadRequest("Invalid Name");

                if (!IsValidEmail(updateAgen.Email))
                    return BadRequest("Invalid Email Address");

                #nullable enable
                Agen? checkEmailAgen = _agenRepository.CheckEmail(updateAgen.Email);
                #nullable disable

                if (checkEmailAgen != null && checkEmailAgen.ID != Id)
                    return BadRequest("This email address is already used by another agen");

                bool updateAgenStatus = _agenRepository.UpdateAgen(checkAgen, updateAgen);
                if (!updateAgenStatus)
                    return StatusCode(500, "Edit Agen Failed");
                return StatusCode(201, "Edit Agen Success");
            }
            catch
            {
                return StatusCode(500, "Server Error occured");
            }
        }

        [HttpDelete("DeleteAgen")]
        public IActionResult DeleteAgen(long Id)
        {
            try
            {
                bool deleteAgen = _agenRepository.DeleteAgen(Id);
                if (!deleteAgen)
                    return StatusCode(500, "Delete Agen Failed");
                return StatusCode(201, "Delete Agen Success");
            }
            catch
            {
                return StatusCode(500, "Server Error occured");
            }
        }

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
                if (!string.IsNullOrWhiteSpace(trimmedEmail) && Regex.IsMatch(trimmedEmail, regex, RegexOptions.IgnoreCase))
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == trimmedEmail;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

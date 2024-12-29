using dormitory_management_be.Data;
using dormitory_management_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace dormitory_management_be.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private readonly SendMail _sendMail;
        private readonly DormitoryContext _context;

        public UsersController(DormitoryContext context)
        {
            _sendMail = new SendMail();
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> SearchRoom(int? gender, int? roomTypeId, int? floorId, int? areaId)
        {
            var query = _context.Room.AsQueryable();

            if (gender.HasValue)
            {
                query = query.Where(r => r.Gender == gender);
            }

            if (roomTypeId.HasValue)
            {
                query = query.Where(r => r.RoomTypeId == roomTypeId.Value);
            }

            if (floorId.HasValue)
            {
                query = query.Where(r => r.FloorId == floorId.Value);
            }

            if (areaId.HasValue)
            {
                query = query.Where(r => r.AreaId == areaId.Value);
            }

            var result = await (from r in query
                                join rr in _context.RoomRegistration on r.Id equals Convert.ToInt32(rr.RoomId) into roomRegs
                                from rr in roomRegs.DefaultIfEmpty()
                                group rr by new
                                {
                                    r.Id,
                                    r.RoomCode,
                                    r.RoomType,
                                    r.StudentCount,
                                    r.RoomStatus,
                                    r.RoomRate,
                                    r.Gender,
                                    r.RoomTypeId,
                                    r.FloorId,
                                    r.AreaId
                                } into grouped
                                select new
                                {
                                    Id = grouped.Key.Id,
                                    RoomCode = grouped.Key.RoomCode,
                                    RoomType = grouped.Key.RoomType,
                                    StudentCount = grouped.Key.StudentCount,
                                    RoomStatus = grouped.Key.RoomStatus,
                                    RoomRate = grouped.Key.RoomRate,
                                    Gender = grouped.Key.Gender,
                                    RoomTypeId = grouped.Key.RoomTypeId,
                                    FloorId = grouped.Key.FloorId,
                                    AreaId = grouped.Key.AreaId,
                                    RegisteredCount = grouped.Count(x => x != null),
                                    RemainingSlots = grouped.Key.StudentCount - grouped.Count(x => x != null)
                                }).ToListAsync();


            return Ok(result);
        }


        [HttpGet]
        public IActionResult Index()
        {
            return Ok("ok");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetAreas()
        {
            return await _context.Area.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomType>>> GetRoomType()
        {
            return await _context.RoomType.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Floor>>> GetFloor()
        {
            return await _context.Floor.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gender>>> GetGender()
        {
            return await _context.Gender.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DateConfig>> GetDate(string id)
        {
            var dateConfig = await _context.DateConfig
                                           .Where(dc => dc.RoomId == id)
                                           .ToListAsync();

            if (dateConfig == null)
            {
                return NotFound();
            }

            return Ok(dateConfig);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetDetail(int? id)
        {
            var query = await (from r in _context.Room
                               join rr in _context.RoomRegistration on r.Id equals Convert.ToInt32(rr.RoomId) into roomRegs
                               from rr in roomRegs.DefaultIfEmpty()
                               where r.Id == id
                               group rr by new
                               {
                                   r.Id,
                                   r.RoomCode,
                                   r.RoomType,
                                   r.StudentCount,
                                   r.RoomStatus,
                                   r.RoomRate,
                                   r.Gender,
                                   r.RoomTypeId,
                                   r.FloorId,
                                   r.AreaId
                               } into grouped
                               select new
                               {
                                   Id = grouped.Key.Id,
                                   RoomCode = grouped.Key.RoomCode,
                                   RoomType = grouped.Key.RoomType,
                                   StudentCount = grouped.Key.StudentCount,
                                   RoomStatus = grouped.Key.RoomStatus,
                                   RoomRate = grouped.Key.RoomRate,
                                   Gender = grouped.Key.Gender,
                                   RoomTypeId = grouped.Key.RoomTypeId,
                                   FloorId = grouped.Key.FloorId,
                                   AreaId = grouped.Key.AreaId,
                                   RegisteredCount = grouped.Count(x => x != null),
                                   RemainingSlots = grouped.Key.StudentCount - grouped.Count(x => x != null)
                               }).FirstOrDefaultAsync();

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoomRegistration(
                    [FromForm] RoomRegistration roomRegistration,
                    [FromForm] IFormFile idPhoto)
        {
            try
            {
                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }

                // Kiểm tra và lưu ID Photo
                if (idPhoto != null && idPhoto.Length > 0)
                {
                    var idPhotoDirectory = Path.Combine(uploadsDirectory, "IDPhotos");
                    if (!Directory.Exists(idPhotoDirectory))
                    {
                        Directory.CreateDirectory(idPhotoDirectory);
                    }
                    var idPhotoPath = Path.Combine(idPhotoDirectory, idPhoto.FileName);

                    if (!IsValidFileExtension(idPhoto.FileName))
                    {
                        return BadRequest("Invalid file type for IDPhoto.");
                    }

                    using (var stream = new FileStream(idPhotoPath, FileMode.Create))
                    {
                        await idPhoto.CopyToAsync(stream);
                    }
                    roomRegistration.IDPhoto = idPhotoPath;
                }
                else
                {
                    return BadRequest("ID Photo is required.");
                }
                string subject = "Bạn đã đăng ký thành công phòng ký túc xá";
                string body = $"Sinh viên {roomRegistration.FullName} đăng ký thành công ký túc xá. Vui lòng thanh toán vào số tài khoản sau. 00547829127 ngân hàn bidv " +
                    $" yêu ghi họ tên số căn cước công dân";
                await _sendMail.SendEmailAsync("taquangthang2002hn@gmail.com", roomRegistration.Email, subject, body);



                await _context.RoomRegistration.AddAsync(roomRegistration);
                await _context.SaveChangesAsync();

                return Ok(roomRegistration);
            }
            catch (Exception ex)
            {
                // Log lỗi ở đây nếu cần
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool IsValidFileExtension(string fileName)
        {
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".pdf" }; // Thêm các định dạng cho phép ở đây
            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            return validExtensions.Contains(fileExtension);
        }

        [HttpPost]
        public async Task<IActionResult> SendConfirmationEmail(string studentName, string toEmail)
        {
           

            try
            {
                return Ok("Email đã được gửi thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Gửi email thất bại: {ex.Message}");
            }
        }

    }
}

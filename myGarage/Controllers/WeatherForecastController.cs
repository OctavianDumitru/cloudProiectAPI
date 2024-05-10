using Microsoft.AspNetCore.Mvc;
using myGarage.Models;

namespace myGarage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private readonly MyGarageContext _dbcontext;

        public WeatherForecastController(MyGarageContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("GetBrands")]
        public async Task<IActionResult> GetBrands()
        {
            try
            {
                List<Brand> brands = _dbcontext.Brands.ToList();
                if (brands != null )
                {
                    return Ok(brands);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetModels")]
        public async Task<IActionResult> GetModels(int BrandID)
        {
            try
            {
                List<Model> models = _dbcontext.Models.Where(x => x.BrandId == BrandID).ToList();
                if (models != null)
                {
                    return Ok(models);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(String username, String password)
        {
            try
            {
                User user = new User();
                user.UserName = username;
                user.Password = password;
                _dbcontext.Users.Add(user);
                _dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                List<User> users = _dbcontext.Users.ToList();
                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(String username, String password)
        {
            try
            {
                User user = _dbcontext.Users.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetCarsByUserID")]
        public async Task<IActionResult> GetCarsByUserID(int userId)
        {
            //try
            //{
            //    List<UserXcar> cars = _dbcontext.UserXcars.Where(x => x.UserId == UserID).ToList();
            //    if (cars != null)
            //    {
            //        return Ok(cars);
            //    }
            //    else
            //    {
            //        return NotFound();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}

            try
            {
                var userCars = (from ux in _dbcontext.UserXcars
                                join user in _dbcontext.Users on ux.UserId equals user.UserId
                                join car in _dbcontext.Models on ux.ModelId equals car.ModelId
                                join brand in _dbcontext.Brands on car.BrandId equals brand.BrandId
                                where ux.UserId == userId
                                select new
                                {
                                    UserId = ux.UserId,
                                    CarName = car.Name,
                                    BrandName = brand.Name,
                                    RecordId = ux.RecordId
                                }).ToList();

                if (userCars.Any())
                {
                    return Ok(userCars);
                }
                else
                {
                    return Ok("No cars or brands found for the specified user.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddCarToUser")]
        public async Task<IActionResult> AddCarToUser(int UserID, int ModelID,int BrandID )
        {
            try
            {

                UserXcar userXcar = new UserXcar();
                userXcar.UserId = UserID;
                userXcar.ModelId = ModelID;
                userXcar.BrandId = BrandID;
                _dbcontext.UserXcars.Add(userXcar);
                _dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("CheckUser")]
        public async Task<IActionResult> CheckUser(String username)
        {
            try
            {
                User user = _dbcontext.Users.Where(x => x.UserName == username).FirstOrDefault();
                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteCar")]
        public async Task<IActionResult> DeleteCar(int RecordID)
        {
            try
            {
                UserXcar userXcar = _dbcontext.UserXcars.Where(x => x.RecordId == RecordID).FirstOrDefault();
                if (userXcar != null)
                {
                    _dbcontext.UserXcars.Remove(userXcar);
                    _dbcontext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateCar")]
        public async Task<IActionResult> UpdateCar(int RecordID,int BrandID, int ModelID)
        {
            try
            {
                UserXcar userXcar = _dbcontext.UserXcars.Where(x => x.RecordId == RecordID).FirstOrDefault();
                if (userXcar != null)
                {
                    userXcar.ModelId = ModelID;
                    userXcar.BrandId = BrandID;
                    _dbcontext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

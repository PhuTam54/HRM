using HRM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public EmployeeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users = _dataContext.Employee.ToList();
            return Ok(users);
        }

        [HttpGet]
        [Route("id")]
        public Employee GetUserById(int id)
        {
            var user = _dataContext.Employee.SingleOrDefault(u => u.Id == id);
            return user;
        }

        [HttpGet]
        [Route("name")]
        public Employee GetUserByName(string name)
        {
            var user = _dataContext.Employee.SingleOrDefault(u => u.Name == name);
            return user;
        }

        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="userCreate"></param>
        /// <remarks>
        /// "email": "employee@gmail.com",
        /// "username": "employee",
        /// "password": "123456",
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateUser([FromBody] Employee userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _dataContext.Employee.Add(userCreate);            

            if (!Save())
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut]
        [Route("id")]
        public IActionResult UpdateUser(int id, [FromBody] Employee updatedUser)
        {
            if (!UserExist(id))
                return NotFound();
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (id != updatedUser.Id)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            _dataContext.Employee.Update(updatedUser);
            if (!Save())
            {
                ModelState.AddModelError("", "Something went wrong updating User!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("id")]
        public IActionResult DeleteUser(int id)
        {
            if (!UserExist(id))
            {
                return NotFound();
            }

            var userToDelete = GetUserById(id);
            _dataContext.Employee.Remove(userToDelete);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Save())
            {
                ModelState.AddModelError("", "Something went wrong deleting User!");
            }

            return NoContent();
        }

        [HttpPost]
        [Route("save")]
        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        [HttpPost]
        [Route("exist")]
        public bool UserExist(int id)
        {
            return _dataContext.Employee.Any(u => u.Id == id);
        }
    }
}

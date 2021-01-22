using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Angular.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Angular.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private StudentContext _studentContext;

        public StudentController(StudentContext context)
        {
            _studentContext = context;
        }

        /// <summary>
        /// Get all Students.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Get()
        {
            return await _studentContext.Students.ToListAsync();
        }

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost("CrearStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Crear([FromBody]Student student)
        {
            if (student == null)
            {
                return NotFound("No se proporcionan datos del alumno");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _studentContext.Students.AddAsync(student);
            await _studentContext.SaveChangesAsync();

            return CreatedAtRoute(routeName: "obtenerunoid", routeValues: new { id = student.StudentId }, value: student);
        }

        [HttpGet("obtenerunoid/{id}",Name="obtenerunoid")]
        public async Task<ActionResult<Student>> ObtenerUnoId(int id)
        {
            if (id <= 0)
            {
                return NotFound("La identificación del estudiante debe ser mayor que cero");
            }
            var todo = await _studentContext.Students.FindAsync(id);

            if (todo == null)
            {
                return NotFound("Estudiante no existe");
            }
            return Ok(todo);
        }
        [HttpPut("Actualizar")]
        public async Task<ActionResult> Actualizar([FromBody] Student student)
        {
            if (student == null)
            {
                return NotFound("No se proporcionan datos del alumno");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var todo = await _studentContext.Students.FindAsync(student.StudentId);

            if (todo == null)
            {
                return NotFound("Estudiante no existe");
            }
            if (_studentContext.Entry<Student>(student).State == EntityState.Detached)
            {
                _studentContext.Set<Student>().Attach(student);
            }
            _studentContext.Entry<Student>(student).State = EntityState.Modified;
            await _studentContext.SaveChangesAsync();

            return CreatedAtRoute(routeName: "obtenerunoid", routeValues: new { id = student.StudentId }, value: student);
        }

        /// <summary>
        /// Deletes a specific Students.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound("No se proporciono el id");
            }
            var todo = await _studentContext.Students.FindAsync(id);

            if (todo == null)
            {
                return NotFound("No existe el alumno con ese id");
            }

            _studentContext.Students.Remove(todo);
            await _studentContext.SaveChangesAsync();

            return Ok("El estudiante fue borrado exitosamente");
        }




        ~StudentController()
        {
            _studentContext.Dispose();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Aplication.Interfaces;
using Aplication.DTOs;
using Domain.Interfaces;
using Aplication.Interfaces.Repositories;
using Aplication.Services;
using Aplication.DTOs.Notes;

namespace PersonalGroupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesService _notesService;
        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var notes = await _notesService.GetNotes();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar notas.", error = ex.Message });
            }
        }


        // GET api/<NotesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var notes = await _notesService.GetNotes();
                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar nota.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateNotesDTO noteDTO)
        {
            try
            {
                var result = await _notesService.AddNoteWithTagsAndMediaAsync(noteDTO);

                return CreatedAtAction(nameof(Get), new { id = result.Id }, result); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno no servidor", detail = ex.Message });
            }
        }

        // PUT api/<NotesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

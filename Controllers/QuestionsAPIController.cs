using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAdmin.Models;
using QuizAdmin.Models.DTO;

namespace QuizAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsAPIController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuestionsAPIController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/QuestionsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionsDTO>>> GetQuestions()
        {
            var questions = await _context.Questions.Include(q => q.Category).ToListAsync();
            return Ok(questions.Select(MapFromDataModel));

        }

        // GET: api/QuestionsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionsDTO>> GetQuestion(int id)
        {
            var question = await _context.Questions.Include(q => q.Category).SingleAsync(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(MapFromDataModel(question));
        }

        // PUT: api/QuestionsAPI/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, QuestionsDTO questionDto)
        {
            if (id != questionDto.Id)
            {
                return BadRequest();
            }

            var question = MapFromDataModel(questionDto);

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/QuestionsAPI
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<QuestionsDTO>> PostQuestion(QuestionsDTO questionDto)
        {
            _context.Questions.Add(MapFromDataModel(questionDto));
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = questionDto.Id }, questionDto);
        }

        // DELETE: api/QuestionsAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Question>> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return question;
        }

        private QuestionsDTO MapFromDataModel(Question question)
        {
            return new QuestionsDTO
            {
                Id = question.Id,
                Category = question.Category.Name,
                CategoryId = question.CategoryId,
                Statement = question.Statement
            };
        }

        private Question MapFromDataModel(QuestionsDTO questionDto)
        {
            return new Question
            {
                Id = questionDto.Id,
                CategoryId = questionDto.CategoryId,
                Statement = questionDto.Statement
            };
        }

        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}

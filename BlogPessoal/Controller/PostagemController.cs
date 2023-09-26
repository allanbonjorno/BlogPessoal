using blogpessoal.Model;
using BlogPessoal.Model;
using BlogPessoal.Service;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.Controller
{
    [Route("~/postagens")]
    [ApiController]
    public class PostagemController : ControllerBase
    {
        private readonly IPostagemService _postagemService;
        private readonly IValidator<Postagem> _postagemValidator;

        public PostagemController(
            IPostagemService postagemService,
            IValidator<Postagem> postagemValidator)
        {
            _postagemService = postagemService;
            _postagemValidator = postagemValidator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _postagemService.GetAll());
        }
        [HttpGet("titulo/{titulo}")]
        public async Task<ActionResult> GetByTitulo(string titulo)
        {
            return Ok(await _postagemService.GetByTitulo(titulo));
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Postagem postagem)
        {
            var validationResult = await _postagemValidator.ValidateAsync(postagem);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _postagemService.Create(postagem);
            return CreatedAtAction(nameof(GetById), new { id = postagem.Id }, postagem);
        }

        private object GetById()
        {
            throw new NotImplementedException();
        }
    }
}

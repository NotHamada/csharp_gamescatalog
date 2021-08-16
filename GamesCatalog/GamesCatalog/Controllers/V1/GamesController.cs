using GamesCatalog.Exceptions;
using GamesCatalog.InputModel;
using GamesCatalog.Services;
using GamesCatalog.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalog.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GamesServices _gamesServices;
        public GamesController(GamesServices gamesServices)
        {
            _gamesServices = gamesServices;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var result = await _gamesServices.Obter(1, 5);
            return Ok(result);
        }

        [HttpGet("{idgame:guid}")]
        public async Task<ActionResult<GameViewModel>> Obter([FromRoute] Guid idGame)
        {
            var game = await _gamesServices.Obter(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }
        
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InserirGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gamesServices.Inserir(gameInputModel);

                return Ok(game);
            }
            
            catch(Exception)
            {
                return UnprocessableEntity("Already exists a game with this name from this producer.");
            }
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> AtualizarGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gamesServices.Atualizar(idGame, gameInputModel);
                
                return Ok();
            }
            
            catch (JogoNaoCadastradoException)
            {
                return NotFound("This game doesn't exist.");
            }
        }

        [HttpPatch("{idGame:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarGame([FromRoute] Guid idGame, [FromRoute] double preco)
        {
            try
            {
                await _gamesServices.Atualizar(idGame, preco);
           
                return Ok(); 
            }
            catch (JogoNaoCadastradoException)
            {
                return NotFound("This game doesn't exist.");
            }
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> ApagarGame([FromRoute] Guid idGame)
        {
            try
            {
                await _gamesServices.Remover(idGame);
                
                return Ok(); 
            }
            catch (JogoNaoCadastradoException)
            {
                return NotFound("This game doesn't exist.");
            }
        }
    }
}

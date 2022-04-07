using Cards.API.Data;
using Cards.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cards.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CardsController : Controller
    {
        //buoc 6
        private readonly CardsDBContext cardsDBContext;
        public CardsController(CardsDBContext cardsDBContext)
        {
            this.cardsDBContext = cardsDBContext;
        }



        //buoc 5
        //get all cards
        [HttpGet]
        public async Task<IActionResult> getAllCards()
        {
            var cards = await cardsDBContext.Cards.ToListAsync();
            return Ok(cards);
        }

        // get single cards
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var cards = await cardsDBContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (cards != null)
            {
                return Ok(cards);
            }
            return NotFound("Cards not found");
        }



        // Add cards
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
            card.Id = Guid.NewGuid();
            await cardsDBContext.Cards.AddAsync(card);
            await cardsDBContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCard), new { id = card.Id}, card);
        }

        //update
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] Card card)
        {
            var existingCard = await cardsDBContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                existingCard.CardholderName = card.CardholderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CVC = card.CVC;
                await cardsDBContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card not Found");
        }


        //Delete
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existingCard = await cardsDBContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                cardsDBContext.Remove(existingCard);
                await cardsDBContext.SaveChangesAsync();
                return Ok(existingCard);
            }
            return NotFound("Card not Found");
        }
    }
}

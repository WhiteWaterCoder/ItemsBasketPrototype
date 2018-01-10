using System;
using System.Threading.Tasks;
using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Responses;
using ItemsBasket.BasketService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ItemsBasket.BasketService.Controllers
{
    /// <summary>
    /// Performs operations on multiple basket items for a given user.
    /// Only Get/Post (update)/Delete (clear all items) operations are supported.
    /// </summary>
    [Produces("application/json")]
    [Route("api/BasketItems")]
    public class BasketItemsController : Controller
    {
        private readonly IBasketItemsRepository _basketItemsRepository;

        public BasketItemsController(IBasketItemsRepository basketItemsRepository)
        {
            _basketItemsRepository = basketItemsRepository;
        }

        /// <summary>
        /// Retrieves all the items currently in the basket of the requested user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve the basket for.</param>
        /// <param name="securityToken">The security token of the user.</param>
        /// <returns>The items of the users basket as well as a response containing 
        /// success/failure of the operation and an error message if one occurs.</returns>
        [HttpGet("{userId}&{securityToken}", Name = "Get")]
        public async Task<GetUserBasketItemsResponse> Get(int userId, string securityToken)
        {
            try
            {
                //TODO: Authenticate based on security token
                return await _basketItemsRepository.GetBasketItems(userId);
            }
            catch (Exception e)
            {
                //TODO :Log
                return GetUserBasketItemsResponse.CreateFailedResult("An error occurred while trying to fetch the users basket items.");
            }
        }

        /// <summary>
        /// Updates the given items from the given users basket. 
        ///     - If an item does not exist it will be added.
        ///     - If an item quantity is set to zero it will be removed.
        /// </summary>
        /// <param name="update">The update object containing the user and basket items details.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpPost]
        public async Task<BasketItemResponse> Post([FromBody]BasketItemsUpdate update)
        {
            try
            {
                //TODO: Authenticate based on security token
                return await _basketItemsRepository.UpdateItems(update.UserId, update.Items);
            }
            catch (Exception e)
            {
                //TODO :Log
                return new BasketItemResponse(false, "An error occurred while trying to update some of theusers basket items.");
            }
        }

        /// <summary>
        /// Deletes all the contents of the basket for the given user.
        /// </summary>
        /// <param name="userId">The ID of the user to empty the basket for.</param>
        /// <param name="securityToken">The security token of the user.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpDelete("{userId}&{securityToken}")]
        public async Task<BasketItemResponse> Delete(int userId, string securityToken)
        {
            try
            {
                //TODO: Authenticate based on security token
                return await _basketItemsRepository.ClearItems(userId);
            }
            catch (Exception e)
            {
                //TODO :Log
                return new BasketItemResponse(false, "An error occurred while trying to clear the users basket items.");
            }
        }
    }
}
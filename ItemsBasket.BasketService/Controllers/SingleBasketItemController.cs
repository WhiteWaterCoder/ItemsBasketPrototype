using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemsBasket.BasketService.Models;
using ItemsBasket.BasketService.Responses;
using ItemsBasket.BasketService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ItemsBasket.BasketService.Controllers
{
    /// <summary>
    /// Performs operations on single basket items for a given user.
    /// Only insert (put)/ update (post) / delete operations are available. Get
    /// makes no sense in this context and is not implemented. We could return all 
    /// items but that would be duplicate from the multiple items controller and
    /// would also be inconsistent since it is a multi-item operation really.
    /// </summary>
    [Produces("application/json")]
    [Route("api/SingleBasketItem")]
    public class SingleBasketItemController : Controller
    {
        private readonly IBasketItemsRepository _basketItemsRepository;

        public SingleBasketItemController(IBasketItemsRepository basketItemsRepository)
        {
            _basketItemsRepository = basketItemsRepository;
        }

        /// <summary>
        /// Updates (or adds if not present) a single item from the basket of the given user. 
        /// The only thing that can be updated is the quantity. If the quantity is set to zero 
        /// then the item will be removed.
        /// </summary>
        /// <param name="update">The update object containing the user and basket item details.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpPost]
        public async Task<BasketItemResponse> Post([FromBody]SingleBasketItemUpdate update)
        {
            try
            {
                //TODO: Authenticate based on security token
                return await _basketItemsRepository.UpdateItem(update.UserId, update.Item);
            }
            catch (Exception e)
            {
                //TODO :Log
                return new BasketItemResponse(false, "An error occurred while trying to add a new item in your basket.");
            }
        }

        /// <summary>
        /// Adds a single item to the basket of the given user. If the items has already been added
        /// then it is updated.
        /// </summary>
        /// <param name="update">The update object containing the user and basket item details.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpPut]
        public async Task<BasketItemResponse> Put([FromBody]SingleBasketItemUpdate update)
        {
            try
            {
                //TODO: Authenticate based on security token
                return await _basketItemsRepository.AddItem(update.UserId, update.Item);
            }
            catch(Exception e)
            {
                //TODO :Log
                return new BasketItemResponse(false, "An error occurred while trying to add a new item in your basket.");
            }
        }

        /// <summary>
        /// Removes a single item to from basket of the given user. 
        /// </summary>
        /// <param name="update">The update object containing the user and basket item details.</param>
        /// <returns>A response containing success/failure of the operation and an error message if one occurs.</returns>
        [HttpDelete]
        public async Task<BasketItemResponse> Delete([FromBody]SingleBasketItemUpdate update)
        {
            try
            {
                //TODO: Authenticate based on security token
                return await _basketItemsRepository.RemoveItem(update.UserId, update.Item);
            }
            catch (Exception e)
            {
                //TODO :Log
                return new BasketItemResponse(false, "An error occurred while trying to remove an existing item from your basket.");
            }
        }
    }
}

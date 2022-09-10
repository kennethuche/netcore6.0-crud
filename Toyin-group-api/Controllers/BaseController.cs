﻿using Microsoft.AspNetCore.Mvc;
using Toyin_group_api.Core.Models;
using Toyin_group_api.Core.Models.Resources;

namespace Toyin_group_api.Controllers
{
    [Consumes("application/json", "text/plain", "image/gif", "image/jpeg", "image/png", "multipart/form-data")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Handle Response Method
        /// </summary>
        /// <typeparam name="TRes"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult<TRes> HandleResponse<TRes>(TRes result) where TRes : StatusResource
        {
            return result.Code switch
            {
                ResourceCodes.ServiceError => StatusCode(StatusCodes.Status500InternalServerError, result),
                ResourceCodes.Unauthenticated or ResourceCodes.Unauthorized => Unauthorized(result),
                ResourceCodes.NoData => NotFound(result),
                ResourceCodes.Success => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}

﻿using CSharp.Authorization.Session.Attributes;
using CSharp.Authorization.Session.Enums;
using CSharp.Authorization.Session.Extensions;
using CSharp.Authorization.Session.Interfaces;
using CSharp.Authorization.Session.Models;
using CSharp.Authorization.Session.Models.Request;
using CSharp.Authorization.Session.Services;
using CSharp.Commons.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace CSharp.Authorization.Session.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {

        private readonly ILogger<AuthorizationController> logger;
        private readonly IAuthorizationService authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService, ILogger<AuthorizationController> logger)
        {
            this.logger = logger;
            this.authorizationService = authorizationService;
        }

        [Route("Session")]
        [AllowPublic]
        [HttpPost]
        [Produces("application/json")]
        public BaseResponse<bool> SetSessionInfo(SessionInfoRequest parameters)
        {
            bool result =  authorizationService.SetSessionInfo(HttpContext, parameters.SessionId, parameters.TimeoutMin);

            if(result)
            {
                return new BaseResponse<bool>
                {
                    Result = true,
                    Data = true
                };
            }
            else
            {
                return new BaseResponse<bool>
                {
                    Result = false,
                    ErrorCode = (int)ErrorCode.SYSTEM_EXCEPTION,
                    ErrorMessage = ErrorCode.SYSTEM_EXCEPTION.ToString(),
                    Data = false
                };
            }
        }

        [Route("Session")]
        [HttpGet]
        [Produces("application/json")]
        public BaseResponse<SessionInfo> GetSessionInfo()
        {
            return new BaseResponse<SessionInfo>
            {
                Result = true,
                Data = HttpContext.GetSessionInfo()
        };
        }
    }
}

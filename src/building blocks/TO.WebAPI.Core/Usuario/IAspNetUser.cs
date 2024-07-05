﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TO.WebAPI.Core.Usuario;
public interface IAspNetUser
{
    string Name { get; }
    Guid ObterUserId();
    string ObterUserEmail();
    string ObterUserToken();
    bool EstaAutenticado();
    bool PossuiRole(string role);
    IEnumerable<Claim> ObterClaims();
    HttpContext ObterHttpContext();
}

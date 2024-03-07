﻿using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public record Request(
    string Email,
    string Password
) : IRequest<Response>;

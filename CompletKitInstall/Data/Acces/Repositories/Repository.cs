﻿using CompletKitInstall.Data;
using CompletKitInstall.Models;
using CompletKitInstall.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompletKitInstall.Repositories
{
    public abstract class Repository<T, U> : IRepository<T, U>
        where T : class, IDbObject
        where U : class, IUIObject
    {
        protected CompletKitDbContext _ctx;
        protected readonly IAuthorizationService _authorizationService;
        protected readonly ILogger<Repository<T,U>> _logger;
        public Repository(CompletKitDbContext ctx,IAuthorizationService authorizationService,ILogger<Repository<T,U>> logger)
        {
            _ctx = ctx;
            _authorizationService = authorizationService;
            _logger = logger;
        }
        public abstract Task<T> Add(U item,ClaimsPrincipal user);

        public abstract Task<IEnumerable<U>> Get(bool asNoTracking = false);

        public abstract Task<U> GetById(int id, bool asNoTracking = false);
        public abstract Task<bool> Remove(U item, ClaimsPrincipal user);
        public abstract Task RemoveById(int id, ClaimsPrincipal user);

        public abstract Task<bool> Update(int id, U newData, ClaimsPrincipal user);

    }
}

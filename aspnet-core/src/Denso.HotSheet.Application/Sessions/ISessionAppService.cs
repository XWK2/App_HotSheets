﻿using System.Threading.Tasks;
using Abp.Application.Services;
using Denso.HotSheet.Sessions.Dto;

namespace Denso.HotSheet.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

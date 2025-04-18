﻿using Abp.AutoMapper;
using Denso.HotSheet.Authentication.External;

namespace Denso.HotSheet.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}

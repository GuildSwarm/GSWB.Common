﻿using Common.Application.Contracts.Services;
using Common.Application.DTOs.Auth;
using Common.Application.DTOs.Discord;
using Common.Application.DTOs.Members;
using Common.Domain.ValueObjects;
using Common.Infrastructure.Communication.ApiRoutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGF.CA.Infrastructure.Communication.Http;
using TGF.CA.Infrastructure.Discovery;
using TGF.Common.ROP.HttpResult;

namespace Common.Infrastructure.Communication.HTTP
{
    public class MembersCommunicationService(IServiceDiscovery aServiceDiscovery, IHttpClientFactory aHttpClientFactory) 
        : HttpCommunicationService(aServiceDiscovery, aHttpClientFactory), IMembersCommunicationService
    {
        private readonly string _serviceName = ServicesDiscoveryNames.Members;

        public async Task<IHttpResult<MemberDTO>> GetExistingMember(ulong aDiscordUserId, CancellationToken aCancellationToken = default)
        => await GetAsync<MemberDTO>(_serviceName, $"/{MembersApiRoutes.private_members_getByDiscordUserId}?aDiscordUserId={aDiscordUserId}", aCancellationToken);

        public async Task<IHttpResult<MemberDetailDTO>> SignUpNewMember(SignUpDataDTO? aSignUpDataDTO, DiscordCookieUserInfo aDiscordCookieUserInfo, CancellationToken aCancellationToken = default)
        => await PutAsync<CreateMemberDTO, MemberDetailDTO>(_serviceName, $"/{MembersApiRoutes.private_members_addNew}", new CreateMemberDTO(aSignUpDataDTO, aDiscordCookieUserInfo), aCancellationToken);

        public async Task<IHttpResult<PermissionsEnum>> GetMemberPermissions(Guid aMemberId, CancellationToken aCancellationToken = default)
        => await GetAsync<PermissionsEnum>(_serviceName, $"{MembersApiRoutes.private_members_getPermissions}?memberId={aMemberId}", aCancellationToken);
    }
}
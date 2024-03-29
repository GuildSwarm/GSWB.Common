﻿using Common.Application.DTOs.Discord;
using TGF.Common.ROP.HttpResult;

namespace Common.Application
{
    /// <summary>
    /// Provides services for communicating with the Mandrill API.
    /// </summary>
    public interface IMandrillCommunicationService
    {
        /// <summary>
        /// Gets a list of Discord user roles from the Mandrill API.
        /// </summary>
        /// <param name="aDiscordUserId">The Discord user ID.</param>
        /// <param name="aCancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A result containing a list of Discord roles IF ANY, otherwise an HTTP error.</returns>
        Task<IHttpResult<IEnumerable<DiscordRoleDTO>>> GetDiscordUserRoleList(string aDiscordUserId, CancellationToken aCancellationToken = default);

        /// <summary>
        /// Gets the discord member nickname in the guild's server via http request against the Mandrill API.
        /// </summary>
        /// <param name="aDiscordUserId">The Discord user ID.</param>
        /// <param name="aCancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A result containing the discord member's profile <see cref="DiscordProfileDTO"/>, otherwise an HTTP error.</returns>
        Task<IHttpResult<DiscordProfileDTO>> GetMemberProfileFromId(string aDiscordUserId, CancellationToken aCancellationToken = default);

        /// <summary>
        /// Fetches the list of all guild server roles from Discord.
        /// </summary>
        /// <returns>List of roles from Discord's server.</returns>
        Task<IHttpResult<IEnumerable<DiscordRoleDTO>>> GetDiscordRoleList(CancellationToken aCancellationToken = default);

    }
}

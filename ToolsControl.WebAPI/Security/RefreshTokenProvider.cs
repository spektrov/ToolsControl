using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace ToolsControl.WebAPI.Security;


public class RefreshTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    public RefreshTokenProvider(
        IDataProtectionProvider dataProtectionProvider,
        IOptions<RefreshTokenProviderOptions> options) 
        : base(dataProtectionProvider, options, NullLogger<DataProtectorTokenProvider<TUser>>.Instance)
    {
    }
}

public class RefreshTokenProviderOptions : DataProtectionTokenProviderOptions { }
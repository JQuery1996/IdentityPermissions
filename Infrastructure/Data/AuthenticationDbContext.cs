using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data; 

public class AuthenticationDbContext<TUser, TRole>(DbContextOptions options) 
    : IdentityDbContext<TUser, TRole, string>(options)
    where TUser : IdentityUser
    where TRole : IdentityRole;
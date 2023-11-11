using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data; 

public class ApplicationDbContext(DbContextOptions options)
    : AuthenticationDbContext<User, Role>(options)
{
    
}
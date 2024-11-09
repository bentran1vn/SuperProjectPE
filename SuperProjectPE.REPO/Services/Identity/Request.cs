using System.ComponentModel.DataAnnotations;

namespace SuperProjectPE.REPO.Services.Identity;

public static class Request
{
    public record Login([Required]string Email, [Required]string Password);
}
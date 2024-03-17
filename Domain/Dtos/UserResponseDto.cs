using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace FirstNetMongo.Domain.Dtos;

public class UserResponseDto
{
    public string Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

}

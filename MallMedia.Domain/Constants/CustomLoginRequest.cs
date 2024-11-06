using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Domain.Constants;

public class CustomLoginRequest
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}

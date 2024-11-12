using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallMedia.Domain.Constants;

public class CurrentUser
{
    public string Id { get; set; }
    public string Username { get; set; }
    public List<string> Roles { get; set; }
}

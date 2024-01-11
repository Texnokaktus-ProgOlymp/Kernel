using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

public interface IApplicationRepository
{
    Application Add(ApplicationInsertModel insertModel);
}

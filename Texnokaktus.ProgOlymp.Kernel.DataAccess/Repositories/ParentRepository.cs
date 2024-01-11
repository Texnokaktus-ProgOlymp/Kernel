using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories;

internal class ParentRepository(AppDbContext context) : IParentRepository
{
    public Parent Add(ParentInsertModel insertModel)
    {
        var entity = new Parent
        {
            Name = insertModel.Name,
            Email = insertModel.Email,
            Phone = insertModel.Phone
        };
        return context.Parents.Add(entity).Entity;
    }
}

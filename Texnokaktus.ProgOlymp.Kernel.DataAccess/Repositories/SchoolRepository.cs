using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories;

internal class SchoolRepository(AppDbContext context) : ISchoolRepository
{
    public School Add(SchoolInsertModel insertModel)
    {
        var entity = new School
        {
            Name = insertModel.Name,
            Region = insertModel.Region
        };
        return context.Schools.Add(entity).Entity;
    }
}

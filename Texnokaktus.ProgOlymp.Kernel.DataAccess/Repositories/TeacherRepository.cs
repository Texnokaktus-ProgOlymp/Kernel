using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories;

internal class TeacherRepository(AppDbContext context) : ITeacherRepository
{
    public Teacher Add(TeacherInsertModel insertModel)
    {
        var teacher = new Teacher
        {
            Name = insertModel.Name,
            School = insertModel.School,
            Email = insertModel.Email,
            Phone = insertModel.Phone
        };
        return context.Teachers.Add(teacher).Entity;
    }
}

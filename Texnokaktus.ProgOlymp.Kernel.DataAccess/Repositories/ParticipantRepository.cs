using Texnokaktus.ProgOlymp.Kernel.DataAccess.Context;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;
using Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Repositories;

internal class ParticipantRepository(AppDbContext context) : IParticipantRepository
{
    public Participant Add(ParticipantInsertModel insertModel)
    {
        var participant = new Participant
        {
            Name = insertModel.Name,
            Email = insertModel.Email,
            BirthDate = insertModel.BirthDate
        };
        return context.Participants.Add(participant).Entity;
    }
}

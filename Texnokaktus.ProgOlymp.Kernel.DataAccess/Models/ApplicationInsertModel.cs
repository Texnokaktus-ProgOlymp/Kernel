using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;

public record ApplicationInsertModel(DateTime Submitted,
                                     string? YandexIdLogin,
                                     string Grade,
                                     bool PersonalDataConsent,
                                     int ContestStageId,
                                     Participant Participant,
                                     School School,
                                     Parent Parent,
                                     Teacher? Teacher);

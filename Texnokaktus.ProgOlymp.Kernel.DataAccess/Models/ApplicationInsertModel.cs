using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;

public record ApplicationInsertModel(DateTime Submitted,
                                     string ContestLocation,
                                     string YandexIdLogin,
                                     string Grade,
                                     bool PersonalDataConsent,
                                     Participant Participant,
                                     School School,
                                     Parent Parent,
                                     Teacher? Teacher);

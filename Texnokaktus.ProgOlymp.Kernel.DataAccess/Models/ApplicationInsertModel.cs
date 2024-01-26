using Texnokaktus.ProgOlymp.Kernel.DataAccess.Entities;

namespace Texnokaktus.ProgOlymp.Kernel.DataAccess.Models;

public record ApplicationInsertModel(int GoogleServiceApplicationId,
                                     DateTime Submitted,
                                     string? YandexIdLogin,
                                     string Grade,
                                     string AgeCategory,
                                     int ContestStageId,
                                     string Name,
                                     string Email,
                                     School School);

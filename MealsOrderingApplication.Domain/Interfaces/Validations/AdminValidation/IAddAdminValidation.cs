﻿using MealsOrderingApplication.Domain.Interfaces.Validations.ApplicationUserValidation;

namespace MealsOrderingApplication.Domain.Interfaces.Validations.AdminValidation
{
    public interface IAddAdminValidation : IBaseAdminValidation, IAddApplicationUserValidation
    {
    }
}

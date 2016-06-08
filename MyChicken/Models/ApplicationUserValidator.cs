using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyChicken.Models
{
    public class ApplicationUserValidator : AbstractValidator<ApplicationUser>
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ApplicationUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .EmailAddress()
                .WithMessage("L'email est obligatoire")
                .Must(BeUniqueEmail)
                .Length(0, 50);
            //RuleFor(x => x.Tel).Length(0,10).NotNull();
            RuleFor(x => x.Adresse).Null();
        }

        private bool BeUniqueEmail(string email)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var res = db.Users.FirstOrDefault(x => x.Email == email) == null;
            return res;
        }
    }
}

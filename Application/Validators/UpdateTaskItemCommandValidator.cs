using Application.Features.TaskItems.Command.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class UpdateTaskItemCommandValidator : AbstractValidator<UpdateTaskItemCommand>
    {
        public UpdateTaskItemCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Güncellenecek görev Id'si boş olamaz.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş bırakılamaz")
                .MaximumLength(100).WithMessage("Başlık 100 karakterden fazla olamaz")
                .MinimumLength(3).WithMessage("Başlık 3 karakterden küçük olamaz");


            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");


            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).WithMessage("Bitiş tarihi bugünden büyük olmalı");


            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Geçersiz statü durumu");
        }
    }
}

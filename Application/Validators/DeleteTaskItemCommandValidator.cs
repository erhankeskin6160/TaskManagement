using Application.Features.TaskItems.Command.Delete;
using FluentValidation;

namespace Application.Validators
{
    public class DeleteTaskItemCommandValidator : AbstractValidator<DeleteTaskItemCommand>
    {
        public DeleteTaskItemCommandValidator()
        {
             
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Silinecek görev Id'si boş olamaz.")
                .NotEqual(Guid.Empty).WithMessage("Geçersiz yanlış ıd  verildi");//Not equel ıd eşit değilde geçersiz ıd diye hata gönder

        }
    }
}

﻿using blogpessoal.Model;
using BlogPessoal.Model;
using FluentValidation;

namespace BlogPessoal.Validator
{
    public class PostagemValidator : AbstractValidator<Postagem>
    {
        public PostagemValidator()
        {
            RuleFor(p => p.Titulo).NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);
            RuleFor(p => p.Texto).NotEmpty()
        .MinimumLength(5)
        .MaximumLength(100);




        }
    }
}

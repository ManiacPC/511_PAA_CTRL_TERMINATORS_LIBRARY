using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2ControlModels.DTO;
using FluentValidation;

namespace App2ControlModels.DAL
{
    public class TerminatorValidator : AbstractValidator<TerminatorDTO>
    {
        public TerminatorValidator()
        {
            RuleFor(terminator => terminator.NumeroSerie).Length(7,7).Must(NotBeRepeated).WithMessage("Escriba un n° de serie de 7 caracteres y que no se encuentre repetido");
            RuleFor(terminator => terminator.Objetivo).NotNull().WithMessage("El objetivo no puede quedar en blanco");
            RuleFor(terminator => terminator.Prioridad).NotNull().WithMessage("La prioridad no puede estar en blanco");
            RuleFor(terminator => terminator.AñoDestino).GreaterThan(1997).LessThanOrEqualTo(3000).WithMessage("El año debe fluctuar entre el período de 1997 y 3000");

        }

        private bool NotBeRepeated(string numeroSerie)
        {
            foreach (TerminatorDTO t in TerminatorDAL.terminators)
            {
                if (t.NumeroSerie == numeroSerie) return false;
            }

            return true;
        }
    }
}

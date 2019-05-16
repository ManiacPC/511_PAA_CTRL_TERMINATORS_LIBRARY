using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2ControlModels.DTO;
using FluentValidation.Results;

namespace App2ControlModels.DAL
{
    public class TerminatorDAL
    {
        public static List<TerminatorDTO> terminators = new List<TerminatorDTO>();

        public static readonly List<Tipo> tipos = new List<Tipo>()
        {
            new Tipo() { Codigo = "T-1", PrioridadBase = 1},
            new Tipo() { Codigo = "T-800", PrioridadBase = 100},
            new Tipo() { Codigo = "T-1000", PrioridadBase = 200},
            new Tipo() { Codigo = "T-3000", PrioridadBase = 300}
        };

        public static bool Ingresar()
        {
            Tipo tipo = new Tipo();
            bool validacion, vSerie, vTipo, vAño, vObjetivo;
            validacion = vSerie = vTipo = vAño = vObjetivo = false;
            string nserie = null;
            int prioridad = 0;
            int año = 0;
            string objetivo = null;

            TerminatorDTO nuevoTerminator = new TerminatorDTO();


            while (!validacion)
            {

                // Serie
                Console.WriteLine("Ingrese número de serie: ");
                nuevoTerminator.NumeroSerie = Console.ReadLine().Trim();
                
                // Tipo
                tipo = ValidarTipo();
                nuevoTerminator.Prioridad = tipo.PrioridadBase;
                vTipo = true;

                // Objetivo
                Console.WriteLine("Ingrese el objetivo del terminator:");
                nuevoTerminator.Objetivo = Console.ReadLine().Trim();
                
                // Año
                Console.WriteLine("Ingrese año:");
                nuevoTerminator.AñoDestino = Convert.ToInt32(Console.ReadLine().Trim());

                TerminatorValidator validador = new TerminatorValidator();
                ValidationResult resultado = validador.Validate(nuevoTerminator);

                if (!resultado.IsValid)
                {
                    foreach (var fallo in resultado.Errors)
                    {
                        Console.WriteLine($"Prop: {fallo.PropertyName}, Error: {fallo.ErrorMessage}");
                    }
                }
                else terminators.Add(nuevoTerminator);
                
                validacion = resultado.IsValid;
                
                Console.ReadKey();
                
            }
            Console.WriteLine("Ha sido agregado un nuevo terminator");
            Console.ReadLine();
            return true;
        }

  
        private static Tipo ValidarTipo()
        {
            bool vTipo = false;

            while (!vTipo)
            {
                Console.WriteLine("Ingrese tipo:");
                Console.Write("(");
                foreach (Tipo t in tipos)
                {
                    Console.Write(t.Codigo);
                    Console.Write(", ");
                }
                Console.Write(")");

                string tipo = Console.ReadLine();

                foreach (Tipo t in tipos)
                {
                    if (t.Codigo == tipo.ToUpper())
                    {
                        vTipo = true;
                        return t;
                    }

                }
            }

            return new Tipo();
        }

    
        public static void Buscar()
        {
            Console.Clear();
            Console.WriteLine("Escriba modelo de terminator a buscar:");
            string modelo = Console.ReadLine().Trim().ToUpper();
            Console.WriteLine("Escriba año de terminator a buscar:");
            int año = Convert.ToInt32(Console.ReadLine().Trim());

           // List<TerminatorDTO> encontrados =
           //     terminators.Where(x => x.Tipo.Codigo == modelo && x.AñoDestino == año).ToList();

           //foreach (TerminatorDTO t in terminators.Where(x => x.Tipo.Codigo == modelo && x.AñoDestino == año).ToList())
           // {
           //         Console.WriteLine($"R:{t.ToString()}");
           // }

            foreach (TerminatorDTO t in terminators)
            {
                if(t.Tipo.Codigo == modelo && t.AñoDestino == año)
                    Console.WriteLine($"R:{t.ToString()}");
            }

            Console.ReadLine(); // Pause

        }

        public static void Listar()
        {
            Console.Clear();
            Console.WriteLine("Listado de terminators");
            foreach (TerminatorDTO t in terminators)
            {
                Console.WriteLine($"R:{t.ToString()}");
            }

            Console.ReadLine();

        }
    }
}

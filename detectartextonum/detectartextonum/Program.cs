using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace detectartextonum
{
    class Program
    {
        static void Main(string[] args)
        {
            char letra;
            do
            {
                Console.WriteLine("Ingrese el texto o numero:");
                string texto = Console.ReadLine();

                //los regex para comprobar el texto y numeros
                Regex numeros = new Regex(@"[0-9]+");
                Regex telefono =new Regex(@"[0-9]{10}");
                Regex textos = new Regex(@"[a-zA-Z ]+");
                Regex telefonoguion = new Regex(@"[0-9]{3}[-][0-9]{3}[-]([0-9]{2}[-][0-9]{2})");
                Regex telefonoguion2 = new Regex(@"[0-9]{3}[-][0-9]{3}[-]([0-9]{4})");

                int caracteres = texto.Length;

                if(caracteres==10)
                {
                    Match telefonico = telefono.Match(texto);
                    if(telefonico.Success)
                    {
                        Console.WriteLine("Es un numero telefonico de");
                    }
                }
                else if (caracteres == 13)
                {
                    Match telefonicoguion = telefonoguion.Match(texto);
                   

                    if(telefonicoguion.Success)
                    {
                        Console.WriteLine("Es un numero telefonico de");
                    }
                    
                }
                else if (caracteres == 12)
                {
                    Match telefonicoguion2 = telefonoguion2.Match(texto);
                    if (telefonicoguion2.Success)
                    {
                        Console.WriteLine("Es un numero telefonico de");
                    }
                }
                else
                {
                    Match textovalido = textos.Match(texto);
                    if (textovalido.Success)
                    {
                        Console.WriteLine("Ingresaste texto");
                    }

                }

                try
                {
                    Console.WriteLine("\n Desea continuar? (Y/N)");
                    letra = Convert.ToChar(Console.ReadLine());
                    if (letra != 'Y' && letra != 'y')
                    {
                        Console.WriteLine("Saliendo del programa, presione cualquier tecla");
                        Console.ReadKey();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    letra = 'n';
                    Console.ReadKey();
                }

            } while (letra == 'Y' || letra == 'y');
        }
    }
}

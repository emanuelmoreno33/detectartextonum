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

                mensaje(texto);

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

        static void mensaje(string texto)
        {
            //Regex para numero enteros, negativos y decimales
            Regex numerosdecimal = new Regex(@"^((-)?[0-9]+)(\.([0-9]+)?)?$");
            //regex para telefono sin guion
            Regex telefono = new Regex(@"^[0-9]{10}$");
            //Regex para telefono con tres guiones
            Regex telefonoguion = new Regex(@"^[0-9]{3}[-][0-9]{3}[-]([0-9]{2}[-][0-9]{2})$");
            //Regex para telefono con dos guiones
            Regex telefonoguion2 = new Regex(@"^[0-9]{3}[-][0-9]{3}[-]([0-9]{4})$");
            //Regex para texto sin simbolos
            Regex textos = new Regex(@"^[a-zA-Z ]+$");
            //Regex para texto que representa direcciones
            Regex direccion = new Regex(@"(^[a-zA-Z])([#. 0-9a-zA-ZÀ-ÿÑñ,-]+$)");
            //Regex para simbolos
            Regex simbolos = new Regex(@"[`~!@#$%^&*()_°¬|+\-=?¡¿;:',\.<>\{\}\[\]\\\/'\u0022']");
            //Regex para ecuaciones
            Regex ecuacion = new Regex(@"^(([A-z0-9_]+)( )?(=)?( )?([A-z]+['\+','\-']{2}|([A-z0-9_]+)([\+\-*/])([A-z0-9_]+))+)$");

            int caracteres = texto.Length;

            //texto con simbolos

            //telefono todo junto
            if (caracteres == 10)
            {
                Match telefonico = telefono.Match(texto);
                if (telefonico.Success)
                {
                    Console.WriteLine("Es un numero telefonico de");
                    return;
                }
            }
            //telefono con doble guion
            else if (caracteres == 12)
            {
                Match telefonicoguion2 = telefonoguion2.Match(texto);
                if (telefonicoguion2.Success)
                {
                    Console.WriteLine("Es un numero telefonico de");
                    return;
                }
            }
            //telefono con guion
            else if (caracteres == 13)
            {
                Match telefonicoguion = telefonoguion.Match(texto);


                if (telefonicoguion.Success)
                {
                    Console.WriteLine("Es un numero telefonico de");
                    return;
                }

            }
                //numeros enteros o decimales
                Match numerosvalidos = numerosdecimal.Match(texto);
                //texto sin simbolos
                Match textovalido = textos.Match(texto);
                //texto de direccion
                Match direcciones = direccion.Match(texto);
                //
                Match textoconsimbolo = simbolos.Match(texto);

                Match ecuaciones = ecuacion.Match(texto);

                if (numerosvalidos.Success)
                {
                    Console.WriteLine("Ingresaste un numero");
                }
                else if (textovalido.Success)
                {
                    Console.WriteLine("Ingresaste texto");
                }
                else if(direcciones.Success)
                {
                    Console.WriteLine("Es un texto de direccion");
                }
                else if (ecuaciones.Success)
                {
                    Console.WriteLine("Es una ecuacion matematica");
                }
                else if (textoconsimbolo.Success)
                {
                    Console.WriteLine("Es un texto con simbolos");
                }
                
                else
                {
                    Console.WriteLine("No identificado");
                }

            }
        }
    }


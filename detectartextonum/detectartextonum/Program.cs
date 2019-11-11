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
                string nuevotexto = texto.Replace("\t","");

                mensaje(nuevotexto);

                try
                {
                    Console.WriteLine("\n Desea continuar? (Y/otra letra)");
                    letra = Convert.ToChar(Console.ReadLine());
                    if (letra != 'Y' && letra != 'y')
                    {
                        Console.WriteLine("Saliendo del programa, presione cualquier tecla");
                        Console.ReadKey();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Ingresaste un valor incorrecto, saliendo del programa");
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
            Regex textos = new Regex(@"^[0-9a-zA-Z ]+$");
            //Regex para texto que representa direcciones
            Regex direccion = new Regex(@"(^[a-zA-Z][ 0-9a-zA-ZÀ-ÿÑñ-]+[.#\-][ 0-9a-zA-ZÀ-ÿÑñ-]+$)");
            //Regex para simbolos
            Regex simbolos = new Regex(@"[`¡¿~´¨!@#$%^&*()_°¬|+\-=?¡¿;:',\.<>\{\}\[\]\\\/'\u0022']");
            //Regex para ecuaciones
            Regex ecuacion = new Regex(@"^(([A-z]([A-z0-9_]+)?)( )?(=)( )?([A-z]+['\+','\-']{2}|([A-z0-9_]+)([\+\-*/])([A-z0-9_]+))+)$");

            int caracteres = texto.Length;
            string[,] ladaarreglo = llenarlada();

            //telefono todo junto
            if (caracteres == 10)
            {
                Match telefonico = telefono.Match(texto);
                string ladaencontrada = lada(texto,ladaarreglo);
                if (telefonico.Success && ladaencontrada != "ninguno")
                {
                    Console.WriteLine("Es un numero telefonico de: ");
                    Console.WriteLine(ladaarreglo[Convert.ToInt32(ladaencontrada), 1]);
                    Console.WriteLine("\nDel estado: " + ladaarreglo[Convert.ToInt32(ladaencontrada), 2]);
                    return;
                }
            }
            //telefono con doble guion
            else if (caracteres == 12)
            {
                Match telefonicoguion2 = telefonoguion2.Match(texto);
                string ladaencontrada = lada(texto, ladaarreglo);
                if (telefonicoguion2.Success && ladaencontrada != "ninguno")
                {
                    Console.WriteLine("Es un numero telefonico de: ");
                    Console.WriteLine(ladaarreglo[Convert.ToInt32(ladaencontrada), 1]);
                    Console.WriteLine("\nDel estado: " + ladaarreglo[Convert.ToInt32(ladaencontrada), 2]);
                    return;
                }
            }
            //telefono con guion
            else if (caracteres == 13)
            {
                Match telefonicoguion = telefonoguion.Match(texto);
                string ladaencontrada = lada(texto, ladaarreglo);
                if (telefonicoguion.Success && ladaencontrada != "ninguno")
                {
                    Console.WriteLine("Es un numero telefonico de: ");
                    Console.WriteLine(ladaarreglo[Convert.ToInt32(ladaencontrada), 1]);
                    Console.WriteLine("\nDel estado: " + ladaarreglo[Convert.ToInt32(ladaencontrada), 2]);
                    return;
                }

            }
                //numeros enteros o decimales
                Match numerosvalidos = numerosdecimal.Match(texto);
                //texto sin simbolos
                Match textovalido = textos.Match(texto);
                //texto de direccion
                Match direcciones = direccion.Match(texto);
                //texto con simbolos
                Match textoconsimbolo = simbolos.Match(texto);
                //ecuacion matematica
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

            public static string lada(string numero,string[,] lada)
            {
                
                char[] numeroseparado = numero.ToArray();
                string encontrado = "ninguno";
                string ladabuscar = numeroseparado[0].ToString() + numeroseparado[1].ToString() + numeroseparado[2].ToString();
                for (int i=0; i<30;i++)
                {
                    if(ladabuscar == lada[i,0])
                    {
                        encontrado = i.ToString();
                    }
                }
            return encontrado;
            }

         public static string [,]  llenarlada()
        {
            string[,] ladasdisponibles = new string[30, 3];
            ladasdisponibles[0, 0] = "615";
            ladasdisponibles[0, 1] = "Villa Alberto A. \n Alvarado A. (El Fundo Legal)";
            ladasdisponibles[0, 2] = "Baja California";

            ladasdisponibles[1, 0] = "616";
            ladasdisponibles[1, 1] = "Camalú \n Colonia Vicente Guerrero \n Ejido General Leandro Valle \n Ejido José María Morelos\n El Rosario \n Isla de Cedros \n Poblado Héroes de Chapultepec \n Punta Cólonet \n Ruben Jaramillo \n San Quintín \n Venustiano Carranza(Santa María)";
            ladasdisponibles[1, 2] = "Baja California";

            ladasdisponibles[2, 0] = "646";
            ladasdisponibles[2, 1] = "Bajamar \n Colonia Benito García (El Zorrillo) \n El Porvenir (Guadalupe) \n El Rincón de Punta Banda \n El Sauzal \n Ensenada \n Erendira \n Francisco R. Serrano (Valle San Matías) \n Héroes de la Independencia \n La Bufadora \n La Misión \n Lázaro Cárdenas (Valle de la Trinidad) \n Maneadero \n Nacionalista de Sánchez Taboada \n Ojos Negros (Real del Castillo) \n San Vicente \n Santo Tomás \n Uruapan \n Valle de la Trinidad \n Villa de Juárez (San Antonio de las Minas) Baja California";
            ladasdisponibles[2, 2] = "Baja California";

            ladasdisponibles[3, 0] = "653";
            ladasdisponibles[3, 1] = "Estación Coahuila";
            ladasdisponibles[3, 2] = "Baja California";

            ladasdisponibles[4, 0] = "658";
            ladasdisponibles[4, 1] = "Benito Juárez (Tecolotes) \n Ciudad Morelos (Cuervos) \n Ejido Chiapas 1 \n Ejido Distrito Federal \n Ejido Dr. Alberto Oviedo Mota (El Indiviso) \n Ejido Guadalajara \n Ejido Lázaro Cárdenas \n Ejido Monterrey (Colonia Bataquez) \n Ejido Querétaro \n Ejido Quintana Roo \n Ejido Yucatán \n Guadalupe Victoria \n Hermosillo \n Ingeniero Francisco Murguía \n Mérida \n Paredones \n República Mexicana \n Saltillo \n Vicente Guerrero (Algodones) \n Villa Hermosa";
            ladasdisponibles[4, 2] = "Baja California";

            ladasdisponibles[5, 0] = "661";
            ladasdisponibles[5, 1] = "Marena Cove \n Playas de Rosarito \n Popotla \n Primo Tapia";
            ladasdisponibles[5, 2] = "Baja California";

            ladasdisponibles[6, 0] = "664";
            ladasdisponibles[6, 1] = "Ejido Ojo de Agua \n San Antonio del Mar \n Tijuana";
            ladasdisponibles[6, 2] = "Baja California";

            ladasdisponibles[7, 0] = "665";
            ladasdisponibles[7, 1] = "Ejido Nueva Colonia Hindú \n El Descanso \n El Hongo \n El Testerazo \n Tecate \n Valle de las Palmas";
            ladasdisponibles[7, 2] = "Baja California";

            ladasdisponibles[8, 0] = "686";
            ladasdisponibles[8, 1] = "Chihuahua \n Colonia La Puerta \n Delta (Estación Delta) \n Durango \n Ejido Guanajuato \n Ejido Sanson Flores \n Ejido Sinaloa \n Islas Agrarias Grupo B \n Jalapa \n Mexicali \n Michoacán de Ocampo \n Nayarit Llamada \n Nuevo Leó \n Puebla \n Rumorosa \n San Felipe";
            ladasdisponibles[8, 2] = "Baja California";

            ladasdisponibles[9, 0] = "612";
            ladasdisponibles[9, 1] = "Chametla \n El Centenario \n El Pescadero \n El Sargento \n La Paz \n San Juan de los Planes \n Todos Santos";
            ladasdisponibles[9, 2] = "Baja California Sur";

            ladasdisponibles[10, 0] = "613";
            ladasdisponibles[10, 1] = "Benito Juárez (Buenavista) \n Ciudad Constitución \n Loreto Baja \n Nopolo Baja \n Puerto Adolfo López Mateos \n Puerto San Carlos \n Villa Ignacio Zaragoza (Las Flores) \n Villa Insurgentes";
            ladasdisponibles[10, 2] = "Baja California Sur";

            ladasdisponibles[11, 0] = "615";
            ladasdisponibles[11, 1] = "Bahía Asunción \n Bahía Tortugas \n Estero de la Bocana \n Guerrero Negro \n Gustavo Díaz Ordaz (Vizcaíno) \n Mulegé \n Punta Abreojos \n San Bruno \n San Ignacio \n San Lucas \n Santa Rosalía";
            ladasdisponibles[11, 2] = "Baja California Sur";

            ladasdisponibles[12, 0] = "624";
            ladasdisponibles[12, 1] = "Buena Vista \n Cabo del Sol \n Cabo San Lucas \n La Ribera \n Las Lagunitas \n Miraflores \n San José del Cabo \n San José Viejo \n Santa Anita \n Santiago";
            ladasdisponibles[12, 2] = "Baja California Sur";

            ladasdisponibles[13, 0] = "622";
            ladasdisponibles[13, 1] = "Empalme \n Fraccionamiento Nuevo Empalme \n Guaymas \n José María Morelos y Pavón \n Ortiz \n San Carlos Nuevo Guaymas";
            ladasdisponibles[13, 2] = "Sonora";

            ladasdisponibles[14, 0] = "623";
            ladasdisponibles[14, 1] = "Aconchi \n Banámichi \n Baviácora \n Carbó \n Guadalupe de Ures \n Huepac \n Mazatán \n Pesqueira \n Pueblo de Alamos \n Rayón \n San Pedro de la Cueva \n Ures \n Villa Pesqueira \n Yécora";
            ladasdisponibles[14, 2] = "Sonora";

            ladasdisponibles[15, 0] = "631";
            ladasdisponibles[15, 1] = "Cibuta \n Nogales";
            ladasdisponibles[15, 2] = "Sonora";

            ladasdisponibles[16, 0] = "632";
            ladasdisponibles[16, 1] = "Ímuris \n Magdalena de Kino \n San Ignacio \n Terrenate";
            ladasdisponibles[16, 2] = "Sonora";

            ladasdisponibles[17, 0] = "633";
            ladasdisponibles[17, 1] = "Agua Prieta \n Esqueda \n Fronteras \n Naco";
            ladasdisponibles[17, 2] = "Sonora";

            ladasdisponibles[18, 0] = "634";
            ladasdisponibles[18, 1] = "Arivechi \n Arizpe \n Bacadéhuachi \n Bacerac \n Bavispe \n Colonia El Tajo \n Cumpas \n Huachinera \n Huasabas \n La Caridad (Fracción G) \n Los Abanicos \n Los Globos \n Moctezuma \n Nacori Chico \n Nacozari \n Sahuaripa \n Tepache \n Villa Hidalgo";
            ladasdisponibles[18, 2] = "Sonora";

            ladasdisponibles[19, 0] = "637";
            ladasdisponibles[19, 1] = "Altar \n Atil \n Caborca \n Pitiquito \n Plutarco Elías Calles Dos (La Y Griega) \n Puerto Libertad \n Saric \n Sasabe \n Tubutama";
            ladasdisponibles[19, 2] = "Sonora";

            ladasdisponibles[20, 0] = "638";
            ladasdisponibles[20, 1] = "La Choya \n Puerto Peñasco";
            ladasdisponibles[20, 2] = "Sonora";

            ladasdisponibles[21, 0] = "641";
            ladasdisponibles[21, 1] = "Benjamin Hill \n El Claro \n Estación Llano \n Querobabi \n Santa Ana \n Trincheras";
            ladasdisponibles[21, 2] = "Sonora";

            ladasdisponibles[22, 0] = "642";
            ladasdisponibles[22, 1] = "Bacabachi \n Chinotahueca \n Masiaca \n Navojoa \n Pueblo Mayo";
            ladasdisponibles[22, 2] = "Sonora";

            ladasdisponibles[23, 0] = "643";
            ladasdisponibles[23, 1] = "Agua Blanca \n Buaysiacobe \n Colonia Jecopaco \n Ejido 31 de Octubre \n Ejido Francisco Javier Mina \n Jecopaco \n Pótam \n Primero de Mayo (Campo 77) \n Pueblo Yaqui \n Quetchehueca \n San Ignacio Río Muerto (Colonia Militar) \n San José de Bacum \n Vícam \n Villa Juárez";
            ladasdisponibles[23, 2] = "Sonora";

            ladasdisponibles[24, 0] = "644";
            ladasdisponibles[24, 1] = "Bacum \n Ciudad Obregón \n Ejido Cuauhtémoc (Campo Cinco) \n Esperanza \n Los Hornos \n Marte R. Gómez (Tobarito) \n Paredón Colorado (Paredón Viejo) \n Providencia";
            ladasdisponibles[24, 2] = "Sonora";

            ladasdisponibles[25, 0] = "645";
            ladasdisponibles[25, 1] = "Bacoachi \n Cananea \n Santa Cruz";
            ladasdisponibles[25, 2] = "Sonora";

            ladasdisponibles[26, 0] = "647";
            ladasdisponibles[26, 1] = "Álamos \n Bacobampo \n Became Nuevo \n El Chucarit \n Etchojoa \n Etchoropo \n Huatabampo \n Quiriego \n Rosario Tesopaco \n Yávaros (Isla Las Viejas)";
            ladasdisponibles[26, 2] = "Sonora";

            ladasdisponibles[27, 0] = "651";
            ladasdisponibles[27, 1] = "Sonoita";
            ladasdisponibles[27, 2] = "Sonora";

            ladasdisponibles[28, 0] = "653";
            ladasdisponibles[28, 1] = "Ejido Lagunitas \n Golfo de Santa Clara \n Independencia \n Ingeniero Luis B. Sánchez \n Islita \n San Luis Río Colorado";
            ladasdisponibles[28, 2] = "Sonora";

            ladasdisponibles[29, 0] = "662";
            ladasdisponibles[29, 1] = "Bahía de Kino \n Ejido La Victoria \n Hermosillo \n Miguel Alemán (La Doce) \n San Pedro O Saucito (San Pedro El Saucito)";
            ladasdisponibles[29, 2] = "Sonora";

            return ladasdisponibles;
        }

    }
}


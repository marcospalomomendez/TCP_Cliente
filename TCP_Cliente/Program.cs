
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TCP_Cliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //creamos cliente TCP sin conectarme aun
            TcpClient clientardo = new TcpClient();

            //intento conectar al server que este en la ip (localhost, es decir, yo mismo)
            //se bloqua hasta que se conecta
            clientardo.Connect("127.0.0.1", 5000);

            //obtengo el flujo de datos para recibir datos
            NetworkStream flujillo = clientardo.GetStream();

            for (int i = 0; i < 5; i++)
            {
                byte[] yoquieroBuffer = new byte[1024];

                //Read se bloquea hasta que el server envie datos
                //devuleve el numero de bytes leidos / recibidos
                int cosas_leidas = flujillo.Read(yoquieroBuffer, 0, yoquieroBuffer.Length);


                string mensajardo;

                //convierto los bytes recibidos a string
                mensajardo = Encoding.UTF8.GetString(yoquieroBuffer, 0, cosas_leidas);

                //muestro el mensaje recibido
                Console.WriteLine("El server dice esto: " + mensajardo);

            }

            //cierra el puerto y la conexion
            clientardo.Close();
        }
    }
}
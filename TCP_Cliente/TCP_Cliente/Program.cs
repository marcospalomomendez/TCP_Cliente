
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCP_Cliente
{
    internal class Program
    {

        public void EjemploTCPCliente()
        {
            //creamos cliente tcp sin conectarme aun
            TcpClient clientardo = new TcpClient();

            //intento conectar al server que este en la ip (localhost, es decir, yo mismo)
            //se bloquea hasta que se conecta
            clientardo.Connect("127.0.0.1", 5000);

            //objtengo el flujo de datos para recibir datos
            NetworkStream flujillo = clientardo.GetStream();

            //Buffer[] yoquiero = new Buffer[256]; 
            byte[] yoquierobuffer = new byte[2048];

            //Read se bloquea hasta que el server envie algo
            //devuelve el numero de bytes leidos / recibidos
            int cosas_leidas = flujillo.Read(yoquierobuffer, 0, yoquierobuffer.Length);


            string mensajardo;

            mensajardo = Encoding.UTF8.GetString(yoquierobuffer, 0, cosas_leidas);

            Console.WriteLine(mensajardo);

            clientardo.Close();
        }

        public void EjemploTCPClienteEscuchador()
        {
            TcpClient cliente = new TcpClient();
            cliente.Connect("127.0.0.1", 5000);

            NetworkStream stream = cliente.GetStream();
            byte[] data = new byte[1024];

            while (true)
            {
                int num_lectura = stream.Read(data, 0, data.Length);
                if (num_lectura <= 0)
                {
                    break;
                }
                string textin = Encoding.UTF8.GetString(data, 0, num_lectura);
                Console.WriteLine(textin);
            }

            cliente.Close();

        }

        public void EjemploTCPMulti()
        {
            TcpClient cliente = new TcpClient();
            cliente.Connect("127.0.0.1", 5000);
            NetworkStream flujo = cliente.GetStream();

            Thread t = new Thread(() =>
            {
                byte[] b = new byte[1024];
                while (true)
                {
                    int num_lectura = flujo.Read(b, 0, b.Length);
                    if (num_lectura <= 0)
                    {
                        break;
                    }

                    Console.WriteLine(Encoding.UTF8.GetString(b, 0, num_lectura));
                }
            });

            t.IsBackground = true;
            t.Start();

            while (true)
            {
                string line = Console.ReadLine();
                if (line == "/salir")
                {
                    break;
                }

                byte[] d = Encoding.UTF8.GetBytes(line + "\n");
                flujo.Write(d, 0, d.Length);
            }

            cliente.Close();
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.EjemploTCPClienteEscuchador();
        }
    }
}
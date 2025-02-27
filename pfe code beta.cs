//Install-Package NModbus4
// bib a installer
using System;
using System.Net.Sockets;
using Modbus.Data;
using Modbus.Device;

class Program
{
    static void Main(string[] args)
    {
        // Configuration de l'automate
        string ipAutomate = "192.168.1.100"; // Remplacez par l'adresse IP de votre automate
        int portAutomate = 502; // Port par défaut pour Modbus TCP

        // Création d'un client TCP
        TcpClient client = new TcpClient(ipAutomate, portAutomate);
        ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

        try
        {
            // Lecture des registres
            // Exemple : Lire les états (Running, Piece OK, Piece NOK)
            bool[] coils = master.ReadCoils(0, 3); // Lire 3 coils à partir de l'adresse 0
            Console.WriteLine($"Running: {coils[0]}, Piece OK: {coils[1]}, Piece NOK: {coils[2]}");

            // Envoi d'une commande (exemple : remise à zéro des compteurs)
            master.WriteSingleCoil(1, true); // Écrire un bit à l'adresse 1 (exemple)
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
        }
        finally
        {
            // Déconnexion
            client.Close();
        }
    }
}
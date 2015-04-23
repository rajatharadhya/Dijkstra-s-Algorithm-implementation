/*
 * Module Name : UserApp
 * Author :Rajath Aradhya
 * brief description : This program controls the flow of the program.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PureMichigan
{
    class UserApp
    {
        static void Main(string[] args)
        {
            MapData map = new MapData();
            StreamWriter log = new StreamWriter(map.GetDirectory(@"\log.txt"));
            map.LoadMapData(log);
            StreamReader readline = new StreamReader(map.GetDirectory(@"\CityPairs.txt"));
            RouteFinder getroute = new RouteFinder();
            while (!readline.EndOfStream)
            {
                log.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
                log.WriteLine(" ");
                string line = readline.ReadLine();
                string[] transaction = line.Split(' ');
                int city1num = map.GetCityNumber(transaction[0]);
                if (city1num == -1)
                {
                    log.WriteLine("START: " + transaction[0]);
                    log.WriteLine("ERROR: start city not in Michigan Map Data");
                }
                else
                {
                    log.WriteLine("START: " + transaction[0] + " (" + city1num + ") " + map.GetCityPeninsula(transaction[0]));
                    int city2num = map.GetCityNumber(transaction[1]);
                    if (city2num == -1)
                    {
                        log.WriteLine("Destination: " + transaction[1]);
                        log.WriteLine("ERROR: destination city not in Michigan Map Data");
                    }
                    else
                    {
                        log.WriteLine("Destination: " + transaction[1] + " (" + city2num + ") " + map.GetCityPeninsula(transaction[1]));
                        getroute.FindShortestRoute(city1num, city2num, log, map);
                    }
                }
                log.WriteLine(" ");
            }
            log.Close();
            readline.Close();
        }
    }
}

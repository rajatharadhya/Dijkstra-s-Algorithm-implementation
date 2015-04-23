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
            map.LoadMapData();
            StreamReader readline = new StreamReader(map.GetDirectory(@"\CityPairs.txt"));
            while (!readline.EndOfStream)
            {
                string line = readline.ReadLine();
                string[] transaction = line.Split(' ');
                RouteFinder getroute = new RouteFinder();
                int city1num = map.GetCityNumber(transaction[0]);
                int city2num = map.GetCityNumber(transaction[1]);
                getroute.FindShortestRoute(city1num, city2num);
            }
        }
    }
}

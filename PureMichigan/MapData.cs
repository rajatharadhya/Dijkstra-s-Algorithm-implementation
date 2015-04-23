/*
 * Module Name : MapData
 * Author :Rajath Aradhya
 * brief description : This program forms the map of mishigan using 2dimensional array .
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PureMichigan
{
    class MapData
    {
        static int MAX_N = 200;
        public int[,] roadDistance = new int[MAX_N, MAX_N];
        string[] upCityList = new string[MAX_N];
        string[] cityNameList = new string[MAX_N];
        public int n;
        int maxintvalue = int.MaxValue;
        public void LoadMapData(StreamWriter log)
        {
            n = 0;
            StreamReader read = new StreamReader(GetDirectory(@"\MichiganRoads.txt"));
            while (!read.EndOfStream)
            {
                string line = read.ReadLine();
                if (line.StartsWith("up"))
                {
                    int len = line.Length;
                    line = line.Substring(5, len - 9);
                    string[] stringSeparators = new string[] { ", " };
                    upCityList = line.Split(stringSeparators, StringSplitOptions.None);
                }
                else if (line.StartsWith("dist"))
                {
                    int len = line.Length;
                    line = line.Substring(5, len - 7);
                    string[] stringSeparators = new string[] { ", " };
                    string[] linesplit = new string[10];
                    linesplit = line.Split(stringSeparators, StringSplitOptions.None);
                    if (n == 0 || n == 1)
                    {
                        StoreCityName(linesplit[0]);
                        StoreCityName(linesplit[1]);
                    }
                    if (cityNameList.Contains(linesplit[0])) ;
                    else StoreCityName(linesplit[0]);
                    if (cityNameList.Contains(linesplit[1])) ;
                    else StoreCityName(linesplit[1]);
                    AddToRoadDistance(linesplit);
                }
            }
            for (int i = 0; i < MAX_N; i++)
                for (int j = 0; j < MAX_N; j++)
                    if (roadDistance[i, j] == 0)
                        roadDistance[i, j] = maxintvalue;
            for (int i = 0; i < MAX_N; i++)
                for (int j = 0; j < MAX_N; j++)
                    if (i == j)
                        roadDistance[i, j] = 0;
            read.Close();
        }
        // **************************************************************************************
        public void AddToRoadDistance(string[] distadd)
        {
            int i = GetCityNumber(distadd[0]);
            int j = GetCityNumber(distadd[1]);
            roadDistance[i, j] = Convert.ToInt32(distadd[2]);
            roadDistance[j, i] = Convert.ToInt32(distadd[2]);
        }
        // **************************************************************************************
        public string GetCityPeninsula(string city)
        {
            if (upCityList.Contains(city))
                return "UP";
            else
                return "LP";
        }
        // **************************************************************************************
        public int GetCityNumber(string city)
        {
            int i = Array.IndexOf(cityNameList, city);
            return i;
        }
        // **************************************************************************************
        public int GetRoadDistance(string city1, string city2)
        {
            int i = GetCityNumber(city1);
            int j = GetCityNumber(city2);
            int dist = roadDistance[i, j];
            return dist;
        }
        // **************************************************************************************
        public string GetCityName(int citynumber)
        {
            string cityname = cityNameList[citynumber];
            return cityname;
        }
        // **************************************************************************************
        private int StoreCityName(string city)
        {
            cityNameList[n] = city;
            n++;
            return n;
        }
        // **************************************************************************************
        public string GetDirectory(string fileToRead)
        {
            string currentDir = Directory.GetCurrentDirectory();
            currentDir = Directory.GetParent(currentDir).ToString();
            currentDir = Directory.GetParent(currentDir).ToString();
            currentDir += fileToRead;
            return currentDir;
        }
    }
}

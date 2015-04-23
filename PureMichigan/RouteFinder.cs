/*
 * Module Name : RouteFinder
 * Author :Rajath Aradhya
 * brief description : This program initialize the arrays required,search the destination and find the 
 * shortest path and finally report the answers.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PureMichigan
{
    class RouteFinder
    {
        int destinationnum;
        int[] distance;
        int[] predecessor;
        bool[] included;
        int track = 0;
        public void FindShortestRoute(int startcitynum, int destcitynum, StreamWriter log, MapData mapData)
        {
            destinationnum = destcitynum;
            InitializeArrays(startcitynum, log, mapData);
            SearchForPath(startcitynum, destcitynum, log, mapData);
            ReportAnswers(log, mapData);
        }
        // **************************************************************************************
        private void InitializeArrays(int startValue, StreamWriter log, MapData mapData)
        {
            distance = new int[mapData.n];
            predecessor = new int[mapData.n];
            included = new bool[mapData.n];
            for (int i = 0; i < mapData.n; i++)
            {
                distance[i] = mapData.roadDistance[startValue, i];
                if (mapData.roadDistance[startValue, i] != int.MaxValue && mapData.roadDistance[startValue, i] != 0)
                {
                    predecessor[i] = startValue;
                }
                else
                {
                    predecessor[i] = -1;
                }
                included[i] = false;
            }
            included[startValue] = true;
        }

        // **************************************************************************************
        private void SearchForPath(int startCity, int destinationCity, StreamWriter log, MapData mapData)
        {
            int targetDistance = int.MaxValue;
            int target = startCity;
            string startcityname = mapData.GetCityName(startCity);
            string destcityname = mapData.GetCityName(destinationCity);
            string startpeninsula = mapData.GetCityPeninsula(startcityname);
            string destpeninsula = mapData.GetCityPeninsula(destcityname);
            if (startpeninsula != destpeninsula && track == 0 && destcityname != "theBridge" && startcityname != "theBridge")
            {
                log.Write("[*****2 different peninsulas, so 2 partial routes*****]");
            }
            log.WriteLine(" ");
            log.Write("TRACE OF TARGETS:  " + mapData.GetCityName(startCity) + " ");
            while (included[destinationCity] != true)
            {
                targetDistance = int.MaxValue;
                for (int i = 0; i < (mapData.n); i++)
                {
                    if (included[i] == false)
                    {
                        if (targetDistance > distance[i])
                        {
                            target = i;
                            targetDistance = distance[i];
                        }

                    }
                }
                log.Write(mapData.GetCityName(target) + " ");
                included[target] = true;
                for (int i = 0; i < (mapData.n); i++)
                {
                    if (included[i] == false)
                    {
                        if (mapData.roadDistance[target, i] != 0 &&
                            mapData.roadDistance[target, i] != int.MaxValue)
                        {
                            if (distance[target] + mapData.roadDistance[target, i] < distance[i])
                            {
                                distance[i] = distance[target] + mapData.roadDistance[target, i];
                                predecessor[i] = target;
                            }
                        }
                    }
                }
                if (startpeninsula != destpeninsula && destcityname != "theBridge"&& startcityname!="theBridge")
                {
                  string  targetname = mapData.GetCityName(target);
                  if (targetname == "theBridge")
                  {
                      destinationnum = target;
                      ReportAnswers(log, mapData);
                      FindShortestRoute(target, destinationCity, log, mapData);
                      track = 1;
                  }
                }
            }
            log.Write("");
        }

        // **************************************************************************************
        private void ReportAnswers(StreamWriter log, MapData mapData)
        {
            if (track == 0)
            {
                int[] finalOrderOfNodes;
                int[] tempOrderOfNodes = new int[mapData.n];
                int loopcounter = 1;
                int pathNodes = 0;
                int counterForReverse;
                if (predecessor[destinationnum] != -1)
                {
                    tempOrderOfNodes[0] = predecessor[destinationnum];
                    while (loopcounter < mapData.n)
                    {
                        int tempValue;
                        tempValue = predecessor[tempOrderOfNodes[loopcounter - 1]];
                        if (tempValue != -1)
                        {
                            tempOrderOfNodes[loopcounter] = tempValue;
                        }
                        else
                        {
                            pathNodes = loopcounter;
                            loopcounter = mapData.n;
                        }
                        loopcounter++;
                    }
                    counterForReverse = pathNodes - 1;
                    finalOrderOfNodes = new int[pathNodes];
                    log.WriteLine(" ");
                    log.WriteLine(" ");
                    log.WriteLine("TOTAL DISTANCE:  " + distance[destinationnum] + " miles");
                    log.Write("SHORTEST ROUTE:  ");
                    for (int i = 0; i < pathNodes; i++)
                    {
                        finalOrderOfNodes[i] = tempOrderOfNodes[counterForReverse];
                        counterForReverse--;
                        log.Write(mapData.GetCityName(finalOrderOfNodes[i]));
                        log.Write(" > ");
                    }
                    log.WriteLine(mapData.GetCityName(destinationnum));
                    log.WriteLine(" ");
                }
                else
                {
                    log.WriteLine(" ");
                    log.WriteLine("TOTAL DISTANCE: " + distance[destinationnum] + " miles");
                    log.Write("SHORTEST ROUTE:   " + mapData.GetCityName(destinationnum));
                    log.WriteLine(" ");
                }
            }
            else track = 0;
        }

        // **************************************************************************************

    }
}

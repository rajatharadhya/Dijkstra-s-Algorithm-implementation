using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureMichigan
{
    class RouteFinder
    {
        int destinationnum;
        int[] distance;
        int[] predecessor;
        bool[] included;
        MapData mapData = new MapData();
        public void FindShortestRoute(int startcitynum, int destcitynum)
        {
            destinationnum = destcitynum;
            InitializeArrays(startcitynum);
            SearchForPath(startcitynum, destcitynum);
            ReportAnswers();
        }
        // **************************************************************************************
        private void InitializeArrays(int startValue)
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
        private void SearchForPath(int startCity, int destinationCity)
        {
           // logFileClass.WriteToLogFile("TRACE OF TARGETS:  " + mapData.GetCityName(startCity) + " ");

            int targetDistance = int.MaxValue;
            int target = startCity;

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
             //   logFileClass.WriteToLogFile(mapData.GetCityName(target) + " ");
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

            }
//logFileClass.WriteLineToLogFile("");
        }

        // **************************************************************************************
        private void ReportAnswers()
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

                //logFileClass.WriteLineToLogFile("SHORTEST ROUTE:  " + distance[DestinationCityNumber] + " miles");
                //logFileClass.WriteToLogFile("PATH:  ");
                for (int i = 0; i < pathNodes; i++)
                {
                    finalOrderOfNodes[i] = tempOrderOfNodes[counterForReverse];
                    counterForReverse--;
                   // logFileClass.WriteToLogFile(mapData.GetCityName(finalOrderOfNodes[i]));

                  //  logFileClass.WriteToLogFile(" / ");


                }
            //    logFileClass.WriteLineToLogFile(mapData.GetCityName(DestinationCityNumber));

            }
            else
            {
                //logFileClass.WriteLineToLogFile("SHORTEST ROUTE:  0");
                //logFileClass.WriteLineToLogFile("PATH: " + mapData.GetCityName(DestinationCityNumber));

            }
        }

        // **************************************************************************************

    }
}

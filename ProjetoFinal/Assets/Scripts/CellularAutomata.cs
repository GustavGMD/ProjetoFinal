using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellularAutomata {

	/// <summary>
    /// Returns a mpa filed with 0s and 1s. 1 is a living cell while 0 is a dead one.
    /// </summary>
    /// <param name="p_width"></param>
    /// <param name="p_height"></param>
    /// <param name="p_density">How many living cells the map should contain, in a percentage</param>
    /// <param name="p_seed"></param>
    /// <returns></returns>
    public static int[][] GetRandomBinaryMap(int p_width, int p_height, float p_density, int p_seed)
    {
        int[][] __map = new int[p_height][];
        for (int i = 0; i < __map.Length; i++)
        {
            __map[i] = new int[p_width];
        }

        Random.seed = p_seed;

        for (int i = 0; i < __map.Length; i++)
        {
            for (int j = 0; j < __map[i].Length; j++)
            {
                __map[i][j] = Random.value >= p_density ? 0 : 1;
            }
        }

        return __map;
    }
    public static int[][] GetRandomBinaryMap(int p_width, int p_height, float p_density)
    { 
        return GetRandomBinaryMap(p_width, p_height, p_density, Random.seed);
    }

    public static int[][] SimulateMap(int[][] p_map)
    {
        int[][] __newMap = p_map;

        for (int i = 0; i < p_map.Length; i++)
        {
            for (int j = 0; j < p_map[i].Length; j++)
            {
                int __sum = 0;
                for (int ii = i-1; ii < i+2; ii++)
                {
                    for (int jj = j - 1; jj < j + 2; jj++)
                    {
                        if (ii >= 0 && jj >= 0 && ii <= p_map.Length-1 && jj <= p_map[i].Length-1)
                        {
                            if (!(ii == i && jj == j))
                            {
                                if (p_map[ii][jj] == 1)
                                {
                                    __sum++;
                                }
                            }
                        }
                        else
                        {
                            __sum++;
                        }
                    }
                }
                
                if (p_map[i][j] == 1)
                {
                    if (__sum >= 4)
                    {
                        p_map[i][j] = 1;
                    }
                    else
                    {
                        p_map[i][j] = 0;
                    }
                }
                else
                {
                    if (__sum >= 5)
                    {
                        p_map[i][j] = 1;
                    }
                    else
                    {
                        p_map[i][j] = 0;
                    }
                }
            }
        }

        return __newMap;
    }

    public static int[][] SimulateMap(int[][] p_map, int p_howManyTimes)
    {
        int[][] __map = p_map;

        for (int i = 0; i < p_howManyTimes ; i++)
        {
            __map = SimulateMap(__map);
        }

        return __map;
    }

    public static float GetFilledPercentage(int[][] p_map)
    {
        float __fill = 0;
        int __total = 0;
        for (int i = 0; i < p_map.Length; i++)
        {
            for (int j = 0; j < p_map[i].Length; j++)
            {
                __fill += p_map[i][j];
                __total++;
            }
        }
        
        return (1 - (__fill / __total));
    }

    public static float GetBestFloodFilledPercentage(int[][] p_map)
    {
        int __fillingGroup = 0;
        int __groupCount = 0;
        int __largestGroupIndex = 0;
        int __largestGroupSize = 0;

                
        for (int i = 0; i < p_map.Length; i++)
        {
            for (int j = 0; j < p_map[i].Length; j++)
            {
                if (p_map[i][j] == 0)
                {
                    p_map = FloodFill(p_map, j, i, 0, (__fillingGroup+2));
                    __fillingGroup++;
                }
            }
        }

        for (int k = 2; k < __fillingGroup+2; k++)
        {
            __groupCount = 0;
            for (int i = 0; i < p_map.Length; i++)
            {
                for (int j = 0; j < p_map[i].Length; j++)
                {
                    if (p_map[i][j] == k)
                    {
                        __groupCount++;
                    }
                }
            }

            if(__groupCount > __largestGroupSize)
            {
                __largestGroupIndex = k;
                __largestGroupSize = __groupCount;
            }
        }

        return (1 - ((float)__largestGroupSize / (p_map.Length*p_map.Length)));
    }

    public static int[][] FloodFill(int[][] p_map, int p_x, int p_y, int p_targetValue, int p_replacementValue)
    {
        int[][] __newMap = p_map;

        if(__newMap[p_y][p_x] == p_targetValue)
        {
            __newMap[p_y][p_x] = p_replacementValue;
            for (int i = p_y - 1; i < p_y + 2; i++)
            {
                for (int j = p_x - 1; j < p_x + 2; j++)
                {                    
                    if (i >= 0 && j >= 0 && i <= __newMap.Length - 1 && j <= __newMap.Length - 1)
                    {
                        if (!(i == p_y && j == p_x))
                        {
                            __newMap = FloodFill(__newMap, j, i, p_targetValue, p_replacementValue);
                        }
                    }
                }                
            }
        }

        return __newMap;
    }

    public static int[][] FillExclaves(int[][] p_map)
    {
        int __fillingGroup = 0;
        int __groupCount = 0;
        int __largestGroupIndex = 0;
        int __largestGroupSize = 0;


        for (int i = 0; i < p_map.Length; i++)
        {
            for (int j = 0; j < p_map[i].Length; j++)
            {
                if (p_map[i][j] == 0)
                {
                    p_map = FloodFill(p_map, j, i, 0, (__fillingGroup + 2));
                    __fillingGroup++;
                }
            }
        }

        for (int k = 2; k < __fillingGroup + 2; k++)
        {
            __groupCount = 0;
            for (int i = 0; i < p_map.Length; i++)
            {
                for (int j = 0; j < p_map[i].Length; j++)
                {
                    if (p_map[i][j] == k)
                    {
                        __groupCount++;
                    }
                }
            }

            if (__groupCount > __largestGroupSize)
            {
                __largestGroupIndex = k;
                __largestGroupSize = __groupCount;
            }
        }

        for (int i = 0; i < p_map.Length; i++)
        {
            for (int j = 0; j < p_map[i].Length; j++)
            {
                if(p_map[i][j] == __largestGroupIndex)
                {
                    p_map[i][j] = 0;
                }
                else
                {
                    p_map[i][j] = 1;
                }
            }
        }

        return p_map;
    }
}

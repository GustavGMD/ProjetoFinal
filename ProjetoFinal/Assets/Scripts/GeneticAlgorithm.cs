using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneticAlgorithm : MonoBehaviour {

    public struct Chromossome
    {
        public int width;
        public int height;
        public int[] genes;
        public float fitness;

        public Chromossome(int p_width, int p_height)
        {
            width = p_width;
            height = p_height;
            genes = new int[p_width * p_height];
            fitness = 0;
        }

        public void InitializeRandom()
        {
            float __chance = (Random.value * 0.1f) - 0.05f;    
            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = Random.value <= 0.5f + __chance ? 0 : 1;                
            }
        }

        public int[][] GetMap()
        {
            int[][] __map = new int[height][];
            for (int i = 0; i < __map.Length; i++)
            {
                __map[i] = new int[width];                
            }
            
            for (int i = 0; i < __map.Length; i++)
            {
                for (int j = 0; j < __map[i].Length; j++)
                {
                    __map[i][j] = genes[(i * width) + j];
                    //Debug.Log(__map[i][j]);
                }
            }

            //Debug.Log("Retornou");
            return __map;
        }

        public static bool operator >(Chromossome c1, Chromossome c2)
        {
            return c1.fitness > c2.fitness;
        }
        public static bool operator <(Chromossome c1, Chromossome c2)
        {
            return c1.fitness < c2.fitness;
        }

        public static bool operator ==(Chromossome c1, Chromossome c2)
        {
            bool __equal = true;
            for (int i = 0; i < c1.genes.Length; i++)
            {
                if (c1.genes[i] != c2.genes[i]) return false;
            }

            return __equal;
        }

        public static bool operator !=(Chromossome c1, Chromossome c2)
        {
            bool __equal = false;
            for (int i = 0; i < c1.genes.Length; i++)
            {
                if (c1.genes[i] != c2.genes[i]) return true;
            }

            return __equal;
        }
    };

    public TestManager testManager;
    public List<Chromossome> population;
    public List<Chromossome> testPop;
    public int generation = 0;

    //fitness targets
    public int mapSize = 50;
    public int populationSize = 50;
    public int eliteSize = 4;
    public float mutationRate = 0.00015f;
    public float fitnessBaseValue = 10000;
    public float fitnessPointsWeight = 0.5f;
    public float fillTarget = 0.4f;
    public Vector2[] fitnessPoints;
    
    public void EvaluateFitness()
    {
        //DebugLogPopulation();

        //testPop = new List<Chromossome>();

        /**
        for (int i = 0; i < population.Count; i++)
        {
            testPop.Add(population[i]);
        }
        /**/

        for (int i = 0; i < population.Count; i++)
        {
            float __tempFitness = 0;
            Chromossome __tempChromossome = population[i];
            int[][] __simulatedMap = CellularAutomata.SimulateMap(__tempChromossome.GetMap(), 5);
            __simulatedMap = CellularAutomata.FillExclaves(__simulatedMap); //by doing this we end up having only the largest cave area            

            //first we calculate the fitness from Fill Ratio
            float __fill = CellularAutomata.GetFilledPercentage(__simulatedMap);
            __tempFitness += (1 - (Mathf.Abs(fillTarget - __fill))) * fitnessBaseValue;

            //now we calculate the fitness based on points
            for (int j = 0; j < fitnessPoints.Length; j++)
            {
                if (__simulatedMap[(int)fitnessPoints[j].y][(int)fitnessPoints[j].x] == 0)
                {
                    __tempFitness += fitnessBaseValue * fitnessPointsWeight;
                }
            }

            __tempChromossome.fitness = __tempFitness;
            
            population[i] = __tempChromossome;
        }

        /**
        for (int i = 0; i < population.Count; i++)
        {
            Debug.Log(testPop[i] == population[i]);
        }
        /**/

        //DebugLogPopulation();
    }
    
    public void ChangeGeneration()
    {
        //DebugLogPopulation();

        List<Chromossome> __newPopulation = new List<Chromossome>();

        for (int i = 0; i < eliteSize; i++)
        {
            __newPopulation.Add(population[i]);
            //Debug.Log("Generation: " + generation + "     Checking Elite " + i + ": " + __newPopulation[i].fitness);
        }

        for (int i = eliteSize; i < populationSize; i += 2)
        {
            Chromossome __parent1 = SelectIndividual();
            Chromossome __parent2 = SelectIndividual(__parent1);
            //Debug.Log("Are parents equal? " + (__parent1 == __parent2));

            Chromossome[] __tempChromossome = Crossover(__parent1, __parent2);

            __newPopulation.Add(__tempChromossome[0]);
            if(i < populationSize-1) __newPopulation.Add(__tempChromossome[1]);
        }

        /**
        for (int i = 0; i < population.Count ; i++)
        {
            Debug.Log("Are populations equal? " + (population[i] == __newPopulation[i]));
        }
        /**/

        //Debug.Log(population.Count + " " + __newPopulation.Count);
        population = __newPopulation;
        generation++;

        //DebugLogPopulation();
    }

    public Chromossome[] Crossover(Chromossome p_parent1, Chromossome p_parent2)
    {
        int __lenght = p_parent1.genes.Length;
        bool __swap = false;
        Chromossome[] __children = new Chromossome[2];

        //initializing
        __children[0] = new Chromossome(mapSize, mapSize);
        __children[1] = new Chromossome(mapSize, mapSize);

        //Debug.Log("Are parents equal? " + (p_parent1 == p_parent2));

        //We are using "Uniform Crossover"
        for (int i = 0; i < __lenght; i++)
        {
            if (Random.value < 0.5f) __swap = !__swap;

            __children[0].genes[i] = __swap ? p_parent2.genes[i] : p_parent1.genes[i];
            __children[1].genes[i] = __swap ? p_parent1.genes[i] : p_parent2.genes[i];

            if (Random.value <= mutationRate)
            {
                __children[0].genes[i] = __children[0].genes[i] == 0 ? 1 : 0;
                //Debug.Log("Mutation");
            }
            if (Random.value <= mutationRate)
            {
                __children[1].genes[i] = __children[1].genes[i] == 0 ? 1 : 0;
                //Debug.Log("Mutation");
            }
        }

        return __children;
    }

    public Chromossome SelectIndividual()
    {
        float __totalFitness = 0;
        float __selectedFitness = 0;
        Chromossome __selectedChromossome = new Chromossome(mapSize, mapSize);

        for (int i = 0; i < populationSize; i++)
        {
            __totalFitness += population[i].fitness;
        }

        __selectedFitness = Random.value * __totalFitness;

        for (int i = 0; i < populationSize; i++)
        {
            if(population[i].fitness >= __selectedFitness)
            {
                __selectedChromossome = population[i];
                break;
            }
            else
            {
                __selectedFitness -= population[i].fitness;
            }            
        }

        return __selectedChromossome;
    }

    public Chromossome SelectIndividual(Chromossome p_differentFromThis)
    {
        float __totalFitness = 0;
        float __selectedFitness = 0;
        int __count = 0;
        Chromossome __selectedChromossome = p_differentFromThis;

        for (int i = 0; i < populationSize; i++)
        {
            __totalFitness += population[i].fitness;
        }

        while (__selectedChromossome == p_differentFromThis && __count < 10)
        {
            __selectedFitness = Random.value * __totalFitness;

            for (int i = 0; i < populationSize; i++)
            {
                if (population[i].fitness >= __selectedFitness)
                {
                    __selectedChromossome = population[i];
                    break;
                }
                else
                {
                    __selectedFitness -= population[i].fitness;
                }
            }

            __count++;
        }

        return __selectedChromossome;
    }

    public void OrderPopulation()
    {
        //DebugLogPopulation();

        List<Chromossome> __ordered = new List<Chromossome>();

        while (population.Count > 0)
        {
            float __bestFitness = 0;
            int __bestFitnessIndex = 0;
            for (int i = 0; i < population.Count; i++)
            {
                if (population[i].fitness > __bestFitness)
                {
                    __bestFitness = population[i].fitness;
                    __bestFitnessIndex = i;
                }
            }

            __ordered.Add(population[__bestFitnessIndex]);
            population.RemoveAt(__bestFitnessIndex);
        }

        population = __ordered;

        //DebugLogPopulation();
    }

    public void Initialize()
    {
        population = new List<Chromossome>();
        for (int i = 0; i < populationSize; i++)
        {
            Chromossome __temp = new Chromossome(mapSize, mapSize);
            __temp.InitializeRandom();
            population.Add(__temp);
        }
        
        EvaluateFitness();
        OrderPopulation();

        //Debug.Log("Generation: " + generation + " Best Fitness: " + population[0].fitness);

        /**
        for (int i = 0; i < population.Count; i++)
        {
            Debug.Log(population[i].fitness);
        }     
        /**/
    }

    public void Simulate()
    {
        //Debug.Log("STARTING SIMULATION -------------------------------------------");
        ChangeGeneration();
        EvaluateFitness();
        OrderPopulation();

        //Debug.Log("Generation: " + generation);
        //Debug.Log(" Best Fitness: " + population[0].fitness);
        //DebugLogPopulation();

        //Debug.Log("------------------------------------------- FINISHED SIMULATION");
    }

    public void DebugLogPopulation()
    {
        string __log = "";
        for (int i = 0; i < population.Count; i++)
        {
            __log += population[i].fitness.ToString() + " ";
        }

        Debug.Log(__log);
    }
}

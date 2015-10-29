using System;
using System.Collections.Generic;

namespace RaceGame.Pathfinding
{
    /// <summary>
    /// Class voor het efficiënter zoeken naar gelijke AStarObjecten in een boom structuur ipv. een lijst.
    /// </summary>
    public class Heap<T> where T : IHeapItem<T>
    {

        T[] items;
        int currentItemCount;

        /// <summary>
        /// Constructor, vereist een max grootte
        /// </summary>
        /// <param name="maxHeapSize"></param>
        public Heap(int maxHeapSize)
        {
            items = new T[maxHeapSize];
        }

        /// <summary>
        /// Voegt een item toe aan de heap
        /// </summary>
        public void Add(T item)
        {
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item);
            currentItemCount++;
        }

        /// <summary>
        /// Verwijdert en retourneert het eerste item in de heap
        /// </summary>
        public T RemoveFirst()
        {
            T firstItem = items[0];
            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);
            return firstItem;
        }

        /// <summary>
        /// Geeft de mogelijkheid om een item te updaten.
        /// </summary>
        public void UpdateItem(T item)
        {
            SortUp(item);
        }

        /// <summary>
        /// Huidige aantal items in de heap
        /// </summary>
        public int Count
        {
            get
            {
                return currentItemCount;
            }
        }

        /// <summary>
        /// Contains functie die checkt in de heap op de positie van item T
        /// </summary>
        public bool Contains(T item)
        {
            return Equals(items[item.HeapIndex], item);
        }

        /// <summary>
        /// Sorteert omlaag en swapped de items totdat het item gevonden is.
        /// </summary>
        void SortDown(T item)
        {
            while (true)
            {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;
                int swapIndex = 0;

                if (childIndexLeft < currentItemCount)
                {
                    swapIndex = childIndexLeft;

                    if (childIndexRight < currentItemCount)
                    {
                        if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0)
                        {
                            swapIndex = childIndexRight;
                        }
                    }

                    if (item.CompareTo(items[swapIndex]) < 0)
                    {
                        Swap(item, items[swapIndex]);
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Sorteert omhoog en swapped de items wanneer gevonden.
        /// </summary>
        void SortUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;

            while (true)
            {
                T parentItem = items[parentIndex];
                if (item.CompareTo(parentItem) > 0)
                {
                    Swap(item, parentItem);
                }
                else
                {
                    break;
                }
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }

        /// <summary>
        /// Wisselt twee items van posities in de heap
        /// </summary>
        void Swap(T itemA, T itemB)
        {
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;
            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }
    }

    /// <summary>
    /// Interface die ervoor zorgt dat een class de juiste properties implementeert.
    /// Vereist dat IComparable<T> geimplementeerd is.
    /// </summary>
    public interface IHeapItem<T> : IComparable<T>
    {
        int HeapIndex
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Class die gebruikt wordt in het A* algoritme om alles bij te houden. Implementeerd IHeapItem<T>
    /// </summary>
    [Serializable]
    public class AstarObject : IHeapItem<AstarObject>
    {
        public int x;
        public int y;
        public Game GameRef;

        /// <summary>
        /// Retourneert het soortweg op de positie van het AStarObject
        /// </summary>
        public RoadType RType
        {
            get { return (RoadType)GameRef.GameField[x, y]; }
        }

        /// <summary>
        /// De gCost, afstand vanaf het startpunt
        /// </summary>
        public int gCost { get; set; }

        /// <summary>
        /// de hCost, afstand vanaf het eindpunt
        /// </summary>
        public int hCost { get; set; }

        /// <summary>
        /// Optelsom van gCost en hCost
        /// </summary>
        public int fCost
        {
            get { return gCost + hCost; }
        }

        /// <summary>
        /// Onthoud de parent waar het huidige object vandaan kwam. 
        /// M.A.W de vorige positie in het pad.
        /// </summary>
        public AstarObject parent { get; set; }

        /// <summary>
        /// Constructor van AstarObject. Vereist een Game Object om GameField[,] te bereiken.
        /// </summary>
        public AstarObject(int x, int y, Game gRef)
        {
            this.x = x;
            this.y = y;
            GameRef = gRef;
        }

        /// <summary>
        /// Conversie naar een Point
        /// </summary>
        /// <returns></returns>
        public Point ToPoint()
        {
            return new Point(x, y);
        }

        /// <summary>
        /// Interface voor IComparable. Om te comparen hoe twee AStarObjects moeten worden gecompared.
        /// Namelijk op basis van de fCost.
        /// </summary>
        public int CompareTo(AstarObject AObjectToCompare)
        {
            int compare = fCost.CompareTo(AObjectToCompare.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(AObjectToCompare.hCost);
            }
            return -compare;
        }

        /// <summary>
        /// Vereiste van IHeapIndex, houd de huidige positie op de heap bij.
        /// </summary>
        public int HeapIndex
        {
            get; set;
        }
    }

    /// <summary>
    /// Enum die het soort weg bijhoudt. NULL is niet een weg.
    /// </summary>
    public enum RoadType
    {
        NULL,
        horizontalStraight,
        verticalStraight,
        bottomleftCorner,
        bottomrightCorner,
        toprightCorner,
        topleftCorner,
        PitstopSpecial
    }

    /// <summary>
    /// Enum die de soorten neighbourhoods bij houdt voor het A* algoritme.
    /// </summary>
    public enum NeighbourhoodType
    {
        Moore,
        Neumann
    }

    /// <summary>
    /// Road class die bijhoudt op welke positie de weg zich bevint en welk soort het is.
    /// </summary>
    public class Road
    {
        public int X;
        public int Y;
        public RoadType roadType;

        /// <summary>
        /// Road constructor
        /// </summary>
        public Road(int x, int y, RoadType T)
        {
            X = x;
            Y = y;
            roadType = T;
        }

        /// <summary>
        /// To string override voor het gemakkeljk console loggen van een weg.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "------\n(" + X + "," + Y + ")\n" + roadType;
        }
    }
    /// <summary>
    /// Enum om de richting bij te houden in gemakkelijk leesbare taal.
    /// </summary>
    public enum Direction
    {
        NULL,
        Top,
        Right,
        Bottom,
        Left
    }


}
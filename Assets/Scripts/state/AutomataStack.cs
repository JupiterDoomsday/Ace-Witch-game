using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace state
{

    public class AutomataStack : ScriptableObject
    {
        Stack<GameState> StateStack;
        public AutomataStack()
        {
            StateStack = new Stack<GameState>();
        }

        //this sorts the GameStates based on their priority,
        //I am utilizing Quicksort Algorithm to sort the states

        public void Sort() {
            GameState[] temp = StateStack.ToArray();
            StateStack.Clear();
            quickSort(temp, 0, (temp.Length - 1));
            StateStack = new Stack<GameState>(temp);
        }

        private void quickSort(GameState[] ar, int low, int high)
        {
            if(low < high)
            {
                int pi = Partition(ar,low, high);
                quickSort(ar, low, pi - 1);
                quickSort(ar, pi + 1, high);
            }
        }

        private int Partition(GameState[] ar, int low, int high)
        {
            int pivot = ar[high].Priority;
            int i = (low - 1);

            for(int j = low; j<= high-1; j++)
            {
                if( ar[j].Priority < pivot)
                {
                    i++;
                    GameState temp = ar[i];
                    ar[i] = ar[j];
                    ar[j] = temp;
                }

            }

            GameState t = ar[i+1];
            ar[i+1] = ar[high];
            ar[high] = t;
            return (i + 1);
        }

        public void EnqueState(GameState state)
        {
            StateStack.Push(state);
        }

        public GAMESTATE_TYPE PeekPrevType()
        {
            return StateStack.Peek().ver;
        }

        public GameState GetCurState()
        {
            return StateStack.Peek();
        }

        public GameState PopOff()
        {
            return StateStack.Pop();
        }
        public int Length()
        {
            return StateStack.Count;
        }
    }
}

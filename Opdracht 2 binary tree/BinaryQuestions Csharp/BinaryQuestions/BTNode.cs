﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryQuestions
{
    [Serializable]
    class BTNode
    {
        string message;
        BTNode noNode;
        BTNode yesNode;
        int score;

        /**
         * Constructor for the nodes: This class holds an String representing 
         * an object if the noNode and yesNode are null and a question if the
         * yesNode and noNode point to a BTNode.
         */
        public BTNode(string nodeMessage)
        {
            message = nodeMessage;
            noNode = null;
            yesNode = null;
        }



        public void query(int q)
        {
            if (q > 20)
            {
                Console.WriteLine("That was the last question. You win!");
            }
            else if (this.isQuestion())
            {
                Console.WriteLine(q + ") " + this.message);
                Console.Write("Enter 'y' for yes and 'n' for no: ");
                char input = getYesOrNo(); //y or n
                if (input == 'y')
                    yesNode.query(q + 1);
                else
                    noNode.query(q + 1);
            }
            else
                this.onQueryObject(q);
        }

        public void onQueryObject(int q)
        {
            Console.WriteLine(q + ") Are you thinking of a(n) " + this.message + "? ");
            Console.Write("Enter 'y' for yes and 'n' for no: ");
            char input = getYesOrNo(); //y or n
            if (input == 'y')
                Console.Write("I Win!\n");
            else
                updateTree();
        }

        private void updateTree()
        {
            Console.Write("You win! What were you thinking of? ");
            string userObject = Console.ReadLine();
            Console.Write("Please enter a question to distinguish a(n) "
                + this.message + " from " + userObject + ": ");
            string userQuestion = Console.ReadLine();
            Console.Write("If you were thinking of a(n) " + userObject
                + ", what would the answer to that question be (\'yes\' or \'no\')? ");
            char input = getYesOrNo(); //y or n
            if (input == 'y')
            {
                this.noNode = new BTNode(this.message);
                this.yesNode = new BTNode(userObject);
            }
            else
            {
                this.yesNode = new BTNode(this.message);
                this.noNode = new BTNode(userObject);
            }
            Console.Write("Thank you! My knowledge has been increased");
            this.setMessage(userQuestion);
        }

        public bool isQuestion()
        {
            if (noNode == null && yesNode == null)
                return false;
            else
                return true;
        }

        /**
         * Asks a user for yes or no and keeps prompting them until the key
         * Y,y,N,or n is entered
         */
        private char getYesOrNo()
        {
            char inputCharacter = ' ';
            while (inputCharacter != 'y' && inputCharacter != 'n')
            {
                inputCharacter = Console.ReadLine().ElementAt(0);
                inputCharacter = Char.ToLower(inputCharacter);
            }
            return inputCharacter;
        }

        //Mutator Methods
        public void setMessage(string nodeMessage)
        {
            message = nodeMessage;
        }

        public string getMessage()
        {
            return message;
        }

        public void setNoNode(BTNode node)
        {
            noNode = node;
        }

        public BTNode getNoNode()
        {
            return noNode;
        }

        public void setYesNode(BTNode node)
        {
            yesNode = node;
        }

        public BTNode getYesNode()
        {
            return yesNode;
        }

        public static void NodeValue(BTNode node)
        {
            if (node.yesNode == null && node.noNode == null)
            {
                node.score = node.message.Length;
                Console.WriteLine($"{node.message} score: {node.score}");
            }
            else
            {
                // not null
                if (node.yesNode != null)
                    NodeValue(node.yesNode);

                // not null?
                if (node.noNode != null)
                    NodeValue(node.noNode);
            }

        }

        public static int MinMax(BTNode node, bool aiAanZet = true)
        {
            if (node.yesNode == null && node.noNode == null)
            {
                node.score = node.message.Length;
                //Console.WriteLine($"{node.message} score: {node.score}");
                return node.score;
            }
            else
            {
                int resultMinMax;

                if (node.yesNode == null)
                {
                    resultMinMax = MinMax(node.noNode, !aiAanZet);
                    Console.WriteLine($"{node.message} resultaat MinMax: {resultMinMax}");
                    return resultMinMax;
                }
                else if (node.noNode == null)
                {
                    resultMinMax = MinMax(node.yesNode, !aiAanZet);
                    Console.WriteLine($"{node.message} resultaat MinMax: {resultMinMax}");
                    return resultMinMax;
                }
                else if (MinMax(node.yesNode, !aiAanZet) >= MinMax(node.noNode, !aiAanZet))
                {
                    if (aiAanZet)
                    {
                        resultMinMax = MinMax(node.yesNode, !aiAanZet);
                        Console.WriteLine($"{node.message} resultaat MinMax: {resultMinMax}");
                        return resultMinMax;
                    }
                    else
                    {
                        resultMinMax = MinMax(node.noNode, !aiAanZet);
                        Console.WriteLine($"{node.message} resultaat MinMax: {resultMinMax}");
                        return resultMinMax;
                    }
                }
                else
                    if (!aiAanZet)
                {
                    resultMinMax = MinMax(node.yesNode, !aiAanZet);
                    Console.WriteLine($"{node.message} resultaat MinMax: {resultMinMax}");
                    return resultMinMax;

                }
                else
                {
                    resultMinMax = MinMax(node.noNode, !aiAanZet);
                    Console.WriteLine($"{node.message} resultaat MinMax: {resultMinMax}");
                    return resultMinMax;
                }
            }
        }

        public static void AlphaBeta(BTNode node, bool aiAanzet)
        {

            if (node.yesNode == null && node.noNode == null)
            {
                node.score = node.message.Length;
            }
            if (aiAanzet)
            {
                Math.Max(node.yesNode.score, node.noNode.score);
                
                
            }
            if (!aiAanzet)
            {
                Math.Min(node.yesNode.score, node.noNode.score);
            }

        }



    }
}

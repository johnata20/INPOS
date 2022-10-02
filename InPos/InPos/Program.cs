using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPos
{
    class Program
    {
        static void Main(string[] args)
        { 
            for (int x = 1; x <= 5; x++)
            {
                Console.WriteLine("Enter a infix String (+,-,*,/,^, A to Z only):");
                String str = Console.ReadLine(); //Put user input inside a variable
                char[] netflix = str.ToCharArray(); //Split user input into an array
                if (!isValid(netflix)) //Calls the isValid function to test the contents of the array
                {
                    Console.WriteLine("You have entered Invalid Character");
                }
                else
                {
                    postFix(netflix); //Converts the user input into postfix
                }
                Console.ReadKey();
            }
        }
        static Boolean isValid(char[] infix)
        {
            for (int i = 0; i < infix.Length; i++)
            {
                char ch = infix[i]; //Puts character in the array in a temporary variable to be tested
                if (!isChar(ch) && ch != '+' && ch != '-' && ch != '*' && ch != '/' && ch != '^' && ch != '(' && ch != ')') //Test the character against valid input characters
                {
                    Console.WriteLine(ch + " is not allowed"); //Displays invalid character
                    return false; //Returns false
                }
            }
            return true; //Returns true
        }
        static void print(char[] postfix, int length)
        {
            for (int i = 0; i <= length; i++)
            {
                Console.Write(postfix[i]); //Prints the needed character3
            }
        }
        static void postFix(char[] netflix)
        {
            Stack stack = new Stack(netflix.Length); //Creates new stack 
            char[] postfix = new char[netflix.Length]; //Creates new array
            int j = 0; //Declares an integer variable
            Console.WriteLine("Scan\t|\tStack\t|\tPostfix");
            for (int i = 0; i < netflix.Length; i++) //Main loop for displaying postfix output
            {
                if (isChar(netflix[i])) //Condition for letters
                {
                    postfix[j] = netflix[i]; //Adds the letter into the array
                    j++; //Edits length of the output string
                }
                else
                {
                    if (netflix[i] == ')') //Condition for closing parentheses
                    {
                        while (stack.peep() != '(')
                        {
                            postfix[j] = stack.pop();
                            j++;
                        }
                        stack.pop(); //Removes from stack
                    }
                    else if (netflix[i] == '(') //Condition for opening parentheses
                    {
                        stack.push(netflix[i]); //Adds onto stack
                    }
                    else if ((prcd(stack.peep())) < prcd(netflix[i])) //Condition for orders of operands
                    {
                        stack.push(netflix[i]); //Adds onto stack
                    }
                    else if ((prcd(stack.peep())) >= prcd(netflix[i])) //Condition for orders of operands
                    {
                        char c = stack.pop(); //Removes from stack
                        postfix[j] = c; //Adds onto end of array
                        j++;
                        stack.push(netflix[i]); //Adds onto stack
                    }
                }
                Console.Write(netflix[i] + "\t|\t"); //Displays input
                stack.print(); //Displays stack
                print(postfix, j - 1); //Displays output string
                Console.Write("\n"); //Next line
                Console.ReadKey(); //Awaits key input before performing next line
            }
            while (!stack.isEmpty()) //Empties the stack
            {
                postfix[j] = stack.pop();
                j++;
                Console.Write("\t|\t");
                stack.print();
                print(postfix, j - 1);
                Console.Write("\n");
                Console.ReadKey();
            }
            Console.WriteLine();
            Console.WriteLine("Postfix String:");
            for (int i = 0; i < postfix.Length; i++) //Loops the array to be displayed as a line
            {
                Console.Write(postfix[i]); //Displays final output string
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static int prcd(char ch)
        {
            //Order of operations
            if (ch == '(' || ch == ')')
                return 1;
            else if (ch == '+' || ch == '-')
                return 2;
            else if (ch == '*' || ch == '/')
                return 3;
            else if (ch == '^')
                return 4;
            return 0;
        }
        static Boolean isChar(char ch)
        {
            //Checks if the character is a letter
            if (ch >= 'a' && ch <= 'z')
            {
                return true;
            }
            else if (ch >= 'A' && ch <= 'Z')
            {
                return true;
            }
            return false;
        }
    }
    public class Stack
    {
        char[] array = null; //Declares an empty array
        int top = -1; //Holds the current place of the top of the stack
        public Stack(int size) //Resizes the array into the stack size
        {
            array = new char[size];
        }
        public Boolean isEmpty() //Tests if the stack is empty
        {
            if (top == -1)
            {
                return true;
            }
            return false;
        }
        public char peep() //Peeks at the top of the stack
        {
            if (!isEmpty())
            {
                return array[top];
            }
            return 'Z';
        }
        public void push(char ch) //Pushes character onto the stack
        {
            top++;
            array[top] = ch;
        }
        public char pop() //Removes character at the top of the stack
        {
            if (!isEmpty())
            {
                char ch = array[top];
                top--;
                return ch;
            }
            return 'Z';
        }
        public void print() //Displays the stack
        {
            for (int i = 0; i <= top; i++)
            {
                Console.Write(array[i]);
            }
            Console.Write("\t|\t");            
        }
    }
}
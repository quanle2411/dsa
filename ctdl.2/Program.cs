using System;
using System.Collections.Generic;

class BrowserHistory
{
    private Stack<string> backStack;
    private Stack<string> forwardStack;
    private string current;

    public BrowserHistory(string homepage)
    {
        backStack = new Stack<string>();
        forwardStack = new Stack<string>();
        current = homepage;
    }

    // Visit a new URL
    public void Visit(string url)
    {
        backStack.Push(current);
        current = url;
        forwardStack.Clear(); // Clear forward history after visiting a new page
    }

    // Go back by a certain number of steps
    public string Back(int steps)
    {
        while (steps > 0 && backStack.Count > 0)
        {
            forwardStack.Push(current);
            current = backStack.Pop();
            steps--;
        }
        return current;
    }

    // Go forward by a certain number of steps
    public string Forward(int steps)
    {
        while (steps > 0 && forwardStack.Count > 0)
        {
            backStack.Push(current);
            current = forwardStack.Pop();
            steps--;
        }
        return current;
    }
}

class Program
{
    static void Main(string[] args)
    {
        BrowserHistory browserHistory = new BrowserHistory("leetcode.com");

        browserHistory.Visit("google.com");
        browserHistory.Visit("facebook.com");
        browserHistory.Visit("youtube.com");

        Console.WriteLine(browserHistory.Back(1)); // facebook.com
        Console.WriteLine(browserHistory.Back(1)); // google.com
        Console.WriteLine(browserHistory.Forward(1)); // facebook.com

        browserHistory.Visit("linkedin.com");
        Console.WriteLine(browserHistory.Forward(2)); // linkedin.com (no forward history)
        Console.WriteLine(browserHistory.Back(2)); // google.com
        Console.WriteLine(browserHistory.Back(7)); // leetcode.com (max back)
    }
}

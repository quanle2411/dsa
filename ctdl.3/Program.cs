using System;
using System.Collections.Generic;

class AuthenticationManager
{
    private int timeToLive;
    private Dictionary<string, int> tokens;

    public AuthenticationManager(int timeToLive)
    {
        this.timeToLive = timeToLive;
        this.tokens = new Dictionary<string, int>();
    }

    // Generate a new token
    public void Generate(string tokenId, int currentTime)
    {
        tokens[tokenId] = currentTime + timeToLive;
    }

    // Renew a token if it hasn't expired
    public void Renew(string tokenId, int currentTime)
    {
        if (tokens.ContainsKey(tokenId) && tokens[tokenId] > currentTime)
        {
            tokens[tokenId] = currentTime + timeToLive;
        }
    }

    // Count unexpired tokens at a given time
    public int CountUnexpiredTokens(int currentTime)
    {
        int count = 0;
        List<string> expiredTokens = new List<string>();

        foreach (var token in tokens)
        {
            if (token.Value > currentTime)
            {
                count++;
            }
            else
            {
                expiredTokens.Add(token.Key); // Collect expired tokens for removal
            }
        }

        // Remove expired tokens
        foreach (var tokenId in expiredTokens)
        {
            tokens.Remove(tokenId);
        }

        return count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        AuthenticationManager authManager = new AuthenticationManager(5);

        authManager.Renew("aaa", 1);
        authManager.Generate("aaa", 2);
        Console.WriteLine(authManager.CountUnexpiredTokens(6)); // 1

        authManager.Generate("bbb", 7);
        authManager.Renew("aaa", 8
S

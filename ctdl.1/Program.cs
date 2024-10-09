using System;

class SongNode
{
    public string SongName;
    public SongNode Next;
    public SongNode Prev;

    public SongNode(string songName)
    {
        SongName = songName;
        Next = null;
        Prev = null;
    }
}

class MusicPlayer
{
    private SongNode head;
    private SongNode tail;
    private SongNode current;

    public MusicPlayer()
    {
        head = null;
        tail = null;
        current = null;
    }

    // Add a song to the end of the playlist
    public void AddSong(string songName)
    {
        SongNode newNode = new SongNode(songName);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
            current = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Prev = tail;
            tail = newNode;
        }
    }

    // Move to the next song in the playlist (loop to the first song if at the end)
    public void PlayNext()
    {
        if (current != null)
        {
            current = current.Next ?? head; // Loop back to the first song if at the end
        }
    }

    // Move to the previous song in the playlist (loop to the last song if at the beginning)
    public void PlayPrevious()
    {
        if (current != null)
        {
            current = current.Prev ?? tail; // Loop to the last song if at the beginning
        }
    }

    // Remove a song by its name
    public void RemoveSong(string songName)
    {
        SongNode temp = head;
        while (temp != null)
        {
            if (temp.SongName == songName)
            {
                if (temp == head)
                {
                    head = temp.Next;
                    if (head != null) head.Prev = null;
                }
                else if (temp == tail)
                {
                    tail = temp.Prev;
                    if (tail != null) tail.Next = null;
                }
                else
                {
                    temp.Prev.Next = temp.Next;
                    if (temp.Next != null) temp.Next.Prev = temp.Prev;
                }
                if (current == temp)
                {
                    current = current.Next ?? head; // Update current if it's the removed song
                }
                return; // Song found and removed
            }
            temp = temp.Next;
        }
    }

    // Display the current playlist
    public void DisplayPlaylist()
    {
        SongNode temp = head;
        while (temp != null)
        {
            Console.Write(temp.SongName + " ");
            temp = temp.Next;
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MusicPlayer player = new MusicPlayer();
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();
            if (input[0] == "ADD")
            {
                player.AddSong(input[1]);
            }
            else if (input[0] == "NEXT")
            {
                player.PlayNext();
            }
            else if (input[0] == "PREV")
            {
                player.PlayPrevious();
            }
            else if (input[0] == "REMOVE")
            {
                player.RemoveSong(input[1]);
            }
            else if (input[0] == "DISPLAY")
            {
                player.DisplayPlaylist();
            }
        }
    }
}

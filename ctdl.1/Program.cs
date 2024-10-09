#include <iostream>
#include <unordered_map>
#include <string>
using namespace std;

class Node {
public:
    string song;
    Node* next;
    Node* prev;
    Node(string s) : song(s), next(nullptr), prev(nullptr) {}
};

class MusicPlayer {
private:
    Node* head;
    Node* tail;
    Node* current;
    unordered_map<string, Node*> songMap; // for quick lookup of song nodes

public:
    MusicPlayer() : head(nullptr), tail(nullptr), current(nullptr) {}

    // Add a song to the playlist
    void addSong(string song) {
        Node* newNode = new Node(song);
        if (!head) {
            head = tail = current = newNode;
        } else {
            tail->next = newNode;
            newNode->prev = tail;
            tail = newNode;
        }
        songMap[song] = newNode;
    }

    // Play next song
    void playNext() {
        if (!current) return;
        current = current->next ? current->next : head;
    }

    // Play previous song
    void playPrevious() {
        if (!current) return;
        current = current->prev ? current->prev : tail;
    }

    // Remove a song from the playlist
    void removeSong(string song) {
        if (songMap.find(song) == songMap.end()) return;
        Node* toDelete = songMap[song];

        if (toDelete == head) head = head->next;
        if (toDelete == tail) tail = tail->prev;

        if (toDelete->prev) toDelete->prev->next = toDelete->next;
        if (toDelete->next) toDelete->next->prev = toDelete->prev;

        if (current == toDelete) current = current->next ? current->next : head;

        songMap.erase(song);
        delete toDelete;
    }

    // Display the current playlist
    void display() {
        Node* temp = head;
        while (temp) {
            cout << temp->song << " ";
            temp = temp->next;
        }
        cout << endl;
    }
};

int main() {
    MusicPlayer mp;
    int n;
    cin >> n;
    string command, song;
    for (int i = 0; i < n; ++i) {
        cin >> command;
        if (command == "ADD") {
            cin >> song;
            mp.addSong(song);
        } else if (command == "NEXT") {
            mp.playNext();
        } else if (command == "PREV") {
            mp.playPrevious();
        } else if (command == "REMOVE") {
            cin >> song;
            mp.removeSong(song);
        } else if (command == "DISPLAY") {
            mp.display();
        }
    }
    return 0;
}


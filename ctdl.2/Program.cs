#include <iostream>
#include <stack>
#include <string>
using namespace std;

class BrowserHistory {
private:
    stack<string> backStack, forwardStack;
    string currentPage;

public:
    BrowserHistory(string homepage) {
        currentPage = homepage;
    }

    void visit(string url) {
        backStack.push(currentPage);
        currentPage = url;
        while (!forwardStack.empty()) forwardStack.pop(); // Clear forward history
    }

    string back(int steps) {
        while (steps-- > 0 && !backStack.empty()) {
            forwardStack.push(currentPage);
            currentPage = backStack.top();
            backStack.pop();
        }
        return currentPage;
    }

    string forward(int steps) {
        while (steps-- > 0 && !forwardStack.empty()) {
            backStack.push(currentPage);
            currentPage = forwardStack.top();
            forwardStack.pop();
        }
        return currentPage;
    }
};

int main() {
    BrowserHistory bh("uit.edu.vn");
    bh.visit("google.com");
    bh.visit("facebook.com");
    bh.visit("youtube.com");
    cout << bh.back(1) << endl;
    cout << bh.back(1) << endl;
    cout << bh.forward(1) << endl;
    bh.visit("linkedin.com");
    cout << bh.forward(2) << endl;
    cout << bh.back(2) << endl;
    cout << bh.back(7) << endl;
    return 0;
}

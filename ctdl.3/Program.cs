#include <iostream>
#include <unordered_map>
using namespace std;

class AuthenticationManager {
private:
    unordered_map<string, int> tokenExpiryMap; // tokenId -> expiryTime
    int timeToLive;

public:
    AuthenticationManager(int ttl) : timeToLive(ttl) {}

    // Generate a new token
    void generate(string tokenId, int currentTime) {
        tokenExpiryMap[tokenId] = currentTime + timeToLive;
    }

    // Renew a token if it has not expired
    void renew(string tokenId, int currentTime) {
        if (tokenExpiryMap.find(tokenId) != tokenExpiryMap.end() && tokenExpiryMap[tokenId] > currentTime) {
            tokenExpiryMap[tokenId] = currentTime + timeToLive;
        }
    }

    // Count how many tokens are still unexpired
    int countUnexpiredTokens(int currentTime) {
        int count = 0;
        for (auto it = tokenExpiryMap.begin(); it != tokenExpiryMap.end(); ) {
            if (it->second <= currentTime) {
                it = tokenExpiryMap.erase(it);
            } else {
                count++;
                it++;
            }
        }
        return count;
    }
};

int main() {
    AuthenticationManager authManager(5);
    authManager.renew("aaa", 1);  // no token exists, so nothing happens
    authManager.generate("aaa", 2);  // generates token "aaa" at time 2
    cout << authManager.countUnexpiredTokens(6) << endl;  // should output 1
    authManager.generate("bbb", 7);  // generates token "bbb" at time 7
    authManager.renew("aaa", 8);  // "aaa" has expired, so nothing happens
    authManager.renew("bbb", 10);  // "bbb" is renewed to expire at time 15
    cout << authManager.countUnexpiredTokens(15) << endl;  // should output 0
    return 0;
}


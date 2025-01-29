#include <iostream>
#include <vector>
#include <queue>
const int inf = 1e9;

void bfs(int start, std::vector<std::vector<int>>& neigh, std::vector<int>& dist) {
    std::queue<int> q;
    q.push(start);
    dist[start] = 0;

    while (!q.empty()) {
        int u = q.front();
        q.pop();
        for (int v: neigh[u]) {
            if (dist[v] == inf) {
                dist[v] = dist[u] + 1;
                q.push(v);
            }
        }
    }
}
void dfs(int node, std::vector<std::vector<int>>& graph, std::vector<bool>& visited) {
    visited[node] = true;
    std::cout << node << " "; // Обработка вершины (вывод)

    for (int neighbor : graph[node]) {
        if (!visited[neighbor]) {
            dfs(neighbor, graph, visited);
        }
    }
}

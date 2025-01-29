#include <iostream>
#include <vector>

struct priority_queue {
    std::vector<int> queue;
    priority_queue() {}

    void siftup(int i) {
        while (i > 0 and queue[(i - 1) / 2] > queue[i]) {
            std::swap(queue[(i - 1) / 2], queue[i]);
            i = (i - 1) / 2;
        }
    }
    void siftdown(int i) {
        int num = i;
        int left_num = 2 * i + 1;
        int right_num = 2 * i + 2;
        if (left_num < queue.size() and queue[left_num] < queue[i]) {
            num = left_num;
        }
        if (right_num < queue.size() and queue[right_num] < queue[num]) {
            num = right_num;
        }
        if (num != i) {
            std::swap(queue[i], queue[num]);
            siftdown(num);
        }
    }

    void insert(int x) {
        if (queue.size()==0){
            queue.push_back(x);
        }else{
            queue.push_back(x);
            siftup((queue.size()-1));
        }
    }

    int extract_min() {
        int min_num=queue[0];
        queue[0]=queue.back();
        queue.pop_back();
        siftdown(0);
        return min_num;
    }

    void decrease_key(int x, int y) {
        for (int i = 0; i < queue.size(); i++) {
            if (queue[i] == x) {
                queue[i] = y;
                siftup(i);
                break;
            }
        }
    }
};

int main() {
    std::ios::sync_with_stdio(0);
    std::cin.tie(0);
    std::cout.tie(0);

    std::vector<priority_queue> all_queue;
    std::string operations;
    while (std::cin >> operations) {
        if (operations == "create") {
            priority_queue new_queue;
            all_queue.push_back(new_queue);
        }

        if (operations == "insert") {
            int k, x;
            std::cin >> k >> x;
            all_queue[k].insert(x);
        }

        if (operations == "extract-min") {
            int k;
            std::cin >> k;

            if(all_queue[k].queue.size()==0){
                std::cout<<'*'<<'\n';
            }else{
                std::cout<<all_queue[k].extract_min()<<'\n';
            }
        }

        if (operations == "merge") {
            int k, m;
            std::cin >> k >> m;

            priority_queue new_queue;
            all_queue.push_back(new_queue);

            for (int i = 0; i < all_queue[k].queue.size(); ++i){
                all_queue[all_queue.size()-1].queue.push_back(all_queue[k].queue[i]);
            }
            for (int i = 0; i < all_queue[m].queue.size(); ++i){
                all_queue[all_queue.size()-1].queue.push_back(all_queue[m].queue[i]);
            }
            for (int i = all_queue[all_queue.size()-1].queue.size()-1; i >=0; --i) {
                all_queue[all_queue.size()-1].siftdown(i);
            }
        }

        if (operations == "decrease-key") {
            int k, x, y;
            std::cin >> k >> x >> y;
            all_queue[k].decrease_key(x, y);
        }
    }
    return 0;
}

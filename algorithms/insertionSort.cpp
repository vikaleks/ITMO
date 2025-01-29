#include <iostream>
using namespace std;

void insertionSort(int arr[], int n) {
    for (int i = 1; i < n; i++) {
        int key = arr[i];
        int j = i - 1;
        while (j >= 0 and arr[j] > key) {
            arr[j + 1] = arr[j];
            j--;
        }
        arr[j + 1] = key;
    }
}
int main() {
    int n;
    cin>>n;
    int x[n];
    for (int i = 0; i < n; i++) {
        cin>>x[i];
    }
    insertionSort(x, n);

    for (int i = 0; i < n; i++) {
        cout << x[i] << " ";
    }
    return 0;
}

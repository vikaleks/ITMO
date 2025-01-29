#include <iostream>
using namespace std;

void quicksort(int arr[], int l, int r) {
    int i = l;
    int j = r;
    int x = (arr[l] + arr[r]) / 2;

    while(i<=j) {
        while (arr[i] < x) {
            i++;
        }
        while (arr[j] > x) {
            j--;
        }
        if (i <= j) {
            swap(arr[i], arr[j]);
            i++;
            j--;
        }
    }
    if (l < j) {
        quicksort(arr, l, j);
    }
    if (i < r) {
        quicksort(arr, i, r);
    }
}
    int main(){
        int n;
        cin>>n;
        int arr[n];
        for (int i = 0; i < n; i++) {
            cin >> arr[i];
        }
        quicksort(arr,0,n-1);
        for (int i = 0; i < n; i++) {
            cout << arr[i]<< " ";
        }
        return 0;
    }

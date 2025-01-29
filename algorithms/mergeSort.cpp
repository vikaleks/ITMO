#include <iostream>
using namespace std;

void merge(int arr[], int arr2[], int l, int mid, int r, long long& k) {
    int i = l;
    int j = mid + 1;
    int q = l;

    while (i <= mid && j <= r) {
        if (arr[i] <= arr[j]) {
            arr2[q] = arr[i];
            i++;
        }
        else {
            arr2[q] = arr[j];
            j++;
            k += mid - i + 1;
        }
        q++;
    }

    while (i <= mid) {
        arr2[q] = arr[i];
        i++;
        q++;
    }
    while (j <= r) {
        arr2[q] = arr[j];
        j++;
        q++;
    }

    for (int c = l; c <= r; c++) {
        arr[c] = arr2[c];
    }
}

void mergesort(int arr[], int arr2[], int l, int r, long long& k) {
    if (l < r) {
        int mid = (l + r) / 2;
        mergesort(arr, arr2, l, mid, k);
        mergesort(arr, arr2, mid + 1, r, k);
        merge(arr, arr2, l, mid, r, k);
    }
}

int main(){
    int n;
    cin>>n;
    int arr[n];
    for (int i = 0; i < n; i++) {
        cin >> arr[i];
    }
    int arr2[n];
    long long k=0;
    mergesort(arr,arr2,0,n-1,k);
    cout << k << "\n";
    return 0;
}

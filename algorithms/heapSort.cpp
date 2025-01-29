#include <iostream>

void heapify(int arr[], int n, int i)
{
    int num=i;
    int left_num=2*i+1;
    int right_num=2*i+2;
    if(left_num<n and arr[num]<arr[left_num]){
        num=left_num;
    }
    if (right_num<n and arr[num]<arr[right_num]){
        num=right_num;
    }
    if(num!=i){
        std::swap(arr[i],arr[num]);
        heapify(arr,n,num);
    }
}
void heapsort(int arr[], int n){
    for (int i = (n-1)/2; i>=0;  --i) {
        heapify(arr,n,i);
    }
    for (int i = n-1; i >= 0; --i) {
        std::swap(arr[0],arr[i]);
        heapify(arr,i,0);
    }
}

int main() {
    int n;
    std::cin >> n;
    int a[n];
    for (int i = 0; i < n; i++) {
        std::cin >> a[i];
    }
    heapsort(a,n);
    for (int i = 0; i < n; ++i) {
        std::cout<<a[i]<<' ';
    }
    return 0;
}

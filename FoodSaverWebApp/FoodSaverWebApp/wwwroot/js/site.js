
let store = document.getElementById("dashboardStoreView");
let item = document.getElementById("dashboardItemView");

function toggleStoreView() {
    store.style.display = 'block';
    item.style.display = 'none';
}

function toggleItemView() {
    store.style.display = 'none';
    item.style.display = 'block';
}
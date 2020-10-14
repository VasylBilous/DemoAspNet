function setFilter(event, type) {
    $("#games").load(`/Game/Filter?type=${encodeURIComponent(type)}&name=${encodeURIComponent(event.target.value)}`)
}

function runSearch(event) {
    if (event.keyCode == 13) {
        $("#games").load(`/Game/Search?searchInput=${encodeURIComponent(event.target.value)}`)
    }
}

function makeSort(event) {
    $("#games").load(`/Game/Sort?sort=${encodeURIComponent(event.target.value)}`);
}

function swapType() {
    if (document.getElementById("imageType").checked) {
        document.getElementById("imageLink").style.display = "none";
        document.getElementById("imagePC").style.display = "inline";
    }
    else {
        document.getElementById("imageLink").style.display = "inline";
        document.getElementById("imagePC").style.display = "none";
    }
}



function bid(id, amount) {
    $(document).ready(function () {
        showPanel('bid');
        document.getElementById("Auction_id").value = id;
        document.getElementById("Amount").value = amount;
    });
}

function showPanel(panel) {
    $(document).ready(function () {
        document.getElementById(panel).style.display = 'block';
    });
}

function closePanel(panel) {
    document.getElementById(panel).style.display = 'none';
}

window.onclick = function (event) {
    var modal = document.getElementsByClassName('modal');
    for (i = 0; i < modal.length; i++) {
        if (event.target == modal[i]) {
            modal[i].style.display = "none";
        }
    }
}
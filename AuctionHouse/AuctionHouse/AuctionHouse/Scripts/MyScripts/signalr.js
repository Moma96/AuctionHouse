$(document).ready(function () {
    
    var hub = $.connection.AuctionHouseHub;
    
    hub.client.updatebid = function (email, name, auction_id, amount, created) {
        if (document.getElementById(auction_id) != null) {
            if (document.getElementById("auction-bids") != null) {
                var bids = $("#auction-bids tbody").html();
                $("#auction-bids tbody").html("<tr><td>" + email + "</td><td>" + amount + "</td><td>" + created + "</td></tr>" + bids);
            } else {
                $("#" + auction_id + " .item-bidding").html(name + ": <span class='item-bidded-amount'>$" + amount + "</span>");
            }
        }
    };

    $.connection.hub.start();
});

function proba() {
    alert("ASDADS");
}
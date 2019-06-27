function load_rows() {
    var rows = localStorage.getItem("rows");

    var i;
    for (i = 0; i < rows; i++) {
        $("#items").append(
            "<div class='row' style='height:520px'>" +
            "</div>"
        );
    }
}

function clear_items() {
    var rows = localStorage.getItem("rows");

    var i;
    for (i = 0; i < rows; i++) {
        $("#items > .row:eq(" + i + ")").html("");
    }
}

function load_items() {
    var items = JSON.parse(localStorage.getItem("items"));

    var page_size = localStorage.getItem("page_size");
    var current_page = localStorage.getItem("current_page");
    var width = localStorage.getItem("width");
    var in_row = localStorage.getItem("in_row");
    var rows = localStorage.getItem("rows");

    var first = (current_page - 1) * page_size;
    var last = (page_size > items.length - first ? items.length : current_page * page_size);
    var i;
    for (i = first; i < last; i++) {
        var curr_row = Math.floor(((i - first) / in_row) % rows);

        var bidder;
        var amount;
        if (items[i].LastBid != null) {
            bidder = items[i].LastBid.firstName + " " + items[i].LastBid.lastName;
            amount = items[i].LastBid.amount;
        }
        else {
            bidder = "Initial price";
            amount = items[i].Auction.starting_price;
        }

        $("#items > .row:eq(" + curr_row + ")").append(
            "<div class='col-sm-" + width + "' style = 'text-align:center;'>" +
                "<div id='" + items[i].Auction.id + "' class='container item'>" +
                    "<div class='row'>" +
                        "<div class='col-sm-12 item-name'>" +
                            "<div class='item-text'>" +
                                items[i].Auction.name +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class='row'>" +
                        "<div class='col-sm-12 item-image'>" +
                            "<img src='http://" + window.location.host + "/images/" + items[i].Auction.id + ".png' style='height:100%;'>" +
                        "</div>" +
                    "</div>" +
                    "<div class='row'>" +
                        "<div class='col-sm-12 item-timer'>" +
                        int_to_time(items[i].Auction.duration) +
                        "</div>" +
                    "</div>" +
                    "<div class='row'>" +
                        "<div class='col-sm-12 item-bidding'>" +
                            bidder + ": <span class='item-bidded-amount'>$" + amount + "</span>" +
                        "</div>" +
                    "</div>" +
                    "<div class='row btn-row'>" +
                        "<div class='col-sm-12' style='text-align:center;'>" +
                            "<button type='button' class='btn item-bid-button'>" +
                            "</button>" +
                        "</div>" +
                    "</div>" +
                "</div>" +
            "</div>"
        );
        var pos = (i - first).toString();
        $("#items .item:eq(" + pos + ") > div[class!='row btn-row']").attr('onclick', 'auctionDetails("' + items[i].Auction.id.toString() + '")');
        
        refresh_state(i, pos);
    }

    $('.item-text').fitText();
}
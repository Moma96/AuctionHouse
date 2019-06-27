function update_time() {
    var items = JSON.parse(localStorage.getItem("items"));
    
    var i;
    for (i = 0; i < items.length; i++) {
        if (items[i].Auction.state == "OPENED") {
            var time = parseInt(items[i].Auction.duration);
            if (time-- > 0) {
                items[i].Auction.duration = time.toString();
            } else {
                items[i].Auction.state = "COMPLETED";
            }
        }
    }

    localStorage.setItem("items", JSON.stringify(items));
}

function refresh_state(item, position) {
    var items = JSON.parse(localStorage.getItem("items"));
    $(document).ready(function () {
        switch (items[item].Auction.state) {
            case "OPENED":
                var amount;
                if (items[item].LastBid != null) {
                    amount = items[item].LastBid.amount;
                }
                else {
                    amount = items[item].Auction.starting_price;
                }
                $("#items .item-bid-button:eq(" + position + ")").html("BID NOW!").addClass('btn-info').attr('onclick', 'bid("' + items[item].Auction.id + '",' + amount + ')');
                $("#items .item:eq(" + position + ")").addClass('opened');
                break;
            case "COMPLETED":
                $("#items .item-bid-button:eq(" + position + ")").html("EXPIRED!").addClass('btn-danger').prop("disabled", true);
                $("#items .item:eq(" + position + ")").addClass('completed');
                break;
            case "READY":
                $("#items .item-bid-button:eq(" + position + ")").html("APPROVE!").addClass('btn-success').attr('onclick', 'approve("' + items[item].Auction.id + '")');
                $("#items .item:eq(" + position + ")").addClass('ready');
                break;
        }
    });
}

function refresh_items() {
    var items = JSON.parse(localStorage.getItem("items"));
    var current_page = localStorage.getItem("current_page");
    var page_size = localStorage.getItem("page_size");

    var first = (current_page - 1) * page_size;
    var last = (page_size > items.length - first ? items.length : current_page * page_size);
    var i;
    for (i = first; i < last; i++) {
        var curr = (i - first).toString();
        $("#items .item-timer:eq(" + curr + ")").html(int_to_time(items[i].Auction.duration));
        refresh_state(i, curr);
    }
}

function int_to_time(seconds) {
    var day = 60 * 60 * 24;

    var d = Math.floor(seconds / day);
    var date = new Date((seconds % day) * 1000);
    var hh = date.getUTCHours();
    var mm = date.getUTCMinutes();
    var ss = date.getSeconds();

    if (hh < 10) { hh = "0" + hh; }
    if (mm < 10) { mm = "0" + mm; }
    if (ss < 10) { ss = "0" + ss; }
    var res = hh + ":" + mm + ":" + ss;
    if (d > 0) {
        return d + "d " + res;
    }
    return res;
}

function adjust_durations(items) {
    var i;
    for (i = 0; i < items.length; i++) {
        if (items[i].Auction.state === "OPENED") {
            var opened = new Date(parseInt(items[i].Auction.opened.substr(6))).getTime() / 1000;
            var now = Date.now() / 1000;
            items[i].Auction.duration = opened + items[i].Auction.duration - now;
        }
        else if (items[i].Auction.state === "COMPLETED") {
            items[i].Auction.duration = 0;
        }
    }
}

function reload_items() {
    clear_items();
    load_items(items);
    clear_pagination();
    load_pagination();
}

function update_items(items) {
    adjust_durations(items);
    localStorage.setItem("items", JSON.stringify(items));
    reload_items();
}
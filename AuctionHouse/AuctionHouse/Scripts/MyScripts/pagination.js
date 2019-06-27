function next_page() {
    var current_page = localStorage.getItem("current_page");
    if (current_page < localStorage.getItem("pages")) {
        localStorage.setItem("current_page", ++current_page);
        reload_items();
    }
}

function previous_page() {
    var current_page = localStorage.getItem("current_page");
    if (current_page > 1) {
        localStorage.setItem("current_page", --current_page);
        reload_items();
    }
}

function on_page(p) {
    if (localStorage.getItem("current_page") != p) {
        localStorage.setItem("current_page", p);
        reload_items();
    }
}

function clear_pagination() {
    $("#pagination").empty();
}

function load_pagination() {
    var pages = localStorage.getItem("pages");
    var current_page = localStorage.getItem("current_page");

    var i;
    $("#pagination").append("<li><button onclick='previous_page()'>«</button></li>");
    for (i = 1; i <= pages; i++) {
        if (i == current_page) {
            $("#pagination").append("<li><button onclick='on_page(" + i + ")' class='active'>" + i + "</button></li>");
        } else {
            $("#pagination").append("<li><button onclick='on_page(" + i + ")'>" + i + "</button></li>");
        }
    }
    $("#pagination").append("<li><button onclick='next_page()'>»</button></li>");

}
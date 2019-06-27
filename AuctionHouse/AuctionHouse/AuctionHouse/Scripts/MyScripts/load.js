var next;

var tick = function () {
    if (!next) {
        next = new Date().getTime();
    }
    next += 1000;

    update_time();
    refresh_items();

    setTimeout(tick, next - new Date().getTime());
};


function load(items, page_size) {
	localStorage.clear();

    current_page = 1;
    localStorage.setItem("current_page", current_page);

    //page_size = 8;
    localStorage.setItem("page_size", page_size);

    in_row = 4;
    localStorage.setItem("in_row", in_row);

    rows = Math.ceil(page_size / in_row);
    localStorage.setItem("rows", rows);

    pages = Math.ceil(items.length / page_size);
    localStorage.setItem("pages", pages);

    width = Math.floor(12 / in_row);
    localStorage.setItem("width", width);

	/*var current_page;
	if (localStorage.getItem("current_page")) {
		current_page = localStorage.getItem("current_page");
	} else {
		current_page = 1;
		localStorage.setItem("current_page", current_page);
	}
	
	var page_size;
	if (localStorage.getItem("page_size")) {
		page_size = localStorage.getItem("page_size");
	} else {
		page_size = 8;
		localStorage.setItem("page_size", page_size);
	}
	
	var in_row;
	if (localStorage.getItem("in_row")) {
		in_row = localStorage.getItem("in_row");
	} else {
		in_row = 4;
		localStorage.setItem("in_row", in_row);
	}
	
	var rows;
	if (localStorage.getItem("rows")) {
		rows = localStorage.getItem("rows");
	} else {
		rows = Math.ceil(page_size / in_row);
		localStorage.setItem("rows", rows);
	}
	
	var pages;
	if (localStorage.getItem("pages")) {
		pages = localStorage.getItem("pages");
	} else {
		pages = Math.ceil(items.length / page_size);
		localStorage.setItem("pages", pages);
	}
	
	var width;
	if (localStorage.getItem("width")) {
		width = localStorage.getItem("width");
	} else {
		width = Math.floor(12/in_row);
		localStorage.setItem("width", width);
	}*/
	
    $(document).ready(function () {

		load_rows();
		
        update_items(items);

        tick();
	});
	
}
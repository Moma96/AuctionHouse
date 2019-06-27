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
    
    localStorage.setItem("page_size", page_size);

    in_row = 4;
    localStorage.setItem("in_row", in_row);

    rows = Math.ceil(page_size / in_row);
    localStorage.setItem("rows", rows);

    pages = Math.ceil(items.length / page_size);
    localStorage.setItem("pages", pages);

    width = Math.floor(12 / in_row);
    localStorage.setItem("width", width);
	
    $(document).ready(function () {

		load_rows();
		
        update_items(items);

        tick();
	});
	
}
function approve(id) {
    $.ajax
        ({
            url: "http://" + window.location.host + "/Auction/Approve",
            method: "POST",
            data: { id: id },
            dataType: "text",
            success: function (response) {
                onSuccessApprove(id);
            },
            error: function (response) {
                onFailure(response);
            }
        });
}

function onSuccessPasswordChange() {
    alert("You have successfully changed password!")
    closePanel('change-password');
}

function onSuccessApprove(id) {
    alert("Auction with GUID " + id + " has been successfully approved!");
}

function onSuccessRegister() {
    closePanel('register');
    showPanel('login');
}

function onSuccessCreated() {
    alert('Auction successfully created!');
}

function onSuccessOrder() {
    alert('Tokens successfully ordered!');
}

function onSuccessParameterChange() {
    alert('System parameters changed successfully!');
    closePanel("edit-parameters");
}

function onSuccessBid(response) {
    closePanel("bid");
}

function onSuccessSearch(response) {
    update_items(response);
}

function onSuccessLogin() {
    if (document.getElementById("account-details") != null) {
        location.reload();
    }
}

function onSuccessLogout() {
    if (document.getElementById("account-details") != null) {
        location.href = "http://" + window.location.host + "/Auction";
    }
}

function onFailure(response) {
    alert(response.statusText);
}

function onFailureBid(response) {
    if (response.statusText === "Forbidden access!") {
        alert("You have to log in!");
        closePanel('bid');
        showPanel('login');
    }
    else {
        onFailure(response);
    }
}

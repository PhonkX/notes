$(document).ready(function () {
    initAutoSave();
   // initManualSave();
});

function sendData(area, id, timeout) {

    var text = area.value;
    var userId = document.getElementById("userid").value;
    $.post("/Home/SaveNote", { id: id, userId: userId, notearea: text });
    //setTimeout(function() {
    //    var text = area.value;
    //    $.post("/Home/SaveNote", { id: id, notearea: text }, null, "object");
    //    //.done()
    //    //.fail(); // TODO: переделать url, вытаскивая action
    //    //$.ajax(
    //    //    {
    //    //        url: "/Home/SaveNote",
    //    //        type: "POST",
    //    //        data: { id: id, text: text },
    //    //        success: null
    //    //    }
    //    //);
    //}, timeout);
}

//function initManualSave(parameters) {
//    $("#savebtn")
//        .click(function () {
//            var id = document.getElementById("noteid").value; // подумать, можно ли вынести
//            var area = document.getElementById("notetextarea");
//            sendData(area, id, 0);
//        });
//}

function initAutoSave() {
    var id = document.getElementById("noteid").value;
    if (!id) {
        return;
    }

    var area = document.getElementById("notetextarea");
    if (area.addEventListener) {
        area.addEventListener('input',
            function () {
                // event handling code for sane browsers
                sendData(area, id, 2000);
            },
            false);
    } else if (area.attachEvent) {
        area.attachEvent('onpropertychange',
            function () {
                // IE-specific event handling code
                sendData(area, id, 2000);
            });
    }
}

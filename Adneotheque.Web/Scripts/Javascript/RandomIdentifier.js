//$(document).ready(function () {



function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

function Guid() {
    $("#DocumentId").effect("highlight", { color: '#008CBA' }, 3000);
    //$("#DocumentId").val = S4();
    var input = document.getElementById("DocumentId");
    input.value = (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
}

    //    $("#GuidButton").click(function () {


    //        //$("#DocumentId").val = (S4() + S4() + "-" + S4() + "-4" + S4().substr(0, 3) + "-" + S4() + "-" + S4() + S4() + S4()).toLowerCase();
            

    //        $("#DocumentId").effect("highlight", { color: 'red' }, 3000);
    //        $("#DocumentId").val = S4();
    //    })
    //})

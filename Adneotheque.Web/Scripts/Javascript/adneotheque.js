$(function () {

    var submitAutocompleteForm = function (event, ui) {
        var $input = $(this);//The this reference will be set up to point to the DOM element that we're interacting with
        $input.val(ui.item.label);//set the value of the input - because even though jQuery autocomplete will automatically populate the input with the item that the user has selected, sometimes that doesn't happen before this select event is raised. So you can get into submit autocomplete form and still have an old input value there,not the new that the user selected
        //var $form = $input.parents("from:first");//dear input, go and look through the parents above you, all the DOM elements above you, and find the first form.
        //$form.submit();//once I find the form, there's a jQuery API where I can tell the form to submit itself
        $input.effect("slide");
        //var $form = $('#documentList');
        //$form.delay(0).fadeOut('slow').delay(5).fadeIn('slow');
        
        $input.submit();
        $input.val(ui.item.label);
    }

    var submitAutocompleteDiv = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        $input.effect("slide");
        var $div = $('#fetchedDocument');
        $input.submit();
    }

    var createAutocomplete = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-adneotheque-autocomplete"),
            select: submitAutocompleteForm
            //close: function (event, ui) {
            //    $(this).val("");
            //    return false;
            //}
        };

        $input.autocomplete(options);
    };

    var createAutocompleteForDocumentId = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-adneotheque-autocomplete-documentId"),
            select: submitAutocompleteDiv
        };

        $input.autocomplete(options);
    };

    var getPage = function () {
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            data: $('#form0').serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var target = $('#documentList');
            $(target).replaceWith(data);
        });

        return false;

    };



    $("input[data-adneotheque-autocomplete]").each(createAutocomplete);
    $(".body-content").on("click", ".pagedList a", getPage);
    $("input[data-adneotheque-autocomplete-documentId]").each(createAutocompleteForDocumentId);
});


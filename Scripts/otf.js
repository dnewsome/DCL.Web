

$(function () {
    //                                                                                                              
    //  jQuery gets the form and will do an set up the .Ajax submition to be used at the bollow of this script      
    //  --  Get form ref and wrap in a jQuery wrapped to access the DOM                                             
    //  --  This form (ajaxFormSubmit) is submitted at the bottom of this file and                                  
    //      is used for AJAX, AutoComplete, and Paging logic at the bottom of this script                           
    //                                                                                                              
    var ajaxFormSubmit = function () {
        var $form = $(this);            // grab ther form being submitted

        var options = {
            url: $form.attr("action"),  // Finds the controller action  
            type: $form.attr("method"), // finds the Get or Post        
            data: $form.serialize()
        };
        // asynchnous call
        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-otf-target")); // Saves the Partial View target
            var $newHtml = $(data);                         // Gets the data jQuery Object  
            $target.replaceWith($newHtml);                  // Replaces the Partial View    
            $newHtml.effect("highlight");                   // Flickers the screen          
        });

        return false;                                       // This keeps the server from continuing in it's own and repainting the page   
    };

    // AutoComplete - jQuery Widget - Submit     
    var submitAutocompleteForm = function (event, ui) {

        var $input = $(this);           
        $input.val(ui.item.label);

        var $form = $input.parents("form:first");   // Find the 1st form above you  
        $form.submit();
    };
    // AutoComplete Function                    
    var createAutocomplete = function () {
        var $input = $(this);   // For each input  

        var options = {
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutocompleteForm      // User selected from the list, set the search list  
        };

        $input.autocomplete(options);
    };


    // Paging                           
    var getPage = function () {
        var $a = $(this);

        var options = {
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-otf-target");
            $(target).replaceWith(data);
        });
        return false;

    };

    //                                                                                      
    //  Used for AJAX, AutoComplete, and Paging -- Based on finding the attribute or class  
    //                                                                                      

    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);     // Wire the submit event                
    $("input[data-otf-autocomplete]").each(createAutocomplete); // Calls createAutocomplete for EACH    
    $(".main-content").on("click", ".pagedList a", getPage);


});
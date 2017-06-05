$(document).ready(function () {
    
    if ($('.employees-content').length < 1)
        return;

    if ($('#datetimepicker-form').length > 0) {
        $('#datetimepicker-form').datepicker();
    }

    InitCreateEmployeeModal();

    $rows = $('.table-row', '.table.table-content.emptable');

    if ($rows.length < 1)
        return;

    $('#confirm-delete').on('show.bs.modal', this, function (e) {
        var $this = $(this);

        var $sender = $(e.relatedTarget);

        var $okbutton = $(this).find('.btn-ok');

        $okbutton.on('click', this, function (ee) {
            var $delInp = $('.x-remove', $sender.closest('.button-wrapper'));
            $delInp.trigger('click');
            $this.modal('hide');

            $okbutton.unbind('click');
        });
    });
    
    InitEmployeeFancyBox($rows);

    InitDeleteEmployees($rows);
});

function InitCreateEmployeeModal() {
    $('#create-emp-modal').on('show.bs.modal', this, function (e) {
        var $this = $(this);

        var $okempbutton = $(this).find('.btn-ok-create-emp');

        $okempbutton.unbind('click');

        $okempbutton.on('click', this, function (ee) {

            var success = CreateEmployee($this);

            if (success) {
                $this.modal('hide');
                
                location.reload();
            }
            
        });
    });
}

function CreateEmployee($this) {

    var result = false;
    var validated = true, eId = 0, firstname = '', lastname = '', email = '', depId = 0;

    var $firstname = $('input#firstname');
    var $lastname = $('input#lastname');
    var $email = $('input#email');
    var $datetime = $('#datetimepicker-form');
    var $selectedLi = $('ul.dropdown-menu li.selected');

    var $genericerror = $('#generic-error.error-message');
    var $successMessage = $('#success-message');

    //reset hidden classes to errors
    $('#firstname-error').addClass('hidden');
    $('#lastname-error').addClass('hidden');
    $('#email-error').addClass('hidden');
    $('#datetime-error').addClass('hidden');
    $('#empdepartment-error').addClass('hidden');
    $genericerror.addClass('hidden');
    $successMessage.addClass('hidden');


    if ($firstname.val() === '') { $('#firstname-error').removeClass('hidden'); } else { firstname = $firstname.val(); }
    if ($lastname.val() === '') { $('#lastname-error').removeClass('hidden'); } else { lastname = $lastname.val(); }
    if ($email.val() === '') { $('#email-error').removeClass('hidden'); } else { email = $email.val(); }

    var date = $datetime.data('date');
    if (date === '' || date === null) { $('#datetime-error').removeClass('hidden'); }

    if ($selectedLi.length < 1) {
        $('#empdepartment-error').removeClass('hidden');
    } else {
        depId = $selectedLi.data('did');
    }


    if (eId === '' || firstname === '' || lastname === '' || email === '' || date === '' || date === null || depId === undefined || depId === 0) {
        validated = false;
    }

    if (validated) {

        $.ajax({
            type: "POST",
            dataType: 'json',
            async: false,
            contentType: 'application/json; charset=UTF-8',
            ContentType: 'application/json; charset=UTF-8',
            url: "/Company/CreateEmployee",
            data: JSON.stringify({ firstName: firstname, lastName: lastname, email: email, birthdate: date, depId: depId }),
            success: function (res) {
                if (res.Response === 0) { //success
                    $genericerror.hide();

                    result = true;

                } else if (res.Response === 1) { // validation error

                    ('span', $genericerror).text(res.Message);
                    $genericerror.removeClass('hidden');

                } else if (res.Response === 2) { // generic error

                    ('span', $genericerror).text(res.Message);
                    $genericerror.removeClass('hidden');
                }
            },
            failure: function (res) {

                ('span', $genericerror).text(res.Message);
                $genericerror.removeClass('hidden');
            },
            error: function (res) {

                ('span', $genericerror).text(res.Message);
                $genericerror.removeClass('hidden');
            }
        });
        
    }

    return result;
}


function InitEmployeeFancyBox($rows) {
    
    $rows.on('dblclick', this, function (e) {
        console.log(this);

        $this = $(this);

        var href = $this.data('url');

        $.fancybox.open({
            href: href,
            type: 'iframe',
            maxWidth: 840,
            fitToView: false,
            autoSize: false,
            closeClick: true,
            openEffect: 'none',
            closeEffect: 'none',
            //width: 600,
            //height: 600,
            padding: 0,
            beforeShow: function () {
                var fancy = $('.fancybox-iframe');
                fancy.contents().find('.fancybox-my-close').on('click', function () { $.fancybox.close(); });

                this.width = ($('.fancybox-iframe').contents().find('.framing-what').width());
                this.height = ($('.fancybox-iframe').contents().find('html').height());
            },
            afterShow: function () {
                var fancyInner = $('.fancybox-inner');
                fancyInner.addClass('fan-emp-inner')
                fancyInner.css({'overflow': 'inherit'});
            },
            afterClose: function () {
                location.reload();
            }
        });
    });

};

function InitDeleteEmployees() {

    $deleteButtons = $('input.x-remove','.table.table-content.emptable');

    $deleteButtons.on('click', this, function (e) {

        var $allError = $('.error-message');
        $allError.hide();
        
        $this = $(this);

        var eId;

        var $tr = $this.closest('tr');

        if ($tr.length > 0) {
            eId = $tr.data('id')
        }
        
        $.ajax({
            type: "POST",
            dataType: 'json',
            async: true,
            contentType: 'application/json; charset=UTF-8',
            ContentType: 'application/json; charset=UTF-8',
            url: "/Company/DeleteEmployee",
            data: JSON.stringify ({ id: eId }),
            //cache: false,
            success: function (res) {

                var $error = $('.error-message', $tr);

                if (res.Response === 0) {
                    $tr.remove();
                    $error.hide();
                } else if (res.Response === 2) {
                    
                    $error.text(res.Message);
                    $error.show();
                }
            },
            failure: function (res) {
            },
            error: function (res) {
            }
        });

    });
};
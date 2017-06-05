$(document).ready(function () {

    $('#datetimepicker').datepicker();

    $(".dropdown-menu li.disabled").click(function () {
        return false;
    });

    //dropdown set selected 
    $('li.department-item').on('click', this, function (e) {
        $this = $(this);

        if ($this.hasClass('disabled')) {
            return false;
        }

        $('button#empdepartment').html($this.text() + ' <span class="caret"></span>')

        //remove selected and add new one
        $('ul.dropdown-menu li').each(function (i) {
            $(this).removeClass('selected');
        });

        $this.addClass('selected');
    });
    
    //dropdown set selected 
    $('.submit-emp-edit').on('click', this, function (e) {

        var validated = true, eId = 0, firstname = '', lastname = '', email = '', depId = 0;
        
        var $form = $('form.employee-edit-form');
        var $firstname = $('input#firstname');
        var $lastname = $('input#lastname');
        var $email = $('input#email');
        var $datetime = $('#datetimepicker');
        var $selectedLi = $('ul.dropdown-menu li.selected');

        var $genericerror = $('#generic-error.error-message');
        var $successMessage = $('#success-message');

        eId = $form.data('eid');

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
        if (date === '') { $('#datetime-error').removeClass('hidden'); }

        if ($selectedLi.length < 1) {
            $('#empdepartment-error').removeClass('hidden');
        } else {
            depId = $selectedLi.data('did');
        }

        if (eId === '' || firstname === '' || lastname === '' || email === '' || date === '' ||  depId === undefined || depId === 0) {
            validated = false;
        }

        if (validated) {

            $.ajax({
                type: "POST",
                dataType: 'json',
                async: true,
                contentType: 'application/json; charset=UTF-8',
                ContentType: 'application/json; charset=UTF-8',
                url: "/Company/EmployeeEdit",
                data: JSON.stringify({ id: eId, firstName: firstname, lastName: lastname, email: email, birthdate: date, depId: depId }),
                success: function (res) {
                    if (res.Response === 0) { //success
                        $genericerror.hide();

                        $('.editpage-content > h3 > span.first-name').text(firstname);
                        $('.editpage-content > h3 > span.last-name').text(lastname);

                        $('span', $successMessage).text(res.Message);
                        $successMessage.removeClass('hidden');

                    } else if (res.Response === 2) {

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

    });
    
});

$(document).ready(function () {
    
    //dropdown set selected 
    $('li.number').on('click', this, function (e) {
        $this = $(this);

        $('button#menu1').html($this.text() + ' <span class="caret"></span>')

        //remove selected and add new one

        $('ul.dropdown-menu li').each(function (i) {
            $(this).removeClass('selected');
        });

        $this.addClass('selected');
    });
    
    //dropdown set selected 
    $('.submit-dep-edit').on('click', this, function (e) {

        var validated = true, maxNumb = 0, dId = 0, dname = '';
        var $selectedLi = $('ul.dropdown-menu li.selected');
        var $form = $('form.department-edit-form');

        var $genericerror = $('#generic-error.error-message');
        var $successMessage = $('#success-message');
        

        dId = $form.data('did');

        //reset hidden classes to errors
        $('#name-error').addClass('hidden');
        $('#maxnumber-error').addClass('hidden');
        $genericerror.addClass('hidden');
        $successMessage.addClass('hidden');

        if ($('#name').val() === '') {
            $('#name-error').removeClass('hidden');
        }
        else {
            dname = $('#name').val();
        }

        if ($selectedLi.length < 1) {
            $('#maxnumber-error').removeClass('hidden');
        } else {
            maxNumb = $('> a', $selectedLi).text();
        }

        if (dId === 0 || maxNumb < 1 || dname === '') {
            validated = false;
        }

        if (validated) {

            $.ajax({
                type: "POST",
                dataType: 'json',
                async: true,
                contentType: 'application/json; charset=UTF-8',
                ContentType: 'application/json; charset=UTF-8',
                url: "/Company/DepartmentEdit",
                data: JSON.stringify({ id: dId, departmentName: dname, maxNumberOfEmployees: maxNumb }),
                success: function (res) {
                    if (res.Response === 0) { //success
                        $genericerror.hide();

                        $('.editpage-content > h3').text(dname);

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

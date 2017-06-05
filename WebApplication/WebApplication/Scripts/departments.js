$(document).ready(function () {

    if ($('.departments-content').length < 1)
        return;

    InitCreateDepartmentModal();

    $rows = $('.table-row', '.table.table-content.dpttable');
    
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
    
    InitFancyBox($rows);

    InitDeleteDepartments($rows);
});

function InitCreateDepartmentModal() {
    $('#create-dep-modal').on('show.bs.modal', this, function (e) {
        var $this = $(this);

        var $okbutton = $(this).find('.btn-ok-create-dep');

        $okbutton.on('click', this, function (ee) {

            var success = CreateDepartment($this);

            if (success) {
                $this.modal('hide');
                location.reload();
            }
        });
    });
}

function CreateDepartment($this) {

    var result = false;
    var validated = true, maxNumb = 0, dId = 0, dname = '';
    var $departName = $('#name', $this);
    var $selectedLi = $('ul.dropdown-menu li.selected', $this);
    var $genericerror = $('#generic-error.error-message');

    //todo: add success message 
    //var $successMessage = $('#success-message');

    //reset hidden classes to errors
    $('#name-error').addClass('hidden');
    $('#maxnumber-error').addClass('hidden');
    $genericerror.addClass('hidden');


    if ($departName.val() === '') {
        $('#name-error', $this).removeClass('hidden');
    }
    else {
        dname = $departName.val();
    }

    if ($selectedLi.length < 1) {
        $('#maxnumber-error', $this).removeClass('hidden');
    } else {
        maxNumb = $('> a', $selectedLi).text();
    }

    if (maxNumb < 1 || dname === '') {
        validated = false;
    }

    if (validated) {

        $.ajax({
            type: "POST",
            dataType: 'json',
            async: false,
            contentType: 'application/json; charset=UTF-8',
            ContentType: 'application/json; charset=UTF-8',
            url: "/Company/CreateDepartment",
            data: JSON.stringify({ departmentName: dname, maxNumberOfEmployees: maxNumb }),
            success: function (res) {
                if (res.Response === 0) { //success
                    $genericerror.hide();

                    result = true;

                } else if (res.Response === 2) {

                    ('span', $genericerror).text(res.Message);
                    $genericerror.removeClass('hidden');

                    result = false;
                }
            },
            failure: function (res) {
                ('span', $genericerror).text(res.Message);
                $genericerror.removeClass('hidden');
                result = false;
            },
            error: function (res) {
                ('span', $genericerror).text(res.Message);
                $genericerror.removeClass('hidden');
                result = false;
            }
        });
        
    }

    return result;
}


function InitFancyBox($rows) {
    
    $rows.on('dblclick', this, function (e) {
        console.log(this);

        $this = $(this);

        var href = $this.data('url');

        $.fancybox.open({
            href: href,
            type: 'iframe',
            maxWidth: 840,
            fitToView: false,
            //autoSize: false,
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
                fancyInner.addClass('fan-dep-inner')
                fancyInner.css({'overflow': 'inherit'});
            },
            afterClose: function () {
                location.reload();
            }
        });
    });

};

function InitDeleteDepartments() {



    $deleteButtons = $('input.x-remove','.table.table-content');

    $deleteButtons.on('click', this, function (e) {

        var $allError = $('.error-message');
        $allError.hide();
        
        $this = $(this);

        var dId;

        var $tr = $this.closest('tr');

        if ($tr.length > 0) {
            dId = $tr.data('id')
        }
        
        $.ajax({
            type: "POST",
            dataType: 'json',
            async: true,
            contentType: 'application/json; charset=UTF-8',
            ContentType: 'application/json; charset=UTF-8',
            url: "/Company/DeleteDepartment",
            data: JSON.stringify ({ id: dId }),
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
                console.log(res);
            },
            error: function (res) {
                console.log(res);
            }
        });

    });
};
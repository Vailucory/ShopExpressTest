$(document).ready(function () {
    $('.flexCheckChecked').change(function () {
        var isChecked = $(this).prop('checked');
        var id = $(this).prop('id')
        var form = $('#af_' + id);
        var token = $('input[name="__RequestVerificationToken"]', form).val();;
        var name = $('#ni_' + id).text()

        $.ajax({
            url: '/ToDoList/UpdateCompletionStatus',
            type: 'POST',
            data: { isCompleted: isChecked, id: id, name: name, __RequestVerificationToken: token, },
            headers: { "__RequestVerificationToken": token },
            success: function (data) {

            },
            error: function (error) {
                console.error('Error updating data:', error);
            }
        });
    });
    });

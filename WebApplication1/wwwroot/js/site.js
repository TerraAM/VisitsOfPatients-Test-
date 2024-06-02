$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
            placeholderElement.on('click', '[data-save="modal"]', function (event) {
                event.preventDefault();
                var form = $(this).parents('.modal').find('form');
                var actionUrl = form.attr('action');
                var dataToSend = form.serialize();

                $.post(actionUrl, dataToSend).done(function (data) {
                    var newBody = $('.modal-body', data);
                    placeholderElement.find('.modal-body').replaceWith(newBody);

                    // find IsValid input field and check it's value
                    // if it's valid then hide modal window
                    var isValid = newBody.find('[name="IsValid"]').val() == 'True';
                    if (isValid) {
                        placeholderElement.find('.modal').modal('hide');
                    }
                });
            });
            placeholderElement.on('click', '[data-dismiss="modal"]', function (event) {
                placeholderElement.find('.modal').modal('hide');
            });
        });
    });
});

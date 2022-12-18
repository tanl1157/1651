(function (window, $) {
    window.BookCreate = {
        init: function () {
            this.regisControl();
        },
        regisControl: function () {
            const me = this;

            $('#btnCreate').off('click').on('click', function (e) {
                e.preventDefault();
                me.submitForm();
            });
        },
        getFormData: function () {
            var model = {
                Code: $('#frmCreate [name="Code"]').val(),
                Name: $('#frmCreate [name="Name"]').val(),
                Author: $('#frmCreate [name="Author"]').val(),
                Category: $('#frmCreate [name="Category"]').val(),
                Publisher: $('#frmCreate [name="Publisher"]').val(),
                Quantity: $('#frmCreate [name="Quantity"]').val(),
                IsActive: $('#frmCreate [name="IsActive"]').is(':checked'),
            };

            return model;
        },
        formValidate: function () {
            var frm = $('#frmCreate');
            frm.validate({
                rules: {
                    Code: {
                        required: true
                    },
                    Name: {
                        required: true
                    },
                    Quantity: {
                        required: true,
                        digits: true
                    },
                },
                errorPlacement: function (error, element) {
                    error.appendTo(element.closest(".error-container"));
                },
            });

            return frm.valid();
        },
        submitForm: function () {
            const me = this;
            if (!me.formValidate()) return;

            var model = me.getFormData();

            $.ajax({
                url: '/Book/Create',
                data: { book: model },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res, function () {
                        if (res.Success) {
                            setTimeout(function () { location.href = '/Book/Index' }, 1000);
                        }
                    });
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    BookCreate.init();
});
(function (window, $) {
    window.UserCreate = {
        init: function () {
            this.regisControl();
            this.initForm();
        },
        regisControl: function () {
            const me = this;

            $('#btnCreate').off('click').on('click', function (e) {
                e.preventDefault();
                me.submitForm();
            });
        },
        loadCbRoles: function () {
            $.ajax({
                url: '/Roles/GetAll',
                success: function (res) {
                    if (res.Success) {
                        html = '';
                        if (res.Data && res.Data.length > 0) {
                            $.each(res.Data, function (i, item) {
                                html += `<option value="${item.Name}">${item.Name}</option>`;
                            });
                        }

                        $('#frmCreate [name="RoleName"]').html(html);
                    }
                }
            });
        },
        initForm: function () {
            const me = this;
            me.loadCbRoles();
        },
        getFormData: function () {
            var model = {
                Email: $('#frmCreate [name="Email"]').val(),
                FullName: $('#frmCreate [name="FullName"]').val(),
                Password: $('#frmCreate [name="Password"]').val(),
                RoleName: $('#frmCreate [name="RoleName"]').val()
            };

            return model;
        },
        formValidate: function () {
            var frm = $('#frmCreate');
            frm.validate({
                rules: {
                    Email: {
                        required: true
                    },
                    FullName: {
                        required: true
                    },
                    Password: {
                        required: true
                    }
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
                url: '/User/Create',
                data: { model: model },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res, function () {
                        if (res.Success) {
                            setTimeout(function () { location.href = '/User/Index' }, 1000);
                        }
                    });
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    UserCreate.init();
});
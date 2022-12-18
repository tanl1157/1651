(function (window, $) {
    window.AudienceUpdate = {
        init: function () {
            this.regisControl();
        },
        regisControl: function () {
            const me = this;

            $('#btnUpdate').off('click').on('click', function (e) {
                e.preventDefault();
                me.submitForm();
            });
        },
        getFormData: function () {
            var model = {
                ID: $('#frmCreate [name="ID"]').val(),
                IdentityCode: $('#frmCreate [name="IdentityCode"]').val(),
                FullName: $('#frmCreate [name="FullName"]').val(),
                PhoneNo: $('#frmCreate [name="PhoneNo"]').val(),
                Address: $('#frmCreate [name="Address"]').val()
            };

            return model;
        },
        formValidate: function () {
            var frm = $('#frmCreate');
            frm.validate({
                rules: {
                    IdentityCode: {
                        required: true
                    },
                    FullName: {
                        required: true
                    },
                    PhoneNo: {
                        required: true
                    },
                    Address: {
                        required: true
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
                url: '/Audience/Update',
                data: { audience: model },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res, function () {
                        if (res.Success) {
                            setTimeout(function () { location.href = '/Audience/Index' }, 1000);
                        }
                    });
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    AudienceUpdate.init();
});
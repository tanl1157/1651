(function (window, $) {
    window.OrderUpdate = {
        init: function () {
            this.regisControl();
            this.initForm();
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
                StrActualEndDate: $('#frmCreate [name="StrActualEndDate"]').val(),
                State: $('#frmCreate [name="State"]').val(),
            };

            return model;
        },
        formValidate: function () {
            $.validator.addMethod(
                "requiredActualEndDate",
                function (value, element) {
                    var state = $('#frmCreate [name="State"]').val();
                    if (state == 1 && !value) {
                        return false;
                    }

                    return true;
                },
                "Yêu cầu nhập"
            );

            $.validator.addMethod(
                "emptyActualEndDate",
                function (value, element) {
                    var state = $('#frmCreate [name="State"]').val();
                    if (state == 0 && value) {
                        return false;
                    }

                    return true;
                },
                "Trạng thái đang mượn không có ngày trả thực"
            );

            var frm = $('#frmCreate');

            frm.validate({
                rules: {
                    StrActualEndDate: {
                        requiredActualEndDate: true,
                        emptyActualEndDate: true
                    }
                },
                errorPlacement: function (error, element) {
                    error.appendTo(element.closest(".error-container"));
                },
            });

            return frm.valid();
        },
        loadCbAudience: function () {
            $.ajax({
                url: '/Audience/GetAll',
                success: function (res) {
                    if (res.Success) {
                        html = '';
                        if (res.Data && res.Data.length > 0) {
                            $.each(res.Data, function (i, item) {
                                html += `<option value="${item.ID}">${item.IdentityCode} - ${item.FullName}</option>`;
                            });
                        }

                        $('#frmCreate [name="Audience"]').html(html);
                        var bVal = parseInt($('#frmCreate [name="Audience"]').attr('value'));
                        $('#frmCreate [name="Audience"]').val(bVal).trigger('change');
                    }
                }
            });
        },
        loadCbBook: function () {
            const me = this;
            $.ajax({
                url: '/Book/GetAll',
                success: function (res) {
                    if (res.Success) {
                        html = '';
                        if (res.Data && res.Data.length > 0) {
                            $.each(res.Data, function (i, item) {
                                html += `<option value="${item.ID}">${item.Name}</option>`;
                            });
                        }

                        $('#frmCreate [name="BookID"]').html(html);
                        var bVal = parseInt($('#frmCreate [name="BookID"]').attr('value'));
                        $('#frmCreate [name="BookID"]').val(bVal).trigger('change');
                    }
                }
            });
        },
        initDatePicker: function (container) {
            $(container).find('.datetimepicker').datepicker({
                format: 'dd/mm/yyyy',
                language: "vi",
                autoclose: true
            });
        },
        initForm: function () {
            const me = this;
            me.initDatePicker($('#frmCreate'));
            me.loadCbAudience();
            me.loadCbBook();

            var bVal = parseInt($('#frmCreate [name="State"]').attr('value'));
            $('#frmCreate [name="State"]').val(bVal).trigger('change');
        },
        submitForm: function () {
            const me = this;
            if (!me.formValidate()) return;

            var model = me.getFormData();
            if (!model) return;

            $.ajax({
                url: '/Order/Update',
                data: { order: model },
                type: 'post',
                success: function (res) {
                    _base.handleResponse(res, function () {
                        if (res.Success) {
                            setTimeout(function () { location.href = '/Order/Index' }, 1000);
                        }
                    });
                }
            });
        }
    }
})(window, jQuery);

$(document).ready(function () {
    OrderUpdate.init();
});
(function (window, $) {
    window.OrderCreate = {
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
        getFormData: function () {
            var model = [];
            var repeaterVal = $('#frmCreate .repeater').repeaterVal();
            var code = $('#frmCreate [name="Code"]').val();
            var audienceID = $('#frmCreate [name="Audience"]').val();

            if (repeaterVal && repeaterVal.books && repeaterVal.books.length > 0) {
                $.each(repeaterVal.books, function (i, item) {
                    item["Code"] = code;
                    item["AudienceID"] = audienceID;

                    model.push(item);
                });

                return model;
            }

            return null;
        },
        formValidate: function () {
            var frm = $('#frmCreate');

            frm.validate({
                rules: {
                    Code: {
                        required: true
                    },
                    Audience: {
                        required: true
                    }
                },
                errorPlacement: function (error, element) {
                    error.appendTo(element.closest(".error-container"));
                },
            });

            $('[name*="BookID"]').each(function () {
                $(this).rules('add', {
                    required: true
                });
            });

            $('[name*="Quantity"]').each(function () {
                $(this).rules('add', {
                    required: true
                });
            });

            $('[name*="StrStartDate"]').each(function () {
                $(this).rules('add', {
                    required: true
                });
            });

            $('[name*="StrEndDate"]').each(function () {
                $(this).rules('add', {
                    required: true
                });
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
                        me.initRepeater();
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
            $(".datetimepicker").datepicker("setDate", new Date());
        },
        initRepeater: function () {
            const me = this;
            $('.repeater').repeater({
                defaultValues: {
                    'Quantity': '1'
                },
                show: function () {
                    $(this).slideDown();
                    me.initDatePicker(this);
                },
                hide: function (deleteElement) {
                    $(this).slideUp(deleteElement);
                },
            })
        },
        loadRandomCode: function () {
            $('#frmCreate [name="Code"]').val((Math.random() + 1).toString(36).substring(6).toUpperCase());
        },
        initForm: function () {
            const me = this;
            me.loadRandomCode();
            me.initDatePicker($('#frmCreate'));
            me.loadCbAudience();
            me.loadCbBook();
        },
        submitForm: function () {
            const me = this;
            if (!me.formValidate()) return;

            var model = me.getFormData();
            if (!model) return;

            $.ajax({
                url: '/Order/Create',
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
    OrderCreate.init();
});
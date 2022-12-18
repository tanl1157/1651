(function (window, $) {
    window.AudienceIndex = {
        init: function () {
            this.regisControl();
            this.loadGrid();
        },
        regisControl: function () {
        },
        initDatatable: function (data) {
            var me = this;

            var html = '';
            if (data && data.length > 0) {
                $.each(data, function (i, item) {
                    html += `
                    <tr id="${item.ID}">
                        <td>${(i + 1)}</td>
                        <td>${item.IdentityCode}</td>
                        <td>${item.FullName}</td>
                        <td>${item.PhoneNo}</td>
                        <td>${item.Address}</td>
                    </tr >`;
                })
            }

            $('#tbIndex tbody').html(html);

            me._table = $('#tbIndex').DataTable({
                scrollY: '55vh',
                language: {
                    url: '/Assets/Libs/DataTables/vi.json'
                },
                scrollCollapse: true,
                select: true,
                rowId: 'Id',
                pageLength: 20,
                lengthChange: false,
                initComplete: function (settings, json) {
                    var btnCreate = $('<button type="button" class="btn btn-outline-secondary" id="btnCreate"></button>').append('<i class="tf-icons bx bx-add-to-queue"></i>');
                    var btnEdit = $('<button type="button" class="btn btn-outline-secondary validate-selected" id="btnUpdate" disabled></button>').append('<i class="tf-icons bx bx-edit"></i>');
                    var btnDelete = $('<button type="button" class="btn btn-outline-secondary validate-selected" id="btnDelete" disabled></button>').append('<i class="tf-icons bx bx-trash"></i>');


                    $('div.eight.column:eq(0)', this.api().table().container()).append($('<div class="btn-group" id="btn-functions" role="group" aria-label="First group"></div>'));
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnCreate);
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnEdit);
                    $('div.eight.column:eq(0) .btn-group', this.api().table().container()).append(btnDelete);

                    me.regisBtnFunction();
                }
            });

            me.regisDatatableFunction();
        },
        regisBtnFunction: function () {
            const me = this;

            $('#btnCreate').off('click').on('click', function (e) {
                e.preventDefault();
                location.href = '/Audience/Create';
            });

            $('#btnUpdate').off('click').on('click', function (e) {
                e.preventDefault();
                location.href = '/Audience/Update/' + me._seletedId;
            });

            $('#btnDelete').off('click').on('click', function (e) {
                e.preventDefault();
                me.deleteItem();
            });
        },
        regisDatatableFunction() {
            const me = this;

            $('#tbIndex tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                } else {
                    me._table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }

                me._seletedId = me._table.row(this).id();
                $('.validate-selected').removeAttr("disabled");
            });
        },
        validateDeleteItem: function () {
            const me = this;
            return $.ajax({
                url: '/Audience/ValidateDelete',
                data: { id: me._seletedId },
                type: 'post'
            });
        },
        deleteItem: function () {
            const me = this;

            me.validateDeleteItem().then(function (validateRes) {
                if (validateRes.Success) {
                    if (validateRes.Data == 0) {
                        $.ajax({
                            url: '/Audience/Delete',
                            data: { id: me._seletedId },
                            type: 'post',
                            success: function (res) {
                                _base.handleResponse(res, function () {
                                    if (res.Success) {
                                        setTimeout(function () { location.href = '/Audience/Index' }, 1000);
                                    }
                                });
                            }
                        })
                    }
                    else {
                        toastr["warning"]("Không thể xóa độc giả đã phát sinh phiếu mượn");
                    }
                }
            });
        },
        loadGrid: function () {
            const me = this;

            $.ajax({
                url: '/Audience/GetAll',
                success: function (res) {
                    if (res.Success) {
                        me.initDatatable(res.Data);
                    }
                }
            });
        },
    }
})(window, jQuery);

$(document).ready(function () {
    AudienceIndex.init();
});
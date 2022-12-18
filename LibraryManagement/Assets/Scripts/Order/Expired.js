(function (window, $) {
    window.OrderExpired = {
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
                        <td>${item.Code}</td>
                        <td>${item.StateTitle}</td>
                        <td>${moment(item.StartDate).format('DD/MM/YYYY')}</td>
                        <td>${moment(item.EndDate).format('DD/MM/YYYY')}</td>
                        <td>${moment(new Date()).diff(moment(item.EndDate), "days")}</td>
                        <td>${item.Quantity}</td>
                        <td>${item.AudienceIdentityCode || ''}</td>
                        <td>${item.AudienceName || ''}</td>
                        <td>${item.BookCode || ''}</td>
                        <td>${item.BookName || ''}</td>
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
                lengthChange: false
            });
        },
        loadGrid: function () {
            const me = this;

            $.ajax({
                url: '/Order/GetExpired',
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
    OrderExpired.init();
});
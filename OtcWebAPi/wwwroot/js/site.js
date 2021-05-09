$(document).ready(function () {
    var tbodyElement = $('.container > table > tbody');
    var h1block = $('h1');
    var theadBlock = $('thead');
    var menu = $('.menu');

    getAllDepartments();

    $(document).on('click', '#ShowAllDepartments', function () {
        getAllDepartments();
    });

    $(document).on('click', "tbody .box", function () {
        let id = $(this).find('td.departmentId').text();
        if (id) {
            showLoading();
            $.get(`/api/getstatisticinfowithemployees?departmentId=${id}`, function (data) {
                h1block.text('Full department statistic');
                theadBlock.html(`<div class = "statisticInfo">${data.departmentName}</div><div class = "statisticInfo">Employees count:${data.employeesCount}</div><div class = "statisticInfo">Middle salary:${data.middleSalary}</div><tr class='box'><td>Id</td><td>Name</td><td>Salary</td><td>DepartmentId</td></tr>`);
                data.employees.forEach(function (el) {
                    let box = `<tr class="box">
                            <td class = "employeeId">${el.id}</td>
                            <td>${el.name}</td>
                            <td>${el.salary}</td>
                            <td>${el.departmentId}</td>
                        </tr>`;
                    tbodyElement.append(box);
                });
                hideLoading();
            });
        }
        else {
            getEmployee($(this).find('td.employeeId').text());
        }
    });

    function getAllDepartments() {
        showLoading();
        $.get("/api/getall/department", function (data) {
            h1block.text('Departments');
            theadBlock.html('<tr class="box"><td>Id</td><td>Name</td><td>Salary</td></tr>');
            data.forEach(function (el) {
                let box = `<tr class="box">
                            <td class = "departmentId">${el.id}</td>
                            <td>${el.name}</td>
                            <td>${el.salary}</td>
                        </tr>`;
                tbodyElement.append(box);
            });
            hideLoading();
        });
    };

    function getEmployee(id) {
        if (id) {
            showLoading();
            $.get(`/api/getbyid/employee?id=${id}`, function (data) {
                h1block.text('Employee');
                theadBlock.html('<tr class="box"><td>Id</td><td>Name</td><td>Salary</td><td>DepartmentId</td></tr>');
                let box = `<tr class="box">
                            <td>${data.id}</td>
                            <td>${data.name}</td>
                            <td>${data.salary}</td>
                            <td class = "departmentId">${data.departmentId}</td>
                        </tr>`;
                tbodyElement.append(box);
                hideLoading();
            });
        };
    }

    function hideLoading() {
        $('.dank-ass-loader').hide();
        menu.show();
    }

    function showLoading() {
        menu.hide();
        theadBlock.html('');
        h1block.html('');
        tbodyElement.html('');
        $('.dank-ass-loader').show();
    }

});
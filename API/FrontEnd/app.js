const API_URL = 'https://localhost:44324/index';

function loadUsers() {

    $(document).ready(function () {

        fetchUsers();

        $('#userForm').on('submit', function (event) {
            //event.preventDefault();
            const userData = getUserFormData();
            createUser(userData);
        });

    });

    function getUserFormData() {

        return {
            name: $('#name').val().trim(),
            lastname: $('#lastname').val().trim()

        }
    }

    function fetchUsers(){
        $.ajax({
            url: API_URL,
            method: 'GET',
            success: function (users) {
                renderUsers(users);
            },
            error: function () {
                alert('Error al cargar losusuarios.');
            }

        });
    }

    function createUser(user) {

        $.ajax({
            url: API_URL,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(user),
            success: function () {
                fetchUsers();
                $('#userForm')[0].reset();
            },
            error: function () {
                alert('Error al crear losusuarios.');
            }

        });
    }

    function deleteUser(userId){
        $.ajax({
            url: `${API_URL}/${userId}`,
            method: 'DELETE',
            success: function () {
                fetchUsers();
            },
            error: function () {
                alert('Error al borrar los usuarios.');
            }
        })
    }

    function renderUsers(users) {

        const userTable = $('#userTable tbody');
        userTable.empty();

        users.forEach((user) => {
            const userRow = createUserRow(user);
            userTable.append(userRow);
        });

        $('.delete-btn').on('click', function () {

            const userId = $(this).data('id');
            deleteUser(userId);

        });

    }

    function createUserRow(user) {

        const row = $('<tr></tr>');
        const nameCell = $('<td></td>').text(user.name);
        const lastnameCell = $('<td></td>').text(user.lastname);

        const deleteButton = $('<button></button>').addClass('delete-btn').attr('data-id', user.id).text('Eliminar');

        const actionCell = $('<td></td>').append(deleteButton);

        row.append(nameCell, lastnameCell, actionCell);

        return row;
    }

}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// RoomCategory
// Wire up all of the checkboxes to run removeItem()
$('.remove-checkbox').on('click', function (elem) {
    removeItem(elem.target);
});

function removeItem(checkbox) {
    checkbox.disabled = true;
    
    var form = checkbox.closest('form');
    form.submit();
}
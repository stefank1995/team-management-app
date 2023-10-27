
// data binding for syncfusion kanban
var kanbanObj;
function onKanbanCreated() {
	kanbanObj = this;
	}

// JavaScript to print kanban board content when clicking on the print button
document.getElementById('printButton').addEventListener('click', function () {
	printCardBody();
	});

function printCardBody() {
		const cardBody = document.querySelector('.card-body').cloneNode(true);

const printWindow = window.open('', '', 'width=1400,height=600');
printWindow.document.open();
printWindow.document.write('<html><head><title>Print</title></head><body>');

	printWindow.document.body.appendChild(cardBody);

	printWindow.document.write('</body></html>');
printWindow.document.close();
printWindow.print();
	}

// JavaScript to check if the client is sure about resetting the Kanbard Board content
function confirmReset() {
	Swal.fire({
		title: 'Are you sure?',
		text: 'This action will reset the board. This cannot be undone.',
		icon: 'warning',
		showCancelButton: true,
		confirmButtonText: 'Yes, reset it!',
		cancelButtonText: 'Cancel',
		reverseButtons: false
	}).then((result) => {
		if (result.isConfirmed) {
			// Submit the form if confirmed
			document.getElementById('reset-form').submit();
		}
	});
return false; // Prevent form submission by default
	}

// JavaScript to track horizontal scroll and adjust left position of the wrapper
var cardHeaderWrapper = document.getElementById('card-header-wrapper');

window.addEventListener('load', function () {
		var scrollLeft = cardHeaderWrapper.scrollLeft;
cardHeaderWrapper.style.left = -scrollLeft + 'px';
	});